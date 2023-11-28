using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class HuffmanTree : IComparable
    {
        public delegate bool Search(HuffmanTree item);
        public delegate void Process(HuffmanTree item);

        private HuffmanTree parent, left, right;
        private char key;
        private int value;

        public char Key
        {
            get { return this.key; }
        }
        public int Value
        {
            get { return this.value; }
        }
        public HuffmanTree Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        public HuffmanTree Left
        {
            get { return this.left; }
            set { this.left = value; }
        }
        public HuffmanTree Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        public HuffmanTree(char key, int value)
        {
            this.key = key;
            this.value = value;
        }

        public static HuffmanTree Find(HuffmanTree root, Search func)
        {
            HuffmanTree left = null, right = null;
            if (root == null || func.Invoke(root)) return root;
            else
            {
                if (root.Left != null)
                {
                    left = Find(root.Left, func);
                }
                if (root.Right != null)
                {
                    right = Find(root.Right, func);
                }
            }
            if (left != null) return left;
            return right;

        }
        public static HuffmanTree FindCode(HuffmanTree root, VariedLengthBinary code)
        {
            if (root == null || root.Key != '\0') return root;
            else
            {
                if (code[code.BitLength - 1])
                    return FindCode(root.Right, code & ~(new VariedLengthBinary(1, 0) << (code.BitLength - 1)));
                else
                    return FindCode(root.Left, code & ~(new VariedLengthBinary(1, 0) << (code.BitLength - 1)));
            }
        }
        public static void Map(HuffmanTree tree, Process func)
        {
            if (tree != null)
            {
                if (tree.Left != null) Map(tree.Left, func);
                func.Invoke(tree);
                if (tree.Right != null) Map(tree.Right, func);
            }
        }

        public int CompareTo(object obj)
        {
            if (this.value < (obj as HuffmanTree).Value) return -1;
            else if (this.value == (obj as HuffmanTree).Value) return 0;
            else return 1;
        }
        public override string ToString()
        {
            return String.Format("{{\"{0}\":\"{1}\"}}", this.key, this.value);
        }
    }
}
