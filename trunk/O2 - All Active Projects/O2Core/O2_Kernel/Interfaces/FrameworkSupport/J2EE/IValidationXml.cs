using System;
using System.Collections.Generic;
using System.Text;

namespace O2.Kernel.Interfaces.FrameworkSupport.J2EE
{
    public interface IValidationXml
    {
        List<IValidation_Form> forms { get; set; }     
    }

    public interface IValidation_Form
    {
        string name { get; set; }
        Dictionary<string,IValidation_Form_Field> fields { get; set; }        
    }

    public interface IValidation_Form_Field
    {
        string property { get; set; }
        string depends { get; set; }
        Dictionary<string, string> vars { get; set; }
    }
}