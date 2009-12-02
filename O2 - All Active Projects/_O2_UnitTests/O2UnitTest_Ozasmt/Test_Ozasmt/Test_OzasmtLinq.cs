using System.Windows.Forms;
using NUnit.Framework;
using O2.External.Evaluant;


namespace O2.UnitTests.Test_Ozasmt.Test_Ozasmt
{
    [TestFixture]
    public class Test_OzasmtLinq
    {
        [Test]
        public void Test_populateComboBoxWithO2FindingsScriptLibrary()
        {
            string titleOfDefaultScript = "Default o2Findings Linq Script";
            var scriptComboBox = new ComboBox();
            OzasmtLinq.populateComboBoxWithO2FindingsLinqScriptLibraryTitles(scriptComboBox);
            Assert.That(scriptComboBox.Items.Count > 0, "There were no O2JavaScript loaded into scriptComboBox");
            string defaultScript = scriptComboBox.Items[0].ToString();
            Assert.That(defaultScript == titleOfDefaultScript,
                        "scriptComboBox.Items[0] should be '" + titleOfDefaultScript + "' and was " +
                        scriptComboBox.Items[0] + "  -");
            Assert.That(OzasmtLinq.getO2FindingLinqScript(defaultScript) != "",
                        "could not retrived the contents of defaultScript");
            Assert.That(OzasmtLinq.getO2FindingLinqScript("nothing here") == "",
                        "requesting for a non existent script should return an empty string");
        }
    }
}