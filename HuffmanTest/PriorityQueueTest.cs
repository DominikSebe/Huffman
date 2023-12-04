using System;
using System.Linq;
using Huffman;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuffmanTest
{
    [TestClass]
    public class PriorityQueueTest
    {
        /// <summary>
        /// Test of creation of an ascending ordered PirorityQueue object, passing the elements to the constructor.
        /// Checks the the pushing, ordering and poping of elements, and the properties of the object in multiple states.
        /// </summary>
        [TestMethod]
        public void MinimumHeapTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 43, 25, 37, 100, 19 });

            Assert.AreEqual(priorityQueue.Size, 5);
            Assert.AreEqual(priorityQueue.Depth, 3);
            Assert.AreEqual(priorityQueue.Ascending, true);

            Assert.AreEqual(priorityQueue.Pop(), 19);
            Assert.AreEqual(priorityQueue.Pop(), 25);
            Assert.AreEqual(priorityQueue.Pop(), 37);
            Assert.AreEqual(priorityQueue.Pop(), 43);
            Assert.AreEqual(priorityQueue.Pop(), 100);

            Assert.AreEqual(priorityQueue.Size, 0);
            Assert.AreEqual(priorityQueue.Depth, 0);
            Assert.AreEqual(priorityQueue.Ascending, true);
        }
        /// <summary>
        /// Test the creation of a descending ordered PriorityQUeue object, populateing it with elements manually.
        /// Checks the the pushing, ordering and poping of elements, and the properties of the object in multiple states.
        /// </summary>
        [TestMethod]
        public void MaximumHeapTest()
        {
            PriorityQueue<float> priorityQueue = new PriorityQueue<float>(false);

            Assert.AreEqual(priorityQueue.Size, 0);
            Assert.AreEqual(priorityQueue.Depth, 0);
            Assert.AreEqual(priorityQueue.Ascending, false);

            priorityQueue.Push((float)12.4);
            priorityQueue.Push((float)16.0);
            priorityQueue.Push((float)9.72);
            priorityQueue.Push((float)31.19);
            priorityQueue.Push((float)40.2);
            priorityQueue.Push((float)17.6);
            priorityQueue.Push((float)8.9);
            priorityQueue.Push((float)10.11);
            priorityQueue.Push((float)25.66);

            Assert.AreEqual(priorityQueue.Size, 9);
            Assert.AreEqual(priorityQueue.Depth, 4);
            Assert.AreEqual(priorityQueue.Ascending, false);

            Assert.AreEqual(priorityQueue.Pop(), (float)40.2);
            Assert.AreEqual(priorityQueue.Pop(), (float)31.19);
            Assert.AreEqual(priorityQueue.Pop(), (float)25.66);
            Assert.AreEqual(priorityQueue.Pop(), (float)17.6);
            Assert.AreEqual(priorityQueue.Pop(), (float)16.0);
            Assert.AreEqual(priorityQueue.Pop(), (float)12.4);
            Assert.AreEqual(priorityQueue.Pop(), (float)10.11);
            Assert.AreEqual(priorityQueue.Pop(), (float)9.72);
            Assert.AreEqual(priorityQueue.Pop() , (float)8.9);

            Assert.AreEqual(priorityQueue.Size, 0);
            Assert.AreEqual(priorityQueue.Depth, 0);
            Assert.AreEqual(priorityQueue.Ascending, false);
        }
        /// <summary>
        /// Test the the Push method throws exception when trying to push a duplicate value.
        /// </summary>
        [TestMethod]
        public void InvalidPushTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new PriorityQueue<int>(new int[2] { 2, 2 }));

            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 });

            Assert.ThrowsException<ArgumentException>(() => priorityQueue.Push(10));
        }
        /// <summary>
        /// Test the Top fucntion.
        /// </summary>
        [TestMethod]
        public void TopTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 });
            Assert.AreEqual(priorityQueue.Top(), 6);

            priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 }, false);
            Assert.AreEqual(priorityQueue.Top(), 92);

            priorityQueue = new PriorityQueue<int>();
            Assert.AreEqual(priorityQueue.Top(), 0);
        }
        /// <summary>
        /// Test the Pop function
        /// </summary>
        [TestMethod]
        public void PopTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 12, 5, 34, 92, 87 });
            Assert.AreEqual(priorityQueue.Pop(), 5);

            priorityQueue = new PriorityQueue<int>(new int[5] { 12, 5, 34, 92, 87 }, false);
            Assert.AreEqual(priorityQueue.Pop(), 92);

            priorityQueue = new PriorityQueue<int>();
            Assert.AreEqual(priorityQueue.Pop(), 0);
        }
        /// <summary>
        /// Test the incrementing of an element and the expcetions of the function.
        /// </summary>
        [TestMethod]
        public void IncrementTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 });

            priorityQueue.Increment(10, 15);
            priorityQueue.Increment(92, 92);


            Assert.ThrowsException<ArgumentException>(() => priorityQueue.Increment(8, 7));
            Assert.ThrowsException<ArgumentException>(() => priorityQueue.Increment(8, 15));
            Assert.ThrowsException<ArgumentException>(() => priorityQueue.Increment(2, 3));
        }
        /// <summary>
        /// Test the traverse method with no result throws no exception.
        /// </summary>
        [TestMethod]
        public void TraverseTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 });

            try
            {
                priorityQueue.Traverse(e => Console.WriteLine(e));
            }
            catch (Exception e) { Assert.Fail(e.Message); }
        }
        /// <summary>
        /// Test the traverse method result.
        /// </summary>
        [TestMethod]
        public void TraverseResultTest()
        {
            int[] t = new int[5] { 10, 17, 8, 92, 6 };
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(t, false);

            int[] result = priorityQueue.Traverse(e => e);

            foreach (int i in t)
                Assert.IsTrue(result.Contains(i));
        }
        /// <summary>
        /// Test the traverse method with no result throws no exception.
        /// </summary>
        [TestMethod]
        public void BreadthFirstTraverseTest()
        {
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(new int[5] { 10, 17, 8, 92, 6 });

            try {
                priorityQueue.BreadthFirstTraverse(e => Console.WriteLine(e));
            }
            catch (Exception e) { Assert.Fail(e.Message); }

        }
        /// <summary>
        /// Test the traverse method result.
        /// </summary>
        [TestMethod]
        public void BredthFirstTraverseResultTest()
        {
            int[] t = new int[5] { 10, 17, 8, 92, 6 };
            PriorityQueue<int> priorityQueue = new PriorityQueue<int>(t, false);

            int[] result = priorityQueue.BreadthFirstTraverse(e => e);

            foreach (int i in t)
                Assert.IsTrue(result.Contains(i));
        }
    }
}
