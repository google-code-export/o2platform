// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Core.FileViewers.JoinTraces;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.FrameworkSupport.J2EE;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.XRules;
using O2.DotNetWrappers.Windows;
using O2.XRules.Database;

namespace O2.Tool.XRules.classes
{
    public class KXRulesDatabase : IXRulesDatabase
	{
        public void installXRulesDatabase(string pathTo_XRulesDatabase_fromO2, string pathTo_XRulesTemplates)
        {
            InstallXRules.installXRulesDatabase(pathTo_XRulesDatabase_fromO2, pathTo_XRulesTemplates);
            //throw new System.NotImplementedException();
        }

        public bool loadArtifact(string fileOrFolder, Dictionary<Type, object> currentArtifacts, bool loadFileAsObject)
        {
            if (File.Exists(fileOrFolder))
                addFileToArtifactsList(fileOrFolder, currentArtifacts, loadFileAsObject);
            else
                addFolderToArtifactsList(fileOrFolder, currentArtifacts, loadFileAsObject);           
            return true;
        }

        public void addFolderToArtifactsList(string fileOrFolder, Dictionary<Type, object> currentArtifacts, bool loadFileAsObject)
        {
            foreach (var file in Files.getFilesFromDir_returnFullPath(fileOrFolder))
                addFileToArtifactsList(file, currentArtifacts,loadFileAsObject);
        }

        public void addFileToArtifactsList(string fileToLoad, Dictionary<Type, object> currentArtifacts, bool loadFileAsObject)
        {
            if (loadFileAsObject)
                switch (Path.GetExtension(fileToLoad))
                {
                    case ".ozasmt":
                        var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), fileToLoad);
                        addToArtifacts_Findings(o2Assessment.o2Findings, currentArtifacts);                        
                        return;
                    case ".O2StrutsMapping":
                        var strutsMappings = StrutsMappingHelpers.loadO2StrutsMappingFile(fileToLoad);
                        addToArtifacts_Object(strutsMappings, currentArtifacts);                        
                        return;

                }
            addFileToListString_WithLoadedArtifacts(fileToLoad, currentArtifacts);

        }

        public void addToArtifacts_Object(object objectToAdd, Dictionary<Type, object> currentArtifacts)
        {
            if (objectToAdd == null)
                return;
            var objectType = objectToAdd.GetType();
            if (currentArtifacts.ContainsKey(objectType))
            {
                DI.log.error("In addToArtifacts_Object, currentArtifacts already had an object of type {0} with value {1}. That value will be overriten with the new value", currentArtifacts[objectType] ?? objectType.FullName, "[null]");
                currentArtifacts[objectType] = objectToAdd;
            }
            else
                currentArtifacts.Add(objectType, objectToAdd);
        }



        // DC - Find a more optimized way of doing this
        public void addToArtifacts_Findings(List<IO2Finding> o2FindingsToAdd, Dictionary<Type, object> currentArtifacts)
        {            
            if (false == currentArtifacts.ContainsKey(typeof(List<IO2Finding>)))
                currentArtifacts.Add(typeof(List<IO2Finding>), new List<IO2Finding>());
            
            ((List<IO2Finding>)currentArtifacts[typeof(List<IO2Finding>)]).AddRange(o2FindingsToAdd);
        }

        public void addFileToListString_WithLoadedArtifacts(string fileToAdd, Dictionary<Type, object> currentArtifacts)
        {            
            if (currentArtifacts.ContainsKey(typeof(List<String>)))
                ((List<String>)currentArtifacts[typeof(List<String>)]).Add(fileToAdd);
            else
                currentArtifacts.Add(typeof(List<String>), new List<String> { fileToAdd });
        }
	}
}
