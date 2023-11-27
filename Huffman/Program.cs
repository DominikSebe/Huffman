using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    struct Occurance : IComparable
    {
        private char key;
        private int value;

        public char Key
        {
            get { return key; }
            set { this.key = value; }
        }
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Occurance(char key, int value)
        {
            this.key = key;
            this.value = value;
        }

        int IComparable.CompareTo(object obj)
        {
            Occurance other = (Occurance)obj;
            if (this.value < other.Value) return -1;
            else if (this.value == other.Value) return 0;
            else return 1;
        }

        public override string ToString()
        {
            return String.Format("{{\"{0}\":\"{1}\"}}", this.key, this.value);
        }
    }

    internal class Program
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
                List<int> bins = new List<int>();
                while(item.Parent != null)
                {
                    if (item.Parent.Right == item) bins.Add(1);
                    else bins.Add(0);
                    item = item.Parent;
                }

                bins.Reverse();

                int i = 0;
                VariedLengthBinary code = 0;
                foreach (int bin in bins)
                {
                    if (bin == 1) code = code | new VariedLengthBinary(0b1 << i);
                    i++;
                }
                codes.Add(code);
            }

            foreach (VariedLengthBinary code in codes)
            {
                Console.WriteLine(code.ToString());
            }


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
