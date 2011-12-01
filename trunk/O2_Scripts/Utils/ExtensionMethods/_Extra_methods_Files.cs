// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
 
namespace O2.XRules.Database.Utils
{	
	public static class Files_Extra_ExtensionMethods
	{
		public static T deserialize<T>(this string _string, bool fromDisk)
		{
			if (fromDisk && _string.fileExists())
				return _string.deserialize<T>();
			
			return (T)Serialize.getDeSerializedObjectFromString(_string, typeof(T));  
		}
		
		public static string parentFolder(this string path)
		{
			return path.directoryName();
		}
		
		public static string fileName_WithoutExtension(this string filePath)
		{
			return Path.GetFileNameWithoutExtension(filePath);
		}
		
		public static List<string> lines(this string text, bool removeEmptyLines)
		{
			if (removeEmptyLines)
				return text.lines();
			return text.fixCRLF()
					   .Split(new string[] { Environment.NewLine }, System.StringSplitOptions.None )
					   .toList();
		}
		
		public static bool deleteFile(this string file)
		{
			return Files.deleteFile(file);
		}
		
		public static List<string> deleteFiles(this List<string> files)
		{
			foreach(var file in files)
				Files.deleteFile(file);
			return files;
		}
		
		public static List<string> filesContains(this List<string> files, string textToSearch)
		{
			return (from file in files
					where file.fileContents().contains(textToSearch)
					select file).toList();
		}
		
		public static List<string> filesContains_RegEx(this List<string> files, string regExToSearch)
		{
			return (from file in files
					where file.fileContents().regEx(regExToSearch)
					select file).toList();
		}
		
		public static string fromLines_getText(this List<string> lines)
		{
			return StringsAndLists.fromStringList_getText(lines);
		}
		
				public static string file(this string folder, string virtualFilePath)
		{
			var mappedFile = folder.pathCombine(virtualFilePath);
			if (mappedFile.fileExists())
				return mappedFile; 
			return null;
		}
		
		public static List<string> files(this List<string> folders)
		{
			return folders.files("*.*");
		}
		
		public static List<string> files(this List<string> folders, string filter)
		{
			return folders.files(filter,false);
		}
		
		public static List<string> files(this List<string> folders, string filter, bool recursive)
		{
			var files = new List<string>();
			foreach(var folder in folders)
				files.AddRange(folder.files(filter, recursive));
			return files;
		}
		
		public static bool deleteIfExists(this string file)
		{
			try
			{
				if (file.fileExists())
					Files.deleteFile(file);
				return true;
			}
			catch(Exception ex)
			{
				"[deleteIfExists] : {0}".error(ex.Message);
				return false;
			}
			
		}
		
		public static string findParentFolderCalled(this string fullPath, string folderToFind)
		{
			var parentFolder = fullPath.directoryName();
			if (folderToFind.valid() && parentFolder.notNull())
			{
				if (parentFolder.fileName() == folderToFind)
					return parentFolder;
				return findParentFolderCalled(parentFolder,folderToFind);
			}
			return null;
		}
		
		//replace pathCombine with this one
		public static string pathCombine_MaxSize(this string folder, string path )
		{			
			var maxLength = 256 - folder.size();
			if(maxLength < 10)
				throw new Exception("in pathCombine_MaxSize folder name is too large: {0}".format(folder.size()));
            if (path.size() > maxLength)
                path = "{0} ({1}){2}".format(
                            path.Substring(0, maxLength - 20),
                            path.hash(),
                            path.Substring(path.size() - 9).extension());
            return folder.pathCombine(path);;
		}

	}
	
}
    	