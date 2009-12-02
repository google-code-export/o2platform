using System.Configuration;
using O2.Kernel.InterfacesBaseImpl;
using O2.Kernel.InterfacesBaseImpl;

// code specific to this app (OSAzer - Ounce Saved Assessment Analyzer)

namespace O2.Legacy.CoreLib.O2Core.O2Environment
{
    public class O2CorLibConfig : KO2Config
    {
        // todo: move to resources file in o2CoreLib (and rename the variable names)


        public string sBaseTempFolder = @"C:\O2\_temp\";
        //public static String sCurrentExecutableDirectory = Application.StartupPath ?? "";
        public string sDefaultCallTraceDir = @"CallTraces\\";
        public string sDefaultCirDumpsDir = @"CirDump\";


        public string sDefaultLocationOfOunceInstallation = @"C:\Program Files\Ounce Labs\bin";
        

        public O2CorLibConfig()
        {
            O2TempDir = sBaseTempFolder;
        }

    

        public string hardCodedPathToO2CoreLibDll
        {
            get { return hardCodedO2LocalBuildDir + @"O2_CoreLib.dll"; }
        }

        public string hardCodedPathToO2UnitTestsDll
        {
            get { return hardCodedO2LocalBuildDir + @"_O2_UnitTests_TestO2CoreLib.dll"; }
        }

        public string getAppSetting(string appSettingName)
        {
            return ConfigurationManager.AppSettings[appSettingName];
        }

/*
        public static string O2TempDir { get; set; }

        public static String getDefaultDir_TempFolder() // for legacy
        {
            return O2TempDir;
        }

        public static String setDefaultDir_TempFolder()
        {
            O2TempDir = Path.Combine(sBaseTempFolder, getCurrentExecutableFileName());

            return Files.checkIfDirectoryExistsAndCreateIfNot(O2TempDir);
        }

        public static String getCurrentO2Directory()
        {
            return sCurrentExecutableDirectory;
        }


        public static String getTempFolderInTempDirectory()
        {
            string tempFolder = Path.Combine(getDefaultDir_TempFolder(), Files.getTempFolderName());
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        public static String getTempFileInTempDirectory(string extension)
        {
            return getTempFileNameInTempDirectory() + "." + extension;
        }

        public static String getTempFileNameInTempDirectory()
        {
            return Path.Combine(getDefaultDir_TempFolder(), Files.getTempFileName());
        }

        

       

        public static string getExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
        */
    }
}

//       public static String sDefaultDir = @"_demoData\\";
//
//      public static String sDefaultNetworkShareLocation = @"";
//      public static String sDefaultO2_Add_OnsDir = @"./";
//      public static String sDefaultO2InstallationDir = @"E:\_o2 - 'engine' release";
//      public static String sDefaultO2Macros = @"_o2_Macros\";

//      public static String sDefaultPlugIn = @"_plugins\";
//       public static String sDefaultWorkflowsDir = @"xoml\";
//       public static String sDefaultWorkflowsDir_dev = @"xoml_dev\";
//       public static String sDefaultZipFileWithDemoData = @"_demoData.zip";

/*     public static String sLogParser_DirToSaveExecutionQueries = @"__ExecutedQueries";
             public static String sLogParser_HeaderFileExtension = @".headers";
             public static String sLogParser_QueryFileExtension = @".query";
               public static String sDefaultLogParserScriptsDir = @"_LogParserScripts\";*/
//     public static String sMdiConfigFile = @"config\mdiControls.xml";
//     public static String sMdiConfigFile_PlugIns = @"_plugins\mdiControls.xml";
//     public static String sO2_Add_Ons_FileNameFilter = @"O2AddOn*.dll";
//     public static String sO2_initial_height = "1000";
//     public static String sO2_initial_left = "30";
//     public static String sO2_initial_top = "30";
//     public static String sO2_initial_width = "1850";

//    public static String sPathToDll_O2_WWF_Workflow = @"O2_WWF_Workflow.dll";
//     public static String sSetup_o2_gui_script = @"execute on O2 load.o2Macro";


/*  public static String getDefaultBaseDir()
        {
            String sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultDir);
            return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
        }*/

/* public static String getDefaultDir_O2Macros()
        {
            String sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultO2Macros);
            return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
        }*/

/*      public static String getDefaultCallTraceDir()
             {
                 String sDirectory = Path.Combine(getDefaultBaseDir(), sDefaultCallTraceDir);
                 return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
             }

             public static String getDefaultCirDumpsDir()
             {
                 String sDirectory = Path.Combine(getDefaultBaseDir(), sDefaultCirDumpsDir);
                 return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
             }

              public static String getDefaultDir_o2Scripts()
             {
                 if (sDefaultO2Scripts == null)
                     return "";
                 String sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultO2Scripts);
                 return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
             }

            

             public static String getDefaultDir_Workflows(bool bReturnDev)
             {
                 String sDirectory;
                 if (bReturnDev)
                     sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultWorkflowsDir_dev);
                 else
                     sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultWorkflowsDir);
                 return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
             }

             public static String getDefaultDir_Workflows_dev()
             {
                 String sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultWorkflowsDir);
                 return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
             }*/


/*      public static String getDefaultDir_LogParserScripts()
        {
            String sDirectory = Path.Combine(sCurrentExecutableDirectory, sDefaultLogParserScriptsDir);
            return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
        }

        public static String getDefaultDir_O2AddOns()
        {
            String sDirectory = Path.GetFullPath(Path.Combine(sCurrentExecutableDirectory, sDefaultO2_Add_OnsDir));
            return Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
        }*/


/*   public static String getDefaultSourceCodeCompilationExampleFile()
           {
               return Path.Combine(getDefaultPlugInSourceCodeDir(), sDefaultSourceCodeCompilationExampleFile);            
           }
           */


/*  public static String getO2AddOnsFileFilter()
        {
            return sO2_Add_Ons_FileNameFilter;
        }*/

/*      public static void fixEnvironmentPathVariableValue()        // this will ensure that the correct dlls requided by the Ounce .net wrappers are discovered
        {
            if (Environment.GetEnvironmentVariable("Path").IndexOf("Ounce Labs") == -1)
            {
                addPathToCurrentExecutableEnvironmentPathVariable(DI.o2CorLibConfig.sDefaultLocationOfOunceInstallation);          // add the Ounce's bin  folder
                //addPathToCurrentExecutableEnvironmentPathVariable(DI.o2CorLibConfig.sCurrentExecutableDirectory);// add the O2 folder (needed to resolve referenced dlls if O2 is executed from outside the O2 folder                
            }
        }*/