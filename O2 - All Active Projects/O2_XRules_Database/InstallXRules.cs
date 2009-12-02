using O2.DotNetWrappers.O2Misc;


namespace O2.XRules.Database
{
    public class InstallXRules
    {
        
        public static void installXRulesDatabase(string pathToRulesDatabase, string pathToRulesTemplates)
        {
            if (pathToRulesDatabase.IndexOf("_SourceCode_O2") == -1)
                SampleScripts.copyResourceFilesIntoDirectory(new XRules_RulesSourceCode(), pathToRulesDatabase,true);            
            SampleScripts.copyResourceFilesIntoDirectory(new XRules_Templates(), pathToRulesTemplates, true);
            //SampleScripts.copyResourceFilesIntoDirectory(new Test(), pathToRulesTemplates);

            /*var xRulesTemplates = SampleScripts.getDictionaryWithSampleScripts(new XRulesTemplates());
            foreach (var xRulesTemplate in xRulesTemplates)
            {
                var xRuleFile = Path.Combine(DI.config.XRulesTemplatesDirectory, xRulesTemplate.Key);
                if (false == File.Exists(xRuleFile))
                    Files.WriteFileContent(xRuleFile, xRulesTemplate.Value);
            }*/
        }
    }
}
