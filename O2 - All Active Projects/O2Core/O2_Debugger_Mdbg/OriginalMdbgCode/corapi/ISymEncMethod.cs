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
    // This interface isn't directly returned or used by any of the classes,
    // but the implementation of the ISymbolMethod also implements ISymEncMethod
    // so you could explicitly cast it to that.
    [
        ComVisible(false)
    ]
    public interface ISymbolEnCMethod : ISymbolMethod
    {
        String GetFileNameFromOffset(int dwOffset);

        int GetLineFromOffset(int dwOffset,
                              out int column,
                              out int endLine,
                              out int endColumn,
                              out int startOffset);
    }
}
