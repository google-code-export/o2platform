//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Text;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    /** A value in the remote process. */

    public class CorValue : WrapperBase
    {
        internal ICorDebugValue m_val;

        internal CorValue(ICorDebugValue value)
            : base(value)
        {
            m_val = value;
        }

        /** The simple type of the value. */

        public CorElementType Type
        {
            get
            {
                CorElementType varType;
                m_val.GetType(out varType);
                return varType;
            }
        }

        /** Full runtime type of the object . */

        public CorType ExactType
        {
            get
            {
                var v2 = (ICorDebugValue2) m_val;
                ICorDebugType dt;
                v2.GetExactType(out dt);
                return new CorType(dt);
            }
        }

        /** size of the value (in bytes). */

        public int Size
        {
            get
            {
                uint s = 0;
                m_val.GetSize(out s);
                return (int) s;
            }
        }

        /** Address of the value in the debuggee process. */

        public long Address
        {
            get
            {
                ulong addr = 0;
                m_val.GetAddress(out addr);
                return (long) addr;
            }
        }

        /** Breakpoint triggered when the value is modified. */

        public CorValueBreakpoint CreateBreakpoint()
        {
            ICorDebugValueBreakpoint bp = null;
            m_val.CreateBreakpoint(out bp);
            return new CorValueBreakpoint(bp);
        }

        // casting operations
        public CorReferenceValue CastToReferenceValue()
        {
            if (m_val is ICorDebugReferenceValue)
                return new CorReferenceValue((ICorDebugReferenceValue) m_val);
            else
                return null;
        }

        public CorHandleValue CastToHandleValue()
        {
            if (m_val is ICorDebugHandleValue)
                return new CorHandleValue((ICorDebugHandleValue) m_val);
            else
                return null;
        }

        public CorStringValue CastToStringValue()
        {
            return new CorStringValue((ICorDebugStringValue) m_val);
        }

        public CorObjectValue CastToObjectValue()
        {
            return new CorObjectValue((ICorDebugObjectValue) m_val);
        }

        public CorGenericValue CastToGenericValue()
        {
            if (m_val is ICorDebugGenericValue)
                return new CorGenericValue((ICorDebugGenericValue) m_val);
            else
                return null;
        }

        public CorBoxValue CastToBoxValue()
        {
            if (m_val is ICorDebugBoxValue)
                return new CorBoxValue((ICorDebugBoxValue) m_val);
            else
                return null;
        }

        public CorArrayValue CastToArrayValue()
        {
            if (m_val is ICorDebugArrayValue)
                return new CorArrayValue((ICorDebugArrayValue) m_val);
            else
                return null;
        }

        public CorHeapValue CastToHeapValue()
        {
            if (m_val is ICorDebugHeapValue)
                return new CorHeapValue((ICorDebugHeapValue) m_val);
            else
                return null;
        }
    } /* class Value */


    public class CorReferenceValue : CorValue
    {
        private readonly ICorDebugReferenceValue m_refVal;

        internal CorReferenceValue(ICorDebugReferenceValue referenceValue) : base(referenceValue)
        {
            m_refVal = referenceValue;
        }

        public Int64 Value
        {
            get
            {
                UInt64 v;
                m_refVal.GetValue(out v);
                return (Int64) v;
            }
            set
            {
                var v = (UInt64) value;
                m_refVal.SetValue(v);
            }
        }

        public bool IsNull
        {
            get
            {
                int bNull;
                m_refVal.IsNull(out bNull);
                return (bNull == 0) ? false : true;
            }
        }

        public CorValue Dereference()
        {
            ICorDebugValue v;
            m_refVal.Dereference(out v);
            return (v == null ? null : new CorValue(v));
        }
    }


    public sealed class CorHandleValue : CorReferenceValue, IDisposable
    {
        private readonly ICorDebugHandleValue m_handleVal;

        internal CorHandleValue(ICorDebugHandleValue handleValue) : base(handleValue)
        {
            m_handleVal = handleValue;
        }

        [CLSCompliant(false)]
        public CorDebugHandleType HandleType
        {
            get
            {
                CorDebugHandleType ht;
                m_handleVal.GetHandleType(out ht);
                return ht;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            // The underlying ICorDebugHandle has a  Dispose() method which will free
            // its resources (a GC handle). We call that now to free things sooner.
            // If we don't call it now, it will still get freed at some random point after
            // the final release (which the finalizer will call).
            try
            {
                // This is just a best-effort to cleanup resources early.
                // If it fails, just swallow and move on.
                // May throw if handle was already disposed, or if process is not stopped.
                m_handleVal.Dispose();
            }
            catch
            {
                // swallow all
            }
        }

        #endregion
    }

    public sealed class CorStringValue : CorValue
    {
        private readonly ICorDebugStringValue m_strVal;

        internal CorStringValue(ICorDebugStringValue stringValue) : base(stringValue)
        {
            m_strVal = stringValue;
        }

        public bool IsValid
        {
            get
            {
                int bValid;
                m_strVal.IsValid(out bValid);
                return (bValid == 0) ? false : true;
            }
        }

        public string String
        {
            get
            {
                uint stringSize;
                var sb = new StringBuilder(Length + 1); // we need one extra char for null
                m_strVal.GetString((uint) sb.Capacity, out stringSize, sb);
                return sb.ToString();
            }
        }

        public int Length
        {
            get
            {
                uint stringSize;
                m_strVal.GetLength(out stringSize);
                return (int) stringSize;
            }
        }
    }


    public sealed class CorObjectValue : CorValue
    {
        private readonly ICorDebugObjectValue m_objVal;

        internal CorObjectValue(ICorDebugObjectValue objectValue) : base(objectValue)
        {
            m_objVal = objectValue;
        }

        public CorClass Class
        {
            get
            {
                ICorDebugClass iclass;
                m_objVal.GetClass(out iclass);
                return (iclass == null) ? null : new CorClass(iclass);
            }
        }

        public bool IsValueClass
        {
            get
            {
                int bIsValueClass;
                m_objVal.IsValueClass(out bIsValueClass);
                return bIsValueClass != 0;
            }
        }

        public CorValue GetFieldValue(CorClass managedClass, int fieldToken)
        {
            ICorDebugValue val;
            m_objVal.GetFieldValue(managedClass.m_class, (uint) fieldToken, out val);
            return new CorValue(val);
        }

        public CorType GetVirtualMethodAndType(int memberToken, out CorFunction managedFunction)
        {
            ICorDebugType dt = null;
            ICorDebugFunction pfunc = null;
            (m_objVal as ICorDebugObjectValue2).GetVirtualMethodAndType((uint) memberToken, out pfunc, out dt);
            if (pfunc == null)
                managedFunction = null;
            else
                managedFunction = new CorFunction(pfunc);
            return dt == null ? null : new CorType(dt);
        }
    }

    public sealed class CorGenericValue : CorValue
    {
        private readonly ICorDebugGenericValue m_genVal;

        internal CorGenericValue(ICorDebugGenericValue genericValue) : base(genericValue)
        {
            m_genVal = genericValue;
        }

        // Convert the supplied value to the type of this CorGenericValue using System.IConvertable.
        // Then store the value into this CorGenericValue.  Any compatible type can be supplied.
        // For example, if you supply a string and the underlying type is ELEMENT_TYPE_BOOLEAN,
        // Convert.ToBoolean will attempt to match the string against "true" and "false".
        public void SetValue(object value)
        {
            try
            {
                switch (Type)
                {
                    case CorElementType.ELEMENT_TYPE_BOOLEAN:
                        bool v = Convert.ToBoolean(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_I1:
                        SByte sbv = Convert.ToSByte(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&sbv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_U1:
                        Byte bv = Convert.ToByte(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&bv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_CHAR:
                        Char cv = Convert.ToChar(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&cv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_I2:
                        Int16 i16v = Convert.ToInt16(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&i16v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_U2:
                        UInt16 u16v = Convert.ToUInt16(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&u16v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_I4:
                        Int32 i32v = Convert.ToInt32(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&i32v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_U4:
                        UInt32 u32v = Convert.ToUInt32(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&u32v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_I:
                        Int64 ip64v = Convert.ToInt64(value);
                        var ipv = new IntPtr(ip64v);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&ipv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_U:
                        UInt64 ipu64v = Convert.ToUInt64(value);
                        var uipv = new UIntPtr(ipu64v);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&uipv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_I8:
                        Int64 i64v = Convert.ToInt64(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&i64v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_U8:
                        UInt64 u64v = Convert.ToUInt64(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&u64v));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_R4:
                        Single sv = Convert.ToSingle(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&sv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_R8:
                        Double dv = Convert.ToDouble(value);
                        unsafe
                        {
                            SetValueInternal(new IntPtr(&dv));
                        }
                        break;

                    case CorElementType.ELEMENT_TYPE_VALUETYPE:
                        var bav = (byte[]) value;
                        unsafe
                        {
                            fixed (byte* bufferPtr = &bav[0])
                            {
                                Debug.Assert(Size == bav.Length);
                                m_genVal.SetValue(new IntPtr(bufferPtr));
                            }
                        }
                        break;

                    default:
                        throw new InvalidOperationException("Type passed is not recognized.");
                }
            }
            catch (InvalidCastException e)
            {
                throw new InvalidOperationException("Wrong type used for SetValue command", e);
            }
        }

        public object GetValue()
        {
            return UnsafeGetValueAsType(Type);
        }

        /// <summary>
        /// Get the value as an array of IntPtrs.
        /// </summary>
        public IntPtr[] GetValueAsIntPtrArray()
        {
            int ptrsize = IntPtr.Size;
            int cElem = (Size + ptrsize - 1)/ptrsize;
            var buffer = new IntPtr[cElem];

            unsafe
            {
                fixed (IntPtr* bufferPtr = &buffer[0])
                {
                    GetValueInternal(new IntPtr(bufferPtr));
                }
            }
            return buffer;
        }

        public Object UnsafeGetValueAsType(CorElementType type)
        {
            switch (type)
            {
                case CorElementType.ELEMENT_TYPE_BOOLEAN:
                    byte bValue = 4; // just initialize to avoid compiler warnings
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (byte));
                        GetValueInternal(new IntPtr(&bValue));
                    }
                    return (bValue != 0);

                case CorElementType.ELEMENT_TYPE_CHAR:
                    char cValue = 'a'; // initialize to avoid compiler warnings
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (char));
                        GetValueInternal(new IntPtr(&cValue));
                    }
                    return cValue;

                case CorElementType.ELEMENT_TYPE_I1:
                    SByte i1Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (SByte));
                        GetValueInternal(new IntPtr(&i1Value));
                    }
                    return i1Value;

                case CorElementType.ELEMENT_TYPE_U1:
                    Byte u1Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Byte));
                        GetValueInternal(new IntPtr(&u1Value));
                    }
                    return u1Value;

                case CorElementType.ELEMENT_TYPE_I2:
                    Int16 i2Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Int16));
                        GetValueInternal(new IntPtr(&i2Value));
                    }
                    return i2Value;

                case CorElementType.ELEMENT_TYPE_U2:
                    UInt16 u2Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (UInt16));
                        GetValueInternal(new IntPtr(&u2Value));
                    }
                    return u2Value;

                case CorElementType.ELEMENT_TYPE_I:
                    IntPtr ipValue = IntPtr.Zero;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (IntPtr));
                        GetValueInternal(new IntPtr(&ipValue));
                    }
                    return ipValue;

                case CorElementType.ELEMENT_TYPE_U:
                    UIntPtr uipValue = UIntPtr.Zero;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (UIntPtr));
                        GetValueInternal(new IntPtr(&uipValue));
                    }
                    return uipValue;

                case CorElementType.ELEMENT_TYPE_I4:
                    Int32 i4Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Int32));
                        GetValueInternal(new IntPtr(&i4Value));
                    }
                    return i4Value;

                case CorElementType.ELEMENT_TYPE_U4:
                    UInt32 u4Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (UInt32));
                        GetValueInternal(new IntPtr(&u4Value));
                    }
                    return u4Value;

                case CorElementType.ELEMENT_TYPE_I8:
                    Int64 i8Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Int64));
                        GetValueInternal(new IntPtr(&i8Value));
                    }
                    return i8Value;

                case CorElementType.ELEMENT_TYPE_U8:
                    UInt64 u8Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (UInt64));
                        GetValueInternal(new IntPtr(&u8Value));
                    }
                    return u8Value;

                case CorElementType.ELEMENT_TYPE_R4:
                    Single r4Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Single));
                        GetValueInternal(new IntPtr(&r4Value));
                    }
                    return r4Value;

                case CorElementType.ELEMENT_TYPE_R8:
                    Double r8Value = 4;
                    unsafe
                    {
                        Debug.Assert(Size == sizeof (Double));
                        GetValueInternal(new IntPtr(&r8Value));
                    }
                    return r8Value;


                case CorElementType.ELEMENT_TYPE_VALUETYPE:
                    var buffer = new byte[Size];
                    unsafe
                    {
                        fixed (byte* bufferPtr = &buffer[0])
                        {
                            Debug.Assert(Size == buffer.Length);
                            GetValueInternal(new IntPtr(bufferPtr));
                        }
                    }
                    return buffer;

                default:
                    Debug.Assert(false, "Generic value should not be of any other type");
                    throw new NotSupportedException();
            }
        }


        private void SetValueInternal(IntPtr valPtr)
        {
            m_genVal.SetValue(valPtr);
        }

        private void GetValueInternal(IntPtr valPtr)
        {
            m_genVal.GetValue(valPtr);
        }
    }

    public sealed class CorBoxValue : CorValue
    {
        private readonly ICorDebugBoxValue m_boxVal;

        internal CorBoxValue(ICorDebugBoxValue boxedValue) : base(boxedValue)
        {
            m_boxVal = boxedValue;
        }

        public CorObjectValue GetObject()
        {
            ICorDebugObjectValue ov;
            m_boxVal.GetObject(out ov);
            return (ov == null) ? null : new CorObjectValue(ov);
        }
    }

    public sealed class CorArrayValue : CorValue
    {
        private readonly ICorDebugArrayValue m_arrayVal;

        internal CorArrayValue(ICorDebugArrayValue arrayValue) : base(arrayValue)
        {
            m_arrayVal = arrayValue;
        }

        //void CreateRelocBreakpoint(ref CORDBLib.ICorDebugValueBreakpoint ppBreakpoint);
        //void GetBaseIndicies(UInt32 cdim, IntPtr indicies);

        public int Count
        {
            get
            {
                uint pnCount;
                m_arrayVal.GetCount(out pnCount);
                return (int) pnCount;
            }
        }


        public CorElementType ElementType
        {
            get
            {
                CorElementType type;
                m_arrayVal.GetElementType(out type);
                return type;
            }
        }

        public int Rank
        {
            get
            {
                uint pnRank;
                m_arrayVal.GetRank(out pnRank);
                return (int) pnRank;
            }
        }

        public bool HasBaseIndicies
        {
            get
            {
                int pbHasBaseIndicies;
                m_arrayVal.HasBaseIndicies(out pbHasBaseIndicies);
                return pbHasBaseIndicies == 0 ? false : true;
            }
        }

        public bool IsValid
        {
            get
            {
                int pbValid;
                m_arrayVal.IsValid(out pbValid);
                return pbValid == 0 ? false : true;
            }
        }

        public int[] GetDimensions()
        {
            Debug.Assert(Rank != 0);
            var dims = new uint[Rank];
            m_arrayVal.GetDimensions((uint) dims.Length, dims);

            int[] sdims = Array.ConvertAll(dims, delegate(uint u) { return (int) u; });
            return sdims;
        }

        public CorValue GetElement(int[] indices)
        {
            Debug.Assert(indices != null);
            ICorDebugValue ppValue;
            m_arrayVal.GetElement((uint) indices.Length, indices, out ppValue);
            return ppValue == null ? null : new CorValue(ppValue);
        }

        public CorValue GetElementAtPosition(int position)
        {
            ICorDebugValue ppValue;
            m_arrayVal.GetElementAtPosition((uint) position, out ppValue);
            return ppValue == null ? null : new CorValue(ppValue);
        }
    }

    public sealed class CorHeapValue : CorValue
    {
        private readonly ICorDebugHeapValue m_heapVal;

        internal CorHeapValue(ICorDebugHeapValue heapValue) : base(heapValue)
        {
            m_heapVal = heapValue;
        }

        //void CreateRelocBreakpoint(ref Microsoft.Samples.Debugging.CorDebug.NativeApi.ICorDebugValueBreakpoint ppBreakpoint);

        //void IsValid(ref Int32 pbValid);
        public bool IsValid
        {
            get
            {
                int bValid;
                m_heapVal.IsValid(out bValid);
                return bValid != 0;
            }
        }

        public CorValueBreakpoint CreateRelocBreakpoint()
        {
            ICorDebugValueBreakpoint bp = null;
            m_heapVal.CreateRelocBreakpoint(out bp);
            return new CorValueBreakpoint(bp);
        }

        [CLSCompliant(false)]
        public CorHandleValue CreateHandle(CorDebugHandleType type)
        {
            ICorDebugHandleValue handle;
            (m_heapVal as ICorDebugHeapValue2).CreateHandle(type, out handle);
            return handle == null ? null : new CorHandleValue(handle);
        }
    }
} /* namespace  */
