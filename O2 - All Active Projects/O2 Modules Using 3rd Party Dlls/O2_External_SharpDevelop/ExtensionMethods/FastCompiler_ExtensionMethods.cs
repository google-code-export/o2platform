using System.Reflection;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.AST;

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
                    if (csharpCompiler.CompilerResults.CompiledAssembly != null)
                        return csharpCompiler.CompilerResults.CompiledAssembly;
            return null;
        }
    }
}
