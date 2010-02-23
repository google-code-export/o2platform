using System;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class IO_ExtensionMethods
    {
        public static string fileName(this string file)
        {
            if (file.valid())
                return Path.GetFileName(file);
            return "";
        }

        public static string directoryName(this string file)
        {
            if (file.valid())
                return Path.GetDirectoryName(file);
            return "";            
        }

        public static string extension(this string file)
        {
            if (file.valid())
                return Path.GetExtension(file).ToLower();
            return "";
        }

        public static bool extension(this string file, string extension)
        {
            if (file.valid())
                return file.extension() == extension;
            return false;
        }

        public static bool exists(this string file)
        {
            if (file.valid())
                return file.fileExists() || file.dirExists();
            return false;
        }

        public static bool fileExists(this string file)
        {
            if (file.valid())
                return File.Exists(file);
            return false;
        }

        public static bool dirExists(this string file)
        {
            if (file.valid())
                return Directory.Exists(file);
            return false;
        }

        public static void create(this string file, string fileContents)
        {
            if (file.valid())
                Files.WriteFileContent(file, fileContents);            
        }

        public static string contents(this string file)
        {
            if (file.valid())
                return Files.getFileContents(file);
            return "";
        }

        public static byte[] contentsAsBytes(this string file)
        {
            if (file.fileExists())
                return Files.getFileContentsAsByteArray(file);
            return null;
        }
    }
}