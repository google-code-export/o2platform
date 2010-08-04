// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;

namespace O2.XRules.Database.Utils
{
    public class FileCache
    {    
    	public string PathLocalCache {get;set;}
    	public bool UseBase64EncodedStringInFileName {get;set;}
    	public string defaultCacheExtension = ".o2Cache";
    	public bool DisableCache { get; set; }
    	
    	public FileCache(string pathLocalCache)
    	{
    		PathLocalCache = pathLocalCache;
    		Files.checkIfDirectoryExistsAndCreateIfNot(PathLocalCache);
    	}
    	
    	public string getCacheAddress(string itemPath)
		{
			return getCacheAddress(itemPath,defaultCacheExtension);
		}
		
    	public string getCacheAddress(string itemPath, string cacheSaveExtension)
		{
			return PathLocalCache.pathCombine(itemPath.safeFileName(UseBase64EncodedStringInFileName) + cacheSaveExtension);
		}
		
		public string cacheGet(string uri)
		{
			return cacheGet(uri,".html", null);
		}
		
		public string cacheGet(string itemPath, string cacheSaveExtension)
		{
			return cacheGet(itemPath,cacheSaveExtension, null);
		}
		
		public string cacheGet(string itemPath,  Func<string> getFunction)
		{
			return cacheGet(itemPath, defaultCacheExtension ,getFunction);
		}
		 		 
		public string cacheGet(string itemPath, string cacheSaveExtension, Func<string> getFunction)
		{
			if (DisableCache)
				return getFunction();
				
			var cacheAddress = getCacheAddress(itemPath,cacheSaveExtension);
			
			if (cacheAddress.fileExists())
			{
				"[FileCache] returning data from local cache: {0}".debug(cacheAddress);
				return cacheAddress.fileContents();
			}
			else if (getFunction.notNull())
				{
					var result = getFunction();
					if (result.valid())
					{
						result.saveAs(cacheAddress);
						return result;
					}
					else
						"[FileCache] getFunction returned not data".error();
				}			
			return "";
			
		}
		public string cacheGet_File(string itemPath)
		{
			return 	cacheGet_File(itemPath, defaultCacheExtension);
		}
		
		public string cacheGet_File(string itemPath, string cacheSaveExtension)
		{
			if (DisableCache)
				return "";
			var cacheAddress = getCacheAddress(itemPath, cacheSaveExtension);
			
			if (cacheAddress.fileExists())
			{
				"[FileCache] found data in local cache: {0}".debug(cacheAddress);
				return cacheAddress;
			}
			return "";
		}
		
		public string cachePut(string itemPath, string cacheValue)
		{
			return 	cachePut(itemPath, ".o2Cache",cacheValue);
		}
		
		public string cachePut(string itemPath, string cacheSaveExtension, string cacheValue)
		{
			if (DisableCache)
				return "";
			cacheValue = cacheValue ?? "";
			var cacheAddress = PathLocalCache.pathCombine(itemPath.safeFileName(UseBase64EncodedStringInFileName) + cacheSaveExtension);
			cacheValue.saveAs(cacheAddress);
			return cacheValue;
		}  	    	    	    
    }
}
