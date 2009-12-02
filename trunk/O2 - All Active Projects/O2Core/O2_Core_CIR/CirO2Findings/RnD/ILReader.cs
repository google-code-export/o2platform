using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace O2.Core.CIR.CirCreator
{
    public class currentSelectedMethod
    {
        public int iToken;
        public MethodBase mbMethodBase;
        public OpCode ocOpcode;
        public List<String> strParameters;
        public string strResolvedToken;
    }

    public class ILReader
    {
        private static readonly OpCode[] s_OneByteOpCodes = new OpCode[0x100];
        private static readonly OpCode[] s_TwoByteOpCodes = new OpCode[0x100];
        private readonly MethodBase m_enclosingMethod;

        public currentSelectedMethod csmCurrentMethod = new currentSelectedMethod();
        public Byte[] m_byteArray;
        public Int32 m_position;
        public Module[] mModulesBeingAnalyzed;
        public Type tTypeBeingAnalyzed;

        static ILReader()
        {
            foreach (FieldInfo fi in typeof (OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var opCode = (OpCode) fi.GetValue(null);
                var value = (UInt16) opCode.Value;
                if (value < 0x100)
                {
                    s_OneByteOpCodes[value] = opCode;
                }
                else if ((value & 0xff00) == 0xfe00)
                {
                    s_TwoByteOpCodes[value & 0xff] = opCode;
                }
            }
        }

        public ILReader(MethodBase enclosingMethod)
        {
            m_enclosingMethod = enclosingMethod;
            MethodBody methodBody = m_enclosingMethod.GetMethodBody();
            m_byteArray = (methodBody == null) ? new Byte[0] : methodBody.GetILAsByteArray();
            m_position = 0;
        }

        //public string GetEnumerator()
        //{
        //    while (m_position < m_byteArray.Length)
        //        yield return Next();
        //    m_position = 0;
        //    yield break;
        //}


        public string Next()
        {
            Int32 offset = m_position;
            OpCode opCode = OpCodes.Nop;
            Int32 token = 0;

            // read first 1 or 2 bytes as opCode
            Byte code = ReadByte();
            if (code != 0xFE)
            {
                opCode = s_OneByteOpCodes[code];
            }
            else
            {
                code = ReadByte();
                opCode = s_TwoByteOpCodes[code];
            }
            csmCurrentMethod.ocOpcode = opCode;
            //       Console.Write(opCode.OperandType.ToString() + ":");
            switch (opCode.OperandType)
            {
                case OperandType.InlineNone:
                    return opCode.ToString(); // "OperandType.InlineNone:";
                case OperandType.ShortInlineBrTarget:
                    SByte shortDelta = ReadSByte();
                    return opCode + "\t" + Convert.ToString(shortDelta, 16);
                    //"ShortInlineBrTargetInstruction(m_enclosingMethod, offset, opCode, shortDelta)";

                case OperandType.InlineBrTarget:
                    Int32 delta = ReadInt32();
                    return opCode + "\t" + Convert.ToString(delta, 16);
                case OperandType.ShortInlineI:
                    Byte int8 = ReadByte();
                    return opCode + "\t" + Convert.ToString(int8, 16);
                case OperandType.InlineI:
                    Int32 int32 = ReadInt32();
                    return opCode + "\t" + Convert.ToString(int32, 16);
                case OperandType.InlineI8:
                    Int64 int64 = ReadInt64();
                    return opCode + "\t" + Convert.ToString(int64, 16);
                case OperandType.ShortInlineR:
                    Single float32 = ReadSingle();
                    return opCode + "\t" + Convert.ToString(float32);
                case OperandType.InlineR:
                    Double float64 = ReadDouble();
                    return opCode + "\t" + Convert.ToString(float64);
                case OperandType.ShortInlineVar:
                    Byte index8 = ReadByte();
                    return opCode + "\t" + Convert.ToString(index8, 16);
                case OperandType.InlineVar:
                    UInt16 index16 = ReadUInt16();
                    return opCode + "\t" + Convert.ToString(index16, 16);
                case OperandType.InlineString:
                    token = ReadInt32();
                    return opCode + "\t" + Convert.ToString(token, 16);
                case OperandType.InlineSig:
                    token = ReadInt32();
                    return opCode + "\t" + Convert.ToString(token, 16);
                case OperandType.InlineField:
                    token = ReadInt32();
                    return opCode + "\t" + Convert.ToString(token, 16);
                case OperandType.InlineTok:
                    token = ReadInt32();
                    return opCode + "\t" + Convert.ToString(token, 16);

                case OperandType.InlineType:
                    token = ReadInt32();
                    string strResultInlineType = "";
                    try
                    {
                        strResultInlineType = tTypeBeingAnalyzed.Module.ResolveType(token).FullName;
                    }
                    catch (Exception Ex)
                    {
                        strResultInlineType = Convert.ToString(token, 16) + "[" + Ex.Message.Substring(0, 40) + "...]";
                    }
                    return opCode + "\t" + strResultInlineType; // Convert.ToString(token, 16);

                case OperandType.InlineMethod:
                    token = ReadInt32();
                    string strResolvedToken = "";
                    try
                    {
                        csmCurrentMethod.iToken = token;
                        csmCurrentMethod.strParameters = new List<string>();
                        if (((token & 0x02000000) == 0x02000000) || ((token & 0x06000000) == 0x06000000))
                        {
                            MethodBase mbMethod = tTypeBeingAnalyzed.Module.ResolveMethod(token);
                            csmCurrentMethod.mbMethodBase = mbMethod;
                            csmCurrentMethod.strResolvedToken = mbMethod.ReflectedType.FullName + "." + mbMethod.Name;
                            foreach (ParameterInfo piParameter in mbMethod.GetParameters())
                                csmCurrentMethod.strParameters.Add(piParameter.ParameterType.FullName);
                            //ParameterInfo piParameters = mbMethod.GetParameters()
                        }
                            //             else if ((token & 0x06000000) == 0x06000000)
                            //             {
                            //                 MethodBase mbMethod = tTypeBeingAnalyzed.Module.ResolveMethod(token);
                            //                 csmCurrentMethod.strResolvedToken = mbMethod.ToString();
                            //                 csmCurrentMethod.mbMethod = mbMethod;
                            //             }
                        else if ((token & 0x04000000) == 0x04000000)
                        {
                            FieldInfo fiField = tTypeBeingAnalyzed.Module.ResolveField(token);
                            csmCurrentMethod.strResolvedToken = fiField + " [Field]";
                            csmCurrentMethod.mbMethodBase = null;
                        }
                        else
                            csmCurrentMethod.strResolvedToken = "Unresolved Type";
                    }
                    catch (Exception Ex)
                    {
                        strResolvedToken = Convert.ToString(token, 16) + "          [" + Ex.Message.Substring(0, 40) +
                                           "...]";
                        csmCurrentMethod.mbMethodBase = null;
                    }

                    return opCode + "  \t" + strResolvedToken; //Convert.ToString(token, 16);

                case OperandType.InlineSwitch:
                    Int32 cases = ReadInt32();
                    var deltas = new Int32[cases];
                    for (Int32 i = 0; i < cases; i++) deltas[i] = ReadInt32();
                    return opCode + "\t" + "cases" + "deltas[]";
                    // "InlineSwitchInstruction(m_enclosingMethod, offset, opCode, deltas)";

                default:
                    throw new BadImageFormatException("unexpected OperandType " + opCode.OperandType);
            }
        }

        private Byte ReadByte()
        {
            return m_byteArray[m_position++];
        }

        private SByte ReadSByte()
        {
            return (SByte) ReadByte();
        }

        private UInt16 ReadUInt16()
        {
            m_position += 2;
            return BitConverter.ToUInt16(m_byteArray, m_position - 2);
        }

        private UInt32 ReadUInt32()
        {
            m_position += 4;
            return BitConverter.ToUInt32(m_byteArray, m_position - 4);
        }

        private UInt64 ReadUInt64()
        {
            m_position += 8;
            return BitConverter.ToUInt64(m_byteArray, m_position - 8);
        }

        private Int32 ReadInt32()
        {
            m_position += 4;
            return BitConverter.ToInt32(m_byteArray, m_position - 4);
        }

        private Int64 ReadInt64()
        {
            m_position += 8;
            return BitConverter.ToInt64(m_byteArray, m_position - 8);
        }

        private Single ReadSingle()
        {
            m_position += 4;
            return BitConverter.ToSingle(m_byteArray, m_position - 4);
        }

        private Double ReadDouble()
        {
            m_position += 8;
            return BitConverter.ToDouble(m_byteArray, m_position - 8);
        }
    }
}