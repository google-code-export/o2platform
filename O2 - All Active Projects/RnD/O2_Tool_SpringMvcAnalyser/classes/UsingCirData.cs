// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Interfaces.CIR;

namespace O2.RnD.SpringMVCAnalyzer.classes
{
    internal class UsingCirData
    {
        public static List<String> findClassesThatImplementTheSpringMvc(ICirData fadCirData)
        {
            var lResolvedClasses = new List<String>();
            if (fadCirData == null)
            {
                DI.log.error("in findClassesThatImplementTheSpringMvc: CirData object is null");
                return lResolvedClasses;
            }

            var sSpringMvcClass = "org.springframework.web.servlet.mvc";

            var dClassesThatSuperClassSpringMVC = new Dictionary<String, ICirClass>();


            // first find which classes direcly implement a Spring MVC 
            foreach (ICirClass ccCirClass in fadCirData.dClasses_bySignature.Values)
            {
                foreach (String sSuperClass in ccCirClass.dSuperClasses.Keys)
                {
                    String sSuperClassSignature = ccCirClass.dSuperClasses[sSuperClass].Signature;
                    if (sSuperClassSignature.IndexOf(sSpringMvcClass) > -1)
                        // && ccCirClass.sSignature.IndexOf(sBaseClass) > -1)
                    {
                        String sSignatureAndSuperClass = String.Format("{0} <-- {1}", ccCirClass.Signature,
                                                                       ccCirClass.dSuperClasses[sSuperClass].Signature);
                        dClassesThatSuperClassSpringMVC.Add(sSignatureAndSuperClass, ccCirClass);
                        // DI.log.debug("{0} <-- {1}" , ccCirClass.sSignature, ccCirClass.dSuperClasses[sSuperClass].sSignature);									
                    }
                }
            }
            findAllClassesThatSuperClassProvidedList_recursive(dClassesThatSuperClassSpringMVC, lResolvedClasses);
            DI.log.debug("There are {0} classes that implement Spring MVC", lResolvedClasses.Count);
            return lResolvedClasses;
            // DI.log.debug("Loaded with {0} classes", fadCirData.dClasses_bySignature.Keys.Count);		
        }

        public static void findAllClassesThatSuperClassProvidedList_recursive(
            Dictionary<String, ICirClass> dClassesToAdd, List<String> lResolvedClasses)
        {
            foreach (ICirClass ccCirClass in dClassesToAdd.Values)
            {
                if (false == lResolvedClasses.Contains(ccCirClass.Signature))
                {
                    lResolvedClasses.Add(ccCirClass.Signature);
                }
                //foreach(String sSuperClass in dClassesToAdd[sClassSignature].dIsSuperClassedBy.Keys)
                findAllClassesThatSuperClassProvidedList_recursive(ccCirClass.dIsSuperClassedBy, lResolvedClasses);
                // DI.log.debug("   " + dClassesToAdd[sClass].dIsSuperClassedBy[sSuperClass].sSignature);
            }
        }
    }
}
