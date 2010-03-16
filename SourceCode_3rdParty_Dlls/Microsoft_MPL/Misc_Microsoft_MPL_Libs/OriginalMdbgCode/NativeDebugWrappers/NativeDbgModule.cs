//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
// Part of managed wrappers for native debugging APIs.
//---------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace O2.Debugger.Mdbg.Debugging.Native
{
    /// <summary>
    /// Represents a module in the debuggee. This specifically contains the name (if available), the base address, and the File handle to close.
    /// </summary>
    public class NativeDbgModule
    {
        private readonly IntPtr m_baseAddress;
        private readonly string m_name;
        private readonly NativeDbgProcess m_process;
        private Int64 m_FileSize;
        private IntPtr m_hFile;
        private int m_size;

        public NativeDbgModule(NativeDbgProcess process, string name, IntPtr baseAddress, IntPtr fileHandle)
        {
            m_name = name;
            m_baseAddress = baseAddress;
            m_hFile = fileHandle;
            m_FileSize = -1;
            m_process = process;
        }

        public NativeDbgProcess Process
        {
            get { return m_process; }
        }


        public string Name
        {
            get { return m_name; }
        }

        public IntPtr BaseAddress
        {
            get { return m_baseAddress; }
        }


        // Cached size

        /// <summary>
        /// Size of module in memory. Addresses between BaseAddress and (BaseAddress + Size) belong to this module.
        /// 0 on error.
        /// </summary>
        /// <remarks> Not valid until after the load-dll event is continued.</remarks>
        public int Size
        {
            get
            {
                if (m_size == 0)
                {
                    var cb = (uint) Marshal.SizeOf(typeof (ModuleInfo));
                    var m = new ModuleInfo();
                    // Not valid until after the LoadDll debug event
                    bool fOk = NativeMethods.GetModuleInformation(Process.Handle, BaseAddress, out m, cb);
                    if (fOk)
                    {
                        m_size = (int) m.SizeOfImage;
                    }
                }
                return m_size;
            }
        }

        // Size of the module

        public int FileSize
        {
            get
            {
                CalculateFileSize();
                return (int) m_FileSize;
            }
        }

        // Calculate the size of the moduel based off file handle.
        protected void CalculateFileSize()
        {
            if (m_FileSize == -1)
            {
                m_FileSize = 0;

                if (m_hFile != IntPtr.Zero)
                {
                    Int64 nFileSize;
                    bool fOk = NativeMethods.GetFileSizeEx(m_hFile, out nFileSize);
                    if (fOk)
                    {
                        m_FileSize = nFileSize;
                    }
                }
            }
        }


        // Handle from LoadDll/CreateProcess debug event. This needs to be closed on the corresponding exit event.

        /// <summary>
        /// Close the handle associated with this module.
        /// </summary>
        public void CloseHandle()
        {
            if (m_hFile != IntPtr.Zero)
            {
                NativeMethods.CloseHandle(m_hFile);
                m_hFile = IntPtr.Zero;
            }
        }
    }
}
