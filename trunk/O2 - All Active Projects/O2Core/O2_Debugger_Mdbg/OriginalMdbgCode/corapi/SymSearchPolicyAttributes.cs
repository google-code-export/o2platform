//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
using System;

namespace O2.Debugger.Mdbg.Debugging.CorSymbolStore
{
    // Only statics, does not need to be marked with the serializable attribute    

    [Serializable, Flags]
    public enum SymSearchPolicies
    {
        // query the registry for symbol search paths
        AllowRegistryAccess = 1,

        // access a symbol server
        AllowSymbolServerAccess = 2,

        // Look at the path specified in Debug Directory
        AllowOriginalPathAccess = 4,

        // look for PDB in the place where the exe is.
        AllowReferencePathAccess = 8,
    }
}