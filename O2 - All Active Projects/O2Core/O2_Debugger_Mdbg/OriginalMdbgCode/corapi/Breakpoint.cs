// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Diagnostics;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    public abstract class CorBreakpoint : WrapperBase
    {
        private readonly ICorDebugBreakpoint m_corBreakpoint;

        [CLSCompliant(false)]
        protected CorBreakpoint(ICorDebugBreakpoint managedBreakpoint) : base(managedBreakpoint)
        {
            Debug.Assert(managedBreakpoint != null);
            m_corBreakpoint = managedBreakpoint;
        }

        public virtual bool IsActive
        {
            get
            {
                int r = 0;
                m_corBreakpoint.IsActive(out r);
                return !(r == 0);
            }
        }

        public virtual void Activate(bool active)
        {
            m_corBreakpoint.Activate(active ? 1 : 0);
        }
    }
} /* namespace */
