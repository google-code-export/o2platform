// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.corapi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorMetadata.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorMetadata.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorMetadata.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorMetadata
{
    public sealed class MetadataFieldInfo : FieldInfo
    {
        private readonly FieldAttributes m_fieldAttributes;
        private readonly int m_fieldToken;
        private readonly IMetadataImport m_importer;
        private readonly string m_name;
        private readonly Object m_value;
        private MetadataType m_declaringType;

        internal MetadataFieldInfo(IMetadataImport importer, int fieldToken, MetadataType declaringType)
        {
            m_importer = importer;
            m_fieldToken = fieldToken;
            m_declaringType = declaringType;

            // Initialize
            int mdTypeDef;
            int pchField, pcbSigBlob, pdwCPlusTypeFlab, pcchValue, pdwAttr;
            IntPtr ppvSigBlob;
            IntPtr ppvRawValue;
            m_importer.GetFieldProps(m_fieldToken,
                                     out mdTypeDef,
                                     null,
                                     0,
                                     out pchField,
                                     out pdwAttr,
                                     out ppvSigBlob,
                                     out pcbSigBlob,
                                     out pdwCPlusTypeFlab,
                                     out ppvRawValue,
                                     out pcchValue
                );

            var szField = new StringBuilder(pchField);
            m_importer.GetFieldProps(m_fieldToken,
                                     out mdTypeDef,
                                     szField,
                                     szField.Capacity,
                                     out pchField,
                                     out pdwAttr,
                                     out ppvSigBlob,
                                     out pcbSigBlob,
                                     out pdwCPlusTypeFlab,
                                     out ppvRawValue,
                                     out pcchValue
                );
            m_fieldAttributes = (FieldAttributes) pdwAttr;
            m_name = szField.ToString();

            // Get the values for static literal fields with primitive types
            FieldAttributes staticLiteralField = FieldAttributes.Static | FieldAttributes.HasDefault |
                                                 FieldAttributes.Literal;
            if ((m_fieldAttributes & staticLiteralField) == staticLiteralField)
            {
                m_value = ParseDefaultValue(declaringType, ppvSigBlob, ppvRawValue);
            }
        }


        public override Type FieldType
        {
            get { throw new NotImplementedException(); }
        }

        public override RuntimeFieldHandle FieldHandle
        {
            get { throw new NotImplementedException(); }
        }

        public override FieldAttributes Attributes
        {
            get { return m_fieldAttributes; }
        }

        public override MemberTypes MemberType
        {
            get { throw new NotImplementedException(); }
        }

        public override String Name
        {
            get { return m_name; }
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override Type ReflectedType
        {
            get { throw new NotImplementedException(); }
        }

        public override int MetadataToken
        {
            get { return m_fieldToken; }
        }

        private static object ParseDefaultValue(MetadataType declaringType, IntPtr ppvSigBlob, IntPtr ppvRawValue)
        {
            IntPtr ppvSigTemp = ppvSigBlob;
            CorCallingConvention callingConv = MetadataHelperFunctions.CorSigUncompressCallingConv(ref ppvSigTemp);
            Debug.Assert(callingConv == CorCallingConvention.Field);

            CorElementType elementType = MetadataHelperFunctions.CorSigUncompressElementType(ref ppvSigTemp);
            if (elementType == CorElementType.ELEMENT_TYPE_VALUETYPE)
            {
                uint token = MetadataHelperFunctions.CorSigUncompressToken(ref ppvSigTemp);

                if (token == declaringType.MetadataToken)
                {
                    // Static literal field of the same type as the enclosing type
                    // may be one of the value fields of an enum
                    if (declaringType.ReallyIsEnum)
                    {
                        // If so, the value will be of the enum's underlying type,
                        // so we change it from VALUETYPE to be that type so that
                        // the following code will get the value
                        elementType = declaringType.EnumUnderlyingType;
                    }
                }
            }

            switch (elementType)
            {
                case CorElementType.ELEMENT_TYPE_CHAR:
                    return (char) Marshal.ReadByte(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_I1:
                    return (sbyte) Marshal.ReadByte(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_U1:
                    return Marshal.ReadByte(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_I2:
                    return Marshal.ReadInt16(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_U2:
                    return (ushort) Marshal.ReadInt16(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_I4:
                    return Marshal.ReadInt32(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_U4:
                    return (uint) Marshal.ReadInt32(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_I8:
                    return Marshal.ReadInt64(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_U8:
                    return (ulong) Marshal.ReadInt64(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_I:
                    return Marshal.ReadIntPtr(ppvRawValue);
                case CorElementType.ELEMENT_TYPE_U:
                case CorElementType.ELEMENT_TYPE_R4:
                case CorElementType.ELEMENT_TYPE_R8:
                    // Technically U and the floating-point ones are options in the CLI, but not in the CLS or C#, so these are NYI
                default:
                    return null;
            }
        }

        public override Object GetValue(Object obj)
        {
            FieldAttributes staticLiteralField = FieldAttributes.Static | FieldAttributes.HasDefault |
                                                 FieldAttributes.Literal;
            if ((m_fieldAttributes & staticLiteralField) != staticLiteralField)
            {
                throw new InvalidOperationException("Field is not a static literal field.");
            }
            if (m_value == null)
            {
                throw new NotImplementedException("GetValue not implemented for the given field type.");
            }
            else
            {
                return m_value;
            }
        }

        public override void SetValue(Object obj, Object value, BindingFlags invokeAttr, Binder binder,
                                      CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override Object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}
