using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using System.Threading;

namespace O2.Views.ASCX.MerlinWizard
{
    public class O2Wizard
    {
        public string Title {get;set;}
        public List<IStep> Steps { get; set; }
        public Object Model;
        public Type ModelType;

        public O2Wizard()
        {
            Title = "Default O2 Wizard";
            Steps = new List<IStep>();
        }
        public O2Wizard(string title) : this()
        {
            Title = title;
        }

        public void setModel(object model)
        {
            if (model != null)
            {
                Model = model;
                ModelType = model.GetType();
            }
        }


        public Thread run()
        {
            return start();
        }
        public Thread start()
        {
            return MerlinUtils.runWizardWithSteps(Steps, Title);
        }

        



    }
}
