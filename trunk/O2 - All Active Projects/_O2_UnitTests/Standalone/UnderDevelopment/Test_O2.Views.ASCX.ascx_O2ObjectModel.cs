// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using O2.Interfaces.O2Core;
using O2.Interfaces.Views;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
using O2.External.WinFormsUI.Forms;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\CoreControls\ascx_O2ObjectModel.cs
//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\CoreControls\ascx_O2ObjectModel.Designer.cs
//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\CoreControls\ascx_O2ObjectModel.Controllers.cs
//O2Tag_AddSourceFile:E:\O2\_SourceCode_O2\O2Core\O2_DotNetWrappers\DotNet\CompileEngine.cs
//O2Tag_AddSourceFile:  E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\DataViewers\ascx_FunctionsViewer.cs
//O2Tag_AddSourceFile:  E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\DataViewers\ascx_FunctionsViewer.Designer.cs
//O2Tag_AddSourceFile:  E:\O2\_SourceCode_O2\O2Core\O2_Views_ASCX\DataViewers\ascx_FunctionsViewer.Controllers.cs


namespace O2.UnitTests.Standalone.UnderDevelopment
{
    [TestFixture]
    public class Test_ascx_O2ObjectModel
    {    
        private readonly static IO2Log log = PublicDI.log;    	    	
    	
    	
        [Test]
        public void openControl2()
        {
            log.info("in openControl2");
            var o2ObjectModelControl = (ascx_O2ObjectModel)O2AscxGUI.openAscx(typeof(ascx_O2ObjectModel),  O2DockState.Float, "O2 Object Model");
            Assert.That(o2ObjectModelControl != null, "o2ObjectModelControl was null");
            //((Form)o2ObjectModelControl.Parent).close();
        }
    	    	
    }
}