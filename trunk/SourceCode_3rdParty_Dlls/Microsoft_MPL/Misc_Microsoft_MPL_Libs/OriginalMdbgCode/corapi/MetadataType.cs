//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
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
    public sealed class MetadataType : Type
    {
        private readonly CorElementType m_enumUnderlyingType;
        private readonly IMetadataImport m_importer;
        private readonly bool m_isEnum;
        private readonly bool m_isFlagsEnum;
        private readonly string m_name;
        private readonly int m_typeToken;
        private List<KeyValuePair<string, ulong>> m_enumValues;

        internal MetadataType(IMetadataImport importer, int classToken)
        {
            Debug.Assert(importer != null);
            m_importer = importer;
            m_typeToken = classToken;

            if (classToken == 0)
            {
                // classToken of 0 represents a special type that contains
                // fields of global parameters.
                m_name = "";
            }
            else
            {
                // get info about the type
                int size;
                int ptkExtends;
                TypeAttributes pdwTypeDefFlags;
                importer.GetTypeDefProps(classToken,
                                         null,
                                         0,
                                         out size,
                                         out pdwTypeDefFlags,
                                         out ptkExtends
                    );
                var szTypedef = new StringBuilder(size);
                importer.GetTypeDefProps(classToken,
                                         szTypedef,
                                         szTypedef.Capacity,
                                         out size,
                                         out pdwTypeDefFlags,
                                         out ptkExtends
                    );

                m_name = GetNestedClassPrefix(importer, classToken, pdwTypeDefFlags) + szTypedef;

                // Check whether the type is an enum
                string baseTypeName = GetTypeName(importer, ptkExtends);

                IntPtr ppvSig;
                if (baseTypeName == "System.Enum")
                {
                    m_isEnum = true;
                    m_enumUnderlyingType = GetEnumUnderlyingType(importer, classToken);

                    // Check for flags enum by looking for FlagsAttribute
                    uint sigSize = 0;
                    ppvSig = IntPtr.Zero;
                    int hr = importer.GetCustomAttributeByName(classToken, "System.FlagsAttribute", out ppvSig,
                                                               out sigSize);
                    if (hr < 0)
                    {
                        throw new COMException("Exception looking for flags attribute", hr);
                    }
                    m_isFlagsEnum = (hr == 0); // S_OK means the attribute is present.
                }
            }
        }

        // properties

        public override int MetadataToken
        {
            get { return m_typeToken; }
        }

        public override string Name
        {
            get { return FullName; }
        }

        public override Type UnderlyingSystemType
        {
            get { throw new NotImplementedException(); }
        }

        public override Type BaseType
        {
            get
            {
                // NOTE: If you ever try to implement this, remember that the base type
                // can be represented in metadata by a TypeDef, TypeRef, or TypeSpec
                // token, depending on the nature and location of the base type.
                //
                // See ECMA Partition II for more details.
                throw new NotImplementedException();
            }
        }

        public override String AssemblyQualifiedName
        {
            get { throw new NotImplementedException(); }
        }

        public override String Namespace
        {
            get { throw new NotImplementedException(); }
        }

        public override String FullName
        {
            get { return m_name; }
        }

        public override RuntimeTypeHandle TypeHandle
        {
            get { throw new NotImplementedException(); }
        }

        public override Assembly Assembly
        {
            get { throw new NotImplementedException(); }
        }

        public override Module Module
        {
            get { throw new NotImplementedException(); }
        }


        public override Guid GUID
        {
            get { throw new NotImplementedException(); }
        }

        public bool ReallyIsEnum
        {
            get { return m_isEnum; }
        }

        public bool ReallyIsFlagsEnum
        {
            get { return m_isFlagsEnum; }
        }

        public CorElementType EnumUnderlyingType
        {
            get { return m_enumUnderlyingType; }
        }


        [CLSCompliant(false)]
        public IList<KeyValuePair<string, ulong>> EnumValues
        {
            get
            {
                if (m_enumValues == null)
                {
                    // Build a big list of field values
                    FieldInfo[] fields = GetFields(BindingFlags.Public);
                    // BindingFlags is actually ignored in the "fake" type,
                    // but we only want the public fields anyway
                    m_enumValues = new List<KeyValuePair<string, ulong>>();
                    FieldAttributes staticLiteralField = FieldAttributes.HasDefault | FieldAttributes.Literal |
                                                         FieldAttributes.Static;
                    for (int i = 0; i < fields.Length; i++)
                    {
                        var field = fields[i] as MetadataFieldInfo;
                        if ((field.Attributes & staticLiteralField) == staticLiteralField)
                        {
                            m_enumValues.Add(new KeyValuePair<string, ulong>(field.Name,
                                                                             Convert.ToUInt64(field.GetValue(null),
                                                                                              CultureInfo.
                                                                                                  InvariantCulture)));
                        }
                    }

                    var comparer = new AscendingValueComparer<string, ulong>();
                    m_enumValues.Sort(comparer);
                }

                return m_enumValues;
            }
        }

        private static string GetTypeName(IMetadataImport importer, int tk)
        {
            // Get the base type name
            var sbBaseName = new StringBuilder();
            var token = new MetadataToken(tk);
            int size;
            TypeAttributes pdwTypeDefFlags;
            int ptkExtends;

            if (token.IsOfType(MetadataTokenType.TypeDef))
            {
                importer.GetTypeDefProps(token,
                                         null,
                                         0,
                                         out size,
                                         out pdwTypeDefFlags,
                                         out ptkExtends
                    );
                sbBaseName.Capacity = size;
                importer.GetTypeDefProps(token,
                                         sbBaseName,
                                         sbBaseName.Capacity,
                                         out size,
                                         out pdwTypeDefFlags,
                                         out ptkExtends
                    );
            }
            else if (token.IsOfType(MetadataTokenType.TypeRef))
            {
                // Some types extend TypeRef 0x02000000 as a special-case
                // But that token does not exist so we can't get a name for it
                if (token.Index != 0)
                {
                    int resolutionScope;
                    importer.GetTypeRefProps(token,
                                             out resolutionScope,
                                             null,
                                             0,
                                             out size
                        );
                    sbBaseName.Capacity = size;
                    importer.GetTypeRefProps(token,
                                             out resolutionScope,
                                             sbBaseName,
                                             sbBaseName.Capacity,
                                             out size
                        );
                }
            }
            // Note the base type can also be a TypeSpec token, but that only happens
            // for arrays, generics, that sort of thing. In this case, we'll leave the base
            // type name stringbuilder empty, and thus know it's not an enum.

            return sbBaseName.ToString();
        }

        private static CorElementType GetEnumUnderlyingType(IMetadataImport importer, int tk)
        {
            IntPtr hEnum = IntPtr.Zero;
            int mdFieldDef;
            uint numFieldDefs;
            int fieldAttributes;
            int nameSize;
            int cPlusTypeFlab;
            IntPtr ppValue;
            int pcchValue;
            IntPtr ppvSig;
            int size;
            int classToken;

            importer.EnumFields(ref hEnum, tk, out mdFieldDef, 1, out numFieldDefs);
            while (numFieldDefs != 0)
            {
                importer.GetFieldProps(mdFieldDef, out classToken, null, 0, out nameSize, out fieldAttributes,
                                       out ppvSig, out size, out cPlusTypeFlab, out ppValue, out pcchValue);
                Debug.Assert(tk == classToken);

                // Enums should have one instance field that indicates the underlying type
                if ((((FieldAttributes) fieldAttributes) & FieldAttributes.Static) == 0)
                {
                    Debug.Assert(size == 2); // Primitive type field sigs should be two bytes long

                    IntPtr ppvSigTemp = ppvSig;
                    CorCallingConvention callingConv =
                        MetadataHelperFunctions.CorSigUncompressCallingConv(ref ppvSigTemp);
                    Debug.Assert(callingConv == CorCallingConvention.Field);

                    return MetadataHelperFunctions.CorSigUncompressElementType(ref ppvSigTemp);
                }

                importer.EnumFields(ref hEnum, tk, out mdFieldDef, 1, out numFieldDefs);
            }

            Debug.Fail("Should never get here.");
            throw new ArgumentException("Non-enum passed to GetEnumUnderlyingType.");
        }


        // methods

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {            
            throw new NotImplementedException();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetNestedType(String name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return null;
            throw new NotImplementedException();
        }

        protected override PropertyInfo GetPropertyImpl(String name, BindingFlags bindingAttr, Binder binder,
                                                        Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override EventInfo GetEvent(String name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetInterface(String name, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetInterfaces()
        {
            throw new NotImplementedException();
        }

        public override FieldInfo GetField(String name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            var al = new ArrayList();
            var hEnum = new IntPtr();

            int fieldToken;
            try
            {
                while (true)
                {
                    uint size;
                    m_importer.EnumFields(ref hEnum, m_typeToken, out fieldToken, 1, out size);
                    if (size == 0)
                        break;
                    al.Add(new MetadataFieldInfo(m_importer, fieldToken, this));
                }
            }
            finally
            {
                m_importer.CloseEnum(hEnum);
            }
            return (FieldInfo[]) al.ToArray(typeof (FieldInfo));
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            var al = new ArrayList();
            var hEnum = new IntPtr();

            int methodToken;
            try
            {
                while (true)
                {
                    int size;
                    m_importer.EnumMethods(ref hEnum, m_typeToken, out methodToken, 1, out size);
                    if (size == 0)
                        break;
                    al.Add(new MetadataMethodInfo(m_importer, methodToken));
                }
            }
            finally
            {
                m_importer.CloseEnum(hEnum);
            }
            return (MethodInfo[]) al.ToArray(typeof (MethodInfo));
        }

        protected override MethodInfo GetMethodImpl(String name,
                                                    BindingFlags bindingAttr,
                                                    Binder binder,
                                                    CallingConventions callConvention,
                                                    Type[] types,
                                                    ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr,
                                                              Binder binder,
                                                              CallingConventions callConvention,
                                                              Type[] types,
                                                              ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override Object InvokeMember(String name, BindingFlags invokeAttr, Binder binder, Object target,
                                            Object[] args, ParameterModifier[] modifiers, CultureInfo culture,
                                            String[] namedParameters)
        {
            throw new NotImplementedException();
        }

        public string[] GetGenericArgumentNames()
        {
            return MetadataHelperFunctions.GetGenericArgumentNames(m_importer, m_typeToken);
        }


        // returns "" for normal classes, returns prefix for nested classes
        private static string GetNestedClassPrefix(IMetadataImport importer, int classToken, TypeAttributes attribs)
        {
            if ((attribs & TypeAttributes.VisibilityMask) > TypeAttributes.Public)
            {
                // it is a nested class
                int enclosingClass;
                importer.GetNestedClassProps(classToken, out enclosingClass);
                var mt = new MetadataType(importer, enclosingClass);
                return mt.Name + ".";
            }
            else
                return String.Empty;
        }

        // member variables
    }

    // Sorts KeyValuePair<string,ulong>'s in increasing order by the value
    internal class AscendingValueComparer<K, V> : IComparer<KeyValuePair<K, V>> where V : IComparable
    {
        #region IComparer<KeyValuePair<K,V>> Members

        public int Compare(KeyValuePair<K, V> p1, KeyValuePair<K, V> p2)
        {
            return p1.Value.CompareTo(p2.Value);
        }

        #endregion

        public bool Equals(KeyValuePair<K, V> p1, KeyValuePair<K, V> p2)
        {
            return Compare(p1, p2) == 0;
        }

        public int GetHashCode(KeyValuePair<K, V> p)
        {
            return p.Value.GetHashCode();
        }
    }


    //////////////////////////////////////////////////////////////////////////////////
    //
    // TypeDefEnum
    //
    //////////////////////////////////////////////////////////////////////////////////

    internal class TypeDefEnum : IEnumerable, IEnumerator, IDisposable
    {
        private readonly CorMetadataImport m_corMeta;
        private IntPtr m_enum;
        private Type m_type;

        public TypeDefEnum(CorMetadataImport corMeta)
        {
            m_corMeta = corMeta;
        }

        #region IDisposable Members

        public void Dispose()
        {
            DestroyEnum();
            GC.SuppressFinalize(this);
        }

        #endregion

        //
        // IEnumerable interface
        //

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion

        //
        // IEnumerator interface
        //

        #region IEnumerator Members

        public bool MoveNext()
        {
            int token;
            uint c;

            m_corMeta.m_importer.EnumTypeDefs(ref m_enum, out token, 1, out c);
            if (c == 1) // 1 new element
                m_type = m_corMeta.GetType(token);
            else
                m_type = null;
            return m_type != null;
        }

        public void Reset()
        {
            DestroyEnum();
            m_type = null;
        }

        public Object Current
        {
            get { return m_type; }
        }

        #endregion

        ~TypeDefEnum()
        {
            DestroyEnum();
        }

        protected void DestroyEnum()
        {
            m_corMeta.m_importer.CloseEnum(m_enum);
            m_enum = new IntPtr();
        }
    }
}
