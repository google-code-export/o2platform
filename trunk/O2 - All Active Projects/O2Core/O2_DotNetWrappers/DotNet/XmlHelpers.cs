using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace O2.DotNetWrappers.DotNet
{
    public class XmlHelpers
    {
        public static string getRootElementText(string fileToProcess)
        {
            // Need to do some more benchmarking but I think the fastest way to get this is to use StreamReader
            try
            {
                TextReader textReader = new StreamReader(fileToProcess);
                var fileLine = textReader.ReadLine();
                if (fileLine.IndexOf("<?xml ") > -1) // check if the first line has <?xml
                {
                    var nextLine = textReader.ReadLine();
                    if (nextLine.IndexOf("<!--") > -1)              // check if there is a comment)
                    {
                        while (nextLine.IndexOf("-->") == -1)
                            // and if so, loop until we find the end of it                        
                            nextLine = textReader.ReadLine();
                        return textReader.ReadLine();
                    }
                    textReader.Close();
                    return nextLine; // if not return the 2nd line
                }

                DI.log.error("in getRootElementText, the file provided did not have <?xml on the first line: {0}   -    {1}", fileToProcess, fileLine);
            }
            catch (Exception ex)
            {
                   DI.log.ex(ex);
            }
            return "";                     
        }
    }
}
