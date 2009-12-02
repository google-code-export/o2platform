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
    /** 
     * Exposes an enumerator for ErrorInfo objects. 
     *
     * This is horribly broken at this point, as ErrorInfo isn't implemented yet.
     */

    internal class CorErrorInfoEnumerator : IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorDebugErrorInfoEnum m_enum;

        private Object m_einfo;

        internal CorErrorInfoEnumerator(ICorDebugErrorInfoEnum erroInfoEnumerator)
        {
            m_enum = erroInfoEnumerator;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorDebugEnum clone = null;
            m_enum.Clone(out clone);
            return new CorErrorInfoEnumerator((ICorDebugErrorInfoEnum) clone);
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
            return false;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_einfo = null;
        }

        public Object Current
        {
            get { return m_einfo; }
        }

        #endregion
    } /* class ErrorInfoEnumerator */
} /* namespace */