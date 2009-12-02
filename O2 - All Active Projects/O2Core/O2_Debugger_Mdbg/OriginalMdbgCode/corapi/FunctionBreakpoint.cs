//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    public sealed class CorFunctionBreakpoint : CorBreakpoint
    {
        private readonly ICorDebugFunctionBreakpoint m_breakpoint;

        internal CorFunctionBreakpoint(ICorDebugFunctionBreakpoint breakpoint) : base(breakpoint)
        {
            m_breakpoint = breakpoint;
        }

        public CorFunction Function
        {
            get
            {
                ICorDebugFunction f = null;
                m_breakpoint.GetFunction(out f);
                return new CorFunction(f);
            }
        }

        public int Offset
        {
            get
            {
                uint off = 0;
                m_breakpoint.GetOffset(out off);
                return (int) off;
            }
        }
    } /* class FunctionBreakpoint */
} /* namespace */
