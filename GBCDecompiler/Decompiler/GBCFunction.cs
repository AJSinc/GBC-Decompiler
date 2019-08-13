using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCDecompiler.Decompiler
{
    class GBCFunction
    {
        int address;
        int paramCount;
        int returnType;

        public GBCFunction(int addr, int paramCount, int returnType)
        {
            this.address = addr;
            this.paramCount = paramCount;
            this.returnType = returnType;
        }

        public int Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public int ParamCount
        {
            get
            {
                return paramCount;
            }
            set
            {
                paramCount = value;
            }
        }
        
        public int ReturnType
        {
            get
            {
                return returnType;
            }
            set
            {
                returnType = value;
            }
        }

        private String retType()
        {
            switch(returnType)
            {
                case 1: return "int";
                case 2: return "int";
                case 3: return "int32";
                case 4: return "fix32";
            }
            return "void";
        }

        public String ToString()
        {
            String paramStr = "";
            for(int i = 0; i < paramCount; i++)
            {
                paramStr += "int p" + i + ((i + 1 == paramCount) ? "" : ", ");
            }
            return retType() + " f" + address.ToString("X") + "(" + paramStr + ")";
        }
    }
}
