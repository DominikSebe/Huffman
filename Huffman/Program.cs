using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{    internal class Program
    {
        static void Main(string[] args)
        {
            string test = "This is a string for tesing the Huffman algorythm.";

            Dictionary<char, int> freq = new Dictionary<char, int>();
            for (int i = 0; i < test.Length; i++)
            {
                if (freq.ContainsKey(test[i])) freq[test[i]]++;
                else freq.Add(test[i], 1);
            }

            PriorityQueue<HuffmanTree> priorityQueue = new PriorityQueue<HuffmanTree>(freq.Select(item => new HuffmanTree(item.Key, item.Value)).ToArray());

            HuffmanTree huffmanTree = BuildTree(priorityQueue);

            List<VariedLengthBinary> codes = new List<VariedLengthBinary>();

            foreach (char key in test)
            {
                HuffmanTree item = HuffmanTree.Find(huffmanTree, t => t.Key == key);
                byte b_code = 0b0;
                int counter = 0;

                while (item.Parent != null)
                {
                    if (item.Parent.Right == item) b_code |= (byte)(0b1 << counter);
                    item = item.Parent;
                    counter++;
                }

                codes.Add(new VariedLengthBinary(b_code, counter));
            }

            VariedLengthBinary totalCode = new VariedLengthBinary(0, 0);

            totalCode |= codes.ElementAt(0);

            for (int i = 1; i < codes.Count(); i++)
            {
                VariedLengthBinary code = codes.ElementAt(i);
                totalCode <<= code.BitLength;
                totalCode |= code;
            }

            foreach (VariedLengthBinary code in codes)
                Console.Write("{0} ", code);

            Console.WriteLine();
            Console.WriteLine(new String('-', totalCode.BitLength));
            Console.WriteLine(totalCode.ToString());



            string decoded = "";

            while (totalCode.BitLength > 0)
            {
                HuffmanTree leaf = HuffmanTree.FindCode(huffmanTree, totalCode);
                if (leaf != null)
                {
                    decoded += leaf.Key;

                    int depth = 0;
                    while (leaf.Parent != null) { leaf = leaf.Parent; depth++; }

                    byte h = 0b0;
                    for (int i = 0; i < depth; i++) h |= (byte)(0b1 << i);

                    totalCode &= ~(new VariedLengthBinary(h, 0) << (totalCode.BitLength - depth));
                }
            }
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(decoded);


            Console.ReadKey();
        }

        public static HuffmanTree BuildTree(PriorityQueue<HuffmanTree> priorityQueue)
        {
            int loop = priorityQueue.Count;
            for (int i = 0; i < loop - 1; i++)
            {
                HuffmanTree t1 = priorityQueue.PopTop(), t2 = priorityQueue.PopTop();
                HuffmanTree newInner = new HuffmanTree('\0', t1.Value + t2.Value);

                newInner.Left = t1; t1.Parent = newInner;
                newInner.Right = t2; t2.Parent = newInner;

                priorityQueue.Insert(newInner);
            }

            return priorityQueue.PopTop();
        }
    }
}
