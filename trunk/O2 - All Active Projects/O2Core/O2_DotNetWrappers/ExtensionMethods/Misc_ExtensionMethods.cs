using O2.DotNetWrappers.Windows;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Misc_ExtensionMethods
    {
        public static void sleep(this object _object, int miliseconds)
        {
            Processes.Sleep(miliseconds);
        }
    }
}
