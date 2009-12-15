using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using System.Reflection;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgVariable
    {
        public bool IsProperty { get; set; }            // if it is not a property it is a field
        public string name { get; set; }

        public string value { get; set; }
        public string type { get; set; }
        public bool complexType { get; set; }
        public O2MDbgVariable parentVariable { get; set; }
        public string parentType { get; set; }
        public string assemblyName { get; set; }

        public string fullName
        {
            get
            {
                return (parentVariable == null) ? name : string.Format("{0}.{1}", parentVariable.fullName, name);
            }
        }

        public O2MDbgVariable(MDbgValue mdbgValue, string _parentType, string _assemblyName)
        {
            IsProperty = false;
            parentType = _parentType;
            assemblyName = _assemblyName;
            name = mdbgValue.Name;
            try
            {
                value = mdbgValue.GetStringValue(false);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2MDbgVariable(MDbgValue mdbgValue), while trying to get value for: " + mdbgValue.Name);
            }
            type = mdbgValue.TypeName;
            complexType = mdbgValue.IsComplexType;
        }

        public O2MDbgVariable(MDbgValue mdbgValue, O2MDbgVariable _parentVariable)
            : this(mdbgValue, _parentVariable.parentType, _parentVariable.assemblyName)
        {
            parentVariable = _parentVariable;
        }

        public O2MDbgVariable(PropertyInfo propertyInfo, O2MDbgVariable _parentVariable)
        {
            IsProperty = true;
            parentVariable = _parentVariable;
            name = propertyInfo.Name;
            /*try
            {
                value = mdbgValue.GetStringValue(false);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2MDbgVariable(MDbgValue mdbgValue), while trying to get value for: " + mdbgValue.Name);
                value = O2MDbgUtils.getPropertyValue(this);
            }*/
            //fullName = parentVariableName + ".get_" + propertyInfo.Name;
            //     value = O2MDbgUtils.getPropertyValue(this);
            type = propertyInfo.PropertyType.FullName;

            //complexType = true;        
        }

        public enum O2MDbgVariableType
        {
            field,
            property
        }
    }
}
