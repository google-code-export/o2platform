using NUnit.Framework;
using O2.DotNetWrappers.Filters;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Analysis
{
    [TestFixture]
    public class Test_FilteredUrl
    {
        [Test]
        public void parseUrls()
        {
            const string urlTest1 = "http://aa";
            const string urlTest2 = "http://aa/page.aspx";
            const string urlTest3 = "http://aa.bb.cc/page.aspx#tag";
            const string urlTest4 = "http://aa.bb.cc/path/page.aspx?param1=aaa";
            const string urlTest5 = "http://aa.bb.cc/path1/path2/page.aspx?param1=aaa&param1=bbb&param3=ccc#fragment1";
            var test1 = new FilteredUrl(urlTest1);
            Assert.IsTrue(test1.host == "aa", "test1");
            var test2 = new FilteredUrl(urlTest2);
            Assert.IsTrue(test2.page == "page.aspx", "test2");
            var test3 = new FilteredUrl(urlTest3);
            Assert.IsTrue(test3.host == "aa.bb.cc" && test3.fragement == "#tag", "test3");
            var test4 = new FilteredUrl(urlTest4);
            Assert.IsTrue(test4.path == "/path/" && test4.page == "page.aspx" && test4.parametersRaw == "param1=aaa" &&
                          test4.parameters[0].name == "param1" && test4.parameters[0].value == "aaa", "test4");
            var test5 = new FilteredUrl(urlTest5);
            Assert.IsTrue(
                test5.path == "/path1/path2/" && test5.wordsInPath[0] == "path1" && test5.wordsInPath[1] == "path2" &&
                test5.wordsInPathAndPage[2] == "page.aspx" && test5.parameters[1].name == "param1" &&
                test5.parameters[2].name == "param3" && test5.parameters[2].value == "ccc", "test5");
            Assert.IsTrue(
                test5.words[0] == "http:" && test5.words[1] == "aa.bb.cc" && test5.words[2] == "path1" &&
                test5.words[3] == "path2" && test5.words[4] == "page.aspx" && test5.words[5] == "param1" &&
                test5.words[6] == "aaa" &&
                test5.words[7] == "param1" && test5.words[8] == "bbb" && test5.words[9] == "param3" &&
                test5.words[10] == "ccc" && test5.words[11] == "fragment1"
                , "test 5 words");
        }
    }
}