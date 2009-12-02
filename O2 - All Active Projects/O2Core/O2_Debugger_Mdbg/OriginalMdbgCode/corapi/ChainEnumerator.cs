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
    /** 
     * Exposes an enumerator for Chains. 
     *
     * This is horribly broken at this point, as Chains aren't implemented yet.
     */

    internal class CorChainEnumerator : IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorDebugChainEnum m_enum;

        private CorChain m_chain;

        internal CorChainEnumerator(ICorDebugChainEnum chainEnumerator)
        {
            m_enum = chainEnumerator;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorDebugEnum clone = null;
            m_enum.Clone(out clone);
            return new CorChainEnumerator((ICorDebugChainEnum) clone);
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
            var a = new ICorDebugChain[1];
            uint c = 0;
            int r = m_enum.Next((uint) a.Length, a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
                m_chain = new CorChain(a[0]);
            else
                m_chain = null;
            return m_chain != null;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_chain = null;
        }

        public Object Current
        {
            get { return m_chain; }
        }

        #endregion
    } /* class ChainEnumerator */
} /* namespace */
