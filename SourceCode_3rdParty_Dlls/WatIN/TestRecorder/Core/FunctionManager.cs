using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public class FunctionManager
    {
        public List<FunctionPage> Functions = new List<FunctionPage>();
        private FileSystemWatcher watcher = new System.IO.FileSystemWatcher();

        public FunctionManager(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return;
            }

            string[] arrFiles = Directory.GetFiles(path, "*.trf");

            for (int i = 0; i < arrFiles.Length; i++)
            {
                FunctionPage page = new FunctionPage(arrFiles[i]);
                Functions.Add(page);
            }

            //WatchFileSystem(path);
        }

        public List<FunctionPage> GetApplicableFunctions(IHTMLElement element, AppSettings.CodeLanguages language)
        {
            List<FunctionPage> applicable = new List<FunctionPage>();
            foreach (FunctionPage page in Functions)
            {
                if (page.IsApplicable(element, language))
                {
                    applicable.Add(page);
                }
            }

            if (applicable.Count==0)
            {
                // load the default page
                applicable.Add(GetPageFromTitle("Default"));
            }

            foreach (FunctionPage page in Functions)
            {
                if (page.ShowOnAll && page.Languages.Contains(language.ToString()))
                {
                    applicable.Add(page);
                }
            }

            return applicable;
        }

        public FunctionPage GetPageFromTitle(string Title)
        {
            foreach (FunctionPage page in Functions)
            {
                if (page.Title == Title)
                {
                    return page;
                }
            }
            return null;
        }

        public void WatchFileSystem(string path)
        {
            watcher.EnableRaisingEvents = true;
            watcher.Filter = Path.Combine(path, "*.htm*");
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Created += new FileSystemEventHandler(watcher_Created);
        }

        void watcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            // new function found
            FunctionPage page = new FunctionPage(e.FullPath);
            Functions.Add(page);
        }
    }
}
