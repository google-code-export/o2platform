// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace O2.Debugger.Mdbg.Debugging.CorSymbolStore
{
    // Interface does not need to be marked with the serializable attribute
    /// <include file='doc\ISymDocumentWriter.uex' path='docs/doc[@for="ISymbolDocumentWriter"]/*' />
    [
        ComImport,
        Guid("B01FAFEB-C450-3A4D-BEEC-B4CEEC01E006"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
        ComVisible(false)
    ]
    internal interface ISymUnmanagedDocumentWriter
    {
        void SetSource(int sourceSize,
                       [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] byte[] source);

        void SetCheckSum(Guid algorithmId,
                         int checkSumSize,
                         [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] checkSum);
    } ;


    internal class SymDocumentWriter : ISymbolDocumentWriter
    {
        private readonly ISymUnmanagedDocumentWriter m_unmanagedDocumentWriter;

        public SymDocumentWriter(ISymUnmanagedDocumentWriter unmanagedDocumentWriter)
        {
            m_unmanagedDocumentWriter = unmanagedDocumentWriter;
        }

        internal ISymUnmanagedDocumentWriter InternalDocumentWriter
        {
            get { return m_unmanagedDocumentWriter; }
        }

        #region ISymbolDocumentWriter Members

        public void SetSource(byte[] source)
        {
            m_unmanagedDocumentWriter.SetSource(source.Length, source);
        }

        public void SetCheckSum(Guid algorithmId, byte[] checkSum)
        {
            m_unmanagedDocumentWriter.SetCheckSum(algorithmId, checkSum.Length, checkSum);
        }

        #endregion

        // Public API
    }
}
