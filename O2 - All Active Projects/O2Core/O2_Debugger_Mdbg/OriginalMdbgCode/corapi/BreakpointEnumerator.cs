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
    /** Exposes an enumerator for Assemblies. */

    internal class CorBreakpointEnumerator : IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorDebugBreakpointEnum m_enum;
        private CorBreakpoint m_br;

        internal CorBreakpointEnumerator(ICorDebugBreakpointEnum breakpointEnumerator)
        {
            m_enum = breakpointEnumerator;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorDebugEnum clone = null;
            m_enum.Clone(out clone);
            return new CorBreakpointEnumerator((ICorDebugBreakpointEnum) clone);
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
            var a = new ICorDebugBreakpoint[1];
            uint c = 0;
            int r = m_enum.Next((uint) a.Length, a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
            {
                ICorDebugBreakpoint br = a[0];
                throw new NotImplementedException();
                /*
                if(a is ICorDebugFunctionBreakpoint)
                    m_br = new CorFunctionBreakpoint((ICorDebugFunctionBreakpoint)br);
                else if( a is ICorDebugModuleBreakpoint)
                    m_br = new CorModuleBreakpoint((ICorDebugModuleBreakpoint)br);
                else if( a is ICorDebugValueBreakpoint)
                    m_br = new ValueBreakpoint((ICorDebugValueBreakpoint)m_br);
                else
                    Debug.Assert(false);
                */
            }
            else
                m_br = null;
            return m_br != null;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_br = null;
        }

        public Object Current
        {
            get { return m_br; }
        }

        #endregion
    } /* class BreakpointEnumerator */
} /* namespace */
