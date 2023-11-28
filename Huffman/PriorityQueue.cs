using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    /// <summary>
    /// Represents 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class PriorityQueue<T>
    {
        #region Delegates
        public delegate U Process<U>(T item);
        public delegate void Process(T item);
        #endregion

        #region Members
        private T[] _items;
        private bool ascending;
        #endregion

        #region Constructors
        public PriorityQueue(bool ascending = true)
        {
            this._items = new T[0];
            this.ascending = ascending;
        }
        public PriorityQueue(T[] items, bool ascending = true) : this(ascending)
        {
            foreach (T item in items) Insert(item);
        }

        #endregion

        #region Properties
        public T[] Items
        {
            get { return this._items.Clone() as T[]; }
        }
        public int Count
        {
            get { return this._items.Length; }
        }
        public int Depth
        {
            get { return (int)Math.Log(this._items.Length, 2); }
        }
        #endregion

        #region Methods
        #region Static

        #endregion

        #region Non-Static
        private void Reorder(int i = 0)
        {
            int left = Left(i), right = Right(i);

            if (left < this._items.Length)
                Reorder(left);
            if (right < this._items.Length)
                Reorder(right);

            T tmp = this._items[i];

            if (left < this._items.Length && right < this._items.Length)
            {
                int prior;
                if ((this.ascending ? Comparer<T>.Default.Compare(this._items[right], this._items[left]) : Comparer<T>.Default.Compare(this._items[left], this._items[right])) >= 0)
                    prior = left;
                else
                    prior = right;

                if ((this.ascending ? Comparer<T>.Default.Compare(this._items[i], this._items[prior]) : Comparer<T>.Default.Compare(this._items[prior], this._items[i])) >= 0)
                {
                    this._items[i] = this._items[prior];
                    this._items[prior] = tmp;
                }
            }
            else if (left < this._items.Length)
            {
                if ((this.ascending ? Comparer<T>.Default.Compare(this._items[i], this._items[left]) : Comparer<T>.Default.Compare(this._items[left], this._items[i])) >= 0)
                {
                    this._items[i] = this._items[left];
                    this._items[left] = tmp;
                }
            }
            else if (right < this._items.Length)
            {
                if ((this.ascending ? Comparer<T>.Default.Compare(this._items[i], this._items[right]) : Comparer<T>.Default.Compare(this._items[right], this._items[i])) >= 0)
                {
                    this._items[i] = this._items[right];
                    this._items[right] = tmp;
                }
            }
        }
        public void Insert(T item)
        {
            Array.Resize(ref this._items, this._items.Length + 1);
            this._items[this._items.Length - 1] = item;
            this.Reorder();
        }
        public void Increase(T item, T key)
        {
            int i = 0;
            while (i < this._items.Length && Comparer<T>.Default.Compare(this._items[i], item) != 0) i++;

            if (i < this._items.Length && (this.ascending ? Comparer<T>.Default.Compare(key, item) : Comparer<T>.Default.Compare(item, key)) >= 0)
            {
                this._items[i] = key;
                this.Reorder();
            }

        }
        public void Map(Process func, int startIndex = 0)
        {
            int left = Left(startIndex);
            int right = Right(startIndex);

            func.Invoke(this._items[startIndex]);

            if (left < this._items.Length)
                Map(func, left);

            if (right < this._items.Length)
                Map(func, right);
        }
        #endregion
        #endregion

        #region Functions
        #region Static
        private static int Parent(int i) => (i % 2) > 0 ? (i - 1) / 2 : (i - 2) / 2;
        private static int Left(int i) => i * 2 + 1;
        private static int Right(int i) => i * 2 + 2;
        #endregion

        #region Non-Static
        public T Top()
        {
            return this._items[0];
        }
        public T PopTop()
        {
            T top = this._items[0];

            if (this._items.Length > 1)
                this._items[0] = this._items[this._items.Length - 1];

            Array.Resize(ref this._items, this._items.Length - 1);

            if (this._items.Length > 1)
                this.Reorder();

            return top;
        }
        public U[] Map<U>(Process<U> func, int startIndex = 0)
        {
            U[] leftArray = new U[0];
            U[] middleArray = new U[1];
            U[] rightArray = new U[0];
            int left = Left(startIndex);
            int right = Right(startIndex);

            middleArray[0] = func.Invoke(this._items[startIndex]);

            if (left < this.Items.Length)
                leftArray = Map<U>(func, left);

            if (right < this.Items.Length)
                rightArray = Map<U>(func, right);

            return middleArray.Concat(leftArray).Concat(rightArray).ToArray();
        }
        #endregion
        #endregion
    }
}
