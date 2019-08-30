using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCDecompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Alex\Desktop\test.gbc";
            if (File.Exists(path))
            {
                GBCDecompiler d = new GBCDecompiler(path);
                d.Decompile();
            }
            Console.Read();
        }
    }
}
