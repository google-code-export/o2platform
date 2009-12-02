using O2.Kernel.Interfaces.O2Findings;

namespace O2.DotNetWrappers.O2Findings
{
    public class OzasmtContext
    {
        public static string getVariableNameFromThisObject(IO2Finding o2Finding)
        {
            return getVariableNameFromThisObject(o2Finding.context);
        }

        public static string getVariableNameFromThisObject(IO2Trace o2Trace)
        {
            return getVariableNameFromThisObject(o2Trace.context);
        }

        public static string getVariableNameFromThisObject(string context)
        {
            int indexOfSpace = context.IndexOf(' ');
            if (indexOfSpace > 0)
            {
            }
            string variable = context.Substring(0, indexOfSpace);
            variable = variable.Replace("this->", "");
            return variable;
        }
    }
}