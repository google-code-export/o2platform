// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using Evaluant.NLinq;
using Evaluant.NLinq.Memory;
using O2.DotNetWrappers.DotNet;

namespace O2.External.Evaluant
{
    public class O2NLinkQuery
    {
        public static object runQuery(string queryText, string sourceText, object sourceObject)
        {
            return runQuery(queryText, new Hashtable {{sourceText, sourceObject}});
        }

        public static object runQuery(string queryText, Hashtable sources)
        {
            try
            {
                O2Timer o2Timer = new O2Timer("Executed Link Query (" + queryText + ") in: ").start();
                object results = runQueryInternal(queryText, sources);
                o2Timer.stop();
                return results;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2NLinkQuery.runQuery");
                return null;
            }
        }

        private static object runQueryInternal(string queryText, IDictionary sources)
        {
            try
            {
                var query = new NLinqQuery(queryText);
                var linq = new LinqToMemory(query);
                foreach (string source in sources.Keys)
                    linq.AddSource(source, (IEnumerable)sources[source]);

                //return linq.Evaluate();
                return linq.Enumerate();
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in runQueryInternal");
                return null;                
            }
            
        }

        public static bool IsQueryValid(string queryText, string sourceText, object sourceObject, bool showError)
        {
            return IsQueryValid(queryText, new Hashtable {{sourceText, sourceObject}}, showError);
        }

        public static bool IsQueryValid(string queryText, Hashtable sources, bool showError)
        {
            try
            {
                runQueryInternal(queryText, sources);
                return true;
            }
            catch (Exception ex)
            {
                if (showError)
                    DI.log.debug("Query Compilation Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
