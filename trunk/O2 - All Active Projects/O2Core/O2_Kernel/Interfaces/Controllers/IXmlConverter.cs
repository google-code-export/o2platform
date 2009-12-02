using System;

namespace O2.Kernel.Interfaces.Controllers
{
    public interface IXmlConverter
    {
        bool loadFileToConvert(String fileToConvert);
        bool convert();
        bool convert(String sTargetOzasmtFile);
    }
}