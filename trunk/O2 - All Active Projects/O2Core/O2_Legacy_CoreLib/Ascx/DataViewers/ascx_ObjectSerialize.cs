// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using O2.Legacy.CoreLib;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_ObjectSerialize : UserControl
    {
        public ascx_ObjectSerialize()
        {
            InitializeComponent();
        }

        public void serializeObjectAndShowResultInWebBrowser(Object oObjectToDeserialize)
        {
            if (oObjectToDeserialize != null)
            {
                try
                {
                    var xsXmlSerializer = new XmlSerializer(oObjectToDeserialize.GetType());
                    var msMemoryStream = new MemoryStream();
                    xsXmlSerializer.Serialize(msMemoryStream, oObjectToDeserialize);
                    byte[] bSerializedData = msMemoryStream.ToArray();
                    String sSerializedData = Encoding.ASCII.GetString(bSerializedData);
                    webBrowser1.DocumentText = sSerializedData;
                    DI.log.debug("Serialization worked");
                }
                catch (Exception ex)
                {
                    DI.log.error("Serializaion error: {0}", ex.Message);
                    if (ex.InnerException != null)
                        DI.log.error("    InnerException: {0}", ex.InnerException.Message);
                }
            }
        }


        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            webBrowser1.Navigate("about:black");
            serializeObjectAndShowResultInWebBrowser(oVar);
        }
    }
}
