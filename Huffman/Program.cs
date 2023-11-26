using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Program
    {
        struct Occurance : IComparable
        {
            private char key;
            private int value;

            public char Key
            {
                get {  return key; }
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
        static void Main(string[] args)
        {
            string test = "This is a string for tesing the Huffman algorythm.";

            Dictionary<char, int> freq = new Dictionary<char, int>();
            for (int i = 0; i < test.Length; i++)
            {
                if (freq.ContainsKey(test[i])) freq[test[i]]++;
                else freq.Add(test[i], 1);
            }

            PriorityQueue<Occurance> priorityQueue = new PriorityQueue<Occurance>(freq.Select(item => new Occurance(item.Key, item.Value)).ToArray(), false);
            while (priorityQueue.Count > 0)
            {
                Console.WriteLine(priorityQueue.PopTop());
            }

            Console.ReadKey();
        }
    }
}
