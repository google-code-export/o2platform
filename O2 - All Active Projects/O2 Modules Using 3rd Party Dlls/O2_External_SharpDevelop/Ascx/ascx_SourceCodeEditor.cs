// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using O2.DotNetWrappers.DotNet;

namespace O2.External.SharpDevelop.Ascx
{
    public partial class ascx_SourceCodeEditor : UserControl
    {        

        public ascx_SourceCodeEditor()
        {
            InitializeComponent();
        }

        public ascx_SourceCodeEditor(String sFileToOpen)
        {
            InitializeComponent();
            this.sFileToOpen = sFileToOpen;
        }


        private void ascx_SourceCodeEditor_Load(object sender, EventArgs e)
        {

            onLoad();
        }

        private bool TextArea_KeyEventHandler(char ch)
        {
            if (ch == '\n')
            {
                if (tecSourceCode.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                {
                    // if we are supporting compilation, see if there must be another controls that want to handle compilation on TextSelected+Enter                    
                    if (btCompileCode.Visible && eEnterInSource_Event != null)                      
                        eEnterInSource_Event();                    
                    else
                        compileSourceCode();
                    return true;
                }
            }
            return false;
        }
        

        private void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            btSave.Enabled = true;
            lbSourceCode_UnsavedChanges.Visible = true;
            btSaveFile.Enabled = true;
            lbSource_CodeFileSaved.Visible = false;
            if (null != eDocumentDataChanged)
                foreach (Delegate dDelegate in eDocumentDataChanged.GetInvocationList())
                    dDelegate.DynamicInvoke(new Object[] {tecSourceCode.Text});
        }

        private void SelectionManager_SelectionChanged(object sender, EventArgs e)
        {
            if (null != eDocumentSelectionChanged_WordAndLine)
                foreach (Delegate dDelegate in eDocumentSelectionChanged_WordAndLine.GetInvocationList())
                {
                    String sWord = "", sObject = "";
                    getAtCaret_WordAndObject(ref sWord, ref sObject);
                    dDelegate.DynamicInvoke(new Object[] {sWord, sObject});
                }
        }

        private void cboxLineNumbers_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowLineNumbers = cboxLineNumbers.Checked;
        }

        private void cboxTabs_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowTabs = cboxTabs.Checked;
        }

        private void cboxSpaces_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowSpaces = cboxSpaces.Checked;
        }

        private void cboxInvalidLines_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowInvalidLines = cboxInvalidLines.Checked;
        }

        private void cboxEOLMarkers_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowEOLMarkers = cboxEOLMarkers.Checked;
        }

        private void cboxHRuler_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowHRuler = cboxHRuler.Checked;
        }

        private void cboxVRuler_CheckedChanged(object sender, EventArgs e)
        {
            tecSourceCode.ShowVRuler = cboxVRuler.Checked;
        }

        
        private void tecSourceCode_Load(object sender, EventArgs e)
        {
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveSourceCode();
        }

        

        private void tbSourceCode_FileLoaded_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void tecSourceCode_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void ascx_SourceCodeEditor_DragEnter(object sender, DragEventArgs e)
        {            
            Dnd.setEffect(e);
        }


        private void tbSourceCode_FileLoaded_DragDrop(object sender, DragEventArgs e)
        {
            processDragAndDropFile(e);
        }


        private void tecSourceCode_DragDrop(object sender, DragEventArgs e)
        {
            processDragAndDropFile(e);
        }

        private void ascx_SourceCodeEditor_DragDrop(object sender, DragEventArgs e)
        {
            processDragAndDropFile(e);
        }

        private void processDragAndDropFile(DragEventArgs e)
        {
            var droppedObject = (DotNetWrappers.Filters.FilteredSignature)Dnd.tryToGetObjectFromDroppedObject(e, typeof(DotNetWrappers.Filters.FilteredSignature));
            if (droppedObject != null)
                tbTextSearch.Text = (droppedObject.sSignature);
            else
                loadSourceCodeFile(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        /*  private void pbSourceCode_MouseDown(object sender, MouseEventArgs e)
        {
            Dnd.DragAndDropAction ddaSourceCode = Dnd.getSourceCodeAction(sPathToFileLoaded);
            DoDragDrop(ddaSourceCode, DragDropEffects.Copy);
        }

        private void pbSourceCode_Click(object sender, EventArgs e)
        {
        }*/

        /*    private void tecSourceCode_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tecSourceCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tecSourceCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }*/

        // this is not working at the moment (need to find a way to implement this highlight
       
        private void tbTextSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                searchForTextInTextEditor_findNext(tbTextSearch.Text);
        }

        private void tbTextSearch_TextChanged(object sender, EventArgs e)
        {
            searchForTextInTextEditor(tbTextSearch.Text);
        }

/*        public static ascx_SourceCodeEditor loadFile(string file)
        {
            var sourceCodeEditor =
                (ascx_SourceCodeEditor)
                DI.windowsForms.openAscx(typeof (ascx_SourceCodeEditor), false, "Editing: " + file);
            sourceCodeEditor.loadSourceCodeFile(file);
            return sourceCodeEditor;
        }*/

        /*  public static void loadFile(string file, int lineToSelect)
        {
            loadFile(file).gotoLine(lineToSelect);
        }*/

        private void tbSourceCode_FileLoaded_TextChanged(object sender, EventArgs e)
        {
            lbSourceCode_UnsavedChanges.Visible = true;
            lbSource_CodeFileSaved.Visible = false;
            btSaveFile.Enabled = true;
        }

        private void tbSourceCode_FileLoaded_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                saveSourceCode();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            groupBoxWithFileAndSaveSettings.Visible = !groupBoxWithFileAndSaveSettings.Visible;
        }

        private void compile_Click(object sender, EventArgs e)
        {
            O2Thread.mtaThread(compileSourceCode);
        }
       
        private void executeSelectedMethod_Click(object sender, EventArgs e)
        {
            O2Thread.mtaThread(executeMethod);
        }

        private void showLogs_Click(object sender, EventArgs e)
        {
            O2Thread.mtaThread(showLogViewerControl);
        }

        private void cBoxSampleScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSampleScript(cBoxSampleScripts.Text);
        }

        private void tecSourceCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbMaxLoadSize_TextChanged(object sender, EventArgs e)
        {
            setMaxLoadSize(tbMaxLoadSize.Text);
        }

        private void btSelectedLineHistory_Click(object sender, EventArgs e)
        {
            tbExecutionHistoryOrLog.Visible = !tbExecutionHistoryOrLog.Visible;
        }

        private void cbCompilationErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSelectedErrorOnSourceCodeFile();
        }

        private void llCurrenlyLoadedObjectModel_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(GetCurrentCSharpObjectModel(), DragDropEffects.Copy);
        }

        private void llReload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reloadCurrentFile();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            O2Thread.mtaThread(compileSourceCode);
        }

        private void btSaveFile_Click(object sender, EventArgs e)
        {
            saveSourceCode();
        }

        private void btDragAssemblyCreated_MouseDown(object sender, MouseEventArgs e)
        {
            if (compiledAssembly != null)
                DoDragDrop(compiledAssembly.Location, DragDropEffects.Copy);
        }

        private void btExecutePythonScript_Click(object sender, EventArgs e)
        {
            executeOnExternalEngine(cbExternalEngineToUse.Text);
        }

        private void btShowHidePythonLogExecutionOutputData_Click(object sender, EventArgs e)
        {
            tbExecutionHistoryOrLog.Visible = !tbExecutionHistoryOrLog.Visible;
        }

        private void cbAutoTryToFixSourceCodeFileReferences_CheckedChanged(object sender, EventArgs e)
        {
            HandleO2MessageOnSD.autoTryToFixSourceCodeFileReferences = cbAutoTryToFixSourceCodeFileReferences.Checked;
        }

        private void btShowHideCompilationErrors_Click(object sender, EventArgs e)
        {
            lboxCompilationErrors.Visible = !lboxCompilationErrors.Visible;
        }

        private void tbShowO2ObjectModel_Click(object sender, EventArgs e)
        {
            scrollBarHorizontalSize_O2ObjectModel.Visible =
                scrollBarVerticalSize_O2ObjectModel.Visible = 
                gbO2ObjectMode.Visible = !gbO2ObjectMode.Visible;
            
        }

        private void scrollBarHorizontalSize_O2ObjectModel_Scroll(object sender, ScrollEventArgs e)
        {
            var difference = gbO2ObjectMode.Width - scrollBarHorizontalSize_O2ObjectModel.Value;
            gbO2ObjectMode.Left += difference;
            gbO2ObjectMode.Width = scrollBarHorizontalSize_O2ObjectModel.Value;
            
        }

        private void scrollBarVerticalSize_O2ObjectModel_Scroll(object sender, ScrollEventArgs e)
        {
            var difference = gbO2ObjectMode.Height - scrollBarVerticalSize_O2ObjectModel.Value;
            gbO2ObjectMode.Top += difference;
            gbO2ObjectMode.Height = scrollBarVerticalSize_O2ObjectModel.Value;
            
        }

        private void gbO2ObjectMode_SizeChanged(object sender, EventArgs e)
        {
            scrollBarVerticalSize_O2ObjectModel.Maximum = gbO2ObjectMode.Height + 20;
            scrollBarVerticalSize_O2ObjectModel.Value = gbO2ObjectMode.Height;

            scrollBarHorizontalSize_O2ObjectModel.Maximum = gbO2ObjectMode.Width + 20;
            scrollBarHorizontalSize_O2ObjectModel.Value = gbO2ObjectMode.Width;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compile_Click(null, null);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tecSourceCode.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null,null);   
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tecSourceCode.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSourceCode();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            openFile();
        }

                                              
    }
}
