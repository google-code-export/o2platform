using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Windows;

namespace O2.Scanner.DotNet.PostSharp
{
    public class Hacks
    {
        public static void patchAssemblyForCecilPostSharpBug(string pathToAssemblyToPatch)
        {
            // patch #1
            var bytesToFind = new byte[] { 0x53, 0x0E, 0x19, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65 };
            var bytesWithPatch = new byte[] { 0x54 };
            patchAssemblyForCecilPostSharpBug(pathToAssemblyToPatch, bytesToFind, bytesWithPatch);

            // patch #2
            bytesToFind = new byte[] { 0x53, 0x0E, 0x14, 0x41, 0x74, 0x74, 0x72, 0x69, 0x62, 0x75, 0x74, 0x65 };
            patchAssemblyForCecilPostSharpBug(pathToAssemblyToPatch, bytesToFind, bytesWithPatch);
        }

        public static void patchAssemblyForCecilPostSharpBug(string pathToAssemblyToPatch, byte[] bytesToFind, byte[] bytesWithPatch)
        {
            DI.log.info("Patching assembly: " + pathToAssemblyToPatch);
            //var fileContents = Files.getFileContentsAsByteArray(pathToAssemblyToPatch);
            var fileContents = Files.getFileContentsAsByteArray(pathToAssemblyToPatch);
            for (int i = 0; i < fileContents.Length; i++)
            {
                if (fileContents[i] == bytesToFind[0])
                {
                    var match = true;
                    for (var j = 0; j < bytesToFind.Length; j++)
                        if (fileContents[i + j] != bytesToFind[j])
                        {
                            match = false;
                            break;
                        }
                    if (match)
                    {
                        for (var j = 0; j < bytesWithPatch.Length; j++)
                        {
                            fileContents[i + j] = bytesWithPatch[j];
                        }
                        DI.log.info("Applied Patch with " + bytesWithPatch.Length + " bytes at position " + i);
                    }
                }
            }         
            Files.WriteFileContent(pathToAssemblyToPatch, fileContents);
        }
    }
}
