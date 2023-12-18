using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman
{    internal class Program
    {
        static void Main(string[] args)
        {
            // Read Input
            StreamReader sr = new StreamReader("..\\..\\Input\\Huffman.txt");
            
            string input_string = sr.ReadToEnd();
            sr.Close();

            // Discover character frequencies
            Dictionary<char, int> freq = new Dictionary<char, int>();
            for (int i = 0; i < input_string.Length; i++)
            {
                if (freq.ContainsKey(input_string[i])) freq[input_string[i]]++;
                else freq.Add(input_string[i], 1);
            }

            // Create priority queue based on character frequencies
            PriorityQueue<HuffmanTree> priorityQueue = new PriorityQueue<HuffmanTree>(freq.Select(item => new HuffmanTree(item.Key, item.Value)).ToArray(), false);

            // Connect Tree elements (build Tree from bottom-up)
            int loop = priorityQueue.Size;
            for (int i = 0; i < loop - 1; i++)
            {

                // Pop in order: right(greater), left(lesser), and create a new element from the sum of the two
                HuffmanTree right = priorityQueue.Pop(), left = priorityQueue.Pop(), center = new HuffmanTree('\0', right.Value + left.Value);

                // Connect the elements
                center.Right = right; right.Parent = center;
                center.Left = left; left.Parent = center;

                // Push the center back to the priority queue
                priorityQueue.Push(center);
            }

            // Pop the last (root) element from the priorityQueue
            HuffmanTree huffmanTree = priorityQueue.Pop();

            // Create list for storing individual codes.
            List<VariedLengthBinary> codes = new List<VariedLengthBinary>();

            // Loop through each character in the string to be encoded.
            foreach (char key in input_string)
            {
                // Find the character in the tree
                HuffmanTree item = HuffmanTree.Find(huffmanTree, t => t.Key == key);
                VariedLengthBinary b_code = 0; // initial code
                int counter = 0; // initial counter

                // The path to the leaf in tree is its code (going from the left to the root, in reverse)
                while (item.Parent != null) // Loop until root, root has no parent
                {
                    if (item.Parent.Left == item) b_code |= (0b1 << counter); // Binary tree: 1(left), right(0)
                    else b_code |= new VariedLengthBinary(counter + 1);
                    item = item.Parent;
                    counter++;
                }

                // Add the path(code) to the list
                codes.Add(b_code);
            }

            // In itial code of the whole string
            VariedLengthBinary totalCode = 0;

            // OR operator to save bits from the first element
            totalCode |= codes.ElementAt(0);

            // Loop through all other elements
            for (int i = 1; i < codes.Count(); i++)
            {
                // Get the code of the character
                VariedLengthBinary code = codes.ElementAt(i);
                totalCode <<= code.BitLength; // Push the bits left by the length of the code (so the OR operator does not combine two codes)
                totalCode |= code; // Save the code of the character into the total
            }

            // Print out each code
            foreach (VariedLengthBinary code in codes)
                Console.Write("{0} ", code);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(new String('-', 20));
            // Print out the total code
            Console.WriteLine(totalCode.ToString());

            // Save encoded string
            StreamWriter sw = new StreamWriter("..\\..\\Output\\huffman_encoded.txt");
            sw.Write(totalCode.ToString());
            sw.Close();

            // Inital decoded string
            string decoded = "";

            // Start decoding codes and removing them from the total code
            bool end = false;
            while (!end)
            {
                // Start from the root
                HuffmanTree leaf = huffmanTree;
                int counter = 0;
                while (leaf.Key == '\0') // '\0' is an empty character given to non-leaf tree-elements
                {
                    // Check which way to procedd in the tree (right - 1, left - 0)
                    if (totalCode[totalCode.BitLength - 1 - counter]) leaf = leaf.Left;
                    else leaf = leaf.Right;
                    counter++;
                }
                decoded += leaf.Key; // Add the key of the left elements to the decoded string
                if (totalCode.BitLength - counter > 0)
                    // Run AND operator on the total code and a new VLB, that is shorter by the length of the found code and all of its bits are 1
                    totalCode &= ~new VariedLengthBinary(totalCode.BitLength - counter); // This will keep only the non-found bits in the total code and shorten its bit-length
                else end = true;

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(new String('-', 20));
            // Print out decoded string
            Console.WriteLine(decoded);

            // Save decoded string
            sw = new StreamWriter("..\\..\\Output\\huffman_decoded.txt");
            sw.Write(decoded);
            sw.Close();

            Console.ReadKey();
        }
    }
}
