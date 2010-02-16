// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ScriptSamples;
using O2.Interfaces.Views;
using O2.Kernel.CodeUtils;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
using System.Threading;
using O2.DotNetWrappers.ViewObjects;

namespace O2.External.SharpDevelop.Ascx
{
    public partial class ascx_SourceCodeEditor
    {
        private bool runOnLoad = true;
        public event Callbacks.dMethod_String eDocumentDataChanged;
        public event Callbacks.dMethod_String_String eDocumentSelectionChanged_WordAndLine;
        public event Callbacks.dMethod eEnterInSource_Event;
        //public delegate void documentDataChanged_EventHandler(String sDocumentText);

        // external compilers
        public static Dictionary<string, Func<string, DataReceivedEventHandler, Process>> externalExecutionEngines = new Dictionary<string, Func<string, DataReceivedEventHandler, Process>>();

        private Ast_CSharp_ShowDetailsInViewer showAstDetails;
        private int iLastFoundPosition;
        public long iMaxFileSize = 200; //  200k
        public String sDirectoryOfFileLoaded = "";
        public String sFileToOpen = "";
        public String sPathToFileLoaded = "";
        private Dictionary<string, string> sampleScripts;
        public List<string> partialFileContents = new List<string>();
        public bool partialFileViewMode;
        public int startLine;
        public int endLine;
        public Assembly compiledAssembly;
        Thread autoCompileThread;

        public void onLoad()
        {
            if (false == DesignMode && runOnLoad)
            {                
                tbMaxLoadSize.Text = iMaxFileSize.ToString();
                if (File.Exists(sFileToOpen))
                    loadSourceCodeFile(sFileToOpen);
                else
                {
                    if (sFileToOpen != "")
                        PublicDI.log.error("in ascx_SourceCodeEditor .ctor: File provided does not exist: {0}", sFileToOpen);
                }
                configureOnLoadMenuOptions();
                configureDefaultSettingsForTextEditor(tecSourceCode);
                tecSourceCode.ActiveTextAreaControl.TextArea.IconBarMargin.MouseDown += new MarginMouseEventHandler(IconBarMargin_MouseDown);
                //        tecSourceCode.ActiveTextAreaControl.TextArea.IconBarMargin.MouseMove += new MarginMouseEventHandler(IconBarMargin_MouseMove);
                //        tecSourceCode.ActiveTextAreaControl.TextArea.IconBarMargin.MouseLeave += new EventHandler(IconBarMargin_MouseLeave);
                //        tecSourceCode.ActiveTextAreaControl.TextArea.KeyPress += TextArea_KeyPress;
                tecSourceCode.ActiveTextAreaControl.TextArea.KeyEventHandler += TextArea_KeyEventHandler;
                tecSourceCode.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(TextArea_DragEnter);
                tecSourceCode.ActiveTextAreaControl.TextArea.DragOver += new DragEventHandler(TextArea_DragOver);
                tecSourceCode.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(TextArea_DragDrop);
                tecSourceCode.ActiveTextAreaControl.SelectionManager.SelectionChanged += SelectionManager_SelectionChanged;
                tecSourceCode.ActiveTextAreaControl.Caret.PositionChanged += SelectionManager_SelectionChanged;

                tecSourceCode.TextEditorProperties.Font = new Font("Courier New", 9, FontStyle.Regular);

                mapExternalExecutionEngines();
                runOnLoad = false;
            }
        }
        

        void IconBarMargin_MouseDown(AbstractMargin sender, Point mousepos, MouseButtons mouseButtons)
        {
            /*    IconBarMargin margin = (IconBarMargin)sender;
                Graphics g = margin.TextArea.CreateGraphics();
                //g.Clear(Color.Red);
                //g.DrawString("B", new Font("Arial", 16), new SolidBrush(Color.Black), 0, mousepos.Y);
                //margin.DrawBookmark(g, mousepos.Y, true);
                margin.DrawBreakpoint(g, mousepos.Y, true,true);
    */
            //tecSourceCode.Document.BookmarkManager.AddMark(bookmark);
        }

        void TextArea_DragOver(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        void TextArea_DragDrop(object sender, DragEventArgs e)
        {
            var data = Dnd.tryToGetObjectFromDroppedObject(e);
            if (data != null)
            {
                var dsa = data.GetType().FullName;
                if (data is List<MethodInfo>)
                {
                    var methods = (List<MethodInfo>)data;
                    if (methods.Count > 0)
                    {
                        var filteredSignature = new FilteredSignature(methods[0]);
                        tecSourceCode.ActiveTextAreaControl.TextArea.InsertString(filteredSignature.sFunctionNameAndParams);
                        //  var functionSignature = new FilteredSignature
                    }
                }
                else
                    tecSourceCode.ActiveTextAreaControl.TextArea.InsertString(data.ToString());
            }
        }

        void TextArea_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);

        }



        public void mapExternalExecutionEngines()
        {
            this.invokeOnThread(
                () =>
                {
                    cbExternalEngineToUse.Items.Clear();
                    foreach (var externalEngine in externalExecutionEngines.Keys)
                        cbExternalEngineToUse.Items.Add(externalEngine);
                    if (cbExternalEngineToUse.Items.Count > 0)
                        cbExternalEngineToUse.SelectedIndex = 0;
                });
        }

        private void configureOnLoadMenuOptions()
        {
            //if (O2Messages.IsGuiLoaded())
            //{
            btShowLogs.Visible = O2Messages.isGuiLoaded();
            //}
        }

        private void configureDefaultSettingsForTextEditor(TextEditorControlBase tecTargetTextEditor)
        {
            tecTargetTextEditor.Document.DocumentChanged += Document_DocumentChanged;

            cboxLineNumbers.Checked = true;

            cboxInvalidLines.Checked = true;
            cboxInvalidLines_CheckedChanged(null, null);

            cboxEOLMarkers.Checked = false;
            cboxEOLMarkers_CheckedChanged(null, null);

            cboxHRuler.Checked = false;
            cboxHRuler_CheckedChanged(null, null);

            cboxSpaces.Checked = false;
            cboxSpaces_CheckedChanged(null, null);

            cboxTabs.Checked = false;
            cboxTabs_CheckedChanged(null, null);

            cboxVRuler.Checked = false;
            cboxVRuler_CheckedChanged(null, null);
        }

        private void loadSourceCodeFileIntoTextEditor(String fileToLoad, TextEditorControlBase tecTargetTextEditor)
        {
            fileToLoad = HandleO2MessageOnSD.tryToResolveFileLocation(fileToLoad, this);
            //this.okThreadSync(delegate
            //ExtensionMethods.invokeOnThread((Control)this, () =>
            tecSourceCode.invokeOnThread(
                () =>
                    {
                        try
                        {
                            partialFileViewMode = false;
                            lbPartialFileView.Visible = false;
                            tecSourceCode.Visible = true;
                            long iCurrentFileSize = Files.getFileSize(fileToLoad);
                            if (iCurrentFileSize > (iMaxFileSize*1024))
                            {
                                PublicDI.log.error("File to load is too big: max is {0}k, this file is {1}k : {2}",
                                             iMaxFileSize,
                                             iCurrentFileSize/1024, fileToLoad);
                                loadPartialFileView(fileToLoad);
                            }
                            else
                            {
                                tecTargetTextEditor.LoadFile(fileToLoad);
                                if (Path.GetExtension(fileToLoad).ToLower() == ".o2")
                                {
                                    var realFileTypeToload = Path.GetFileNameWithoutExtension(fileToLoad);
                                    tecSourceCode.Document.HighlightingStrategy =
                                        HighlightingStrategyFactory.CreateHighlightingStrategyForFile(realFileTypeToload);
                                }
                                lbSourceCode_UnsavedChanges.Visible = false;
                                btSaveFile.Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            PublicDI.log.error("in loadSourceCodeFileIntoTextEditor: {0}", ex.Message);
                        }
                        return null;
                    });

        }

        public void loadPartialFileView(string fileToLoad)
        {
            fileToLoad = HandleO2MessageOnSD.tryToResolveFileLocation(fileToLoad, this);
            this.invokeOnThread(() =>
                    {
                        partialFileViewMode = true;
                        lbPartialFileView.Visible = true;
                        tecSourceCode.Visible = false;
                        partialFileContents = Files.loadLargeSourceFileIntoList(fileToLoad, true);
                        showLinesFromPartialFileContents(0, 1000);
                    });
        }

        public void showLinesFromPartialFileContents(int _startLine, int _endLine)
        {
            startLine = _startLine;
            endLine = _endLine;
            this.invokeOnThread(() =>
                    {
                        var numberOflinesInCurrentFile = partialFileContents.Count;
                        if (startLine > numberOflinesInCurrentFile)
                        {
                            startLine = numberOflinesInCurrentFile - 1000;
                            if (startLine < 0)
                                startLine = 0;
                        }
                        if (endLine < startLine)
                            endLine = startLine + 1000;
                        if (endLine > numberOflinesInCurrentFile)
                            endLine = numberOflinesInCurrentFile - 1;
                        lbPartialFileView.Items.Clear();
                        for (int i = startLine; i < endLine; i++)
                            lbPartialFileView.Items.Add(partialFileContents[i]);


                    });
        }

        public void selectLineFromPartialFileContents(uint lineToSelect)
        {
            // handle the special case where 0 is provided as the line to select 
            //  if (lineToSelect == 0)
            //      lineToSelect = 1;
            this.invokeOnThread(() =>
                    {
                        if (startLine < lineToSelect && lineToSelect < endLine)
                        {
                            var selectedIndex = (int)lineToSelect - startLine - 1;
                            if (lbPartialFileView.Items.Count > selectedIndex)
                                lbPartialFileView.SelectedIndex = selectedIndex;
                        }
                        else
                        {
                            var newStartLine = (int)lineToSelect - 500;
                            var newEndLine = (int)lineToSelect + 500;
                            if (newStartLine < 0)
                            {
                                newEndLine += -newStartLine;    // add it to the end
                                newStartLine = 0;               // make it start at 0
                            }
                            showLinesFromPartialFileContents(newStartLine, newEndLine);
                            if (lineToSelect - startLine > 0 && lineToSelect - startLine < endLine)
                                lbPartialFileView.SelectedIndex = (int)lineToSelect - startLine;
                        }
                        tbExecutionHistoryOrLog.Text = string.Format("{0}: {1}{2}", lbPartialFileView.SelectedIndex + 1, lbPartialFileView.Text, Environment.NewLine) +
                                                       tbExecutionHistoryOrLog.Text;

                    });
        }


        public void saveSourceCodeFile(String sTargetLocation)
        {
            try
            {
                tecSourceCode.SaveFile(sTargetLocation);
                if (sPathToFileLoaded != sTargetLocation)
                {
                    sPathToFileLoaded = sTargetLocation;

                }

                PublicDI.log.info("Source code saved to: {0}", sTargetLocation);
                tbSourceCode_FileLoaded.Text = Path.GetFileName(sTargetLocation);
                lbSource_CodeFileSaved.Visible = true;
                lbSourceCode_UnsavedChanges.Visible = false;
                btSaveFile.Enabled = false;
                tecSourceCode.LineViewerStyle = LineViewerStyle.None;
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in saveSourceCodeFile {0}", ex.Message);
            }
        }

        public bool loadSourceCodeFile(String pathToSourceCodeFileToLoad)
        {
            if (pathToSourceCodeFileToLoad == "")
                return false;

            pathToSourceCodeFileToLoad = HandleO2MessageOnSD.tryToResolveFileLocation(pathToSourceCodeFileToLoad, this);

            var fileExtension = Path.GetExtension(pathToSourceCodeFileToLoad).ToLower();
            if (fileExtension == ".dll" || fileExtension == ".exe" || fileExtension == ".class" || hasNonRendredChars(pathToSourceCodeFileToLoad))
            {
                PublicDI.log.error("Skipping file load due to its extension or contents: {0}", Path.GetFileName(pathToSourceCodeFileToLoad));
                return false;
            }
            return (bool)(this.invokeOnThread(() =>
                                 {
                                     try
                                     {
                                         if (File.Exists(pathToSourceCodeFileToLoad))
                                         {
                                             if (sPathToFileLoaded != pathToSourceCodeFileToLoad)
                                             {
                                                 PublicDI.log.debug("Loading File: {0}",
                                                              pathToSourceCodeFileToLoad);
                                                 if (pathToSourceCodeFileToLoad != "")
                                                 {

                                                     //tbSourceCode.Text = files.GetFileContent(config.getDefaultSourceCodeCompilationExampleFile());

                                                     //  tbSourceCode_PathToFileLoaded.Text = sPathToSourceCodeFileToLoad;
                                                     sDirectoryOfFileLoaded = Path.GetDirectoryName(pathToSourceCodeFileToLoad);
                                                     tbSourceCode_DirectoryOfFileLoaded.Text = sDirectoryOfFileLoaded;
                                                     tbSourceCode_FileLoaded.Text = Path.GetFileName(pathToSourceCodeFileToLoad);
                                                     sPathToFileLoaded = pathToSourceCodeFileToLoad;
                                                     loadSourceCodeFileIntoTextEditor(pathToSourceCodeFileToLoad, tecSourceCode);

                                                     //compileSourceCode();
                                                     lbSource_CodeFileSaved.Visible = false;
                                                     lbSourceCode_UnsavedChanges.Visible = false;
                                                     btSaveFile.Enabled = false;


                                                     PublicDI.log.debug("Source code file loaded: {0}", pathToSourceCodeFileToLoad);
                                                     setCompileAndInvokeButtonsState(sPathToFileLoaded);
                                                 }
                                                 return true;
                                             }
                                         }
                                         else
                                             tecSourceCode.Text = "";
                                     }
                                     catch (Exception ex)
                                     {
                                         PublicDI.log.ex(ex, "in loadSourceCodeFile");
                                     }
                                     return false;
                                 }));
        }

        private bool hasNonRendredChars(string pathToSourceCodeFileToLoad)
        {
            var fileContents = Files.getFileContents(pathToSourceCodeFileToLoad);
            if (fileContents.Contains("\0"))
                return true;
            return false;
        }

        private void setCompileAndInvokeButtonsState(string pathToFileLoaded)
        {
            bool supportCSharpCompileAndExecute = true;
            if (Path.GetExtension(pathToFileLoaded).ToLower() == ".o2")
                pathToFileLoaded = Path.GetFileNameWithoutExtension(pathToFileLoaded);
            switch (Path.GetExtension(pathToFileLoaded))
            {               
                case ".cs":
                    btExecuteOnExternalEngine.Visible = false;
                    lbExecuteOnEngine.Visible = false;
                    cbExternalEngineToUse.Visible = false;
                    btShowHidePythonLogExecutionOutputData.Visible = false;
                    break;
                default:
                    btExecuteOnExternalEngine.Visible = true;
                    lbExecuteOnEngine.Visible = true;
                    cbExternalEngineToUse.Visible = true;
                    btShowHidePythonLogExecutionOutputData.Visible = true;
                    supportCSharpCompileAndExecute = false;
                    break;

            }
            lbCompileCode.Visible = supportCSharpCompileAndExecute;
            btCompileCode.Visible = supportCSharpCompileAndExecute;
            btDragAssemblyCreated.Visible = supportCSharpCompileAndExecute;
            btSelectedLineHistory.Visible = supportCSharpCompileAndExecute;
            tsCompileStripSeparator.Visible = supportCSharpCompileAndExecute;
        }

        /// <summary>
        /// Thread sage way to get value of tecSourceCode
        /// </summary>
        /// <returns></returns>
        public String getSourceCode()
        {
            return (string)this.invokeOnThread(() => tecSourceCode.Text);

            /*try
            {
                string sourceCode = "";
                var sync = new AutoResetEvent(false);
                if (tecSourceCode.InvokeRequired)
                    tecSourceCode.Invoke(new EventHandler((sender,e) =>
                                                    {
                                                        sourceCode = tecSourceCode.Text;
                                                        sync.Set();
                                                    }));
                else
                    return tecSourceCode.Text;
                sync.WaitOne();
                return sourceCode;
            }

            catch (Exception)
            {
                return "";                
            }  */
        }

        public TextEditorControl getObject_TextEditorControl()
        {
            return tecSourceCode;
        }

        public string getFullPathTOCurrentSourceCodeFile()
        {
            Files.checkIfDirectoryExistsAndCreateIfNot(tbSourceCode_DirectoryOfFileLoaded.Text);
            //return Path.Combine(sDirectoryOfFileLoaded, tbSourceCode_FileLoaded.Text);            
            return Path.Combine(tbSourceCode_DirectoryOfFileLoaded.Text, tbSourceCode_FileLoaded.Text);
        }

        public void saveSourceCode()
        {
            if (partialFileViewMode == false)
            {
                // if (lbSourceCode_UnsavedChanges.Visible)
                // {
                String sFilePath = getFullPathTOCurrentSourceCodeFile();
                saveSourceCodeFile(sFilePath);
                if (File.Exists(sFilePath))
                    sPathToFileLoaded = sFilePath;
            }
            // }
        }

        public void highlightLineWithColor(Int32 iLineToSelect, Color cColor)
        {
            //TextEditorControl tecControl = tecSourceCode.ActiveTextAreaControl;            
            //ICSharpCode.TextEditor.Document.LineSegment lsLineSegment = tecSourceCode.Document.GetLineSegment(iLineToSelect);
            //lsLineSegment.
        }

        public void cleanHighLights()
        {
        }

        public void gotoLine(Int32 iLineToSelect)
        {
            try
            {
                if (partialFileViewMode)
                    selectLineFromPartialFileContents((uint)iLineToSelect);
                else
                    this.okThreadSync(delegate
                                                            {
                                                                tecSourceCode.LineViewerStyle = LineViewerStyle.FullRow;
                                                                TextAreaControl teaControl =
                                                                    tecSourceCode.ActiveTextAreaControl;
                                                                if (iLineToSelect < 1)
                                                                    teaControl.JumpTo(0);
                                                                else
                                                                    teaControl.JumpTo(iLineToSelect - 1);

                                                                tbExecutionHistoryOrLog.Text =
                                                                    string.Format("{0}: {1}{2}", getSelectedLineNumber(),
                                                                                  getSelectedLineText(),
                                                                                  Environment.NewLine) +
                                                                    tbExecutionHistoryOrLog.Text;
                                                            });
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in SourceCodeEditor.gotoLine: {0}", ex.Message);
            }
        }

        public void gotoLine(String sPathSourceCodeFile, string lineToSelect)
        {
            try
            {
                gotoLine(sPathSourceCodeFile, Int32.Parse(lineToSelect));
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in gotoLine: {0}", ex.Message);
            }
        }
        public void gotoLine(String sPathSourceCodeFile, Int32 iLineToSelect)
        {

            if (sPathToFileLoaded != sPathSourceCodeFile)
            // only load if the current file is different from the one already loaded
            {
                // tecSourceCode.Visible = false;   
                loadSourceCodeFile(sPathSourceCodeFile);
                //  tecSourceCode.Visible = true;
            }
            gotoLine(iLineToSelect);

        }

        // code snippet from http://owasp-code-central.googlecode.com/svn/trunk/labs/ReportGenerator/ascx/ascxXsltEditor.cs (which I wrote a while back of OWASP Report Generator tool)
        private void searchForTextInTextEditor(string sTextToSearch)
        {
            lbSearch_textNotFound.Visible = false;
            //int iOriginalTextCaret = tecSourceCode.ActiveTextAreaControl.TextArea.Caret.Offset;
            int iOffsetOfEndOfCurrentSelection = tecSourceCode.ActiveTextAreaControl.TextArea.Caret.Offset +
                                                 tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.
                                                     SelectedText.Length;

            int iFoundPos = tecSourceCode.Text.IndexOf(sTextToSearch, iOffsetOfEndOfCurrentSelection);
            //start from the cursor position
            if (iFoundPos == -1) // didn't find anything
                iFoundPos = tecSourceCode.Text.IndexOf(sTextToSearch, 0); // start from the top
            if (iFoundPos > -1) // if there is a match process it
            {
                tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
                tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(
                    new DefaultSelection(tecSourceCode.Document,
                                         tecSourceCode.Document.OffsetToPosition(iFoundPos),
                                         tecSourceCode.Document.OffsetToPosition(iFoundPos + sTextToSearch.Length)));

                tecSourceCode.ActiveTextAreaControl.Caret.Position =
                    tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition;
                tecSourceCode.ActiveTextAreaControl.TextArea.ScrollToCaret();


                tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.FireSelectionChanged();
            }
            else
            {
                lbSearch_textNotFound.Visible = true;
                lbSearch_textNotFound.Text = string.Format("Provided search string not found: '{0}'", sTextToSearch);
            }
        }

        // code snippet from http://owasp-code-central.googlecode.com/svn/trunk/labs/ReportGenerator/ascx/ascxXsltEditor.cs (which I wrote a while back of OWASP Report Generator tool) 
        private void searchForTextInTextEditor_findNext(String sTextToSearch)
        {
            if (iLastFoundPosition > tecSourceCode.Text.Length)
                iLastFoundPosition = 0;
            int iFoundPos = tecSourceCode.Text.IndexOf(sTextToSearch, iLastFoundPosition);
            if (iFoundPos == -1) // try from the begginig
                iFoundPos = tecSourceCode.Text.IndexOf(sTextToSearch, 0);
            if (iFoundPos > -1 & iLastFoundPosition != iFoundPos)
            {
                lbSearch_textNotFound.Visible = false;
                tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
                tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(
                    new DefaultSelection(tecSourceCode.Document,
                                         tecSourceCode.Document.OffsetToPosition(iFoundPos),
                                         tecSourceCode.Document.OffsetToPosition(iFoundPos + sTextToSearch.Length)));

                tecSourceCode.ActiveTextAreaControl.Caret.Position =
                    tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition;

                tecSourceCode.ActiveTextAreaControl.TextArea.ScrollToCaret();
                //tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.FireSelectionChanged();

                iLastFoundPosition = iFoundPos + sTextToSearch.Length;
            }
            else
            {
                lbSearch_textNotFound.Visible = true;
            }
        }

        public void setDirectoryOfFileLoaded(string newPath)
        {
            if (((tbSourceCode_DirectoryOfFileLoaded)).okThread(delegate { setDirectoryOfFileLoaded(newPath); }))
                if (newPath != tbSourceCode_DirectoryOfFileLoaded.Text)
                {
                    tbSourceCode_DirectoryOfFileLoaded.Text = newPath;
                    sDirectoryOfFileLoaded = newPath;
                    lbSourceCode_UnsavedChanges.Visible = true;
                    btSaveFile.Enabled = true;
                    lbSource_CodeFileSaved.Visible = false;
                }
        }

        public void getAtCaret_WordAndObject(ref String sWord, ref String sObject)
        {
            Int32 iCurrentWord = 0;
            Caret cCaret = tecSourceCode.ActiveTextAreaControl.TextArea.Caret;
            LineSegment lsLineSegment =
                tecSourceCode.ActiveTextAreaControl.TextArea.Document.GetLineSegment(cCaret.Position.Y);
            if (lsLineSegment.Words != null)
            {
                foreach (TextWord twWord in lsLineSegment.Words)
                {
                    if (twWord.Offset < cCaret.Position.X && cCaret.Position.X < (twWord.Offset + twWord.Length))
                        break;
                    iCurrentWord++;
                }

                if (iCurrentWord < lsLineSegment.Words.Count)
                {
                    sWord = lsLineSegment.Words[iCurrentWord].Word;

                    // look before
                    int iIndex = iCurrentWord;
                    while (iIndex > 0)
                        if (lsLineSegment.Words[iIndex].Word == "." ||
                            lsLineSegment.Words[iIndex].Type == TextWordType.Word)
                            sObject = lsLineSegment.Words[iIndex--].Word + sObject;
                        else
                            break;
                    //look after
                    // sObject += "   --  ";
                    iIndex = iCurrentWord + 1;
                    while (iIndex < lsLineSegment.Words.Count)
                        if (lsLineSegment.Words[iIndex].Word != ";" &&
                            (lsLineSegment.Words[iIndex].Word == "." ||
                             lsLineSegment.Words[iIndex].Type == TextWordType.Word))
                            sObject += lsLineSegment.Words[iIndex++].Word;
                        else
                            break;

                    PublicDI.log.debug("Word: {0}     Object:{1}", sWord, sObject);
                }
            }
        }

        public void setSelectedLineNumber(string fileName, int lineNumber)
        {
            gotoLine(fileName, lineNumber);
        }

        public void setSelectedLineNumber(int lineNumber)
        {
            gotoLine(lineNumber);
        }

        public int getSelectedLineNumber()
        {
            return (int)this.invokeOnThread(() => tecSourceCode.ActiveTextAreaControl.Caret.Line + 1);
        }

        public string getSelectedLineText()
        {
            return (string) this.invokeOnThread(
                                () =>
                                    {
                                        var currentLine = tecSourceCode.ActiveTextAreaControl.Caret.Line;
                                        var lineSegment = tecSourceCode.ActiveTextAreaControl.TextArea.TextView.Document.GetLineSegment(currentLine);
                                        return
                                            tecSourceCode.ActiveTextAreaControl.TextArea.TextView.Document.GetText(lineSegment.Offset, lineSegment.Length);
                                    });
        }

        private void executeMethod()
        {
            this.invokeOnThread(
                () => {
                           if (cboxCompliledSourceCodeMethods.SelectedItem != null)
                           {
                               var method = (Reflection_MethodInfo)cboxCompliledSourceCodeMethods.SelectedItem;
                               method.invokeMTA(new object[0]);
                           }
                           return null;
                       });
        }


        public void compileSourceCode()
        {
            if (getSourceCode() != "")
                if (partialFileViewMode == false)
                    this.invokeOnThread(() =>
                            {
                                var fileExtention = Path.GetExtension(sPathToFileLoaded).ToLower();
                                if (fileExtention == ".o2")
                                    fileExtention = Path.GetExtension(Path.GetFileNameWithoutExtension(sPathToFileLoaded));
                                //PublicDI.log.info("in compileSourceCode");
                                switch (fileExtention)
                                {
                                    case ".cs":
                                        compileDotNetCode();
                                        break;
                                    default:
                                        var currentExternalEngine = cbExternalEngineToUse.Text;
                                        //PublicDI.log.info("Going to execute PY");
                                        executeOnExternalEngine(currentExternalEngine);
                                        break;

                                }
                            }
                        );

        }

        private void compileDotNetCode()
        {
            try
            {
                compiledAssembly = null;
                var compileEngine = new CompileEngine();
                var previousExecutedMethod = cboxCompliledSourceCodeMethods.Text;
                if (getSourceCode() != "")
                {
                    saveSourceCode();
                    // always save before compiling                                                
                    compileEngine.compileSourceFile(sPathToFileLoaded);
                    compiledAssembly = compileEngine.compiledAssembly ?? null;
                }

                // set gui options depending on compilation result
                var assemblyCreated = compiledAssembly != null;

                btShowHideCompilationErrors.Visible = tvCompilationErrors.Visible = lbCompilationErrors.Visible =
                    !assemblyCreated && compileEngine.sbErrorMessage != null;

                btDebugMethod.Visible = executeSelectedMethodToolStripMenuItem.Visible =
                    btDragAssemblyCreated.Visible = btExecuteSelectedMethod.Visible =
                    lbExecuteCode.Visible = cboxCompliledSourceCodeMethods.Visible
                    = assemblyCreated;
                cboxCompliledSourceCodeMethods.Items.Clear();

                clearBookmarksAndMarkers();
                // if there is compiledAssembly, show errors
                if (!assemblyCreated)
                {
                    compileEngine.addErrorsListToTreeView(tvCompilationErrors);
                    showErrorsInSourceCodeEditor(compileEngine.sbErrorMessage.ToString());
                }
                else
                {
                    // if not, populate the cbox for dynamic execution                
                    O2Messages.dotNetAssemblyAvailable(compiledAssembly.Location);
                    foreach (var method in PublicDI.reflection.getMethods(compiledAssembly))
                        if (false == method.IsAbstract && false == method.IsSpecialName)
                            cboxCompliledSourceCodeMethods.Items.Add(new Reflection_MethodInfo(method));
                    // remap the previously executed method
                    if (cboxCompliledSourceCodeMethods.Items.Count > 0)
                    {
                        foreach (var method in cboxCompliledSourceCodeMethods.Items)
                            if (method.ToString() == previousExecutedMethod)
                            {
                                cboxCompliledSourceCodeMethods.SelectedItem = method;
                            }
                        cboxCompliledSourceCodeMethods.SelectedIndex = 0;
                    }
                }
                tecSourceCode.Refresh();
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in compileDotNetCode:{0}", ex.Message);
            }
        }

        private void showErrorsInSourceCodeEditor(string errorMessages)
        {
            String[] sSplitedErrorMessage = errorMessages.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string sSplitMessage in sSplitedErrorMessage)
            {
                string[] sSplitedLine = sSplitMessage.Split(new[] { "::" }, StringSplitOptions.None);
                if (sSplitedLine.Length == 5)
                {
                    if (sPathToFileLoaded.ToLower() == sSplitedLine[4].ToLower())            // only show errors for the loaded file
                    {
                        int iLine = Int32.Parse(sSplitedLine[0]);
                        int iColumn = Int32.Parse(sSplitedLine[1]);
                        setSelectedText(iLine, iColumn, true);
                    }
                    //O2Messages.fileErrorHighlight(sSplitedLine[4], iLine, iColumn);                    
                }
            }
        }


        public static void addExternalEngine(string engineName, Func<string, DataReceivedEventHandler, Process> engineExecuteFunction)
        {
            externalExecutionEngines.Add(engineName, engineExecuteFunction);
        }

        private void executeOnExternalEngine(string engineToUse)
        {
            //PublicDI.log.info("executing PY");
            /*if (!File.Exists(ironPhytonExe))
                PublicDI.log.error("Count not find Iron Phyton executable: {0}", ironPhytonExe);
            else
            {*/
            saveSourceCode();
            // make the log visible
            tbExecutionHistoryOrLog.Visible = true;
            tbExecutionHistoryOrLog.Text =
                "****************************************************************************************" + Environment.NewLine +
                "O2 Message: Executing External Engine script: " + Path.GetFileName(sPathToFileLoaded) + Environment.NewLine +
                "****************************************************************************************" + Environment.NewLine + Environment.NewLine;
            // execute script

            if (externalExecutionEngines.ContainsKey(engineToUse))
            {
                if (externalExecutionEngines[engineToUse] != null)
                    externalExecutionEngines[engineToUse](sPathToFileLoaded, externalEngineExecutionLogCallback);
            }
            else
                writeMessageTo_ExecutionHistoryOrLog(string.Format("COULD NOT EXECUTE SCRIPT. Engine Not supported: {0}", engineToUse));
            /*switch(engineToUse)
            {
                case "IronPython":
                    IronPythonExec.executePythonFile(sPathToFileLoaded, pythonExecutionLogCallback);
                    break;
                case "Jython":
                    JythonExec.executePythonFile(sPathToFileLoaded, pythonExecutionLogCallback);
                    break;
                case "CPython":
                    CPythonExec.executePythonFile(sPathToFileLoaded, pythonExecutionLogCallback);
                    break;
                default:
                        
                    break;
            }*/
            //Processes.startProcessAsConsoleApplication(ironPhytonExe, sPathToFileLoaded, pythonExecutionLogCallback);
            //}

        }

        private void externalEngineExecutionLogCallback(object sender, DataReceivedEventArgs e)
        {
            writeMessageTo_ExecutionHistoryOrLog(e.Data);
        }

        public void writeMessageTo_ExecutionHistoryOrLog(string logMessage)
        {
            if (!string.IsNullOrEmpty(logMessage))
            {
                this.invokeOnThread(() => tbExecutionHistoryOrLog.AppendText(logMessage + Environment.NewLine));
                PublicDI.log.info("\t:{0}:", logMessage);
            }
        }

        public void showLogViewerControl()
        {
            O2Messages.setAscxDockStateAndOpenIfNotAvailable("ascx_LogViewer", O2DockState.DockBottom, "O2 Logs");
        }


        public List<String> getSampleScriptsNames()
        {
            return (sampleScripts != null) ? new List<string>(sampleScripts.Keys) : new List<string>();
        }

        public void loadSampleScripts()
        {
            loadSampleScripts(typeof(O2SampleScripts));
        }

        public void loadSampleScripts(object resourcesObjectWithSampleScripts)
        {
            this.invokeOnThread(
                () => {
                           sampleScripts = SampleScripts.getDictionaryWithSampleScripts(resourcesObjectWithSampleScripts);
                           cBoxSampleScripts.Items.Clear();
                           foreach (var scriptName in sampleScripts.Keys)
                               cBoxSampleScripts.Items.Add(scriptName);
                           if (cBoxSampleScripts.Items.Count > 0)
                           {
                               cBoxSampleScripts.SelectedIndex = 0;
                               lbSampleScripts.Visible = true;
                               cBoxSampleScripts.Visible = true;
                           }
                           return null;
                       });

            /*    O2Forms.newTreeNode(tvSampleScripts.Nodes, scriptName, 1, sampleScripts[scriptName]);

            if (tvSampleScripts.Nodes.Count > 0)
                tvSampleScripts.SelectedNode = tvSampleScripts.Nodes[0];*/
        }

        public void loadSampleScript(string scriptToLoad)
        {
            if (scriptToLoad != "" && sampleScripts.ContainsKey(scriptToLoad))
            {
                var tempScriptFileName = PublicDI.config.TempFileNameInTempDirectory + "_" + Path.GetFileName(scriptToLoad);
                Files.WriteFileContent(tempScriptFileName, sampleScripts[scriptToLoad]);
                loadSourceCodeFile(tempScriptFileName);
                tecSourceCode.Focus();
                //compileSourceCode();
            }
        }

        private void setMaxLoadSize(string newMaxLoadSize)
        {
            try
            {
                iMaxFileSize = Int32.Parse(newMaxLoadSize);
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex, "in setMaxLoadSize");
            }

        }

        private void showSelectedErrorOnSourceCodeFile()
        {
            var selectedError = getSelectedError();
            if (selectedError == "")
                return;
            PublicDI.log.info(selectedError);

            string[] sSplitedLine = selectedError.Split(new[] { "::" }, StringSplitOptions.None);
            if (sSplitedLine.Length == 5)
            {
                int iLine = Int32.Parse(sSplitedLine[0]);
                gotoLine(iLine);
            }
        }

        public string getSelectedError()
        {
            if (tvCompilationErrors.SelectedNode != null)
                return tvCompilationErrors.SelectedNode.Text;
            return "";
        }

        private object GetCurrentCSharpObjectModel()
        {
            var timer = new O2Timer("Calculated O2 Object Model for referencedAssesmblies").start();
            var signatures = new List<string>();
            var referencedAssemblies = new CompileEngine().getListOfReferencedAssembliesToUse();

            //compileEngine.lsGACExtraReferencesToAdd();
            foreach (var referencedAssesmbly in referencedAssemblies)
                if (File.Exists(referencedAssesmbly))
                    foreach (var method in PublicDI.reflection.getMethods(referencedAssesmbly))
                        signatures.Add(new FilteredSignature(method).sSignature);
            timer.stop();
            return signatures;
        }

        public void reloadCurrentFile()
        {
            var currentLoadedFile = sPathToFileLoaded;// getFullPathTOCurrentSourceCodeFile();
            if (File.Exists(currentLoadedFile))
            {
                PublicDI.log.info("reloading file: {0}", currentLoadedFile);
                sPathToFileLoaded = ""; // reset this value (since that would prevent this file from being opened again
                loadSourceCodeFile(currentLoadedFile);
            }
        }

        public void openFile()
        {
            PublicDI.log.info("Select file to open");
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PublicDI.log.info("Loading file: {0}", openFileDialog.FileName);
                loadSourceCodeFile(openFileDialog.FileName);
            }
        }

        public void setDocumentContents(string documentContents)
        {
            setDocumentContents(documentContents, "", true);
        }

        public void setDocumentContents(string documentContents, string fileNameOrExtension)
        {
            setDocumentContents(documentContents, fileNameOrExtension, true);
        }

        public void setDocumentContents(string documentContents, string fileNameOrExtension, bool clearFileLocationValues)
        {
            this.invokeOnThread(
                    () =>
                    {
                        try
                        {
                            tecSourceCode.Document.TextContent = documentContents;
                            tecSourceCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(fileNameOrExtension);
                            if (clearFileLocationValues)
                            {
                                sPathToFileLoaded = "";
                                tbSourceCode_DirectoryOfFileLoaded.Text = "";
                                tbSourceCode_FileLoaded.Text = "";
                            }
                            tecSourceCode.ActiveTextAreaControl.Refresh();
                        }
                        catch (Exception ex)
                        {
                            PublicDI.log.error("In setDocumentContents: ", ex.Message);
                        }
                    });
        }

        public void setSelectedText(int line, int column, bool color)
        {
            this.invokeOnThread(
                () => setSelectedText2(line, column, color));
        }

        public void setSelectedText2(int line, int column, bool color)
        {
            line--; // since the first line is at 0
            column--; // same for column
            var text = tecSourceCode.ActiveTextAreaControl.TextArea.Text;
            var location = new TextLocation(column, line);
            var bookmark = new Bookmark(tecSourceCode.Document, location);

            tecSourceCode.Document.BookmarkManager.AddMark(bookmark);
            var offset = bookmark.Anchor.Offset;
            var length = bookmark.Anchor.Line.Length - column;
            var newMarker = new TextMarker(offset, length, TextMarkerType.WaveLine);
            tecSourceCode.Document.MarkerStrategy.AddMarker(newMarker);
            // tecSourceCode.ActiveTextAreaControl.Refresh();
        }

        public void clearBookmarksAndMarkers()
        {
            tecSourceCode.Document.BookmarkManager.Clear();
            // since there is easy way to remove markers, lets make the current ones invisible
            var textMarkers = new List<TextMarker>(tecSourceCode.Document.MarkerStrategy.TextMarker);
            foreach (var textMarker in textMarkers)
                tecSourceCode.Document.MarkerStrategy.RemoveMarker(textMarker);
        }

        public void openO2ObjectModel()
        {
            O2Messages.openControlInGUI(typeof(ascx_O2ObjectModel), O2DockState.Float, "O2 Object Model");
        }

        public void setAutoCompileStatus(bool state)
        {
            if (autoCompileThread != null && state == false)
                autoCompileThread.Abort();
            else
                if (autoCompileThread == null && state)
                {
                    autoCompileThread = O2Thread.mtaThread(
                        () =>
                        {
                            PublicDI.log.debug("Starting auto compile loop");
                            while (true)
                            {
                                compileSourceCode();
                                Processes.Sleep(10000, true);
                            }
                            //PublicDI.log.debug("Exiting auto compile loop");
                        });

                    autoCompileThread.Priority = ThreadPriority.Lowest;
                }

        }


        public void addBreakpointOnCurrentLine()
        {
            var currentLine = tecSourceCode.ActiveTextAreaControl.Caret.Line;
            var lineSegment = tecSourceCode.ActiveTextAreaControl.TextArea.TextView.Document.GetLineSegment(currentLine);
            O2Messages.raiseO2MDbg_SetBreakPointOnFile(sPathToFileLoaded, lineSegment.LineNumber + 1);
        }

        public void createStandAloneExeAndDebugMethod(string loadDllsFrom)
        {
            this.invokeOnThread(
                () =>
                {
                    if (cboxCompliledSourceCodeMethods.SelectedItem != null && cboxCompliledSourceCodeMethods.SelectedItem is Reflection_MethodInfo)
                    {
                        ((Reflection_MethodInfo)cboxCompliledSourceCodeMethods.SelectedItem).raiseO2MDbgDebugMethodInfoRequest(loadDllsFrom);
                    }
                });
        }

        private void setDebugButtonEnableState()
        {
            this.invokeOnThread(
            () =>
            {
                if (O2Messages.isDebuggerAvailable())
                {
                    btDebugMethod.Visible = true;
                    if (cboxCompliledSourceCodeMethods.SelectedItem != null && cboxCompliledSourceCodeMethods.SelectedItem is Reflection_MethodInfo)
                    {
                        var selectedMethod = ((Reflection_MethodInfo)cboxCompliledSourceCodeMethods.SelectedItem).Method;
                        if (selectedMethod != null)
                        {
                            if (selectedMethod.IsStatic &&
                                    selectedMethod.ReturnType.FullName == "System.Void" &&
                                    selectedMethod.GetParameters().Length == 0)
                            {
                                btDebugMethod.Enabled = true;
                                return;
                            }
                            else
                                PublicDI.log.debug("Debugging note: the only supported methods to start the debugging session are: static methods, with no parameters and returning void");
                        }
                        btDebugMethod.Enabled = false;
                    }
                }
                else
                    btDebugMethod.Visible = false;
            });
        }

        private static void listinLogViewCurrentAssemblyReferencesAutomaticallyAdded()
        {
            PublicDI.log.debug(StringsAndLists.fromStringList_getText(new CompileEngine().getListOfReferencedAssembliesToUse()));
        }
    }
}
