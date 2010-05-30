using System;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Text;

namespace DemoApp
{
    public class AppSettings : Settings
    {
        public string BaseIEName = "ie";
        public string PopupIEName = "iepopup";
        public double TypingTime = 1000;
        public bool WarnWhenUnsaved = true;
        public enum ScriptFormats { Snippet, Console, NUnit, MBUnit, VS2005Library }
        public ScriptFormats ScriptFormatting = ScriptFormats.Snippet;
        public string CompilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\Compilations\\";

        public System.Drawing.Font ScriptWindowFont;
        public System.Drawing.Color DOMHighlightColor = System.Drawing.Color.Yellow;
        public string NUnitFrameworkPath = "";
        public string MBUnitFrameworkPath = "";
        public string VS2005FrameworkPath = "";
        public System.Collections.Specialized.StringCollection ReferencedAssemblies = new System.Collections.Specialized.StringCollection();
        public bool HideDOSWindow = false;
        XmlDocument xmlDocument = new XmlDocument();
        string documentPath = Application.StartupPath + "//settings.xml";
        public RichTextBox rtbTarget = null;
        public string FindPattern = "id, name, href, url, src, value, style, innertext";
        public enum CodeLanguages { CSharp, VBNet, PHP, Python, Perl }
        public CodeLanguages CodeLanguage = CodeLanguages.CSharp;
        public string DefaultSaveTemplate = "";
        public string DefaultRunTemplate = "";
        public string DefaultCompileTemplate = "";
        public int RunCount = 0;

        public AppSettings(string path) : base(path){}

        public void LoadSettings(RichTextBox rtb)
        {
            BaseIEName = GetSetting("BaseIEName", "ie");
            PopupIEName = GetSetting("PopupIEName", "iepopup");
            RunCount = Convert.ToInt32(GetSetting("RunCount", "0"));
            TypingTime = Convert.ToDouble(GetSetting("TypingTime", "1000"));
            WarnWhenUnsaved = GetSetting("WarnWhenUnsaved", 1) == 1 ? true : false;
            HideDOSWindow = GetSetting("HideDOSWindow", 1) == 1 ? true : false;
            CompilePath = GetSetting("CompilePath", AppDirectory);
            FindPattern = GetSetting("FindPattern", "name, href, url, src, value, style, innertext");
            DOMHighlightColor = Color.FromName(GetSetting("DOMHighlightColor", "Yellow"));
            ScriptFormatting = (ScriptFormats)System.Enum.Parse(typeof(ScriptFormats), GetSetting("ScriptFormatting", "Snippet"), true);
            CodeLanguage = (CodeLanguages)System.Enum.Parse(typeof(CodeLanguages), GetSetting("CodeLanguage", "CSharp"), true);

            DefaultSaveTemplate = GetSetting("DefaultSaveTemplate", "");
            DefaultRunTemplate = GetSetting("DefaultRunTemplate", "");
            DefaultCompileTemplate = GetSetting("DefaultCompileTemplate", "");

            string _fontName = GetSetting("FontName", rtb.Font.FontFamily.Name);
            float _fontSize = float.Parse(GetSetting("FontSize", rtb.Font.Size.ToString()));
            if (_fontName == "")
            {
                ScriptWindowFont = rtb.Font;
            }
            else
            {
                ScriptWindowFont = new Font(_fontName, _fontSize);
                rtb.Font = ScriptWindowFont;
            }
        }

        public void SaveSettings(RichTextBox rtb)
        {
            try
            {
                PutSetting("BaseIEName", BaseIEName);
                PutSetting("PopupIEName", PopupIEName);
                PutSetting("TypingTime", TypingTime.ToString());
                PutSetting("FindPattern", FindPattern);
                PutSetting("WarnWhenUnsaved", WarnWhenUnsaved ? 1 : 0);
                PutSetting("HideDOSWindow", HideDOSWindow ? 1 : 0);
                PutSetting("CompilePath", CompilePath);
                PutSetting("DOMHighlightColor", DOMHighlightColor.ToKnownColor().ToString());
                PutSetting("CodeLanguage", System.Enum.GetName(typeof(CodeLanguages), CodeLanguage));
                PutSetting("ScriptFormatting", System.Enum.GetName(typeof(ScriptFormats), ScriptFormatting));
                PutSetting("FontName", rtb.Font.FontFamily.Name);
                PutSetting("FontSize", rtb.Font.Size.ToString());
                PutSetting("RunCount", ++RunCount);

                PutSetting("DefaultSaveTemplate", DefaultSaveTemplate);
                PutSetting("DefaultRunTemplate", DefaultRunTemplate);
                PutSetting("DefaultCompileTemplate", DefaultCompileTemplate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving settings: {0}",ex.Message));
            }

        }
    }
}
