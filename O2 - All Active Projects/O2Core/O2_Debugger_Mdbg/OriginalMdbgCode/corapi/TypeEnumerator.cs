//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    /** Exposes an enumerator for Types. */

    public class CorTypeEnumerator : IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorDebugTypeEnum m_enum;
        private CorType m_ty;

        internal CorTypeEnumerator(ICorDebugTypeEnum typeEnumerator)
        {
            m_enum = typeEnumerator;
        }

        public int Count
        {
            get
            {
                if (m_enum == null) return 0;
                uint count = 0;
                m_enum.GetCount(out count);
                return (int) count;
            }
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorDebugEnum clone = null;
            if (m_enum != null)
                m_enum.Clone(out clone);
            return new CorTypeEnumerator((ICorDebugTypeEnum) clone);
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
            if (m_enum == null)
                return false;

            var a = new ICorDebugType[1];
            uint c = 0;
            int r = m_enum.Next((uint) a.Length, a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
                m_ty = new CorType(a[0]);
            else
                m_ty = null;
            return m_ty != null;
        }

        public void Reset()
        {
            if (m_enum != null)
                m_enum.Reset();
            m_ty = null;
        }

        public Object Current
        {
            get { return m_ty; }
        }

        #endregion

        public void Skip(int celt)
        {
            m_enum.Skip((uint) celt);
            m_ty = null;
        }

        // Returns total elements in the collection.
    } /* class TypeEnumerator */
} /* namespace */