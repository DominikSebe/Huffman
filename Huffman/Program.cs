using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VariedLengthBinary v = 0b0010_0001_1000_1000;
            VariedLengthBinary v2 = 0b1110_0010;
            VariedLengthBinary v3 = v | v2;
            VariedLengthBinary v4 = v2 << 9;
            VariedLengthBinary v5 = v >> 10;

            Console.WriteLine(v.ToString());
            Console.WriteLine(v.Value);
            Console.WriteLine(v.BitLength);
            Console.WriteLine("-----------");
            Console.WriteLine(v2.ToString());
            Console.WriteLine(v2.Value);
            Console.WriteLine(v2.BitLength);
            Console.WriteLine("-----------");
            Console.WriteLine(v3.ToString());
            Console.WriteLine(v3.Value);
            Console.WriteLine(v3.BitLength);
            Console.WriteLine("-----------");
            Console.WriteLine(v4.ToString());
            Console.WriteLine(v4.Value);
            Console.WriteLine(v4.BitLength);
            Console.WriteLine("-----------");
            Console.WriteLine(v5.ToString());
            Console.WriteLine(v5.Value);
            Console.WriteLine(v5.BitLength);

            Console.ReadKey();
        }
    }
}
