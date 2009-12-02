using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using System.IO;

namespace O2.UnitTests.Test_DotNetWrappers.Test_Windows
{
    [TestFixture]
    public class Test_Win32
    {
        [Test]
        public void test_FindFileOnLocalPath()
        {
            var fileToFind = "notepad.exe";

            var mappedFile = Win32.findFileOnLocalPath(fileToFind);
            Assert.That(File.Exists(mappedFile), "mappedFile didn't exist");
        }
    }
}
