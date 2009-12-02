//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


/*
 * Utility code to update a PE Header with new PDB signature
 * 
 ************************************************
 * This code is not intended to act as a sample *
 ************************************************
 * 
 * Most programs that write managed symbol infromation into a pdb file 
 * would also be emitting an image (exe or dll) file at the same time.
 * A real compiler would clearly need to fully generate the PE file 
 * from scratch and this code just finds and updates the debug info 
 * section of the file.
 * 
 * For a good reference on the PE/COFF file format, see this document:
 * http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
 */

using System.IO;
using O2.Debugger.Mdbg.pdb2xml;
using O2.Debugger.Mdbg.pdb2xml;
using O2.Debugger.Mdbg.pdb2xml;

namespace O2.Debugger.Mdbg
{
    internal class PEFile
    {
        // Private members to track information at runtime
        private const ushort PE32 = 0x010B;
        private const ushort PE32PLUS = 0x020B;
        private const byte PESignatureOffsetLoc = 0x3C;
        private const byte SizeOfCOFFHeader = 0x14;
        private const byte SizeOfDebugDirectory = 0x1C;
        private const byte SizeOfDebugInfo = 0x18;
        private const byte SizeOfSection = 0x28;
        private readonly byte m_COFFHeaderOffset;
        private readonly uint m_DebugInfoFilePointer;
        private readonly string m_ExePath;
        private readonly ushort m_NumberOfSections;
        private readonly ushort m_PEFormat;
        private readonly ushort m_PEHeaderStart;
        private readonly byte m_PESignatureOffset;
        private readonly ushort m_SizeOfOptionalHeader;
        private readonly ushort m_StartOfSections;


        internal PEFile(string path)
        {
            m_ExePath = path;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    // Find the address of the PE Header
                    br.BaseStream.Seek(PESignatureOffsetLoc, SeekOrigin.Begin);
                    m_PESignatureOffset = br.ReadByte();
                    br.BaseStream.Seek(m_PESignatureOffset, SeekOrigin.Begin);
                    // Start of PE Signature
                    if (br.ReadByte() != 'P')
                        Util.Error("PE Signature corrupted");
                    if (br.ReadByte() != 'E')
                        Util.Error("PE Signature corrupted");
                    if (br.ReadByte() != '\0')
                        Util.Error("PE Signature corrupted");
                    if (br.ReadByte() != '\0')
                        Util.Error("PE Signature corrupted");
                    // Start of COFF Header
                    m_COFFHeaderOffset = (byte) br.BaseStream.Position;
                    // COFF: 0x02 contains 2 bytes that indicate the NumberOfSections
                    // COFF: 0x10 contains 2 bytes that indicate the SizeOfOptionalHeader
                    br.BaseStream.Seek(m_COFFHeaderOffset + 0x02, SeekOrigin.Begin);
                    m_NumberOfSections = br.ReadUInt16();
                    br.BaseStream.Seek(m_COFFHeaderOffset + 0x10, SeekOrigin.Begin);
                    m_SizeOfOptionalHeader = br.ReadUInt16();

                    // Start of PE Header
                    m_PEHeaderStart = (ushort) (m_COFFHeaderOffset + SizeOfCOFFHeader);
                    m_StartOfSections = (ushort) (m_PEHeaderStart + m_SizeOfOptionalHeader);
                    br.BaseStream.Seek(m_PEHeaderStart, SeekOrigin.Begin);

                    m_PEFormat = (ushort) br.ReadInt16();
                    if (m_PEFormat != PE32 && m_PEFormat != PE32PLUS)
                    {
                        Util.Error("Unrecognized PE Format: " + m_PEFormat);
                    }
                    // PEHeader fields vary based upon the PE_Format setting
                    int DebugDirectoryOffset = m_PEHeaderStart;
                    DebugDirectoryOffset += m_PEFormat == PE32 ? 0x90 : 0xA0;

                    br.BaseStream.Seek(DebugDirectoryOffset, SeekOrigin.Begin);
                    int DebugRVA = br.ReadInt32();

                    uint VirtualAddress = GetVirtualAddressforRVA(br, DebugRVA);
                    m_DebugInfoFilePointer =
                        (uint) (DebugRVA - VirtualAddress + m_StartOfSections + SizeOfSection*m_NumberOfSections + 0x10);
                }
            }
        }

        private uint GetVirtualAddressforRVA(BinaryReader br, int RVA)
        {
            // Find a section where:
            // VirtualAddress <= StartRVA <= VirtualAddress+VirtualSize

            int SizeOfSection = 0x28;
            for (int i = 0; i < m_NumberOfSections; i++)
            {
                // VirtualSize at Section:0x08 (size 4)
                // VirtualAddress at Section:0x0C (size 4)
                // PointerToRawData at Section:0x14 (size 4)
                br.BaseStream.Seek(m_StartOfSections + i*SizeOfSection, SeekOrigin.Begin);
                br.BaseStream.Seek(0x08, SeekOrigin.Current);
                uint VirtualSize = br.ReadUInt32();
                uint VirtualAddress = br.ReadUInt32();
                if (VirtualAddress <= RVA && RVA <= VirtualAddress + VirtualSize)
                    return VirtualAddress;
            }
            Util.Error("Section not found");
            return 0;
        }

        internal void UpdateHeader(byte[] DebugInfo)
        {
            using (var fs = new FileStream(m_ExePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    // Get to the DebugInfo section
                    bw.Seek((int) (m_DebugInfoFilePointer + SizeOfDebugDirectory), SeekOrigin.Begin);
                    int count = DebugInfo.Length;
                    if (count > SizeOfDebugInfo)
                        count = SizeOfDebugInfo;
                    bw.Write(DebugInfo, 0, count);
                }
            }
        }
    }
}