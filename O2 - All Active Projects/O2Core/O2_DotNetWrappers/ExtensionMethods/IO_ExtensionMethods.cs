using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class IO_ExtensionMethods
    {
        public static string fileName(this string file)
        {
            return Path.GetFileName(file);
        }

        public static string directoryName(this string file)
        {
            return Path.GetDirectoryName(file);
        }

        public static string extension(this string file)
        {
            return Path.GetExtension(file).ToLower();
        }

        public static bool extension(this string file, string extension)
        {
            return file.extension() == extension;
        }

        public static bool exists(this string file)
        {
            return file.fileExists() || file.dirExists();
        }

        public static bool fileExists(this string file)
        {
            return File.Exists(file);
        }

        public static bool dirExists(this string file)
        {
            return Directory.Exists(file);
        }

        public static void create(this string file, string fileContents)
        {
            Files.WriteFileContent(file, fileContents);
        }

        public static string contents(this string file)
        {
            return Files.getFileContents(file);
        }
    }
}