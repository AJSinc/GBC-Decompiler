using GBCDecompiler.Decompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCDecompiler
{
    partial class GBCDecompiler
    {

        private List<String> stackCode = new List<String>();
        bool inCodeSeg = true;
        bool codeSegEnded = false;
        
        private String decompileOpcode(OP op)
        {
            bool inCodeSegPrev = inCodeSeg;
            String str = "";
            switch (op)
            {
                case OP.DONE: return opDONE();
                case OP.MAIN: str = opMAIN(); break;
                case OP.AMEM: str = opAMEM(); break;
                case OP.CLNE: str = opCLNE(); break;
                case OP.SWP: str = opSWP(); break;
                case OP.POP: str = opPOP(); break;
                case OP.JMP: str = opJMP(); break;
                case OP.JMPZ: str = opJMPZ(); break;
                case OP.JMPNZ: str = opJMPNZ(); break;
                case OP.BRZ: str = opBRZ(); break;
                case OP.BRNZ: str = opBRNZ(); break;
                case OP.BRT: str = opBRT(); break;
                case OP.CMB: str = opCMB(); break;
                case OP.CMBZ: str = opCMBZ(); break;
                case OP.CBW: str = opCBW(); break;
                case OP.CBWE: str = opCBWE(); break;
                case OP.CBWS: str = opCBWS(); break;
                case OP.CBCR: str = opCBCR(); break;
                case OP.CBCW: str = opCBCW(); break;
                case OP.CALL: str = opCALL(); break;
                case OP.RET: str = opRET(); break;
                case OP.VRET: str = opVRET(); break;
                case OP.LCB: str = opLCB(); break;
                case OP.LCW: str = opLCW(); break;
                case OP.LPTR: str = opLPTR(); break;
                case OP.LCD: str = opLCD(); break;
                case OP.LDBX:
                case OP.LDB:
                case OP.LDW:
                case OP.LDWX:
                case OP.LDD: str = opLDD(); break;
                case OP.LDBI:
                case OP.LDBIX:
                case OP.LDWI:
                case OP.LDDI: str = opLDDI(); break;
                case OP.MSET: str = opMSET(); break;
                case OP.MCPY: str = opMCPY(); break;
                case OP.STB:
                case OP.STW:
                case OP.STD: str = opSTD(); break;
                case OP.STBI:
                case OP.STWI:
                case OP.STDI: str = opSTDI(); break;
                case OP.STWR: str = opSTWR(); break;
                
                case OP.INCB:
                case OP.INCW: 
                case OP.INCF: // test
                case OP.INCD: str = opINCD(); break;
                case OP.DECB:
                case OP.DECW:
                case OP.DECF: // test
                case OP.DECD: str = opDECD(); break;
                case OP.UTOB: str = opUTOB(); break;
                case OP.UTOW: str = opUTOW(); break;
                case OP.ITOB: str = opITOB(); break;
                case OP.ITOW: str = opITOW(); break;
                case OP.DIV: str = opDIV(); break;
                case OP.MOD: str = opMOD(); break;
                case OP.NEG: str = opNEG(); break;
                case OP.COMP: str = opCOMP(); break;
                case OP.INV: str = opINV(); break;
                case OP.ITOF: str = opITOF(); break;
                case OP.FTOI: str = opFTOI(); break;
                case OP.DIVF: str = opDIVF(); break;
                case OP.MUL: str = opMUL(); break;
                case OP.MULF: str = opMULF(); break;
                case OP.SUB: str = opSUB(); break;
                case OP.ADD: str = opADD(); break;
                case OP.SRX:
                case OP.SR: str = opSR(); break;
                case OP.SL: str = opSL(); break;
                case OP.GT: str = opGT(); break;
                case OP.GE: str = opGE(); break;
                case OP.LT: str = opLT(); break;
                case OP.LEX:
                case OP.LE: str = opLE(); break;
                case OP.EQ: str = opEQ(); break;
                case OP.NE: str = opNE(); break;
                case OP.ANDB: str = opANDB(); break;
                case OP.XORB: str = opXORB(); break;
                case OP.ORB: str = opORB(); break;
                case OP.AND: str = opAND(); break;
                case OP.XOR: str = opXOR(); break;
                case OP.OR: str = opOR(); break;
                case OP.ABS: str = opABS(); break;
                case OP.RAND: str = opRAND(); break;
                case OP.FLOOR: str = opFLOOR(); break;
                case OP.CEIL: str = opCEIL(); break;
                case OP.ROUND: str = opROUND(); break;
                case OP.MIN: str = opMIN(); break;
                case OP.MAX: str = opMAX(); break;
                case OP.CLAMP: str = opCLAMP(); break;
                case OP.MODF: str = opMODF(); break;
                case OP.LERP: str = opLERP(); break;
                case OP.SIN: str = opSIN(); break;
                case OP.COS: str = opCOS(); break;
                case OP.TAN: str = opTAN(); break;
                case OP.ASIN: str = opASIN(); break;
                case OP.ACOS: str = opACOS(); break;
                case OP.ATAN: str = opATAN(); break;
                case OP.ATAN2: str = opATAN2(); break;
                case OP.R2D: str = opR2D(); break;
                case OP.D2R: str = opD2R(); break;
                case OP.SQRT: str = opSQRT(); break;
                case OP.SQ: str = opSQ(); break;
                case OP.EXP: str = opEXP(); break;
                case OP.LOG: str = opLOG(); break;
                case OP.LOG2: str = opLOG2(); break;
                case OP.POW: str = opPOW(); break;
                case OP.POWF: str = opPOWF(); break;
                case OP.CRUN: str = opCRUN(); break;
                case OP.CRST: str = opCRST(); break;
                case OP.CPSE: str = opCPSE(); break;
                case OP.CSTP: str = opCSTP(); break;
                case OP.MXYH: str = opMXYH(); break;
                case OP.MXYV: str = opMXYV(); break;
                case OP.MXYC: str = opMXYC(); break;
                case OP.MXYR: str = opMXYR(); break;
                case OP.RMAP: str = opRMAP(); break;
                case OP.RSWP: str = opRSWP(); break;
                case OP.RRST: str = opRRST(); break;
                case OP.KMAP: str = opKMAP(); break;
                case OP.KRST: str = opKRST(); break;
                case OP.MMAP: str = opMMAP(); break;
                case OP.MRST: str = opMRST(); break;
                case OP.RMSK: str = opRMSK(); break;
                case OP.SVAL: str = opSVAL(); break;
                case OP.SVALI: str = opSVALI(); break;
                case OP.GVAL: str = opGVAL(); break;
                case OP.GVALA: str = opGVALA(); break;
                case OP.GPRV: str = opGPRV(); break;
                case OP.ISACT: str = opISACT(); break;
                case OP.ISRES: str = opISRES(); break;
                case OP.TMACT: str = opTMACT(); break;
                case OP.TMRES: str = opTMRES(); break;
                case OP.EVACT: str = opEVACT(); break;
                case OP.EVRES: str = opEVRES(); break;
                case OP.CKACT: str = opCKACT(); break;
                case OP.CKRES: str = opCKRES(); break;
                case OP.INHB: str = opINHB(); break;
                case OP.BATS: str = opBATS(); break;
                case OP.BATG: str = opBATG(); break;
                case OP.BATGA: str = opBATGA(); break;
                case OP.BATR: str = opBATR(); break;
                case OP.LEDS: str = opLEDS(); break;
                case OP.LEDG: str = opLEDG(); break;
                case OP.LEDGA: str = opLEDGA(); break;
                case OP.LEDVS: str = opLEDVS(); break;
                case OP.LEDVG: str = opLEDVG(); break;
                case OP.LEDR: str = opLEDR(); break;
                case OP.FFBS: str = opFFBS(); break;
                case OP.FFBG: str = opFFBG(); break;
                case OP.FFBGA: str = opFFBGA(); break;
                case OP.FFBR: str = opFFBR(); break;
                case OP.KSTS: str = opKSTS(); break;
                case OP.KCHK: str = opKCHK(); break;
                case OP.MSTS: str = opMSTS(); break;
                case OP.KSST: str = opKSST(); break;
                case OP.KPTU: str = opKPTU(); break;
                case OP.MSST: str = opMSST(); break;
                case OP.MPTU: str = opMPTU(); break;
                case OP.KSGT: str = opKSGT(); break;
                case OP.MSGT: str = opMSGT(); break;
                case OP.MRUN: str = opMRUN(); break;
                case OP.MRTM: str = opMRTM(); break;
                case OP.MSTP: str = opMSTP(); break;
                case OP.MREC: str = opMREC(); break;
                case OP.PMSN: str = opPMSN(); break;
                case OP.PMLN: str = opPMLN(); break;
                case OP.PMLD: str = opPMLD(); break;
                case OP.PMSV: str = opPMSV(); break;
                case OP.PMR: str = opPMR(); break;
                case OP.PMRB:
                case OP.PMRW:
                case OP.PMRD: str = opPMRD(); break;
                case OP.PMWB:
                case OP.PMWW:
                case OP.PMWD: str = opPMWD(); break;
                case OP.SCPL: str = opSCPL(); break;
                case OP.MSLG: str = opMSLG(); break;
                case OP.MSLL: str = opMSLL(); break;
                case OP.MSLC: str = opMSLC(); break;
                case OP.SYSTM: str = opSYSTM(); break;
                case OP.ELPTM: str = opELPTM(); break;
                case OP.PWSRC: str = opPWSRC(); break;
                case OP.PSTS: str = opPSTS(); break;
                case OP.PCONN: str = opPCONN(); break;
                case OP.PCONX: str = opPCONX(); break;
                case OP.PDIS: str = opPDIS(); break;
                case OP.PIFFB: str = opPIFFB(); break;
                case OP.PPFFB: str = opPPFFB(); break;
                case OP.PTOFF: str = opPTOFF(); break;
                case OP.PUON: str = opPUON(); break;
                case OP.PUOFF: str = opPUOFF(); break;
                case OP.DOVR: str = opDOVR(); break;
                case OP.PRTF: str = opPRTF(); break;
                default:
                    Console.Write("\r\nUknown OP: 0x" + ((byte)op).ToString("X") + " at " + (reader.BaseStream.Position-1).ToString("X") + "\r\n");
                    break;
            }
            
            if (!inCodeSegPrev && !inCodeSeg)
            {
                str = "\r\nmain {\r\n" + str;
                inCodeSeg = true;
            }
            if(inCodeSegPrev && !inCodeSeg)
            {
                str = "}\r\n" + str;
            }
            return str;
        }

        private String opDONE()
        {
            if (inCodeSeg)
            {
                inCodeSeg = false;
                return "}";
            }
            return "";
        }

        private String opMAIN()
        {
            inCodeSeg = true;
            return "}\r\n\r\nmain {";
        }

        private String opAMEM()
        {
            reader.BaseStream.Position += 2; // advance reader 2 bytes
            return "";
        }

        private String opCLNE() // fix
        {
            return SetLastStackCode(GetLastStackCode());
        }

        private String opSWP()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            SetLastStackCode(var1);
            return SetLastStackCode(var2);
        }

        private String opPOP()
        {
            if (prevDecOpCode == OP.CALL) ;
            return PopLastStackCode() + ";"; // need to return the last idx and pop it off the stack
        }

        #region JUMP and BRANCH

        private String opJMP()
        {
            int offset = ReadNextUShort();
            if (nestedEndOffset.Contains(reader.BaseStream.Position))
            {
                String str = "";
                for(int i = 0; i < nestedEndOffset.Count; i++)
                {
                    if (nestedEndOffset[i] == reader.BaseStream.Position)
                    {
                        str += "\r\n}";
                        nestedEndOffset[i] = 0;
                    }
                }
                nestedEndOffset.Add(offset);
                return str + "\r\nelse {";
            }
            else
            {
                jumpLabelOffset.Add(offset);
            }
            return "goto label" + offset.ToString("X") + ";";
        }

        private String opJMPZ() // check
        {
            return opJMPNZ();
        }

        private String opJMPNZ()
        {
            int offset = ReadNextUShort();
            long pos = reader.BaseStream.Position;
            reader.BaseStream.Position = offset - 1;
            OP b = (OP)reader.ReadByte();
            reader.BaseStream.Position = pos;
            if (b == OP.OR)
            {
                return GetLastStackCode();
            }
            return "";
        }

        private String opBRZ() // check - fix else
        {
            int offset = ReadNextUShort();
            String str = "";
            nestedEndOffset.Add(offset);
            if((OP)prevLineEndOpCode == OP.JMP)
            {
                str = codeLine.ElementAt(codeLine.Count - 1).Code;
                str = str.Substring(0, str.Length - 1);
                codeLine.RemoveAt(codeLine.Count - 1);
                nestedEndOffset.Remove(offset);
            }
            return str + "if(" + PopLastStackCode() + ") {";
        }

        private String opBRNZ()
        {
            int offset = ReadNextUShort();
            return "if(" + PopLastStackCode() + ") goto label" + offset.ToString("X");
        }

        private String opBRT()  //FIX
        {
            int var = ReadNextUShort();
            int var2 = reader.ReadByte();
            int var3 = ReadNextUShort();
            return PopLastStackCode() + " + goto;";
        }

        #endregion

        #region COMBO FLOW

        private String opCMB()
        {
            String str = (inCodeSeg ?  "}" : "") + "\r\n\r\ncombo c" + ReadNextUShort().ToString("X") + " {";
            reader.BaseStream.Position += 4; // skip 4 positions?
            inCodeSeg = true;
            return str;
        }

        private String opCMBZ()
        {
            reader.BaseStream.Position += 2; // skip 4 positions?
            inCodeSeg = false;
            return "";
        }
        
        private String opCBW()
        {
            reader.BaseStream.Position += 2;
            String str = ("wait(" + ReadNextUShort() + ");");
            reader.BaseStream.Position += 2;
            return str;
        }
        
        private String opCBWE()  //check
        {
            reader.BaseStream.Position += 4;
            return "";
        }
        
        private String opCBWS() // check
        {
            reader.BaseStream.Position += 4;
            return "wait(" + PopLastStackCode() + ");";
        }

        private String opCBCR()
        {
            int addr = ReadNextUShort();
            reader.BaseStream.Position += 2; // skip 2 ukn
            return "call(c" + addr.ToString() + ");";
        }

        private String opCBCW()
        {
            reader.BaseStream.Position += 6;
            return "";
        }

        #endregion

        private String opCALL()  //FIX
        {
            int addr = ReadNextUShort();
            int paramCount = reader.ReadByte();
            int i;
            for (i = 0; i < codeFunction.Count; i++)
            {
                if (codeFunction[i].Address == addr) break;
            }
            if (i == codeFunction.Count)
            {
                int returns = ((ReadNextOp() == OP.POP||StackCount() > 0) ? 1 : 0); // test
                codeFunction.Add(new GBCFunction(addr, paramCount, returns));
            }
            
            String str = "f" + addr.ToString("X") + "()";

            if (codeFunction[i].ReturnType > 0)
                return SetLastStackCode(str);
            return str + ";";
        }

        private String opRET()
        {
            return "return;";
        }

        private String opVRET()
        {
            return "return " + PopLastStackCode() + ";";
        }

        #region get

        private String opLCB()
        {
            return SetLastStackCode(reader.ReadByte().ToString());
        }

        private String opLCW()
        {
            return SetLastStackCode(ReadNextUShort().ToString());
        }

        private String opLPTR()
        {
            long currPos = reader.BaseStream.Position + 2;
            long offset = reader.BaseStream.Position = ReadNextUShort();
            String str = "";
            if (offset < codeBeginOffset)
            {
                str = "\"" + ReadNextCharString() + "\"";
            }
            else
            {
                str = "var" + offset.ToString("X");
            }
            reader.BaseStream.Position = currPos;
            return SetLastStackCode(str);
        }
        
        private String opLCD()
        {
            return SetLastStackCode(ReadNextInt32().ToString());
        }
        
        private String opLDD()
        {
            String str = "var" + ReadNextUShort().ToString("X");
            return SetLastStackCode(str);
        }

        private String opLDDI()
        {
            return "";// SetLastStackCode(GetArrayIndexFromStack(PopLastStackCode()));
        }

        #endregion

        private String opMSET()
        {
            return "memset(&var" + ReadNextUShort().ToString("X") + ", " + reader.ReadByte() + ", " + ReadNextUShort().ToString() + ");";
        }

        private String opMCPY()
        {
            return "memcpy(&var" + ReadNextUShort().ToString("X") + ", &var" + ReadNextUShort().ToString("X") + ", " + ReadNextUShort().ToString() + ");";
        }
        
        #region set

        private String opSTD()
        {
            return "var" + ReadNextUShort().ToString("X") + " = " + PopLastStackCode() + ";";
        }

        private String opSTDI()
        {
            String val = PopLastStackCode();
            return "";// GetArrayIndexFromStack(PopLastStackCode()) + " = " + val + ";";
        }

        #endregion

        private String opSTWR() //FIX
        {
            int b = reader.ReadByte();
            return "";
        }

        #region Operands

        private String opINCD() //FIX
        {
            int CF = reader.ReadByte(); // CF flag - 81?
            if (CF == 0x01)
            {
                if (prevDecOpCode == OP.SWP)
                {
                    return SetLastStackCode(PopLastStackCode() + "++");
                }
                if (OP.LDBI <= ReadNextOp() && ReadNextOp() <= OP.LDDI)
                {
                    reader.BaseStream.Position += 1; // skip these
                    return SetLastStackCode("(++" + PopLastStackCode().Substring(1));
                }
                //return "--" + PopLastStackCode()
            }
            else if (CF == 0x81)
            {
                int varAddr = ReadNextUShort();
                String var = "var" + varAddr.ToString("X");
                if (StackCount() > 0 && GetLastStackCode().Equals(var))
                {
                    PopLastStackCode();
                    return SetLastStackCode(var + "++");
                }
                else
                {
                    if (ReadNextOp() >= OP.LDB && ReadNextOp() <= OP.LDD)
                    {
                        reader.ReadByte(); // skip
                        if (varAddr == ReadNextUShort()) // compare if the next addr used is the same as the decemented one
                        {
                            return SetLastStackCode("++" + var);
                        }
                        reader.BaseStream.Position -= 3;
                    }
                }
                return "++" + var + ";";
            }
            return "DEC UKN CTRL FLAG";
        }

        private String opDECD() //FIX
        {
            int CF = reader.ReadByte(); // CF flag - 81?
            if (CF == 0x01)
            {
                PopLastStackCode();
                return SetLastStackCode("(--" + PopLastStackCode() + ")");
            }
            else if (CF == 0x81)
            {
                int varAddr = ReadNextUShort();
                String var = "var" + varAddr.ToString("X");
                if (StackCount() > 0 && GetLastStackCode().Equals(var))
                {
                    PopLastStackCode();
                    return SetLastStackCode(var + "--");
                }
                else
                { 
                    if (ReadNextOp() >= OP.LDB && ReadNextOp() <= OP.LDD)
                    {
                        reader.ReadByte(); // skip
                        if (varAddr == ReadNextUShort()) // compare if the next addr used is the same as the decemented one
                        {
                            return SetLastStackCode("--" + var);
                        }
                        reader.BaseStream.Position -= 3;
                    }
                }
                return "--" + var + ";";
            }
            return "DEC UKN CTRL FLAG";
        }

        private String opUTOB()
        {
            return SetLastStackCode("(uint8) " + PopLastStackCode());
        }

        private String opUTOW()
        {
            return SetLastStackCode("(uint16) " + PopLastStackCode());
        }

        private String opITOB()
        {
            return SetLastStackCode("(uint8) " + PopLastStackCode());
        }

        private String opITOW()
        {
            return SetLastStackCode("(uint16) " + PopLastStackCode());
        }

        private String opMOD()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " % " + var1 + ")");
        }

        private String opNEG()
        {
            stackCode.Add("!" + PopLastStackCode());
            return GetLastStackCode();
        }

        private String opCOMP()
        {
            return SetLastStackCode("~" + PopLastStackCode());
        }

        private String opINV()
        {
            return SetLastStackCode("inv(" + PopLastStackCode() + ")");
        }

        private String opITOF()
        {
            return SetLastStackCode("(fix32) " + PopLastStackCode());
        }

        private String opFTOI()
        {
            stackCode.Add("(int32) " + PopLastStackCode());
            return GetLastStackCode();
        }

        private String opDIV()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " / " + var1 + ")");
        }

        private String opDIVF()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " / " + var1 + ")");
        }

        private String opMUL()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " * " + var1 + ")");
        }

        private String opMULF()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " * " + var1 + ")");
        }

        private String opSUB()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " - " + var1 + ")");
        }

        private String opADD()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " + " + var1 + ")");
        }

        private String opSR()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " >> " + var1 + ")");
        }

        private String opSL()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " << " + var1 + ")");
        }
        
        private String opGE()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " >= " + var1);
        }

        private String opLE()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " <= " + var1);
        }
        
        private String opGT()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " > " + var1);
        }
        
        private String opLT()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " < " + var1);
        }
        
        private String opEQ()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " == " + var1);
        }

        private String opNE()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode(var2 + " != " + var1);
        }

        private String opANDB()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " & " + var1 + ")");
        }

        private String opXORB()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " ^ " + var1 + ")");
        }

        private String opORB()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " | " + var1 + ")");
        }

        private String opAND()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " && " + var1 + ")");
        }

        private String opXOR()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " ^^ " + var1 + ")");
        }

        private String opOR()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("(" + var2 + " || " + var1 + ")");
        }

        #endregion
        
        #region MATH

        private String opABS()
        {
            return SetLastStackCode("abs(" + PopLastStackCode() + ")");
        }

        private String opRAND()
        {
            return SetLastStackCode("rand()");
        }

        private String opFLOOR()
        {
            return SetLastStackCode("floor(" + PopLastStackCode() + ")");
        }

        private String opCEIL()
        {
            return SetLastStackCode("ceil(" + PopLastStackCode() + ")");
        }

        private String opROUND()
        {
            return SetLastStackCode("round(" + PopLastStackCode() + ")");
        }
        
        private String opMIN()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("min(" + var2 + ", " + var1 + ")");
        }

        private String opMAX()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("max(" + var2 + ", " + var1 + ")");
        }

        private String opCLAMP()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode(), var3 = PopLastStackCode();
            return SetLastStackCode("clamp(" + var3 + ", " + var2 + ", " + var1 + ")");
        }

        private String opMODF()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("mod(" + var2 + ", " + var1 + ")");
        }

        private String opLERP()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode(), var3 = PopLastStackCode();
            return SetLastStackCode("lerp(" + var3 + ", " + var2 + ", " + var1 + ")");
        }

        private String opSIN()
        {
            return SetLastStackCode("sin(" + PopLastStackCode() + ")");
        }

        private String opCOS()
        {
            return SetLastStackCode("cos(" + PopLastStackCode() + ")");
        }

        private String opTAN()
        {
            return SetLastStackCode("tan(" + PopLastStackCode() + ")");
        }

        private String opASIN()
        {
            return SetLastStackCode("asin(" + PopLastStackCode() + ")");
        }

        private String opACOS()
        {
            return SetLastStackCode("acos(" + PopLastStackCode() + ")");
        }

        private String opATAN()
        {
            return SetLastStackCode("atan(" + PopLastStackCode() + ")");
        }

        private String opATAN2()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("atan2(" + var2 + ", " + var1 + ")");
        }
        
        private String opR2D()
        {
            return SetLastStackCode("rad2deg(" + PopLastStackCode() + ")");
        }

        private String opD2R()
        {
            return SetLastStackCode("deg2rad(" + PopLastStackCode() + ")");
        }

        private String opSQRT()
        {
            return SetLastStackCode("sqrt(" + PopLastStackCode() + ")");
        }

        private String opSQ()
        {
            return SetLastStackCode("sq(" + PopLastStackCode() + ")");
        }

        private String opEXP()
        {
            return SetLastStackCode("exp(" + PopLastStackCode() + ")");
        }

        private String opLOG()
        {
            return SetLastStackCode("log(" + PopLastStackCode() + ")");
        }

        private String opLOG2()
        {
            return SetLastStackCode("log2(" + PopLastStackCode() + ")");
        }

        private String opPOW()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("pow(" + var2 + ", " + var1 + ")");
        }

        private String opPOWF()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("pow(" + var2 + ", " + var1 + ")");
        }



        #endregion

        #region COMBO func

        private String opCRUN()
        {
            return "combo_run(c" + ReadNextUShort().ToString("X") + ");";
        }

        private String opCRST()
        {
            return "combo_restart(c" + ReadNextUShort().ToString("X") + ");";
        }

        private String opCPSE()
        {
            return "combo_pause(c" + ReadNextUShort().ToString("X") + ");";
        }

        private String opCSTP()
        {
            return "combo_stop(c" + ReadNextUShort().ToString("X") + ");";
        }

        #endregion

        #region KBM Mapping

        private String opMXYH()
        {
            return "mxyconverter_hoffset(" + PopLastStackCode() + ")";
        }

        private String opMXYV()
        {
            return "mxyconverter_voffset(" + PopLastStackCode() + ")";
        }

        private String opMXYC() // add data to add this to data list
        {
            int dataOffset = ReadNextUShort();
            int size = reader.ReadByte();
            // add to data list for second pass through
            return SetLastStackCode("mxyconverter(" + ((size > 0) ? ("data" + dataOffset.ToString("X")) : "") + ")");
        }

        private String opMXYR()
        {
            return "mxyconverter_reset();";
        }

        private String opRMAP() // add code to metadata to get this data back
        {
            return SetLastStackCode("remapper(data" + ReadNextUShort().ToString("X") + ")");
        }

        private String opRSWP() // add code to metadata to get this data back
        {
            int buttonIdx = reader.ReadByte(), buttonIdx2 = reader.ReadByte();
            if (buttonIdx2 == 0x26)
                return SetLastStackCode("remapper_disable(BUTTON_" + (buttonIdx + 1) + ")");
            return SetLastStackCode("remapper_swap(BUTTON_" + (buttonIdx + 1) + ", BUTTON_" + (buttonIdx2 + 1) + ")");
        }

        private String opRMSK()
        {
            return "remapper_mask(" + PopLastStackCode() + ");";
        }

        private String opRRST()
        {
            return "remapper_reset();";
        }

        private String opKMAP() // add data to add this to data list
        {
            int dataOffset = ReadNextUShort();
            int size = reader.ReadByte();
            // add to data list for second pass through
            return SetLastStackCode("keymapping(" + ((size > 0) ? ("data" + dataOffset.ToString("X")) : "") + ")");
        }

        private String opKRST()
        {
            return "keymapping_reset();";
        }

        private String opMMAP() // add data to add this to data list
        {
            int dataOffset = ReadNextUShort();
            int size = reader.ReadByte();
            // add to data list for second pass through
            return SetLastStackCode("mousemapping(" + ((size > 0) ? ("data" + dataOffset.ToString("X")) : "") + ")");
        }

        private String opMRST()
        {
            return "mousemapping_reset();";
        }

        #endregion

        #region IO

        private String opSVAL()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "set_val(" + var2 + ", " + var1 + ");";
        }

        private String opSVALI()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "set_val(" + var2 + ", " + var1 + ");";
        }

        private String opGVAL()
        {
            return SetLastStackCode("get_val(" + PopLastStackCode() + ")");
        }

        private String opGVALA()
        {
            return SetLastStackCode("get_actual(" + PopLastStackCode() + ")");
        }

        private String opGPRV()
        {
            return SetLastStackCode("get_prev(" + PopLastStackCode() + ")");
        }

        private String opISACT()
        {
            return SetLastStackCode("is_active(" + PopLastStackCode() + ")");
        }

        private String opISRES()
        {
            return SetLastStackCode("is_release(" + PopLastStackCode() + ")");
        }
        
        private String opTMACT()
        {
            return SetLastStackCode("time_active(" + PopLastStackCode() + ")");
        }

        private String opTMRES()
        {
            return SetLastStackCode("time_release(" + PopLastStackCode() + ")");
        }

        private String opEVACT()
        {
            return SetLastStackCode("event_active(" + PopLastStackCode() + ")");
        }

        private String opEVRES()
        {
            return SetLastStackCode("event_release(" + PopLastStackCode() + ")");
        }

        private String opCKACT()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("check_active(" + var2 + ", " + var1 + ")");
        }

        private String opCKRES()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return SetLastStackCode("check_release(" + var2 + ", " + var1 + ")");
        }

        private String opINHB()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "inhibit(" + var2 + ", " + var1 + ");";
        }

        #endregion

        #region BATTERY

        private String opBATS()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "battery_set(" + var2 + ", " + var1 + ");";
        }

        private String opBATG()
        {
            return SetLastStackCode("battery_get(&" + PopLastStackCode() + ")");
        }

        private String opBATGA()
        {
            return SetLastStackCode("battery_get_actual(&" + PopLastStackCode() + ")");
        }
        
        private String opBATR()
        {
            return "battery_reset();";
        }

        #endregion

        #region LED

        private String opLEDS() // add code to get identifier from first var idx
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "led_set(" + reader.ReadByte() + ", " + var2 + ", " + var1 + ");";
        }

        private String opLEDG()
        {
            return SetLastStackCode("led_get(" + reader.ReadByte() + ", &" + PopLastStackCode() + ")");
        }

        private String opLEDGA()
        {
            return SetLastStackCode("led_get_actual(" + reader.ReadByte() + ", &" + PopLastStackCode() + ")");
        }

        private String opLEDVS()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode() , var3 = PopLastStackCode();
            return "led_set(" + reader.ReadByte() + ", " + var3 + ", " + var2 + ", " + var1 + ");";
        }

        private String opLEDVG()
        {
            return SetLastStackCode("led_vmget(" + reader.ReadByte() + ")");
        }

        private String opLEDR()
        {
            return "led_reset();";
        }

        #endregion

        #region FFB

        private String opFFBS()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "ffb_set(" + reader.ReadByte() + ", " + var2 + ", " + var1 + ");";
        }

        private String opFFBG()
        {
            return SetLastStackCode("ffb_get(" + reader.ReadByte() + ", &" + PopLastStackCode() + ")");
        }

        private String opFFBGA()
        {
            return SetLastStackCode("ffb_get_actual(" + reader.ReadByte() + ", &" + PopLastStackCode() + ")");
        }

        private String opFFBR()
        {
            return "ffb_reset();";
        }

        #endregion

        #region KBM IO 

        private String opKSTS()
        {
            return SetLastStackCode("key_status(" + PopLastStackCode() + ")");
        }

        private String opKCHK()
        {
            return SetLastStackCode("key_check()");
        }

        private String opMSTS()
        {
            return SetLastStackCode("mouse_status(" + PopLastStackCode() + ")");
        }

        private String opKSST()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "key_set(" + var2 + ", " + var1 + ");";
        }

        private String opKPTU()
        {
            return "key_passthru();";
        }

        private String opMSST()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "mouse_set(" + var2 + ", " + var1 + ");";
        }

        private String opMPTU()
        {
            return "mouse_passthru();";
        }

        private String opKSGT()
        {
            return SetLastStackCode("key_get(" + PopLastStackCode() + ")");
        }

        private String opMSGT()
        {
            return SetLastStackCode("mouse_get(" + PopLastStackCode() + ")");
        }

        #endregion

        #region MACRO

        private String opMRUN()
        {
            return "macro_run(" + PopLastStackCode() + ");";
        }

        private String opMRTM()
        {
            return SetLastStackCode("macro_time()");
        }

        private String opMSTP()
        {
            return "macro_stop();";
        }
        
        private String opMREC()
        {
            return "macro_rec(" + PopLastStackCode() + ");";
        }

        #endregion

        #region PMEM

        private String opPMSN()
        {
            return "pmem_save(" + PopLastStackCode() + ");";
        }

        private String opPMLN()
        {
            return "pmem_load(" + PopLastStackCode() + ");";
        }

        private String opPMLD()
        {
            return "pmem_load();";
        }

        private String opPMSV()
        {
            return "pmem_save();";
        }

        private String opPMR()
        {
            return SetLastStackCode("pmem_read(" + reader.ReadByte() + ")");
        }

        private String opPMRD()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "pmem_read(" + var2 + ", &" + var1 + ");";
        }

        private String opPMWD()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "pmem_write(" + var2 + ", " + var1 + ");";
        }

        #endregion

        private String opMSLG()
        {
            return SetLastStackCode("mslot_get()");
        }

        private String opMSLL()
        {
            return "mslot_load(" + PopLastStackCode() + ");";
        }

        private String opMSLC()
        {
            return SetLastStackCode("mslot_check(" + PopLastStackCode() + ")");
        }

        private String opSCPL()
        {
            return SetLastStackCode("script_load(" + PopLastStackCode() + ")");
        }

        private String opSYSTM()
        {
            return SetLastStackCode("system_time()");
        }

        private String opELPTM()
        {
            return SetLastStackCode("elapsed_time()");
        }

        #region PORT

        private String opPWSRC()
        {
            return SetLastStackCode("power_source()");
        }

        private String opPSTS()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode(), var3 = PopLastStackCode();
            return SetLastStackCode("port_status(" + var3 + ", &" + var2 + ", &" + var1 + ")");
        }

        private String opPCONN()
        {
            return "port_connect();";
        }

        private String opPCONX()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "port_connect(" + var2 + ", " + var1 + ");";
        }

        private String opPDIS()
        {
            return "port_disconnect();";
        }

        private String opPIFFB()
        {
            return "port_inhibit_ffb(" + PopLastStackCode() + ");";
        }

        private String opPPFFB()
        {
            return "port_permit_ffb(" + PopLastStackCode() + ");";
        }

        private String opPTOFF()
        {
            return "port_turnoff(" + PopLastStackCode() + ");";
        }

        private String opPUON()
        {
            return "port_usb_poweron(" + PopLastStackCode() + ");";
        }

        private String opPUOFF()
        {
            return "port_usb_poweroff(" + PopLastStackCode() + ");";
        }

        #endregion

        private String opDOVR()
        {
            String var1 = PopLastStackCode(), var2 = PopLastStackCode();
            return "display_overlay(" + var2 + ", " + var1 + ");";
        }

        private String opPRTF()
        {
            String paramStr = ");"; 
            int paramCount = reader.ReadByte();
            for(int i = 1; i < paramCount; i++)
            {
                paramStr = PopLastStackCode() + ((i-1==0) ? "" : ", ") + paramStr;
            }
            return "printf(" + PopLastStackCode() + (paramCount > 1 ? (", " + paramStr) : ");");
        }
        
    }
}
