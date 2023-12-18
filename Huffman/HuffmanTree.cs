using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    /// <summary>
    /// Represents implementation of a binary tree element, that stores a character and an integer value.
    /// Implements Icomparable and stores an extra pointer to its parent.
    /// </summary>
    internal class HuffmanTree : IComparable
    {
        #region Members
        private HuffmanTree parent, left, right;
        private char key;
        private int value;
        #endregion

        #region Properties
        /// <summary>
        /// Get the character value stored.
        /// </summary>
        public char Key
        {
            get { return this.key; }
        }
        /// <summary>
        /// Get the integer value stored.
        /// </summary>
        public int Value
        {
            get { return this.value; }
        }
        /// <summary>
        /// Get or set the pointer to the parent element.
        /// </summary>
        public HuffmanTree Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }
        /// <summary>
        /// Get or set the pointer to the element of the left brench.
        /// </summary>
        public HuffmanTree Left
        {
            get { return this.left; }
            set { this.left = value; }
        }
        /// <summary>
        /// Get or set the pointer to the element of the right brench.
        /// </summary>
        public HuffmanTree Right
        {
            get { return this.right; }
            set { this.right = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializea new HuffmanTree object.
        /// </summary>
        /// <param name="key">Character value to store.</param>
        /// <param name="value">Integer value to store.</param>
        public HuffmanTree(char key, int value)
        {
            this.key = key;
            this.value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Find an element that returns true for a passed function.
        /// </summary>
        /// <param name="root">The element to start the search from.</param>
        /// <param name="func">The function to decide if the element is found or not.</param>
        /// <returns>The element that returns true for the passed function or null if there is no such element.</returns>
        public static HuffmanTree Find(HuffmanTree root, Func<HuffmanTree, bool> func)
        {
            HuffmanTree left = null, right = null;
            if (root == null || func(root)) return root;
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
        /// <summary>
        /// Find an element by specifying which branch to search for each step.
        /// </summary>
        /// <param name="root">The element to start the search from.</param>
        /// <param name="code">A VariedLengthBinary object to specify which branch to take at each step.</param>
        /// <returns>The leaf element at the end of the steps.</returns>
        public static HuffmanTree FindCode(HuffmanTree root, VariedLengthBinary code)
        {
            if (root == null || root.Key != '\0') return root;
            else
            {
                if (code[code.BitLength - 1])
                    return FindCode(root.Right, code & ~((VariedLengthBinary)1) << (code.BitLength - 1));
                else
                    return FindCode(root.Left, code & ~((VariedLengthBinary)1) << (code.BitLength - 1));
            }
        }
        /// <summary>
        /// Invoke a function for each element in the tree in Depth-first LNR order.
        /// </summary>
        /// <param name="tree">The element to start traversing from.</param>
        /// <param name="func">The function to invoke for each elements</param>
        public static void Traverse(HuffmanTree tree, Action<HuffmanTree> func)
        {
            if (tree != null)
            {
                if (tree.Left != null) Traverse(tree.Left, func);
                func(tree);
                if (tree.Right != null) Traverse(tree.Right, func);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Implementation of the CompareTo function. Greater is the HuffmanTree object, whose stored integer value is greater.
        /// </summary>
        /// <param name="obj">The HuffmanTree object to compare to.</param>
        /// <exception cref="NullReferenceException">Thrown if the object can not be converted to a Huffmantree object.</exception>
        /// <returns>Integer indicating if this or the other object is greater</returns>
        public int CompareTo(object obj)
        {
            if (this.value < (obj as HuffmanTree).Value) return -1;
            else if (this.value == (obj as HuffmanTree).Value) return 0;
            else return 1;
        }
        /// <summary>
        /// Return a string representing the HuffmanTree object.
        /// </summary>
        /// <returns>A string representing the HuffmanTree object.</returns>
        public override string ToString()
        {
            return String.Format("{{\"{0}\":\"{1}\"}}", this.key, this.value);
        }
        #endregion
    }
}
