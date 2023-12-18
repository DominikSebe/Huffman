using System;
using System.Linq;
using Huffman;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuffmanTest
{
    [TestClass]
    public class VariedLengthBinaryTest
    {
        /// <summary>
        /// Test the initialization of a new VariedLengthBinary object, by specifying its length.
        /// </summary>
        [TestMethod]
        public void LengthConstructorTest()
        {
            VariedLengthBinary vlb = new VariedLengthBinary(8);
            Assert.AreEqual(vlb.BitLength, 8);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.Bytes[0], 0);
            Assert.AreEqual(vlb.MostSignificantBit, 0);
        }
        /// <summary>
        /// Test that the constructor with the bit-length as the paramateter trows the exception when necesarry.
        /// </summary>
        [TestMethod]
        public void LegnthConstructorExceptionTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=>new VariedLengthBinary(0));
        }
        /// <summary>
        /// Test the initialization ofa  new VariedLengthBinary object, by specifying an array of bytes.
        /// </summary>
        [TestMethod]
        public void BytesConstructorTest()
        {

            VariedLengthBinary vlb = new VariedLengthBinary(new byte[2] { 0b10010011, 0b11010000 });
            Assert.AreEqual(vlb.BitLength, 16);
            Assert.AreEqual(vlb.Bytes.Length, 2);
            Assert.AreEqual(vlb.MostSignificantBit, 16);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], true);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], false);
            Assert.AreEqual(vlb[4], true);
            Assert.AreEqual(vlb[5], false);
            Assert.AreEqual(vlb[6], false);
            Assert.AreEqual(vlb[7], true);
            Assert.AreEqual(vlb[8], false);
            Assert.AreEqual(vlb[9], false);
            Assert.AreEqual(vlb[10], false);
            Assert.AreEqual(vlb[11], false);
            Assert.AreEqual(vlb[12], true);
            Assert.AreEqual(vlb[13], false);
            Assert.AreEqual(vlb[14], true);
            Assert.AreEqual(vlb[15], true);
            Assert.AreEqual((int)vlb, 53395);
        }
        /// <summary>
        /// Test that the indexer throws exception when necessary.
        /// </summary>
        [TestMethod]
        public void IndexerExceptionTest()
        {
            VariedLengthBinary vlb = new VariedLengthBinary(new byte[2] { 0b10010011, 0b11010000 });
            Assert.ThrowsException<IndexOutOfRangeException>(() => vlb[-1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => vlb[16]);
        }
        /// <summary>
        /// Test the implicit conversion of an integer value to an VariedLengthBinary object.
        /// </summary>
        [TestMethod]
        public void IntegerToVLBTest()
        {
            VariedLengthBinary vlb = 0;
            Assert.AreEqual(vlb.BitLength, 1);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.MostSignificantBit, 0);
            Assert.AreEqual(vlb[0], false);
            vlb = 10;
            Assert.AreEqual(vlb.BitLength, 4);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.MostSignificantBit, 4);
            Assert.AreEqual(vlb[0], false);
            Assert.AreEqual(vlb[1], true);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], true);
            vlb = 257;
            Assert.AreEqual(vlb.BitLength, 9);
            Assert.AreEqual(vlb.Bytes.Length, 2);
            Assert.AreEqual(vlb.MostSignificantBit, 9);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], false);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], false);
            Assert.AreEqual(vlb[4], false);
            Assert.AreEqual(vlb[5], false);
            Assert.AreEqual(vlb[6], false);
            Assert.AreEqual(vlb[7], false);
            Assert.AreEqual(vlb[8], true);
            vlb = 1924;
            Assert.AreEqual(vlb.BitLength, 11);
            Assert.AreEqual(vlb.Bytes.Length, 2);
            Assert.AreEqual(vlb.MostSignificantBit, 11);
            Assert.AreEqual(vlb[0], false);
            Assert.AreEqual(vlb[1], false);
            Assert.AreEqual(vlb[2], true);
            Assert.AreEqual(vlb[3], false);
            Assert.AreEqual(vlb[4], false);
            Assert.AreEqual(vlb[5], false);
            Assert.AreEqual(vlb[6], false);
            Assert.AreEqual(vlb[7], true);
            Assert.AreEqual(vlb[8], true);
            Assert.AreEqual(vlb[9], true);
            Assert.AreEqual(vlb[10], true);
            vlb = int.MaxValue;
            Assert.AreEqual(vlb.BitLength, 31);
            Assert.AreEqual(vlb.Bytes.Length, 4);
            Assert.AreEqual(vlb.MostSignificantBit, 31);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], true);
            Assert.AreEqual(vlb[2], true);
            Assert.AreEqual(vlb[3], true);
            Assert.AreEqual(vlb[4], true);
            Assert.AreEqual(vlb[5], true);
            Assert.AreEqual(vlb[6], true);
            Assert.AreEqual(vlb[7], true);
            Assert.AreEqual(vlb[8], true);
            Assert.AreEqual(vlb[9], true);
            Assert.AreEqual(vlb[10], true);
            Assert.AreEqual(vlb[11], true);
            Assert.AreEqual(vlb[12], true);
            Assert.AreEqual(vlb[13], true);
            Assert.AreEqual(vlb[14], true);
            Assert.AreEqual(vlb[15], true);
            Assert.AreEqual(vlb[16], true);
            Assert.AreEqual(vlb[17], true);
            Assert.AreEqual(vlb[18], true);
            Assert.AreEqual(vlb[19], true);
            Assert.AreEqual(vlb[20], true);
            Assert.AreEqual(vlb[21], true);
            Assert.AreEqual(vlb[22], true);
            Assert.AreEqual(vlb[23], true);
            Assert.AreEqual(vlb[24], true);
            Assert.AreEqual(vlb[25], true);
            Assert.AreEqual(vlb[26], true);
            Assert.AreEqual(vlb[27], true);
            Assert.AreEqual(vlb[28], true);
            Assert.AreEqual(vlb[29], true);
            Assert.AreEqual(vlb[30], true);
        }
        /// <summary>
        /// Test that the implicit conversion from integer to VariedLengthBinary throwss exception when necesarry.
        /// </summary>
        [TestMethod]
        public void IntegerToVLBExceptionTest()
        {
            Assert.ThrowsException<ArgumentException>(() => { VariedLengthBinary vlb = -10; });
        }
        /// <summary>
        /// Test the conversion of a char value to VariedLengthBinary
        /// </summary>
        [TestMethod]
        public void CharToVLBTest()
        {
            VariedLengthBinary vlb = 'a';
            Assert.AreEqual(vlb.BitLength, 7);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.MostSignificantBit, 7);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], false);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], false);
            Assert.AreEqual(vlb[4], false);
            Assert.AreEqual(vlb[5], true);
            Assert.AreEqual(vlb[6], true);
            vlb = 'k';
            Assert.AreEqual(vlb.BitLength, 7);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.MostSignificantBit, 7);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], true);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], true);
            Assert.AreEqual(vlb[4], false);
            Assert.AreEqual(vlb[5], true);
            Assert.AreEqual(vlb[6], true);
            vlb = '1';
            Assert.AreEqual(vlb.BitLength, 6);
            Assert.AreEqual(vlb.Bytes.Length, 1);
            Assert.AreEqual(vlb.MostSignificantBit, 6);
            Assert.AreEqual(vlb[0], true);
            Assert.AreEqual(vlb[1], false);
            Assert.AreEqual(vlb[2], false);
            Assert.AreEqual(vlb[3], false);
            Assert.AreEqual(vlb[4], true);
            Assert.AreEqual(vlb[5], true);
        }
        /// <summary>
        /// Test the conversion of a VariedLengthBinary object to an integer value;
        /// </summary>
        [TestMethod]
        public void ToIntegerTest()
        {
            VariedLengthBinary vlb = 0;
            Assert.AreEqual((int)vlb, 0);

            vlb = 19765;
            Assert.AreEqual((int)vlb, 19765);

            vlb = int.MaxValue;
            Assert.AreEqual((int)vlb, int.MaxValue);
        }
        /// <summary>
        /// Test the conversion of a VariedLengthBinary object to a string value;
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            VariedLengthBinary vlb = 0b1110_0100_1010_0000;
            Assert.AreEqual(vlb.ToString(), "11100100 10100000");
            Assert.AreEqual((string)vlb, "11100100 10100000");

            vlb = 0;
            Assert.AreEqual(vlb.ToString(), "00000000");
            Assert.AreEqual((string)vlb, "00000000");

            vlb = int.MaxValue;
            Assert.AreEqual(vlb.ToString(), "01111111 11111111 11111111 11111111");
            Assert.AreEqual((string)vlb, "01111111 11111111 11111111 11111111");
        }
        /// <summary>
        /// Test the shifting of the bits to the left.
        /// </summary>
        [TestMethod]
        public void LeftShiftTest()
        {
            VariedLengthBinary vlb = 1;
            VariedLengthBinary vlb_test = vlb << 1;
            Assert.AreEqual(vlb_test.BitLength, 2);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test.MostSignificantBit, 2);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], true);
            Assert.AreEqual((int)vlb_test, 2);

            vlb_test = vlb << 5;
            Assert.AreEqual(vlb_test.BitLength, 6);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test.MostSignificantBit,6);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], true);
            Assert.AreEqual((int)vlb_test, 32);

            vlb_test = vlb << 8;
            Assert.AreEqual(vlb_test.BitLength, 9);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 9);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], true);
            Assert.AreEqual((int)vlb_test, 256);

            vlb_test = vlb << 12;
            Assert.AreEqual(vlb_test.BitLength, 13);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 13);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], true);
            Assert.AreEqual((int)vlb_test, 4096);

            vlb = 0b0000_1000_0000_0010; // 2050
            vlb_test = vlb << 1; // 0b0001_0000 0000_0100 - 4100
            Assert.AreEqual(vlb_test.BitLength, 13);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 13);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], true);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], true);
            Assert.AreEqual((int)vlb_test, 4100);

            vlb_test = vlb << 5; // 0b0000_0001 0000_0000 0100_0000 - 65600
            Assert.AreEqual(vlb_test.BitLength, 17);
            Assert.AreEqual(vlb_test.Bytes.Length, 3);
            Assert.AreEqual(vlb_test.MostSignificantBit, 17);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], true);
            Assert.AreEqual((int)vlb_test, 65600);

            vlb_test = vlb << 8; // 0b0000_1000 0000_0010 0000_0000 - 524800
            Assert.AreEqual(vlb_test.BitLength, 20);
            Assert.AreEqual(vlb_test.Bytes.Length, 3);
            Assert.AreEqual(vlb_test.MostSignificantBit, 20);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], true);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], false);
            Assert.AreEqual(vlb_test[17], false);
            Assert.AreEqual(vlb_test[18], false);
            Assert.AreEqual(vlb_test[19], true);
            Assert.AreEqual((int)vlb_test, 524800);

            vlb_test = vlb << 12; // 0b1000_0000 0010_0000 0000_0000 - 8396800
            Assert.AreEqual(vlb_test.BitLength, 24);
            Assert.AreEqual(vlb_test.Bytes.Length, 3);
            Assert.AreEqual(vlb_test.MostSignificantBit, 24);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], true);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], false);
            Assert.AreEqual(vlb_test[17], false);
            Assert.AreEqual(vlb_test[18], false);
            Assert.AreEqual(vlb_test[19], false);
            Assert.AreEqual(vlb_test[20], false);
            Assert.AreEqual(vlb_test[21], false);
            Assert.AreEqual(vlb_test[22], false);
            Assert.AreEqual(vlb_test[23], true);
            Assert.AreEqual((int)vlb_test, 8396800);

            vlb_test = vlb << 15; // 0b0000_0100 0000_0001 0000_0000 0000_0000 - 67174400
            Assert.AreEqual(vlb_test.BitLength, 27);
            Assert.AreEqual(vlb_test.Bytes.Length, 4);
            Assert.AreEqual(vlb_test.MostSignificantBit, 27);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], true);
            Assert.AreEqual(vlb_test[17], false);
            Assert.AreEqual(vlb_test[18], false);
            Assert.AreEqual(vlb_test[19], false);
            Assert.AreEqual(vlb_test[20], false);
            Assert.AreEqual(vlb_test[21], false);
            Assert.AreEqual(vlb_test[22], false);
            Assert.AreEqual(vlb_test[23], false);
            Assert.AreEqual(vlb_test[24], false);
            Assert.AreEqual(vlb_test[25], false);
            Assert.AreEqual(vlb_test[26], true);
            Assert.AreEqual((int)vlb_test, 67174400);
        }
        /// <summary>
        /// Test the shifting of the bits to the right.
        /// </summary>
        [TestMethod]
        public void RightShiftTest()
        {
            VariedLengthBinary vlb = 0b0100_1000_0100_1000_1000_0010; // 4737154
            VariedLengthBinary vlb_test = vlb >> 1; // 0b0010_0100 0010_0100 0100_0001 - 2368577
            Assert.AreEqual(vlb_test.BitLength, 22);
            Assert.AreEqual(vlb_test.Bytes.Length, 3);
            Assert.AreEqual(vlb_test.MostSignificantBit, 22);
            Assert.AreEqual(vlb_test[0], true);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], true);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], true);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], false);
            Assert.AreEqual(vlb_test[17], false);
            Assert.AreEqual(vlb_test[18], true);
            Assert.AreEqual(vlb_test[19], false);
            Assert.AreEqual(vlb_test[20], false);
            Assert.AreEqual(vlb_test[21], true);
            Assert.AreEqual((int)vlb_test, 2368577);

            vlb_test = vlb >> 7; // 0b1001_0000 1001_0001 - 37009
            Assert.AreEqual(vlb_test.BitLength, 16);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 16);
            Assert.AreEqual(vlb_test[0], true);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], true);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], true);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], true);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], true);
            Assert.AreEqual((int)vlb_test, 37009);

            vlb_test = vlb >> 8; // 0b0100_1000 0100_1000 - 18504
            Assert.AreEqual(vlb_test.BitLength, 15);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 15);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], true);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], true);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], true);
            Assert.AreEqual((int)vlb_test, 18504);

            vlb_test = vlb >> 12; // 0b0000_0100 1000_0100 - 1156
            Assert.AreEqual(vlb_test.BitLength, 11);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test.MostSignificantBit, 11);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], true);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], true);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], true);
            Assert.AreEqual((int)vlb_test, 1156);

            vlb_test = vlb >> 15; // 01001_0000 - 144
            Assert.AreEqual(vlb_test.BitLength, 8);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test.MostSignificantBit, 8);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], true);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], true);
            Assert.AreEqual((int)vlb_test, 144);
        }
        /// <summary>
        /// Test the NOT operator.
        /// </summary>
        [TestMethod]
        public void NegateTest()
        {
            VariedLengthBinary vlb = 0;
            VariedLengthBinary vlb_test = ~vlb;
            Assert.AreEqual(vlb_test.BitLength, 1);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test[0], true);

            vlb = int.MaxValue;
            vlb_test = ~vlb;
            Assert.AreEqual(vlb_test.BitLength, 31);
            Assert.AreEqual(vlb_test.Bytes.Length, 4);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], false);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], false);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], false);
            Assert.AreEqual(vlb_test[12], false);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], false);
            Assert.AreEqual(vlb_test[16], false);
            Assert.AreEqual(vlb_test[17], false);
            Assert.AreEqual(vlb_test[18], false);
            Assert.AreEqual(vlb_test[19], false);
            Assert.AreEqual(vlb_test[20], false);
            Assert.AreEqual(vlb_test[21], false);
            Assert.AreEqual(vlb_test[22], false);
            Assert.AreEqual(vlb_test[23], false);
            Assert.AreEqual(vlb_test[24], false);
            Assert.AreEqual(vlb_test[25], false);
            Assert.AreEqual(vlb_test[26], false);
            Assert.AreEqual(vlb_test[27], false);
            Assert.AreEqual(vlb_test[28], false);
            Assert.AreEqual(vlb_test[29], false);
            Assert.AreEqual(vlb_test[30], false);
        }
        /// <summary>
        /// Test the AND operator.
        /// </summary>
        [TestMethod]
        public void AndTest()
        {
            VariedLengthBinary vlb = 0b1001_1001_0100_1011;
            VariedLengthBinary vlb2 = 0b0100_0101;
            VariedLengthBinary vlb_test = vlb & vlb2; // 0b0100_0001 - 65
            Assert.AreEqual(vlb_test.BitLength, 7);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test[0], true);
            Assert.AreEqual(vlb_test[1], false);
            Assert.AreEqual(vlb_test[2], false);
            Assert.AreEqual(vlb_test[3], false);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual((int)vlb_test, 65);

            vlb = 0;
            vlb2 = int.MaxValue;
            vlb_test = vlb & vlb2; // 0b0000_0000 - 0
            Assert.AreEqual(vlb_test.BitLength, 1);
            Assert.AreEqual(vlb_test.Bytes.Length, 1);
            Assert.AreEqual(vlb_test[0], false);
            Assert.AreEqual((int)vlb_test, 0);
        }
        /// <summary>
        /// Test the OR operator.
        /// </summary>
        [TestMethod]
        public void OrTest()
        {
            VariedLengthBinary vlb = 0b1001_1001_0100_1011;
            VariedLengthBinary vlb2 = 0b0100_0101;
            VariedLengthBinary vlb_test = vlb | vlb2; // 0b1001_1001_0100_1111 - 39247
            Assert.AreEqual(vlb_test.BitLength, 16);
            Assert.AreEqual(vlb_test.Bytes.Length, 2);
            Assert.AreEqual(vlb_test[0], true);
            Assert.AreEqual(vlb_test[1], true);
            Assert.AreEqual(vlb_test[2], true);
            Assert.AreEqual(vlb_test[3], true);
            Assert.AreEqual(vlb_test[4], false);
            Assert.AreEqual(vlb_test[5], false);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual(vlb_test[7], false);
            Assert.AreEqual(vlb_test[8], true);
            Assert.AreEqual(vlb_test[9], false);
            Assert.AreEqual(vlb_test[10], false);
            Assert.AreEqual(vlb_test[11], true);
            Assert.AreEqual(vlb_test[12], true);
            Assert.AreEqual(vlb_test[13], false);
            Assert.AreEqual(vlb_test[14], false);
            Assert.AreEqual(vlb_test[15], true);
            Assert.AreEqual((int)vlb_test, 39247);

            vlb = 0;
            vlb2 = int.MaxValue;
            vlb_test = vlb | vlb2; // 0b0111_1111 1111_1111 1111_1111 1111_1111 - 2147483647
            Assert.AreEqual(vlb_test.BitLength, 31);
            Assert.AreEqual(vlb_test.Bytes.Length, 4);
            Assert.AreEqual(vlb_test[0], true);
            Assert.AreEqual(vlb_test[1], true);
            Assert.AreEqual(vlb_test[2], true);
            Assert.AreEqual(vlb_test[3], true);
            Assert.AreEqual(vlb_test[4], true);
            Assert.AreEqual(vlb_test[5], true);
            Assert.AreEqual(vlb_test[6], true);
            Assert.AreEqual(vlb_test[7], true);
            Assert.AreEqual(vlb_test[8], true);
            Assert.AreEqual(vlb_test[9], true);
            Assert.AreEqual(vlb_test[10], true);
            Assert.AreEqual(vlb_test[11], true);
            Assert.AreEqual(vlb_test[12], true);
            Assert.AreEqual(vlb_test[13], true);
            Assert.AreEqual(vlb_test[14], true);
            Assert.AreEqual(vlb_test[15], true);
            Assert.AreEqual(vlb_test[16], true);
            Assert.AreEqual(vlb_test[17], true);
            Assert.AreEqual(vlb_test[18], true);
            Assert.AreEqual(vlb_test[19], true);
            Assert.AreEqual(vlb_test[20], true);
            Assert.AreEqual(vlb_test[21], true);
            Assert.AreEqual(vlb_test[22], true);
            Assert.AreEqual(vlb_test[23], true);
            Assert.AreEqual(vlb_test[24], true);
            Assert.AreEqual(vlb_test[25], true);
            Assert.AreEqual(vlb_test[26], true);
            Assert.AreEqual(vlb_test[27], true);
            Assert.AreEqual(vlb_test[28], true);
            Assert.AreEqual(vlb_test[29], true);
            Assert.AreEqual(vlb_test[30], true);
            Assert.AreEqual((int)vlb_test, int.MaxValue);
        }
    }
}
