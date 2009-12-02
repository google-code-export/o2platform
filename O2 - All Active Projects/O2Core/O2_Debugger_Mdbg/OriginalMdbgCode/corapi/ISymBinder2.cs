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
using System.Runtime.InteropServices.ComTypes;

namespace O2.Debugger.Mdbg.Debugging.CorSymbolStore
{
    [
        ComVisible(false)
    ]
    public interface ISymbolBinder2
    {
        ISymbolReader GetReaderForFile(Object importer, String filename, String searchPath);

        ISymbolReader GetReaderForFile(Object importer, String fileName,
                                       String searchPath, SymSearchPolicies searchPolicy);

        ISymbolReader GetReaderForFile(Object importer, String fileName,
                                       String searchPath, SymSearchPolicies searchPolicy,
                                       IntPtr callback);

        ISymbolReader GetReaderFromStream(Object importer, IStream stream);
    }
}
