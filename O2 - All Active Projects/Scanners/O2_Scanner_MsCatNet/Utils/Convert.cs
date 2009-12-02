using System;
using O2.DotNetWrappers.DotNet;
using O2.Scanner.MsCatNet.RnD.Xsd;

namespace O2.Scanner.MsCatNet.Utils
{
    public class Convert
    {
        public static String sConvertMsCatNetResultsFileIntoOzasmt(String sCatNetFileToConvert)
        {
            var rReport = new Report();
            Object oSerializedObject = null;
            try
            {
                oSerializedObject = Serialize.getDeSerializedObjectFromXmlFile(sCatNetFileToConvert, rReport.GetType());
                rReport = (Report) oSerializedObject;


                //o2.Scanners.MSCatNet.xsd.Report
            }
            catch
            {
                try
                {
                    String sTempFile = DI.config.TempFileNameInTempDirectory;

                    Serialize.createSerializedXmlFileFromObject(oSerializedObject, sTempFile);

                    rReport = (Report) Serialize.getDeSerializedObjectFromXmlFile(sTempFile, typeof (Report));
                }
                catch (Exception ex)
                {
                    DI.log.error("In sConvertMsCatNetResultsFileIntoOzasmt: {0}", ex.Message);
                }
            }
            return "";
        }
    }
}