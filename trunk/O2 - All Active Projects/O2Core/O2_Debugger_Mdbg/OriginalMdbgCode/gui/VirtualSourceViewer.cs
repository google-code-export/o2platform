//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;

namespace O2.Debugger.Mdbg.Tools.Mdbg.Extension
{
    // "Virtual Source window". This is a source view constructed at runtime instead
    // of from a file. This can be used for viewing IL source.
    internal class VirtualSourceViewerForm : SourceViewerBaseForm
    {
        // Get the object that keys this SourceViewer in our global hash.         


        private readonly MDbgFunction m_function;

        // Maps IL offsets to rows.

        // Track if current source-position is in this doc.
        // Null if no current source, or if it's  in another doc.
        //MDbgSourcePosition m_pos;

        private int m_CurrentIpRow = -1; // 1-based value for row of "current Ip" glyph in this doc? (-1 means no).
        private int[] m_il2RowMapping;

        internal VirtualSourceViewerForm(MainForm parent, MDbgFunction function)
        {
            m_function = function;
            Debug.Assert(function != null);

            // Now actually right in text. do this first so that we can get the current font.
            BeginInit(parent);

            // Get fonts
            FontCache cache;
            {
                Font fontCurrent = richText.Font;
                var emphasis = new Font(
                    fontCurrent.FontFamily,
                    fontCurrent.Size,
                    FontStyle.Bold
                    );

                cache = new FontCache(emphasis);
            }

            // Underlying writer to the window.
            var rawWriter = new RawWriter(cache);

            // Il2Native mapping can be used to find out what IL offsets we can actually stop on.
            Il2NativeIterator il2nativeIterator = null;

            // Actual IL disassembly in string form.
            ILDasmIterator ilDasm = null;

            // Iterator through sequence points and source files.
            SequencePointIterator seqIterator = null;

            string fullName = "?";
            int token = 0;

            ulong nativeStartAddress = 0;
            CorDebugJITCompilerFlags codeFlags = CorDebugJITCompilerFlags.CORDEBUG_JIT_DEFAULT;

            // Make cross-thread call to worker thread to collect raw information.
            // This needs to access MDbg and so can't be done on our UI thread.
            parent.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                              {
                                                                  Debug.Assert(proc != null);
                                                                  Debug.Assert(!proc.IsRunning);
                                                                  Debug.Assert(function.Module.Process == proc);

                                                                  // Get some properties about this function to display.
                                                                  token = function.CorFunction.Token;
                                                                  nativeStartAddress =
                                                                      function.CorFunction.NativeCode.Address;
                                                                  codeFlags =
                                                                      function.CorFunction.NativeCode.CompilerFlags;


                                                                  CorCode ilCode = function.CorFunction.ILCode;
                                                                  Debug.Assert(ilCode.IsIL);
                                                                  byte[] code = ilCode.GetCode();
                                                                  fullName = function.FullName;

                                                                  // This does the real disassembly work. 
                                                                  string[] lines = null; // strings of IL.
                                                                  ILDisassembler.Disassemble(code,
                                                                                             function.Module.Importer,
                                                                                             out lines,
                                                                                             out m_il2RowMapping);

                                                                  ilDasm = new ILDasmIterator(rawWriter, m_il2RowMapping,
                                                                                              lines);

                                                                  IL2NativeMap[] il2nativeMapping =
                                                                      function.CorFunction.NativeCode.
                                                                          GetILToNativeMapping();
                                                                  il2nativeIterator = new Il2NativeIterator(rawWriter,
                                                                                                            il2nativeMapping,
                                                                                                            code);

                                                                  // Get sequence points
                                                                  ISymbolMethod symMethod = function.SymMethod;

                                                                  // Sequence point information
                                                                  int[] seqIlOffsets = null;
                                                                  string[] seqPaths = null;
                                                                  int[] seqStartLines = null,
                                                                        seqEndLines = null,
                                                                        seqStartColumns = null,
                                                                        seqEndColumns = null;
                                                                  int seqCount = 0;

                                                                  if (symMethod != null)
                                                                  {
                                                                      seqCount = symMethod.SequencePointCount;
                                                                      seqIlOffsets = new int[seqCount];
                                                                      var seqDocuments = new ISymbolDocument[seqCount];
                                                                      seqPaths = new string[seqCount];
                                                                      seqStartLines = new int[seqCount];
                                                                      seqEndLines = new int[seqCount];
                                                                      seqStartColumns = new int[seqCount];
                                                                      seqEndColumns = new int[seqCount];

                                                                      symMethod.GetSequencePoints(seqIlOffsets,
                                                                                                  seqDocuments,
                                                                                                  seqStartLines,
                                                                                                  seqStartColumns,
                                                                                                  seqEndLines,
                                                                                                  seqEndColumns);

                                                                      for (int i = 0; i < seqCount; i++)
                                                                      {
                                                                          seqPaths[i] = seqDocuments[i].URL;
                                                                      }
                                                                  }
                                                                  seqIterator = new SequencePointIterator(rawWriter,
                                                                                                          parent,
                                                                                                          seqIlOffsets,
                                                                                                          seqPaths,
                                                                                                          seqStartLines,
                                                                                                          seqStartColumns,
                                                                                                          seqEndLines,
                                                                                                          seqEndColumns);
                                                              }
                ); // end worker call

            // We assume sequence points are sorted by IL offset. We assert that in the iterators below.
            // Now we need to go through and stitch the IL + Source together.
            // This also works even if we have no source (since that's just the degenerate case of 0 sequence points)

            // Print out header information
            Debug.Assert(token != 0);
            rawWriter.WriteLine(String.Format(CultureInfo.InvariantCulture,
                                              "> Function name:{0} (token={1:x})", fullName, token));
            rawWriter.WriteLine(String.Format(CultureInfo.InvariantCulture,
                                              "> Native Code Address =0x{0:x}, flags={1}", nativeStartAddress, codeFlags));

            // Walk through the IL in order and write out interleaved IL and Sequence Points.
            while (!seqIterator.IsDone)
            {
                // Add IL snippets that occur before this sequence point.   
                WriteIlAndNative(ilDasm, il2nativeIterator, seqIterator.IlOffset);

                seqIterator.WriteSource();
                seqIterator.Next();
            }
            // Write the IL that's after the last sequence point
            WriteIlAndNative(ilDasm, il2nativeIterator, ilDasm.IlLength);


            // Set the text.
            InitLines(null, rawWriter.Lines, rawWriter.FormatList);

            EndInit(fullName);
        }

        protected override object GetHashKey()
        {
            return m_function;
        }

        private static void WriteIlAndNative(ILDasmIterator ilDasm, Il2NativeIterator il2nativeIterator, int ilEndOffset)
        {
            while (ilDasm.IlOffset < ilEndOffset)
            {
                ilDasm.WriteIl();
                il2nativeIterator.WriteNative(ilDasm.IlOffset);
                ilDasm.Next();
            }
        }


        // Notify this window that it's entering break mode.
        // Called on UI thread.
        protected override void OnBreakWorker()
        {
            ClearHighlight();
            m_CurrentIpRow = -1;

            MainForm.ExecuteOnWorkerThreadIfStoppedAndBlock(delegate(MDbgProcess proc)
                                                                {
                                                                    Debug.Assert(proc != null);
                                                                    Debug.Assert(!proc.IsRunning);

                                                                    MDbgThread t = proc.Threads.Active;
                                                                    MDbgFrame f = t.CurrentFrame;
                                                                    if ((f != null) && (f.Function == m_function))
                                                                    {
                                                                        uint offsetIl;
                                                                        CorDebugMappingResult result;
                                                                        f.CorFrame.GetIP(out offsetIl, out result);

                                                                        // mapping is 0-based; row is 1-based.
                                                                        m_CurrentIpRow = m_il2RowMapping[offsetIl] + 1;
                                                                    }
                                                                });

            if (m_CurrentIpRow != -1)
            {
                HighlightRow(m_CurrentIpRow, MainForm.IsCurrentSourceActive);
            }
        }

        protected override void OnRunWorker()
        {
            m_CurrentIpRow = -1;
        }

        protected override void DrawGlyphWorker(PaintEventArgs e)
        {
            // Draw current source-line arrows in the glyph bar.            
            if (m_CurrentIpRow >= 0)
            {
                Bitmap bmp = MainForm.IsCurrentSourceActive ? m_glyphs.CurrentLineArrow : m_glyphs.NotCurrentLineArrow;
                DrawGlyphAtLine(e, bmp, m_CurrentIpRow);
            }
        }

        protected override void ToggleBreakpointAtLine(int row)
        {
            MessageBox.Show(
                "IL window does not implement adding breakpoints. You can set breakpoints from a source window or from the command line",
                "IL breakpoints not implement");
        }

        // Do a IL-level step over.
        protected override bool DoStepOver()
        {
            MainForm.AsyncProcessEnteredText("il_next");
            return true;
        }

        // Do a IL-level step in.
        protected override bool DoStepIn()
        {
            MainForm.AsyncProcessEnteredText("il_step");
            return true;
        }

        #region Helper classes for stitching

        // All of these execute entirely on the UI thread, unless otherwise specified.

        // Helper to cache the fonts we want

        #region Nested type: FontCache

        private class FontCache
        {
            private readonly Font m_emphasis;

            public FontCache(Font emphasis)
            {
                m_emphasis = emphasis;
            }

            public Font Emphasis
            {
                get { return m_emphasis; }
            }
        }

        #endregion

        // Raw writer for spewing IL and other interjected stuff (like source and native).
        // This is biased towards writing IL rows.

        // Iterator through the IL-to-Native mapping.
        // This is biased by IL offset, so native code that doesn't map to IL (such as 
        // prolog and epilogue) won't show up. 

        #region Nested type: Il2NativeIterator

        private class Il2NativeIterator
        {
            // This will sort the map in-place.

            // how many bytes of IL? We can use this as the ending range on the last il2native entry.
            private readonly int m_CodeLength;

            // Entry of (IL start) --> { Native Start, Native End} ranges.
            // This list will be sorted by starting IL offset..
            private readonly IL2NativeMap[] m_il2nativeMapping;
            private readonly RawWriter m_writer;

            // Index into IL2Native map entry for the current entry.
            private int m_il2NativeCursor;

            // (IL end) value for the 'current' entry.
            private int m_il2NativeEndIl;

            public Il2NativeIterator(RawWriter w, IL2NativeMap[] il2nativeMapping, byte[] code)
            {
                m_writer = w;
                m_il2nativeMapping = il2nativeMapping;

                if (m_il2nativeMapping == null)
                {
                    // No native info, so don't display it. Put range just out of reach.
                    m_il2NativeEndIl = (code.Length + 1);
                    return;
                }

                m_CodeLength = code.Length;

                SortMapByIL(m_il2nativeMapping);

                // Skip past initial entries for prolog, epilogue, unmapped, etc.
                // Since we sorted by IL, all of those entries (which get negative IL offsets) will come first.
                while ((m_il2NativeCursor < m_il2nativeMapping.Length) &&
                       (m_il2nativeMapping[m_il2NativeCursor].IlOffset < 0))
                {
                    m_il2NativeCursor++;
                }

                CalculateNextEndOffset();
            }

            // Il2Native entries only include the starting IL offset. 
            // Calculate the end IL offset so that we have a real IL range of the current (il2NativeCursor) IL2Native entry.
            private void CalculateNextEndOffset()
            {
                m_il2NativeEndIl = (m_il2NativeCursor + 1 >= m_il2nativeMapping.Length)
                                       ?
                                           (m_CodeLength - 1)
                                       :
                                           (m_il2nativeMapping[m_il2NativeCursor + 1].IlOffset - 1);
            }

            // Sort an IL to native map entry by IL offset.
            // Non-IL regions get ommitted.
            private static void SortMapByIL(IL2NativeMap[] map)
            {
                Array.Sort(map, delegate(IL2NativeMap x, IL2NativeMap y)
                                    {
                                        if (x.IlOffset < y.IlOffset) return -1;
                                        if (x.IlOffset == y.IlOffset) return 0;
                                        return 1;
                                    });
            }

            // Handle writing any native info for the given IL offset.
            // This may output nothing if there is no native info for the given IL offset.
            // Caller is expected to pass monotonically increasing ilCursor values.
            // Our policy is to write out the native info after the last IL offset it applies too.
            public void WriteNative(int ilCursor)
            {
                // check if this matches any native code? Print this after the IL code.
                // We want to print the Native range after 
                int ilNext = ilCursor + 1;
                if (ilNext > m_il2NativeEndIl)
                {
                    int nativeStart = m_il2nativeMapping[m_il2NativeCursor].NativeStartOffset;
                    int nativeEnd = m_il2nativeMapping[m_il2NativeCursor].NativeEndOffset;
                    string nativeInfo = String.Format(CultureInfo.InstalledUICulture,
                                                      "    IL:0x{0:x},0x{1:x} --> Native:0x{2:x},0x{3:x}  (N. size=0x{4:x} bytes)",
                                                      m_il2nativeMapping[m_il2NativeCursor].IlOffset,
                                                      m_il2NativeEndIl + 1,
                                                      nativeStart, nativeEnd,
                                                      nativeEnd - nativeStart);

                    m_writer.WriteLine(nativeInfo);
                    m_writer.FormatLastRow(Color.DarkGray);
                    m_writer.WriteLine(""); // extra line for easier viewing

                    m_il2NativeCursor++;

                    CalculateNextEndOffset();
                }
            }
        }

        #endregion

        // Expected loop:
        // while (i.IlOffset < __) {
        //   i.WriteIl(); // this will write out current IL
        //   // do other work (eg, write native offsets or stuff)
        //   i.Next(); // this will move to next IL offset.
        // }

        #region Nested type: ILDasmIterator

        private class ILDasmIterator
        {
            // Iterator to loop through all IL and write it out to the supplied RawWriter.
            // It will also adjust the il2RowMapping as it goes.

            private readonly int[] m_il2RowMapping;
            private readonly string[] m_lines;
            private readonly RawWriter m_writer;

            // The IL offset within the document. This is a monotonically increasing value
            // that covers every single IL offset (Even ones in the middle of an IL opcode.).
            private int m_ilCursor;
            private int m_lastRow = -1; // unique value

            public ILDasmIterator(RawWriter w, int[] il2RowMapping, string[] ilDissembly)
            {
                m_writer = w;
                m_il2RowMapping = il2RowMapping;
                m_lines = ilDissembly;
            }

            public int IlOffset
            {
                get { return m_ilCursor; }
            }

            // Get total number of bytes of IL.
            public int IlLength
            {
                get { return m_il2RowMapping.Length; }
            }

            // Write any new IL lines at this offset.
            // This should be called once (and exactly once) at each offset.
            // Called at each IL offset.
            public void WriteIl()
            {
                int thisRow = m_il2RowMapping[m_ilCursor];
                if (thisRow != m_lastRow)
                {
                    m_writer.WriteILStringLine("  " + m_lines[thisRow]);
                    Debug.Assert(thisRow > m_lastRow);
                    m_lastRow = thisRow;
                }
                m_il2RowMapping[m_ilCursor] += m_writer.Adjust;
            }

            // Go to the next IL offset.
            public void Next()
            {
                m_ilCursor++;
            }
        }

        #endregion

        #region Nested type: RawWriter

        private class RawWriter
        {
            // Get list of fonts to use with formatting.
            private readonly FontCache m_cache;


            // Actual string content being written.
            private readonly ArrayList m_finalLines = new ArrayList();

            private readonly ArrayList m_formats = new ArrayList();
            private int m_adjust;

            public RawWriter(FontCache cache)
            {
                m_cache = cache;
            }

            public FontCache Fonts
            {
                get { return m_cache; }
            }

            public ArrayList FormatList
            {
                get { return m_formats; }
            }

            public ArrayList Lines
            {
                get { return m_finalLines; }
            }

            // Get how much the IL rows have shifted by. This is the count of non-IL rows written.
            public int Adjust
            {
                get { return m_adjust; }
            }

            // Format the last row to be the given color           
            public void FormatLastRow(Color c)
            {
                m_formats.Add(new FormatRow(m_finalLines.Count - 1, c));
            }

            // Format a span of the last row to be the given font
            public void FormatLastRow(int columnStart, int columnEnd, Font f, Color c)
            {
                m_formats.Add(new FormatSpan(m_finalLines.Count - 1, columnStart, columnEnd, f, c));
            }


            // Write a non-IL string.
            public void WriteLine(string text)
            {
                m_finalLines.Add(text);
                m_adjust++;
            }

            // Write an IL-based string.
            // This is separate from plain WriteLine so that we can properly update the Adjust property.
            public void WriteILStringLine(string text)
            {
                m_finalLines.Add(text);
                FormatLastRow(Color.Blue);
            }
        }

        #endregion

        // Iterate through source-level sequence points and writing source.

        #region Nested type: SequencePointIterator

        private class SequencePointIterator
        {
            private readonly string m_lineFormatPrefix = "//{0,7}:{1}"; // args: line #, source contents.
            private readonly MainForm m_parent;
            private readonly int m_PrefixLen = 2 + 7 + 1; // length of prefix in characters
            private readonly int m_seqCount;
            private readonly int[] m_seqEndColumns;
            private readonly int[] m_seqEndLines;
            // Set of sequence points.
            private readonly int[] m_seqIlOffsets;
            private readonly string[] m_seqPaths;
            private readonly int[] m_seqStartColumns;
            private readonly int[] m_seqStartLines;
            private readonly RawWriter m_writer;

            // Index into sequence point array
            private int m_index;

            private int m_lastSeqIlOffset = -1;

            public SequencePointIterator(RawWriter w, MainForm parent,
                                         int[] seqIlOffsets,
                                         string[] seqPaths,
                                         int[] seqStartLines,
                                         int[] seqStartColumns,
                                         int[] seqEndLines,
                                         int[] seqEndColumns)
            {
                m_writer = w;
                m_parent = parent;

                m_seqIlOffsets = seqIlOffsets;
                m_seqPaths = seqPaths;
                m_seqStartLines = seqStartLines;
                m_seqStartColumns = seqStartColumns;
                m_seqEndLines = seqEndLines;
                m_seqEndColumns = seqEndColumns;

                Debug.Assert(m_seqCount == 0);
                if (m_seqIlOffsets != null)
                {
                    m_seqCount = m_seqIlOffsets.Length;

                    // All arrays should be same length.
                    Debug.Assert(seqIlOffsets.Length == m_seqCount);
                    Debug.Assert(seqPaths.Length == m_seqCount);
                    Debug.Assert(seqStartLines.Length == m_seqCount);
                    Debug.Assert(seqStartColumns.Length == m_seqCount);
                    Debug.Assert(seqEndLines.Length == m_seqCount);
                    Debug.Assert(seqEndColumns.Length == m_seqCount);
                }
            }

            public bool IsDone
            {
                get { return m_index >= m_seqCount; }
            }

            // Get the IL offset for the current sequence point.
            public int IlOffset
            {
                get { return m_seqIlOffsets[m_index]; }
            }

            public void Next()
            {
                Debug.Assert(!IsDone);

                // Assert out assumption that sequence points are sorted by IL offsets.
                Debug.Assert(m_seqIlOffsets[m_index] > m_lastSeqIlOffset);
                m_lastSeqIlOffset = m_seqIlOffsets[m_index];

                m_index++;
            }

            // Called once per sequence point to write Source lines.
            public void WriteSource()
            {
                string path = m_seqPaths[m_index];
                ;
                SourceViewerForm file = GetSourceFile(m_parent, path);

                // Now add text for this sequence point
                if (file != null)
                {
                    int lineStart = m_seqStartLines[m_index];
                    int lineEnd = m_seqEndLines[m_index];

                    for (int j = lineStart; j <= lineEnd; j++)
                    {
                        // Need to count how many source lines we've injected so that we can adjust
                        // the il2row mapping accordingly. 

                        if (j == 0xFeeFee)
                        {
                            m_writer.WriteLine("// !hidden!");
                            //m_writer.FormatLastRow(Color.DarkGreen);
                        }
                        else if (j > file.Count)
                        {
                            m_writer.WriteLine(String.Format(CultureInfo.InvariantCulture, m_lineFormatPrefix, j,
                                                             "<out of range>"));
                            //m_writer.FormatLastRow(Color.DarkGreen);
                        }
                        else
                        {
                            // Write out: // line# :  <contents>
                            string t = String.Format(CultureInfo.InvariantCulture, m_lineFormatPrefix, j,
                                                     file.GetLine(j));
                            m_writer.WriteLine(t);

                            // Handle format across multiple lines.                            
                            int columnStart = m_seqStartColumns[m_index] + m_PrefixLen;
                            int columnEnd = m_seqEndColumns[m_index] + m_PrefixLen;

                            if (j > lineStart)
                            {
                                columnStart = m_PrefixLen + 1;
                            }
                            if (j < lineEnd)
                            {
                                columnEnd = t.Length + 1;
                            }


                            //m_writer.FormatLastRow(columnStart, columnEnd, Color.Green);
                            m_writer.FormatLastRow(columnStart, columnEnd, m_writer.Fonts.Emphasis, Color.Red);
                        }
                        //m_writer.FormatLastRow(Color.Green);
                    }
                }
                else
                {
                    // Source file not found.
                    m_writer.WriteLine("// File '" + path + "' missing. Line " + m_seqStartLines[m_index] + " to line " +
                                       m_seqEndLines[m_index]);
                    m_writer.FormatLastRow(Color.DarkGreen);
                }
            }
        }

        #endregion

// SequencePointIterator

        #endregion Helper classes for stitching
    } // end class
}
