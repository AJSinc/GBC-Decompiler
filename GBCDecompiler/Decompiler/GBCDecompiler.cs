﻿using GBCDecompiler.Decompiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCDecompiler
{

    partial class GBCDecompiler
    {
        private BinaryReader reader;
        private long codeBeginOffset;
        private long currLineBeginOffset;
        List<CodeMemAddress> codeLine;
        List<GBCFunction> codeFunction;
        private List<long> ifStatementEndOffset;
        private List<long> accountedIfStatmentEndOffset;
        private List<long> elseStatementEndOffset;
        private List<long> whileStatementEndOffset;
        private List<long> jumpLabelOffset;

        private List<String> stackCode;
        bool inCodeSeg;

        OP currOpCode;
        OP prevLineEndOpCode;
        OP prevDecOpCode;

        public GBCDecompiler(String path) : this()
        {
            reader = new BinaryReader(File.Open(path, FileMode.Open));
        }

        public GBCDecompiler() {
            codeLine = new List<CodeMemAddress>();
            codeFunction = new List<GBCFunction>();
            ifStatementEndOffset = new List<long>();
            accountedIfStatmentEndOffset = new List<long>();
            elseStatementEndOffset = new List<long>();
            whileStatementEndOffset = new List<long>();
            jumpLabelOffset = new List<long>();
            stackCode = new List<String>();
            inCodeSeg = true;
        }
        
        public String Decompile()
        {
            reader.BaseStream.Position = 0;
            Console.Write(DecompileMetadata());
            Console.WriteLine("\r\n\r\n");
            Console.Write(DecompileCodeBlocks());
            return "";
        }

        private String DecompileMetadata()
        {
            String metadataCode = "#pragma METAINFO(\"";
            // first 4 bytes {0 - jump, 2&3 - code start offset, 4 - ukn}
            reader.ReadByte(); // skip the jump op
            byte[] date;
            codeBeginOffset = GetCodeStartOffset();
            reader.ReadByte(); // advance forward 1 byte
            if (reader.BaseStream.Position+1 < codeBeginOffset)
                date = reader.ReadBytes(3);              // next 3 Bytes = date
            if ((reader.BaseStream.Position + 1) < codeBeginOffset)
            {
                metadataCode += ReadNextCharString() + "\", ";      // next bytes = Script name till delimited \0
                metadataCode += reader.ReadByte() + ", ";       // version (1 byte)
                metadataCode += reader.ReadByte() + ", \"";     // version decimal part (1 byte)
                metadataCode += ReadNextCharString() + "\")";       // next bytes = Author name till delimited \0
            }
            
            reader.BaseStream.Position = codeBeginOffset;
            return metadataCode;
        }

        private long GetCodeStartOffset()
        {
            reader.BaseStream.Position = 1;
            int offset = (reader.ReadByte() << 8) + reader.ReadByte();
            if (offset > reader.BaseStream.Length)
                offset &= 0x00FF;
            return offset;
        }

        private String DecompileCodeBlocks()
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                String newCode = "";
                long pos = reader.BaseStream.Position;
                
                foreach (int offset in ifStatementEndOffset)
                {
                    if (offset == reader.BaseStream.Position) newCode += "}\r\n";
                }

                // Function address check
                foreach (GBCFunction f in codeFunction)
                {
                    if (f.Address == pos) newCode += f.ToString() + "{ \r\n";
                }

                newCode += DecompileNextCodeLine();

                foreach (int offset in elseStatementEndOffset)
                {
                    if (offset == reader.BaseStream.Position) {
                        newCode += "\r\n}";
                        break;
                    }
                }
                
                codeLine.Add(new CodeMemAddress(newCode, (uint)pos));
                
            }
            String str = "init {\r\n";
            foreach (CodeMemAddress c in codeLine)
            {
                if (jumpLabelOffset.Count > 0 && c.Address == jumpLabelOffset[0])
                {
                    str += "label_0x" + c.Address.ToString("X") + ":\r\n";
                    jumpLabelOffset.RemoveAt(0);
                }
                if (str.Length == 0) continue;
                str += (c.Code + "\r\n");
            }
            if (prevDecOpCode != OP.DONE) str += "}";
            return str;
        }

        private String DecompileNextCodeLine()
        {
            currLineBeginOffset = reader.BaseStream.Position;
            String str = "";
            OP op = 0;
            do
            {
                op = (OP)reader.ReadByte();
                currOpCode = (OP)op;
                str = decompileOpcode(op);
                prevDecOpCode = (OP)op;
            } while (StackCount() > 0); 
            prevLineEndOpCode = (OP)op;
            return str;

        }

        private String ReadNextCharString()
        {
            String tmp = "";
            long j = reader.BaseStream.Position;
            while (reader.PeekChar() != 0)
            {
                tmp += (char)reader.ReadByte();
            }
            reader.ReadByte(); // get rid of null character
            return tmp;
        }
        
        private int ReadNextInt32()
        {
            int[] b = new int[4];
            b[0] = reader.ReadByte(); b[1] = reader.ReadByte(); b[2] = reader.ReadByte(); b[3] = reader.ReadByte();
            return (b[0] << 24) + (b[1] << 16) + (b[2] << 8) + b[3];
        }

        private int ReadNextUShort()
        {
            int[] b = new int[2];
            b[0] = reader.ReadByte(); b[1] = reader.ReadByte();
            return (b[0] << 8) + b[1];
        }
        
        private String GetLastStackCode()
        {
            return stackCode[stackCode.Count - 1];
        }

        private String PopLastStackCode()
        {
            String str = stackCode[stackCode.Count - 1];
            stackCode.RemoveAt(stackCode.Count - 1);
            return str;
        }

        private int StackCount()
        {
            return stackCode.Count;
        }

        private String SetLastStackCode(String str)
        {
            stackCode.Add(str);
            return str;
        }
        
        /*
        private String GetArrayIndexFromStack(String str) // get better solution -> hacky now
        {
            OP arrOp = ((currOpCode == OP.CLNE) ? PeekNextOp() : currOpCode);
            String arrName = "";
            String idx = "";
            if (str.StartsWith("((")) // flip the array to the front
            {
                String tmp = str.Substring(str.LastIndexOf("+") + 1);
                str = str.Substring(1, (str.LastIndexOf("+")-1));
                tmp = tmp.Substring(0, tmp.Length - 1) + " + ";
                str = tmp + str.Trim() + ")";
            }

            arrName = str.Substring(1, (str.IndexOf("+")-2));
            
            if(arrOp == OP.LDBI || arrOp == OP.LDBIX || arrOp == OP.STBI || arrOp == OP.DECB)
            {
                idx = str.Substring(str.IndexOf("+")+2);
                idx = idx.Substring(0, idx.Length-1);
            }
            else
            {

            }
            if (currOpCode == OP.CLNE && arrOp != OP.DECB) reader.BaseStream.Position += 1;
            return "(" + arrName + "[" + idx + "]" + ")";
        }
        */

    }
}
