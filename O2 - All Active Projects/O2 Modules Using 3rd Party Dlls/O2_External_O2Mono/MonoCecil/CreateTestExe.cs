using System;
using Mono.Cecil;
using O2.External.O2Mono;

namespace O2.External.O2Mono.MonoCecil
{
    public class CreateTestExe
    {
        public static string exeName = "BasicHelloWorld";
        public CecilAssemblyBuilder cecilAssemblyBuilder;

        public CreateTestExe createBasicHelloWorldExe()
        {
            return createBasicHelloWorldExe(false);
        }

        public CreateTestExe createBasicHelloWorldExe(bool bWithPressEnter)
        {
            cecilAssemblyBuilder = new CecilAssemblyBuilder(exeName, AssemblyKind.Console);
            TypeDefinition tdType = cecilAssemblyBuilder.addType("BasicTest", "Program");
            MethodDefinition mdMain = cecilAssemblyBuilder.addMainMethod(tdType);
            cecilAssemblyBuilder.codeBlock_ConsoleWriteLine(mdMain,
                                                            String.Format(
                                                                "Hello World " + Environment.NewLine +
                                                                Environment.NewLine +
                                                                "(Created at {0})" + Environment.NewLine +
                                                                Environment.NewLine +
                                                                "(by {1})", DateTime.Now, Environment.UserName));
            if (bWithPressEnter)
                cecilAssemblyBuilder.codeBlock_PressEnter(mdMain);
            return this;
        }

        public string save()
        {
            return save(DI.config.O2TempDir);
        }

        public string save(string targetFolder)
        {
            return cecilAssemblyBuilder.Save(targetFolder);
        }

        public string save(string targetFolder, string fileName)
        {
            return cecilAssemblyBuilder.Save(targetFolder, fileName);
        }
    }
}