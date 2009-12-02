// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
    /** Exposes an enumerator for AppDomains. */

    internal class CorAppDomainEnumerator : IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorDebugAppDomainEnum m_enum;
        private CorAppDomain m_ad;

        internal CorAppDomainEnumerator(ICorDebugAppDomainEnum appDomainEnumerator)
        {
            m_enum = appDomainEnumerator;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorDebugEnum clone = null;
            m_enum.Clone(out clone);
            return new CorAppDomainEnumerator((ICorDebugAppDomainEnum) clone);
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
            var a = new ICorDebugAppDomain[1];
            uint c = 0;
            int r = m_enum.Next((uint) a.Length, a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
                m_ad = new CorAppDomain(a[0]);
            else
                m_ad = null;
            return m_ad != null;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_ad = null;
        }

        public Object Current
        {
            get { return m_ad; }
        }

        #endregion
    } /* class AppDomainEnumerator */
} /* namespace */
