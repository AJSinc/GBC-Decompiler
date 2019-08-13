using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCDecompiler.Decompiler
{
    class CodeMemAddress
    {

        private String code;
        private uint addr;

        public CodeMemAddress(String code, uint addr)
        {
            this.code = code;
            this.addr = addr; 
        }

        public String Code
        {
            get
            {
                return code;
            }
            set
            {
                Code = value;
            }
        }

        public uint Address
        {
            get
            {
                return addr;
            }
            set
            {
                addr = value;
            }
        }
    }
}
