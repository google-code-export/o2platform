// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using PostSharp.Laos;
using System.Web;

namespace O2.External.PostSharp
{
    public class Patches
    {
        /*public static void applyPatch(object[] readOnlyArguments, MethodExecutionEventArgs context)
        {
            var stringToFind = "QQQQ";
            if (readOnlyArguments != null)
                for (int i = 0; i < readOnlyArguments.Length; i++)
                {
                    var argument = readOnlyArguments[i];
                    if (argument is string && argument.ToString() == stringToFind)
                    {
                        Trace.WriteLine("****************************************************************************");
                        Trace.WriteLine("     - Patch point     ");
                        Trace.WriteLine("****************************************************************************");
                        var writeableArguments = context.GetWritableArgumentArray();
                        writeableArguments[i] = " *** PATCHED QQQQ ";                        

                        // now lets see if the patch values are there

                        var postPatchReadOnly = context.GetReadOnlyArgumentArray();
                        if (postPatchReadOnly[i].ToString() == " *** PATCHED QQQQ ")
                            Trace.WriteLine("!!!!!!! Yap Patch is there ");

                    }
                }

        }*/


        public static void applyPatch(MethodInvocationEventArgs context)
        {
            var stringToFind = "<script>";
            var argumentArray = context.GetArgumentArray();
            if (argumentArray != null)
                for (int i = 0; i < argumentArray.Length; i++)
                {
                    var argument = argumentArray[i];
                    if (argument is string && argument.ToString().IndexOf(stringToFind) > -1)
                    {
                        Trace.WriteLine("****************************************************************************");
                        Trace.WriteLine("     - Patch point     ");
                        Trace.WriteLine("****************************************************************************");

                        argumentArray[i] = htmlEncode(argumentArray[i].ToString());//.ToString().Replace(stringToFind, "script]");

                        /*// now lets see if the patch values are there

                        var postPatchReadOnly = context.GetReadOnlyArgumentArray();
                        if (postPatchReadOnly[i].ToString() == " *** PATCHED QQQQ ")
                            Trace.WriteLine("!!!!!!! Yap Patch is there ");
                        */
                    }
                }

        }

        public static string htmlEncode(string stringToEncode)
        {
            return HttpUtility.HtmlEncode(stringToEncode);
        }

        public static void applyPatch_encodeAllStringParameters(MethodInvocationEventArgs context)
        {
            var argumentArray = context.GetArgumentArray();
            if (argumentArray != null)
                for (int i = 0; i < argumentArray.Length; i++)
                {
                    var argument = argumentArray[i];
                    if (argument is string)
                    {
                        Trace.WriteLine("**** Patching value:" + argumentArray[i]);
                        argumentArray[i] = htmlEncode(argumentArray[i].ToString());
                    }
                }
        }    

    }
}
