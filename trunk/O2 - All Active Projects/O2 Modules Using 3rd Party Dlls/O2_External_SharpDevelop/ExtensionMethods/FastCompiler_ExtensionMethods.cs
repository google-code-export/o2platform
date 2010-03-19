using System.Reflection;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.DotNetWrappers.Windows;

namespace O2.External.SharpDevelop.ExtensionMethods
{
    public static class FastCompiler_ExtensionMethods
    {
        public static Assembly compile(this string pathToFileToCompile)
        {
            var csharpCompiler = new CSharp_FastCompiler();
            var compileProcess = new System.Threading.AutoResetEvent(false);
            csharpCompiler.compileSourceCode(pathToFileToCompile.contents());
            csharpCompiler.onCompileFail = () => compileProcess.Set();
            csharpCompiler.onCompileOK = () => compileProcess.Set();
            compileProcess.WaitOne();
            return csharpCompiler.assembly();
        }

        public static Assembly assembly(this CSharp_FastCompiler csharpCompiler)
        {
            if (csharpCompiler != null)
                if (csharpCompiler.CompilerResults != null)
                    if (csharpCompiler.CompilerResults.Errors.HasErrors == false)
                    {
                        if (csharpCompiler.CompilerResults.CompiledAssembly != null)
                            return csharpCompiler.CompilerResults.CompiledAssembly;
                    }
                    else
                        "CompilationErrors:".line().add(csharpCompiler.CompilationErrors).error();

            return null;
        }

        public static Assembly compile(this string pathToFileToCompile, string targetAssembly)
        {
            var assembly = pathToFileToCompile.compile(true);
            Files.Copy(assembly.Location, targetAssembly);
            return assembly;
        }

        public static Assembly compile(this string pathToFileToCompile, bool compileToFileAndWithDebugSymbols)
        {
            string generateDebugSymbolsTag = @"//debugSymbols".line();
            if (pathToFileToCompile.fileContains(generateDebugSymbolsTag).isFalse())
                pathToFileToCompile.fileInsertAt(0, generateDebugSymbolsTag);
            return pathToFileToCompile.compile();

        }
    }
}
