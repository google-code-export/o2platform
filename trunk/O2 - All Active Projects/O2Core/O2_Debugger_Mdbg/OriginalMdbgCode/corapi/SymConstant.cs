//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace O2.Debugger.Mdbg.Debugging.CorSymbolStore
{
    // Interface does not need to be marked with the serializable attribute

    [
        ComImport,
        Guid("48B25ED8-5BAD-41bc-9CEE-CD62FABC74E9"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
        ComVisible(false)
    ]
    internal interface ISymUnmanagedConstant
    {
        void GetName(int cchName,
                     out int pcchName,
                     [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name);

        void GetValue(out Object pValue);

        void GetSignature(int cSig,
                          out int pcSig,
                          [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] byte[] sig);
    }

    internal class SymConstant : ISymbolConstant
    {
        private readonly ISymUnmanagedConstant m_target;

        public SymConstant(ISymUnmanagedConstant target)
        {
            m_target = target;
        }

        #region ISymbolConstant Members

        public String GetName()
        {
            int count;
            m_target.GetName(0, out count, null);
            var name = new StringBuilder(count);
            m_target.GetName(count, out count, name);
            return name.ToString();
        }

        public Object GetValue()
        {
            Object value = null;
            m_target.GetValue(out value);
            return value;
        }

        public byte[] GetSignature()
        {
            int count = 0;
            m_target.GetSignature(0, out count, null);
            var sig = new byte[count];
            m_target.GetSignature(count, out count, sig);
            return sig;
        }

        #endregion
    }
}
