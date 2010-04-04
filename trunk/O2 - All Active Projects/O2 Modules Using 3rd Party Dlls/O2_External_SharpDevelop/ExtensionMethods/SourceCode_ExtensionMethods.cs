﻿using System;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.AST;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using System.Collections.Generic;
using System.Drawing;
using O2.API.AST.CSharp;
using System.CodeDom;
using ICSharpCode.SharpDevelop.Dom;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class SourceCode_ExtensionMethods
    {
        public static ascx_SourceCodeEditor add_SourceCodeEditor(this GroupBox groupBox)
        {
            return (ascx_SourceCodeEditor) groupBox.invokeOnThread(
                                               () =>
                                                   {
                                                       var sourceCodeEditor = new ascx_SourceCodeEditor();
                                                       sourceCodeEditor.getObject_TextEditorControl().Document.
                                                           FormattingStrategy =
                                                           new DefaultFormattingStrategy();
                                                       sourceCodeEditor.Dock = DockStyle.Fill;
                                                       groupBox.Controls.Add(sourceCodeEditor);
                                                       return sourceCodeEditor;
                                                   });
        }

        public static ascx_SourceCodeEditor add_SourceCodeEditor(this Control control)
        {
            return (ascx_SourceCodeEditor)control.invokeOnThread(
                                              () =>
                                                  {
                                                      var sourceCodeEditor = new ascx_SourceCodeEditor();
                                                      sourceCodeEditor.getObject_TextEditorControl().Document.
                                                          FormattingStrategy = new DefaultFormattingStrategy();
                                                      sourceCodeEditor.Dock = DockStyle.Fill;
                                                      control.Controls.Add(sourceCodeEditor);
                                                      return sourceCodeEditor;
                                                  });
        }

        public static ascx_SourceCodeViewer add_SourceCodeViewer(this Control control)
        {
            return (ascx_SourceCodeViewer)control.add_Control(typeof(ascx_SourceCodeViewer));
        }

        public static TextEditorControl textEditor(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.textEditorControl();
        }

        public static TextEditorControl textEditor(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.textEditorControl();
        }

        public static TextEditorControl textEditorControl(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.getObject_TextEditorControl();
        }

        public static TextEditorControl textEditorControl(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().getObject_TextEditorControl();
        }

        public static int caretLine(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.textEditorControl().ActiveTextAreaControl.Caret.Line;
        }

        public static ascx_SourceCodeEditor colorCodeForExtension(this ascx_SourceCodeEditor sourceCodeEditor, string extension)
        {
            return (ascx_SourceCodeEditor)sourceCodeEditor.invokeOnThread(() =>
            {

                var tecSourceCode = sourceCodeEditor.textEditorControl();
                var dummyFileName = string.Format("aaa.{0}", extension);
                tecSourceCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(dummyFileName);
                return sourceCodeEditor;
            });
        }

        public static TextEditorControl showAstValueInSourceCode(this TextEditorControl textEditorControl, AstValue<object> astValue)
        {

            PublicDI.log.error("{0} {1} - {2}", astValue.Text, astValue.StartLocation, astValue.EndLocation);
            
            var start = new TextLocation(astValue.StartLocation.X - 1,
                                                                astValue.StartLocation.Y - 1);
            var end = new TextLocation(astValue.EndLocation.X - 1, astValue.EndLocation.Y - 1);
            var selection = new DefaultSelection(textEditorControl.Document, start, end);
            textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(selection);
            setCaretToCurrentSelection(textEditorControl);
            return textEditorControl;
        }

        public static TextEditorControl setCaretToCurrentSelection(this TextEditorControl textEditorControl)
        {
            var finalCaretPosition = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition;
            var tempCaretPosition = new TextLocation
            {
                X = finalCaretPosition.X,
                Y = finalCaretPosition.Y + 10
            };
            textEditorControl.ActiveTextAreaControl.Caret.Position = tempCaretPosition;
            textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
            textEditorControl.ActiveTextAreaControl.Caret.Position = finalCaretPosition;
            textEditorControl.ActiveTextAreaControl.TextArea.ScrollToCaret();
            return textEditorControl;
        }

        public static ascx_SourceCodeViewer set_Text(this ascx_SourceCodeViewer sourceCodeViewer, string text)
        {
             // ToCheckOutLater: I don't really understand why I need to run this of a different thread 
            // (but when developing ascx_O2_Command_Line there was a case where I had an
            // "In setDocumentContents: GapTextBufferStategy is not thread-safe!"
            // error
            return (ascx_SourceCodeViewer)sourceCodeViewer.invokeOnThread(           
                () =>
                    {
                        sourceCodeViewer.setDocumentContents(text);
                        return sourceCodeViewer;
                    });
            
        }

        public static string get_Text(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().getSourceCode();
        }

        public static ascx_SourceCodeViewer onTextChanged(this ascx_SourceCodeViewer sourceCodeViewer, Action<string> textChanged)
        {
            sourceCodeViewer.editor().eDocumentDataChanged += textChanged;
            return sourceCodeViewer;
        }

        public static ascx_SourceCodeEditor editor(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.getSourceCodeEditor();
        }

        public static ascx_SourceCodeViewer allowCompile(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor().allowCodeCompilation = value;
            return sourceCodeViewer;
        }

        public static ascx_SourceCodeViewer astDetails(this ascx_SourceCodeViewer sourceCodeViewer, bool value)
        {
            sourceCodeViewer.editor()._ShowSearchAndAstDetails = value;
            return sourceCodeViewer;
        }

        public static ascx_SourceCodeEditor vScroolBar_Enabled(this ascx_SourceCodeEditor sourceCodeEditor, bool value)
        {
            sourceCodeEditor.getObject_TextEditorControl().ActiveTextAreaControl.VScrollBar.Enabled = value;
            return sourceCodeEditor;
        }

        public static ascx_SourceCodeEditor vScroolBar_Visible(this ascx_SourceCodeEditor sourceCodeEditor, bool value)
        {
            sourceCodeEditor.getObject_TextEditorControl().ActiveTextAreaControl.VScrollBar.Visible = value;
            return sourceCodeEditor;
        }

        public static ascx_SourceCodeEditor hScroolBar_Enabled(this ascx_SourceCodeEditor sourceCodeEditor, bool value)
        {
            sourceCodeEditor.getObject_TextEditorControl().ActiveTextAreaControl.HScrollBar.Enabled = value;
            return sourceCodeEditor;
        }

        public static ascx_SourceCodeEditor hScroolBar_Visible(this ascx_SourceCodeEditor sourceCodeEditor, bool value)
        {
            sourceCodeEditor.getObject_TextEditorControl().ActiveTextAreaControl.HScrollBar.Visible = value;
            return sourceCodeEditor;
        }
        public static ascx_SourceCodeViewer set_ColorsForCSharp(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            sourceCodeViewer.editor().setDocumentHighlightingStrategy("aa.cs");
            return sourceCodeViewer;
        }

        public static IDocument document(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.getObject_TextEditorControl().Document;
        }

        public static IDocument document(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().document();
        }

        public static ascx_SourceCodeEditor set_ColorsForCSharp(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            sourceCodeEditor.setDocumentHighlightingStrategy("aa.cs");
            return sourceCodeEditor;
        }

        public static ascx_SourceCodeEditor open(this ascx_SourceCodeEditor sourceCodeEditor, string fileToOpen)
        {
            sourceCodeEditor.loadSourceCodeFile(fileToOpen);
            return sourceCodeEditor;
        }

        public static ascx_SourceCodeViewer open(this ascx_SourceCodeViewer sourceCodeViewer, string fileToOpen)
        {
            sourceCodeViewer.editor().loadSourceCodeFile(fileToOpen);
            return sourceCodeViewer;
        }

        public static ascx_SourceCodeViewer enableCodeComplete(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            sourceCodeViewer.editor().enableCodeComplete();
            return sourceCodeViewer;
        }

        // this wasn't working as expected
        /*public static void enableCodeComplete(this ascx_SourceCodeEditor sourceCodeEditor, ascx_SourceCodeEditor sourceCodeEditorToGrabCodeFrom)
        {
            var o2CodeComplete = sourceCodeEditor.enableCodeComplete();
            o2CodeComplete.textEditorToGrabCodeFrom = sourceCodeEditorToGrabCodeFrom.getObject_TextEditorControl();
        }
        public static void enableCodeComplete(this ascx_SourceCodeViewer sourceCodeViewer, ascx_SourceCodeViewer sourceCodeViewerToGrabCodeFrom)
        {
            sourceCodeViewer.editor().enableCodeComplete(sourceCodeViewerToGrabCodeFrom.editor());
        }*/
        public static TextArea textArea(this ascx_SourceCodeViewer sourceCodeViewer)
        {
            return sourceCodeViewer.editor().textArea();
        }
        public static TextArea textArea(this ascx_SourceCodeEditor sourceCodeEditor)
        {
            return sourceCodeEditor.textEditorControl().ActiveTextAreaControl.TextArea;
        }

        public static O2CodeCompletion updateCodeComplete(this ascx_SourceCodeViewer sourceCodeViewer, CSharp_FastCompiler csharpFastCompiler)
        { 
            return sourceCodeViewer.editor().updateCodeComplete(csharpFastCompiler);
        }

        public static O2CodeCompletion updateCodeComplete(this ascx_SourceCodeEditor sourceCodeEditor, CSharp_FastCompiler csharpFastCompiler)
        {
            if (sourceCodeEditor.o2CodeCompletion != null)
            {
                foreach (var extraReference in csharpFastCompiler.ExtraSourceCodeFilesToCompile)
                    sourceCodeEditor.o2CodeCompletion.parseFile(extraReference);
                //var currentCode = csharpFastCompiler.processedCode();
               var currentCode = csharpFastCompiler.SourceCode;
                sourceCodeEditor.o2CodeCompletion.parseSourceCode(currentCode);
               sourceCodeEditor.o2CodeCompletion.CodeCompleteCaretLocationOffset = csharpFastCompiler.getGeneratedSourceCodeMethodLineOffset();
                
                sourceCodeEditor.o2CodeCompletion.CodeCompleteTargetText = currentCode;
                // i might not need these
                sourceCodeEditor.textArea().CodeCompleteCaretLocationOffset = csharpFastCompiler.getGeneratedSourceCodeMethodLineOffset();

            }
            return sourceCodeEditor.o2CodeCompletion;
        }


        public static ascx_SourceCodeEditor showInCodeEditor(this string fileOrCode)
        {
            if (fileOrCode.fileExists())
                return fileOrCode.showInCodeEditor(fileOrCode.extension());
            else
                return fileOrCode.showInCodeEditor(".cs");
        }

        public static ascx_SourceCodeEditor showInCodeEditor(this string fileOrCode, string mapAsExtension)
        {
            var codeEditor = O2Gui.open<ascx_SourceCodeEditor>();
            if (fileOrCode.fileExists())
                codeEditor.open(fileOrCode);
            else
                codeEditor.setDocumentContents(fileOrCode);
            codeEditor.setDocumentHighlightingStrategy(mapAsExtension);
            return codeEditor;
        }

        public static ascx_SourceCodeViewer showInCodeViewer(this string fileOrCode)
        {
            if (fileOrCode.fileExists())
                return fileOrCode.showInCodeViewer(fileOrCode.extension());
            else
                return fileOrCode.showInCodeViewer(".cs");
        }

        public static ascx_SourceCodeViewer showInCodeViewer(this string fileOrCode, string mapAsExtension)
        {
            var codeViewer = O2Gui.open<ascx_SourceCodeViewer>();
            if (fileOrCode.fileExists())
                codeViewer.open(fileOrCode);
            else
                codeViewer.setDocumentContents(fileOrCode);
            codeViewer.editor().setDocumentHighlightingStrategy(mapAsExtension);
            return codeViewer;
        }

        public static ascx_SourceCodeViewer set_Text(this ascx_SourceCodeViewer codeViewer, string text, string highlightForExtension)
        {
            codeViewer.editor().set_Text(text, highlightForExtension);
            return codeViewer;
        }

        public static ascx_SourceCodeEditor set_Text(this ascx_SourceCodeEditor codeEditor, string text, string highlightForExtension)
        {
            codeEditor.set_Text(text);
            codeEditor.setDocumentHighlightingStrategy(highlightForExtension);
            return codeEditor;
        }

        public static ascx_SourceCodeEditor set_Text(this ascx_SourceCodeEditor codeEditor, string text)
        {
            codeEditor.setDocumentContents(text);
            return codeEditor;
        }


        public static ascx_SourceCodeViewer load(this ascx_SourceCodeViewer codeViewer, string fileOrCode)
        {
            codeViewer.editor().load(fileOrCode);
            return codeViewer;
        }

        public static ascx_SourceCodeEditor load(this ascx_SourceCodeEditor codeEditor, string fileOrCode)
        {
            if (fileOrCode.fileExists())
            {
                codeEditor.open(fileOrCode);
            }
            else
            {
                codeEditor.set_ColorsForCSharp();
                codeEditor.setDocumentContents(fileOrCode);
            }
            return codeEditor;
        }

        public static Caret caret(this ascx_SourceCodeEditor codeEditor)
        {
            return codeEditor.textArea().Caret;
        }

        public static ascx_SourceCodeEditor caret(this ascx_SourceCodeEditor codeEditor, int line, int column)
        {
            return codeEditor.caret(line, column, 3);
        }

        public static ascx_SourceCodeEditor caret(this ascx_SourceCodeEditor codeEditor, int line, int column, int viewOffset)
        {
            return (ascx_SourceCodeEditor)codeEditor.invokeOnThread(
                () =>
                {
                    codeEditor.caret().Line = line - 1 + viewOffset;  // so that the selected line is not at the bottom of the screen
                    codeEditor.caret().Line = line - 1;
                    codeEditor.caret().Column = column - 1;
                    return codeEditor;
                });
        }

        public static ascx_SourceCodeEditor caret_Line(this ascx_SourceCodeEditor codeEditor, int value)
        {
            return codeEditor.caret_Line(value, 3);
        }

        public static ascx_SourceCodeEditor caret_Line(this ascx_SourceCodeEditor codeEditor, int value, int viewOffset)
        {
            return (ascx_SourceCodeEditor)codeEditor.invokeOnThread(
                () =>
                {
                    codeEditor.caret().Line = value + viewOffset;  // so that the selected line is not at the bottom of the screen
                    codeEditor.caret().Line = value;
                    return codeEditor;
                });
        }

        public static ascx_SourceCodeViewer onCaretMove(this ascx_SourceCodeViewer codeViewer, Action<Caret> callback)
        {
            codeViewer.editor().onCaretMove(callback);
            return codeViewer;
        }

        public static ascx_SourceCodeEditor onCaretMove(this ascx_SourceCodeEditor codeEditor, Action<Caret> callback)
        {
            codeEditor.textArea().Caret.PositionChanged += (sender, e) => callback(codeEditor.caret());
            return codeEditor;
        }

        public static ascx_SourceCodeEditor setSelectionText(this ascx_SourceCodeEditor codeEditor, Location startLocation, Location endLocation)
        {
            return (ascx_SourceCodeEditor)codeEditor.invokeOnThread(() =>
            {
                var start = new ICSharpCode.TextEditor.TextLocation(startLocation.X - 1, startLocation.Y - 1);
                var end = new ICSharpCode.TextEditor.TextLocation(endLocation.X - 1, endLocation.Y - 1);
                var selection = new ICSharpCode.TextEditor.Document.DefaultSelection(codeEditor.document(), start, end);
                codeEditor.textArea().SelectionManager.SetSelection(selection);
                codeEditor.caret_Line(start.Line);
                return codeEditor;
            });
        }

        public static ascx_SourceCodeEditor selectTextWithColor(this ascx_SourceCodeEditor codeEditor, int startLine, int startColumn, int endLine, int endColumn)
        {
            return codeEditor.selectTextWithColor(new TextLocation(startColumn - 1, startLine - 1), new TextLocation(endColumn - 1, endLine - 1));
        }

        public static ascx_SourceCodeEditor selectTextWithColor(this ascx_SourceCodeEditor codeEditor, TextLocation startLocation, TextLocation endLocation)
        {
            if (startLocation > endLocation)
            {
                "in ascx_SourceCodeEditor.selectTextWithColor startLocation > endLocation".error();
                return codeEditor;
            }
            return codeEditor.selectTextWithColor(new DefaultSelection(codeEditor.document(), startLocation, endLocation));
        }

        public static ascx_SourceCodeEditor selectTextWithColor(this ascx_SourceCodeEditor codeEditor, DefaultSelection selection)
        {
            return codeEditor.selectTextWithColor(selection, TextMarkerType.SolidBlock, Color.LightBlue);
        }

        public static ascx_SourceCodeEditor selectTextWithColor(this ascx_SourceCodeEditor codeEditor, DefaultSelection selection, TextMarkerType textMarkerType, Color color)
        {
            if (selection.Length < 0)
            {
                "in ascx_SourceCodeEditor.selectTextWithColor selection.Length was <  0".error();
                return codeEditor;
            }
            "offset: {0} : lenght {1}".format(selection.Offset, selection.Length).info();
            return (ascx_SourceCodeEditor)codeEditor.invokeOnThread(
            () =>
            {
                "offset: {0} : lenght {1}".format(selection.Offset, selection.Length).info();
                var newMarker = new TextMarker(
                                        selection.Offset,
                                        selection.Length,
                                        textMarkerType, color);
                codeEditor.document().MarkerStrategy.AddMarker(newMarker);
                return codeEditor;
            });
        }

        public static TextLocation textLocation(this Location location)
        {
            return new TextLocation(location.Column - 1, location.Line - 1);
        }

        public static ascx_SourceCodeEditor selectTextWithColor(this ascx_SourceCodeEditor codeEditor, INode node)
        {
            return codeEditor.selectTextWithColor(node.StartLocation.textLocation(), node.EndLocation.textLocation());
        }

        public static ascx_SourceCodeEditor colorINodes(this ascx_SourceCodeEditor codeEditor, List<INode> nodes)
        {
            codeEditor.clearMarkers();
            foreach (var node in nodes)
                codeEditor.selectTextWithColor(node);
            codeEditor.refresh();
            return codeEditor;
        }

      

    }
}