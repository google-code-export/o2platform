using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.InteropServices;

namespace DemoApp
{
    public class Templates
    {
        public List<Template> TemplateList = new List<Template>();

        // create loads the list of templates for language
        public Templates(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return;
            }

            string[] arrFiles = System.IO.Directory.GetFiles(path, "*.trt");

            for (int i = 0; i < arrFiles.Length; i++)
            {
                Template temp = new Template(arrFiles[i]);
                TemplateList.Add(temp);
            }
        }

        public Template GetTemplateByIndex(AppSettings.CodeLanguages language, int Index)
        {
            List<Template> resultlist = new List<Template>();
            foreach (Template tfile in TemplateList)
            {
                if (language != tfile.CodeLanguage)
                {
                    continue;
                }
                resultlist.Add(tfile);
            }

            if (Index > resultlist.Count - 1)
            {
                return null;
            }
            return resultlist[Index];
        }

        public string GetSaveFilter(AppSettings.CodeLanguages language)
        {
            StringBuilder sbResult = new StringBuilder();
            foreach (Template tfile in TemplateList)
            {
                if (language != tfile.CodeLanguage)
                {
                    continue;
                }
                sbResult.Append("|"+tfile.Name+" ("+tfile.FileExtension+")|");
            }
            sbResult.Insert(0,Properties.Resources.TestRecorderCodeType);
            return sbResult.ToString();
        }

        public List<Template> GetListForRun(AppSettings.CodeLanguages language)
        {
            List<Template> resultlist = new List<Template>();
            foreach (Template tfile in TemplateList)
            {
                if (language != tfile.CodeLanguage)
                {
                    continue;
                }
                if (tfile.CanRun)
                {
                    resultlist.Add(tfile);
                }
            }
            return resultlist;
        }

        public List<Template> GetListForCompile(AppSettings.CodeLanguages language)
        {
            List<Template> resultlist = new List<Template>();
            foreach (Template tfile in TemplateList)
            {
                if (language != tfile.CodeLanguage)
                {
                    continue;
                }
                if (tfile.CanCompile)
                {
                    resultlist.Add(tfile);
                }
            }
            return resultlist;
        }

        public List<Template> GetListForLanguage(AppSettings.CodeLanguages language)
        {
            List<Template> resultlist = new List<Template>();
            foreach (Template tfile in TemplateList)
            {
                if (language != tfile.CodeLanguage)
                {
                    continue;
                }
                resultlist.Add(tfile);
            }
            return resultlist;
        }

        public Template GetTemplateByName(string templatename)
        {
            foreach (Template tfile in TemplateList)
            {
                if (templatename == tfile.Name)
                {
                    return tfile;
                }
            }
            return null;
        }
    }

    public class Template : Settings
    {
        public string Name = "";
        string TemplatePath = "";
        public AppSettings.CodeLanguages CodeLanguage = AppSettings.CodeLanguages.CSharp;
        public string FileExtension = "*.cs";
        public bool CanCompile = true;
        public bool CanRun = true;
        public StringCollection ReferencedAssemblies = new StringCollection();
        string UsingFormat = "using USINGNAMESPACE;";
        public StringCollection IncludedFiles = new StringCollection();
        string CodePage = "";
        string MethodBlock = "";
        public string StartupApplication = "";

        public Template(string path) : base(path)
        {
            Name = GetSetting("TemplateName", "(Name Not Set)");
            StartupApplication = GetSetting("StartupApplication", "");
            FileExtension = GetSetting("FileExtension", "*.cs");
            TemplatePath = path;

            switch (GetSetting("CodeLanguage", "CSharp"))
            {
                case "CSharp": CodeLanguage = AppSettings.CodeLanguages.CSharp; break;
                case "VBNet": CodeLanguage = AppSettings.CodeLanguages.VBNet; break;
                case "PHP": CodeLanguage = AppSettings.CodeLanguages.PHP; break;
                case "Python": CodeLanguage = AppSettings.CodeLanguages.Python; break;
                case "Perl": CodeLanguage = AppSettings.CodeLanguages.Perl; break;
            }

            CanCompile = GetSetting("CanCompile", 1) == 1 ? true : false;
            CanRun = GetSetting("CanRun", 1) == 1 ? true : false;

            string[] arrAssemblies = GetSetting("ReferencedAssemblies", "").Split(Environment.NewLine.ToCharArray());
            for (int i = 0; i < arrAssemblies.Length; i++)
            {
                if (arrAssemblies[i].Trim() != "")
                {
                    ReferencedAssemblies.Add(arrAssemblies[i]);
                }
            }

            string[] arrInclude = GetSetting("IncludedFiles", "").Split(Environment.NewLine.ToCharArray());
            for (int i = 0; i < arrInclude.Length; i++)
            {
                if (arrInclude[i].Trim() != "")
                {
                    IncludedFiles.Add(arrInclude[i]);
                }
            }

            UsingFormat = GetSetting("UsingFormat", "");
            CodePage = GetSetting("code", "");
            MethodBlock = GetSetting("Method", "");
        }

        public void SaveTemplate()
        {
            PutSetting("Name", Name);
            switch (CodeLanguage)
            {
                case AppSettings.CodeLanguages.CSharp: PutSetting("CodeLanguage", "CSharp"); break;
                case AppSettings.CodeLanguages.VBNet: PutSetting("CodeLanguage", "VBNet"); break;
                case AppSettings.CodeLanguages.PHP: PutSetting("CodeLanguage", "PHP"); break;
                case AppSettings.CodeLanguages.Python: PutSetting("CodeLanguage", "Python"); break;
                case AppSettings.CodeLanguages.Perl: PutSetting("CodeLanguage", "Perl"); break;
            }

            PutSetting("CanCompile", CanCompile ? 1 : 0);
            PutSetting("CanRun", CanRun ? 1 : 0);
            PutSetting("ReferencedAssemblies", JoinList(ReferencedAssemblies));
            PutSetting("IncludedFiles", JoinList(IncludedFiles));
            PutSetting("UsingFormat", UsingFormat);
            PutSetting("code", CodePage);
            PutSetting("Method", MethodBlock);
            PutSetting("StartupApplication", StartupApplication);
        }

        public static string JoinList(StringCollection slist)
        {
            StringBuilder sbList = new StringBuilder();
            for (int i = 0; i < slist.Count; i++)
            {
                if (slist[i].Trim()=="")
                {
                    continue;
                }
                sbList.AppendLine(slist[i]);
            }
            return sbList.ToString();
        }

        public static string JoinList(string[] slist)
        {
            StringBuilder sbList = new StringBuilder();
            for (int i = 0; i < slist.Length; i++)
            {
                if (slist[i].Trim() == "")
                {
                    continue;
                }
                sbList.AppendLine(slist[i]);
            }
            return sbList.ToString();
        }

        public StringCollection GetAssemblyList()
        {
            StringCollection scAssemblies = new StringCollection();
            string NetPath = RuntimeEnvironment.GetRuntimeDirectory();

            for (int i = 0; i < ReferencedAssemblies.Count; i++)
            {
                if (ReferencedAssemblies[i].Trim()=="")
                {
                    continue;
                }

                // Replace NETPATH and APPPath
                string assembly = ReferencedAssemblies[i].Trim().Replace("%NETPATH%", NetPath);
                assembly = assembly.Replace("%APPPATH%", AppDirectory);
                assembly = assembly.Replace(@"\\", @"\");

                scAssemblies.Add(assembly);
            }
            return scAssemblies;
        }

        public static bool AllFilesExistInList(StringCollection fileList)
        {
            string NetPath = RuntimeEnvironment.GetRuntimeDirectory();

            for (int i = 0; i < fileList.Count; i++)
            {
                // Replace NETPATH and APPPath
                string assembly = fileList[i].Trim().Replace("%NETPATH%", NetPath);
                assembly = assembly.Replace("%APPPATH%", AppDirectory);
                assembly = assembly.Replace(@"\\", @"\");

                if (!System.IO.File.Exists(assembly))
                {
                    return false;
                }
            }
            return true;
        }

        public void ModifyAssemblyPath(string OldPath, string NewPath)
        {
            for (int i = 0; i < ReferencedAssemblies.Count; i++)
            {
                if (System.IO.Path.GetFileName(OldPath).ToLower()==System.IO.Path.GetFileName(ReferencedAssemblies[i]).ToLower())
                {
                    ReferencedAssemblies[i] = NewPath;
                    SaveTemplate();
                    return;
                }
            }
        }

        public void ModifyIncludePath(string OldPath, string NewPath)
        {
            for (int i = 0; i < IncludedFiles.Count; i++)
            {
                if (System.IO.Path.GetFileName(OldPath).ToLower() == System.IO.Path.GetFileName(IncludedFiles[i]).ToLower())
                {
                    IncludedFiles[i] = NewPath;
                    SaveTemplate();
                    return;
                }
            }
        }

        public void ModifyStartupApplication(string NewPath)
        {
            StartupApplication = NewPath;
            SaveTemplate();
        }

        private string TabAllLinesTwice(string TestCode)
        {
            string[] lines = TestCode.Split(Environment.NewLine.ToCharArray());
            StringBuilder sbcode = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                sbcode.Append("\t\t"+ lines[i]+Environment.NewLine);
            }
            return sbcode.ToString();
        }

        public string PrepareScript(NameValueCollection testcode)
        {
            StringBuilder sbCode = new StringBuilder();
            sbCode.AppendLine(CodePage);

            StringBuilder sbNames = new StringBuilder();
            StringBuilder sbMethods = new StringBuilder();
            for (int i = 0; i < testcode.Count; i++)
            {
                if (CodeLanguage == AppSettings.CodeLanguages.VBNet || CodeLanguage == AppSettings.CodeLanguages.Python)
                {
                    sbNames.AppendLine(testcode.GetKey(i) + "()");
                }
                else
                {
                    sbNames.AppendLine(testcode.GetKey(i) + "();");
                }
                

                sbMethods.AppendLine(MethodBlock);
                sbMethods.Replace("TESTNAME", testcode.GetKey(i));
                sbMethods.Replace("TESTCODE", TabAllLinesTwice(JoinList(testcode.GetValues(i))));
            }

            sbCode.Replace("TESTMETHODLIST", sbNames.ToString());
            sbCode.Replace("TESTMETHODCODE", sbMethods.ToString());

            return sbCode.ToString();
        }
    }
}
