// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Tag_OnlyAddReferencedAssemblies
//O2Reg:System.dll
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
//O2Ref:System.Core.dll
using System.Linq;
//O2Ref:O2_ImportExport_OunceLabs.dll
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
//O2Ref:O2_Kernel.dll
//O2Ref:O2_Interfaces.dll
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Interfaces.XRules;
using O2.Kernel;
//O2Ref:O2_DotNetWrappers.dll
using O2.DotNetWrappers.O2Findings;
//O2Ref:O2_Views_ASCX.dll
using O2.Views.ASCX.O2Findings;
//O2Ref:O2_XRules_Database.exe
//O2File:xUtils_Findings_v0_1.cs
using O2.XRules.Database._Rules;

namespace O2.XRules.Database._Rules
{
    public class XRule_Findings_Filter : KXRule
    {
        private readonly IO2Log log = PublicDI.log;

        //bool openOnNewWindow = true;

        [XRule(Name = "Open.Findings.In.New.O2.GUI.Window")]
        public void openInNewWindow(List<IO2Finding> o2Findings)
        {
            XUtils_Findings_v0_1.openFindingsInNewWindow(o2Findings);
        }

        public XRule_Findings_Filter()
        {
            Name = "XRule_Findings_Filter";
        }		

        [XRule(Name="All findings")]
        public List<IO2Finding> allFindings(List<IO2Finding> o2Findings)
        {        	
            return o2Findings;        	
        }

        [XRule(Name="Only Findings With Traces")]
        public List<IO2Finding> onlyTraces(List<IO2Finding> o2Findings)
        {        	
            return 
                (from IO2Finding o2Finding in o2Findings 
                 where o2Finding.o2Traces.Count > 0  select o2Finding).ToList();
            //return o2Assesment.o2Findings;
        }

		
        [XRule(Name="Only.findings.where.vulnName.CONTAINS")]
        public List<IO2Finding> whereVulnName_Contains(List<IO2Finding> o2Findings, string text)
        {        	
            return 
                (from IO2Finding o2Finding in o2Findings
                 where o2Finding.vulnName.IndexOf(text) > -1
                 select o2Finding).ToList();            
        }

        [XRule(Name = "Only.findings.where.vulnName.IS")]
        public List<IO2Finding> whereVulnName_Is(List<IO2Finding> o2Findings, string text)
        {
            return
                (from IO2Finding o2Finding in o2Findings
                 where o2Finding.vulnName == text
                 select o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.Source.IS")]
        public List<IO2Finding> whereSource_Is(List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Source == text 
                 select (IO2Finding)o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.Source.CONTAINS")]
        public List<IO2Finding> whereSource_Contains(List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Source.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.Sink.IS")]
        public List<IO2Finding> whereSink_Is(List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Sink == text
                 select (IO2Finding)o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.Sink.CONTAINS")]
        public List<IO2Finding> whereSink_Contains(List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.Sink.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.Context.CONTAINS")]
        public List<IO2Finding> whereContext_Contains(List<IO2Finding> o2Findings, string text)
        {
            return
                (from O2Finding o2Finding in o2Findings
                 where o2Finding.context.IndexOf(text) > -1
                 select (IO2Finding)o2Finding).ToList();
        }

        [XRule(Name = "Only.findings.where.SourceAndSink.CONTAINS.Regex")]
        public List<IO2Finding> whereSourceAndSink_ContainsRegex(List<IO2Finding> o2Findings, string source, string sink )
        {
            return XUtils_Findings_v0_1.calculateFindings(o2Findings, source, sink);
        }
    }
}
