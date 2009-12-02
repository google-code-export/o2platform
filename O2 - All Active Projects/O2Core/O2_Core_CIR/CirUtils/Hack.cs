using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using O2.Core.CIR;
using O2.DotNetWrappers.Windows;

namespace O2.Core.CIR.CirUtils
{
    public class Hack
    {
        public static void tryToFixCirDumpProblems(String sPathToFileToProcess)
        {
            DI.log.info("in tryToFixCirDumpProblems: {0}", sPathToFileToProcess);
            if (false == dotnetHack.fixDotNetCirDumpFile(sPathToFileToProcess))
                if (false == fixCirDumpProblem_method1(sPathToFileToProcess, '\x07') &&
                    false == fixCirDumpProblem_method1(sPathToFileToProcess, '\x0C'))
                    fixCirDumpProblem_method2(sPathToFileToProcess);

            // fixCirDumpProblem(sPathToFileToProcess, '\x05');
            // fixCirDumpProblem(sPathToFileToProcess, '\x0C');
            /*      fixCirDumpProblem(sPathToFileToProcess, '\x01');
                  fixCirDumpProblem(sPathToFileToProcess, '\x02');
                  fixCirDumpProblem(sPathToFileToProcess, '\x1F');
                  fixCirDumpProblem(sPathToFileToProcess, '\x1E');  */
        }

        public static bool fixCirDumpProblem_method1(String sPathToFileToProcess, char cCharToFind)
        {
            String sFileContents = Files.getFileContents(sPathToFileToProcess);
            if (sFileContents.IndexOf(cCharToFind) > -1)
            {
                DI.log.info("in tryToFixCirDumpProblems: char {0} detected: removing them (replacing with 0)",
                            ((int) cCharToFind).ToString());
                sFileContents = sFileContents.Replace(cCharToFind, '0');
                Files.WriteFileContent(sPathToFileToProcess, sFileContents);
            }
            try
            {
                var xdDXmlocument = new XmlDocument();
                xdDXmlocument.Load(sPathToFileToProcess);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool fixCirDumpProblem_method2(String sPathToFileToProcess)
        {
            String sFileContents = Files.getFileContents(sPathToFileToProcess);
            int iIndexOfChar1F = sFileContents.IndexOf('\x1F');
            if (iIndexOfChar1F > -1)
            {
                int iIndexOfDoubleQuoteBefore = iIndexOfChar1F;
                int iIndexOfDoubleQuoteAfter = iIndexOfChar1F;
                while (sFileContents[iIndexOfDoubleQuoteBefore] != '\"')
                    iIndexOfDoubleQuoteBefore--;
                iIndexOfDoubleQuoteBefore++;
                while (sFileContents[iIndexOfDoubleQuoteAfter] != '\"')
                    iIndexOfDoubleQuoteAfter++;
                String sBadString = sFileContents.Substring(iIndexOfDoubleQuoteBefore,
                                                            iIndexOfDoubleQuoteAfter - iIndexOfDoubleQuoteBefore);
                sFileContents = sFileContents.Replace(sBadString, "0");
                Files.WriteFileContent(sPathToFileToProcess, sFileContents);
            }
            try
            {
                var xdDXmlocument = new XmlDocument();
                xdDXmlocument.Load(sPathToFileToProcess);
                DI.log.info("Sucessfully corrected CirDumpFile problem:{0}", sPathToFileToProcess);
                return true;
            }
            catch
            {
                DI.log.debug("Was NOT able to corrected CirDumpFile problem:{0}", sPathToFileToProcess);
                return false;
            }
        }

        #region Nested type: dotnetHack

        public class dotnetHack
        {
            public static bool fixDotNetCirDumpFile(String sFileToFix)
            {
                DI.log.debug("Fixing File {0}:", sFileToFix);

                if (false == canFileBeLoaded(sFileToFix))
                {
                    List<String> lsLines = Files.getFileLines(sFileToFix);
                    for (int i = 0; i < lsLines.Count; i++)
                    {
                        int i1stSymbolRef = lsLines[i].IndexOf("SymbolRef");
                        if (i1stSymbolRef > -1)
                        {
                            int iLastIndexOfSymbolRef = lsLines[i].Substring(i1stSymbolRef).LastIndexOf("SymbolRef");
                            if (iLastIndexOfSymbolRef > 0)
                            {
                                String sExtraSymbolDef = lsLines[i].Substring(i1stSymbolRef + iLastIndexOfSymbolRef);
                                int i1stQuote = sExtraSymbolDef.IndexOf('"');
                                int i2ndQuote = sExtraSymbolDef.Substring(i1stQuote + 1).IndexOf('"');
                                String sBadString = sExtraSymbolDef.Substring(0, i1stQuote + i2ndQuote + 2);
                                lsLines[i] = lsLines[i].Replace(sBadString, "");
                                DI.log.info("Patched line {0} with {1} (by removing {2})", i, lsLines[i], sBadString);
                            }
                        }
                    }
                    Files.saveAsFile_StringList(sFileToFix, lsLines);
                }
                else
                    return true;
                return canFileBeLoaded(sFileToFix);

                /*
                Int32 iLine = 0;
                Int32 iPosition = 0;
                bool bFileCanBeLoaded = false;
                while (bFileCanBeLoaded == false)                    
                {
                    bFileCanBeLoaded = (canFileBeLoaded_And_IfNot_CalculateLineAndLocationOfProblem(sFileToFix, ref iLine, ref iPosition));
                    if (bFileCanBeLoaded)
                        return true;                    
                    if (iLine == 0 || false == patchLine(sFileToFix, iLine, iPosition))
                        return false;                    
                }
                return false; ;*/
                //if (iLine == 0)
                //    return false;
                //else
                //    return true;
            }


            public static bool canFileBeLoaded(String sFileToLoad)
            {
                try
                {
                    var xdXmlDocument = new XmlDocument();
                    xdXmlDocument.Load(sFileToLoad);
                    return true;
                }
                catch (Exception ex)
                {
                    DI.log.error("in canFileBeLoaded: {0}", ex.Message);
                    return false;
                }
            }

            public static bool canFileBeLoaded_And_IfNot_CalculateLineAndLocationOfProblem(String sFileToLoad,
                                                                                           ref Int32 iLine,
                                                                                           ref Int32 iPosition)
            {
                iLine = 0;
                iPosition = 0;
                try
                {
                    var xdXmlDocument = new XmlDocument();
                    xdXmlDocument.Load(sFileToLoad);
                    return true;
                }
                catch (Exception ex)
                {
                    String sErrorMessage = ex.Message;
                    String[] sSplittedErrorMessage = sErrorMessage.Split(' ');
                    if (sErrorMessage.IndexOf("'SymbolRef' is a duplicate attribute name.") > -1 &&
                        sSplittedErrorMessage.Length == 10)
                    {
                        iLine = Int32.Parse(sSplittedErrorMessage[7].Replace(",", ""));
                        iPosition = Int32.Parse(sSplittedErrorMessage[9].Replace(".", ""));
                        //	DI.log.debug("Found problem on Line {0} Column {1}", iLine, iPosition);				
                    }
                    else
                        DI.log.error(" Unrecognized error in Xml File:{0}", sErrorMessage);
                }

                return false;
            }

            public static bool patchLine(String sFileWithProblem, Int32 iLine, Int32 iPosition)
            {
                try
                {
                    DI.log.debug("Fixing File: {0} (Line: {1}, Position: {2})", Path.GetFileName(sFileWithProblem),
                                 iLine, iPosition);
                    //String sFileContents = Files.getFileContents(sFileWithProblem);
                    List<String> lsFileLines = Files.getFileLines(sFileWithProblem);
                    //new List<String>(sFileContents.Split(new String[] { Environment.NewLine } , StringSplitOptions.None));
                    DI.log.info(" #lines = {0} , # chars in Line #{1} = {2}", lsFileLines.Count, iLine,
                                lsFileLines[iLine - 1].Length);
                    DI.log.info("{0}", lsFileLines[iLine - 1]);
                    String sTextBefore = lsFileLines[iLine - 1].Substring(0, iPosition - 1);
                    String sTextAfter = lsFileLines[iLine - 1].Substring(iPosition - 1);
                    // apply fix removing the 2nd SymbolRef
                    DI.log.info(" Text removed from line: {0} ", sTextAfter);
                    lsFileLines[iLine - 1] = String.Format("{0}/>", sTextBefore);
                    // rebuild the file with the fixed line
                    //   sFileWithProblem += ".fixed.xml";
                    Files.saveAsFile_StringList(sFileWithProblem, lsFileLines);
                    //StringBuilder sbFileContents = new StringBuilder();
                    //foreach (String sLine in lsFileLines)
                    //    sbFileContents.Append(sLine + Environment.NewLine);
                    // save the file
                    //if (sFileWithProblem != "")
                    // {
                    //   Files.WriteFileContent(sFileWithProblem, sbFileContents.ToString());
                    DI.log.info("FixedFile Saved to: {0} ", sFileWithProblem);
                    //}
                    //else
                    //    DI.log.error("sFileWithProblem = \"\" ");
                    return true;
                }
                catch (Exception ex)
                {
                    DI.log.error("in patchLine: {0}", ex.Message);
                    return false;
                }
            }
        }

        #endregion
    }
}