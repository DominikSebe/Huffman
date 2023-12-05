using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    /// <summary>
    /// Represents a generic max or minimum heap implementation of priority queue.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the queue.</typeparam>
    public class PriorityQueue<T> where T: IComparable
    {
        #region Members
        private T[] _items;
        private bool _ascending;
        private readonly Func<T, T, int> _comparer;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new empty PriorityQueue object.
        /// </summary>
        /// <param name="ascending">Whether elements should be stored lowest or highest first (max or minimum heap).</param>
        public PriorityQueue(bool ascending = true)
        {
            this._items = new T[0]; // Initialize array of elements
            this._ascending = ascending; // Store order

            // Depending on the order store the default comparer or the T type
            if (ascending) 
                this._comparer = Comparer<T>.Default.Compare; // Returns value based on whether the first T value passed is greater
            else
                this._comparer = (T x, T y) => Comparer<T>.Default.Compare(y, x); // Reversed, so returns value based on whether the first T value passed is lesser
        }
        /// <summary>
        /// Initializes a new PriorityQueue object with elements.
        /// </summary>
        /// <param name="items">The elements to push into the queue once it has been created.</param>
        /// <param name="ascending">Whether elements should be stored lowest or highest first (max or minimum heap).</param>
        public PriorityQueue(T[] items, bool ascending = true) : this(ascending) // Invoke default Constructor first
        {
            // Push all the elements into the stack.
            foreach (T item in items) Push(item);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of elemens stored in the queue.
        /// </summary>
        public int Size
        {
            get { return this._items.Length; }
        }
        /// <summary>
        /// Gets the depth of the queue.
        /// </summary>
        public int Depth
        {
            get { return this._items.Length > 1 ? (int)Math.Ceiling(Math.Log(this._items.Length, 2)): this._items.Length; }
        }
        /// <summary>
        /// Gets whether there any elemens inside the queue or not.
        /// </summary>
        public bool Empty
        {
            get { return this._items.Length == 0; }
        }
        /// <summary>
        /// Gets if the elements are stored in ascending order or not.
        /// </summary>
        public bool Ascending
        {
            get { return this._ascending; }
        }
        #endregion

        #region Methods
        #region Private
        /// <summary>
        /// Orders the elements of the queue starting from the specified element.
        /// </summary>
        /// <param name="index">The index of the element, where to start the ordering.</param>
        private void Reorder(int index = 0)
        {
            // Get the possible indecies of the two branches of the node
            int left = Left(index), right = Right(index);

            // Recursively put the brances into order if they exist
            if (left < this._items.Length)
                Reorder(left);
            if (right < this._items.Length)
                Reorder(right);

            // If both brances are valid
            if (left < this._items.Length && right < this._items.Length) 
            {
                // Find the one with higher prority
                int prior;
                if (this._comparer(this._items[right], this._items[left]) >= 0)
                    prior = left;
                else
                    prior = right;

                // If the priority of that branch is higher the the nodes, swap them
                this.Order(prior);
            }
            // If only the left branch is valid
            else if (left < this._items.Length)
            {
                // If the priority of the left branch is higher the the nodes, swap them
                this.Order(left);
            }
            // If only the right branch is valid
            else if (right < this._items.Length)
            {
                // If the priority of the right branch is higher the the nodes, swap them
                this.Order(right);
            }
        }
        /// <summary>
        /// Go through and pass each stored element to the passed method depth-first in pre-order.
        /// </summary>
        /// <param name="method">Method to call with each element.</param>
        /// <param name="startIndex">Index of the element, where to start the search.</param>
        private void Traverse(Action<T> method, int startIndex = 0)
        {
            // Get the possible indecies of the two branches of the node
            int left = Left(startIndex);
            int right = Right(startIndex);

            // Invoke the method for this node.
            method(this._items[startIndex]);

            // Recursively search through the brances if they are valid and invoke the method for the nodes in them as well.
            if (left < this._items.Length)
                Traverse(method, left);
            if (right < this._items.Length)
                Traverse(method, right);
        }
        /// <summary>
        /// Move a single element forward in the queue, while its priority is greater than the priority of the element preceding it.
        /// </summary>
        /// <param name="index">Index of the element to start ordering.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index of the parent index are out of range of the array of elements.</exception>
        private void Order(int index)
        {
            // Get the index of the preceeding elements
            int parent = Parent(index);

            // If the the priority of the this node is higher, swap the two elements
            if (parent >= 0 && this._comparer(this._items[parent],this._items[index]) > 0)
            {
                T tmp = this._items[index];
                this._items[index] = this._items[parent];
                this._items[parent] = tmp;

                // And recursively call the method for the now replaced element
                if (parent > 0 ) Order(parent);
            }
        }
        /// <summary>
        /// Modify the number of elements stored in the queue.
        /// </summary>
        /// <param name="size">The new number of elements.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the new size is less than zero.</exception>
        private void Resize(int size)
        {
            // If the size is less than zero, throw and Exception
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Cannot create an array with a negative size.");
            // Action is only required, when the new size differs from the current
            if (size != this._items.Length)
            {
                // Create a new array to store elements in
                T[] new_items = new T[size];
                // Get the lesser of the two sizes
                int lesser = size < this._items.Length? size : this._items.Length;
                int i;

                // Copy the elements until the lesser size
                // If the new size was the lesser, the remaining elements are removed.
                // If the old size was the lesser, the remaining indecies are filled with default values.
                for (i = 0; i < lesser; i++)
                {
                    new_items[i] = this._items[i];
                }

                this._items = new_items;
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Add and a unique element to the queue.
        /// </summary>
        /// <param name="item">The element to be added.</param> 
        /// <exception cref="ArgumentException">Thrown when the item is equal to an already stored element of the queue.</exception>
        public void Push(T item)
        {
            if (this._items.Contains(item)) throw new ArgumentException("Element is already contained within queue.", nameof(item));

            this.Resize(this._items.Length + 1); // Increase the array of elements
            this._items[this._items.Length - 1] = item; // Set last index to the element
            this.Order(this._items.Length - 1); // Order the element
        }
        /// <summary>
        /// Replaces a element with another of equal or greater.
        /// </summary>
        /// <param name="original">The element to be replaced. </param>
        /// <param name="replacement">The new element.</param>
        /// <exception cref="ArgumentException">Thrown when:
        /// - The new item is lower priority than the original.
        /// - The new item is not equal to the the original, but another already stoed element of the queue.
        /// - The element to be incremented is not part of the queue.</exception>
        public void Increment(T original, T replacement)
        {
            if (this._comparer(replacement, original) < 0) throw new ArgumentException("The replacement must equal or greater", nameof(replacement));

            int i = 0;

            if (this._comparer(original, replacement) != 0)
            {
                while (i < this._items.Length && this._comparer(this._items[i], replacement) != 0) i++;
                if (i < this._items.Length) throw new ArgumentException("Elements ia alredy contained within queue.", nameof(replacement));
            }

            i = 0;
            while (i < this._items.Length && this._comparer(this._items[i], original) != 0) i++;

            if (i >= this._items.Length) throw new ArgumentException("The element is not part of the queue", nameof(original));

            this._items[i] = replacement;
            this.Order(this._items.Length - 1);
        }
        /// <summary>
        /// Go through and pass each stored element to the passed method depth-first in pre-order.
        /// </summary>
        /// <param name="method">Method call with each element.</param>
        public void Traverse(Action<T> method)
        {
            int left = Left(0);
            int right = Right(0);

            method(this._items[0]);

            if (left < this._items.Length)
                Traverse(method, left);

            if (right < this._items.Length)
                Traverse(method, right);
        }
        /// <summary>
        /// Go through and pass each stored element to the passed method breadth-first.
        /// </summary>
        /// <param name="method">The method to call with each element.</param>
        public void BreadthFirstTraverse(Action<T> method)
        {
            foreach (T item in this._items) 
                method(item);
        }
        #endregion
        #endregion

        #region Functions
        #region Static
        /// <summary>
        /// Calculate the parent index of another.
        /// </summary>
        /// <param name="i">The index to calculate tha parent of.</param>
        /// <returns>The parent index.</returns>
        private static int Parent(int i) => (i % 2) > 0 ? (i - 1) / 2 : i / 2 - 1;
        /// <summary>
        /// Calculate the left node index of another.
        /// </summary>
        /// <param name="i">The index to calculate left node of.</param>
        /// <returns>The left node index.</returns>
        private static int Left(int i) => i * 2 + 1;
        /// <summary>
        /// Calculate the right node index of another.
        /// </summary>
        /// <param name="i">The index to calculate right node of.</param>
        /// <returns>The right node index.</returns>
        private static int Right(int i) => i * 2 + 2;
        #endregion

        #region Non-Static
        #region Private
        /// <summary>
        /// Go through and pass each stored element depth-first in pre-order and return the results.
        /// </summary>
        /// <typeparam name="U">The return type of the function passed.</typeparam>
        /// <param name="function">The function to call with each element.</param>
        /// <param name="startIndex">The index of the element to start from.</param>
        /// <returns>An array of results.</returns>
        private U[] Traverse<U>(Func<T, U> function, int startIndex = 0)
        {
            int left = Left(startIndex);
            int right = Right(startIndex);

            U[] result;

            result = new U[1] { function(this._items[startIndex]) };

            if (left < this._items.Length)
            {
                U[] leftResult = Traverse<U>(function, left);
                U[] new_result = new U[result.Length + leftResult.Length];

                int i;
                for (i = 0; i < result.Length; i++)
                    new_result[i] = result[i];

                for (; i < new_result.Length; i++)
                    new_result[i] = leftResult[i - result.Length];

                result = new_result;
            }

            if (right < this._items.Length)
            {
                U[] rightResult = Traverse<U>(function, right);
                U[] new_result = new U[result.Length + rightResult.Length];

                int i;
                for (i = 0; i < result.Length; i++)
                    new_result[i] = result[i];

                for (; i < new_result.Length; i++)
                    new_result[i] = rightResult[i - result.Length];

                result = new_result;
            }

            return result;
        }
        #endregion

        #region Public
        /// <summary>
        /// Get the element of highest priority.
        /// </summary>
        /// <returns>The top element in the queue.</returns>
        public T Top()
        {
            if (this.Size > 0)
                return this._items[0];
            return default(T);
        }
        /// <summary>
        /// Get the element of highest priority and remove it from the queue.
        /// </summary>
        /// <returns>The top element in the queue.</returns>
        public T Pop()
        {
            if (this.Size > 0)
            {
                T top = this._items[0];

                if (this._items.Length > 1)
                    this._items[0] = this._items[this._items.Length - 1];

                this.Resize(this._items.Length - 1);

                if (this._items.Length > 1)
                    this.Reorder();


                return top;
            }
            return default(T);
        }
        /// <summary>
        /// Go through and pass each stored element depth-first in pre-order and return the results.
        /// </summary>
        /// <typeparam name="U">The return type of the function passed.</typeparam>
        /// <param name="function"></param>
        /// <returns>An array of results.</returns>
        public U[] Traverse<U>(Func<T, U> function)
        {
            int left = Left(0);
            int right = Right(0);

            U[] result;

            result = new U[1]{ function(this._items[0])};

            if (left < this._items.Length)
            {
                U[] leftResult = Traverse<U>(function, left);
                U[] new_result = new U[result.Length + leftResult.Length];

                int i;
                for (i = 0; i < result.Length; i++)
                    new_result[i] = result[i];

                for (; i < new_result.Length; i++)
                    new_result[i] = leftResult[i - result.Length];

                result = new_result;
            }

            if (right < this._items.Length)
            {
                U[] rightResult = Traverse<U>(function, right);
                U[] new_result = new U[result.Length + rightResult.Length];

                int i;
                for (i = 0; i < result.Length; i++)
                    new_result[i] = result[i];

                for (; i < new_result.Length; i++)
                    new_result[i] = rightResult[i - result.Length];

                result = new_result;
            }

            return result;
        }
        /// <summary>
        /// Go through and pass each stored element breadth-first and return the results.
        /// </summary>
        /// <typeparam name="U">The return type of the function passed.</typeparam>
        /// <param name="function"></param>
        /// <returns>An array of results.</returns>
        public U[] BreadthFirstTraverse<U>(Func<T, U> function)
        {
            U[] result = new U[this._items.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = function(this._items[i]);

            return result;
        }
        #endregion
        #endregion
        #endregion
    }
}
