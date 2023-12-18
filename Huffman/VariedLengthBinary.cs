using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    /// <summary>
    /// Represents a binary number of varying amount of bits.
    /// </summary>
    public class VariedLengthBinary
    {
        #region Members
        private byte[] _bytes;
        private int _bitLength;
        #endregion

        #region Properties
        public int BitLength
        {
            get { return this._bitLength; }
        }
        /// <summary>
        /// Get a copy of the stored bytes of the binary.
        /// </summary>
        public byte[] Bytes
        {
            get { return this._bytes.Clone() as byte[]; }
        }
        /// <summary>
        /// Get the value of the bit stored at the index.
        /// </summary>
        /// <param name="index">A 0 based index of the bit.</param>
        /// <returns>True if the bit is 1, False if 0.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index is less than zero or equal to or greater than the length of bits.</exception>
        public bool this[int index]
        {
            get
            {
                if (index < 0 || index >= this._bitLength) throw new IndexOutOfRangeException("Index pointed outside of the bits.");
                return Convert.ToBoolean(this._bytes[index / 8] & (0b1 << (index % 8)));
            }
        }
        /// <summary>
        /// Gets the index of the most significant bit (the largest bit with a value of 1).
        /// </summary>
        public int MostSignificantBit
        {
            get
            {
                int i = 7;
                while (i >= 0 && (this._bytes[this._bytes.Length - 1] & 0b1 << i) < 1) i--;

                return (this._bytes.Length - 1) * 8 + i + 1;
            }
        }
        #endregion

        #region Constructors
        private VariedLengthBinary(byte[] bytes, int bitLength): this(bitLength)
        {
            this._bytes = bytes;
        }
        /// <summary>
        /// Initializes a new VariedLengthBinary of specified number of bits.
        /// </summary>
        /// <param name="bitLength"></param>
        public VariedLengthBinary(int bitLength)
        {
            if (bitLength < 1) throw new ArgumentOutOfRangeException(nameof(bitLength), "Number of bits must be one or greater.");
            this._bitLength = bitLength;
            this._bytes = new byte[(int)Math.Ceiling(bitLength / 8.0)];
        }
        /// <summary>
        /// Initializes a new VariedLengthBinary object.
        /// </summary>
        /// <param name="bytes">An array of _bytes used to build the object.</param>
        public VariedLengthBinary(byte[] bytes)
        {
            this._bitLength += bytes.Length * 8;
            this._bytes = bytes;
        }
        #endregion

        #region Functions
        #region Static
        /// <summary>
        /// Convert an integer value to a VariedLengthBinary object.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        public static implicit operator VariedLengthBinary(int value)
        {
            byte[] bytes;
            int bitLength;

            if (value < 0) throw new ArgumentException("Value must be positive", nameof(value));
            else if (value == 0) bitLength = 1;
            else bitLength = (int)Math.Log(value, 2) + 1;

            if (value < 256)
                bytes = new byte[1] { (byte)value };
            else
            {
                bytes = new byte[bitLength / 8 + Convert.ToInt32(bitLength % 8 > 0)];
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = (byte)(value >> i * 8 & 0b1111_1111);
            }

            return new VariedLengthBinary(bytes, bitLength);
        }
        public static explicit operator VariedLengthBinary(char key) => (int)key;
        public static explicit operator VariedLengthBinary(byte value) => (int)value;
        /// <summary>
        /// Convert a VariedLengthBinary object to an integer value.
        /// </summary>
        /// <param name="binary">The VariedLengthBinary object to convert.</param>
        public static explicit operator int(VariedLengthBinary binary)
        {
            return binary.Bytes.Select((b, i) => (int)(b << i * 8)).ToArray().Sum();
        }
        public static explicit operator string(VariedLengthBinary binary)
        {
            return binary.ToString();
        }
        /// <summary>
        /// Implementation of the bitwise left-shift operation for the VariedLengthBinary class.
        /// Moves each stored bit left by the specified amount.
        /// </summary>
        /// <param name="binary">The VariedLengthBinary object used for the shifting operation.</param>
        /// <param name="shift">The amount by which to shift the bits left.</param>
        /// <returns>A new VariedLengthBinary object with the shifted bits.</returns>
        public static VariedLengthBinary operator <<(VariedLengthBinary binary, int shift)
        {
            // 0b0000_1000 0000_0010 << 1     => 0b0001_0000 0000_0100
            // 0b0000_1000 0000_0010 << 5     => 0b0000_0001 0000_0000 0100_0000
            // 0b0000_1000 0000_0010 << 8     => 0b0000_1000 0000_0010 0000_0000
            // 0b0000_1000 0000_0010 << 12    => 0b1000_0000 0010_0000 0000_0000
            // 0b0000_1000 0000_0010 << 15    => 0b0000_0100 0000_0001 0000_0000 0000_0000

            int byteShift = shift / 8, bitShift = shift % 8; bool extraShift = (binary.BitLength - 1) % 8 + bitShift >= 8;
            int bitLength = binary.BitLength + shift, byteLength = bitLength / 8 + Convert.ToInt32(bitLength % 8 > 0);

            byte[] bytes = new byte[byteLength];

            int i = byteLength - 1;

            if(extraShift)
            {
                bytes[i] = (byte)(binary.Bytes[i - byteShift - 1] >> (8 - bitShift));
                i--;
            }

            for (; i > byteShift; i--)
                bytes[i] = (byte)(binary.Bytes[i - byteShift] << bitShift | binary.Bytes[i - byteShift - 1] >> (8 - bitShift));


            bytes[i] = (byte)(binary.Bytes[i - byteShift] << bitShift);

            return new VariedLengthBinary(bytes, bitLength);
        }
        /// <summary>
        /// Implementation of the bitwise right-shift operation for the VariedLengthBinary class.
        /// Moves each stored bit right by the specified amount.
        /// </summary>
        /// <param name="binary">The VariedLengthBinary object used for the shifting operation.</param>
        /// <param name="shift">The amount by which to shift the bits right.</param>
        /// <returns></returns>
        public static VariedLengthBinary operator >>(VariedLengthBinary binary, int shift)
        {
            // 0b0100_1000 0100_1000 1000_0010 >> 1     => 0b0010_0100 0010_0100 0100_0001
            // 0b0100_1000 0100_1000 1000_0010 >> 7     => 0b1001_0000 1001_0001
            // 0b0100_1000 0100_1000 1000_0010 >> 8     => 0b0100_1000 0100_1000
            // 0b0100_1000 0100_1000 1000_0010 >> 12    => 0b0000_0100 1000_0100
            // 0b0100_1000 0100_1000 1000_0010 >> 15    => 01001_0000
            if (shift == 0) return binary;

            int byteshift = shift / 8, bitShift = shift % 8; bool extraShift = (binary.BitLength - 1) % 8 < bitShift;
            int bitLength = binary.BitLength - shift, byteLength = bitLength / 8 + Convert.ToInt32(bitLength % 8 > 0);

            byte[] bytes = new byte[byteLength];

            int i = 0;
            for (; i < bytes.Length - Convert.ToInt32(!extraShift); i++)
                bytes[i] = (byte)((binary.Bytes[i + byteshift] >> bitShift) | binary.Bytes[i + byteshift + 1] << (8 - bitShift));


            if (!extraShift)
                bytes[i] = (byte)(binary.Bytes[i + byteshift] >> bitShift);

            return new VariedLengthBinary(bytes, binary.BitLength - shift);

        }
        public static VariedLengthBinary operator ~(VariedLengthBinary binary)
        {
            byte[] bytes = binary.Bytes;
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)~bytes[i];

            return new VariedLengthBinary(bytes, binary.BitLength);
        }
        /// <summary>
        /// Implementation of the bitwise AND operator for the VariedLengthBinary class.
        /// Computes the value of the AND operation for each of the corresponding bits for two VariedLengthBinary objects.
        /// </summary>
        /// <param name="a">The first VariedLengthBinary object used in the AND operation.</param>
        /// <param name="b">The second VariedLengthBinary object used in the AND operation.</param>
        /// <returns>A new VariedLengthBinary with results of the AND operations as its bits.</returns>
        public static VariedLengthBinary operator &(VariedLengthBinary a, VariedLengthBinary b)
        {
            int length = Math.Min(a.Bytes.Length, b.Bytes.Length);
            byte[] bytes_a = a.Bytes, bytes_b = b._bytes, bytes = new byte[length];


            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(bytes_a[i] & bytes_b[i]);

            return new VariedLengthBinary(bytes, Math.Min(a.BitLength, b.BitLength));
        }
        /// <summary>
        /// Implementation of the bitwise OR operator for the VariedLengthBinary class.
        /// Computes the value of the OR operation for each of the corresponding bits for two VariedLengthBinary objects.
        /// </summary>
        /// <param name="a">The first VariedLengthBinary object used in the OR operation.</param>
        /// <param name="b">The second VariedLengthBinary object used in the OR operation.</param>
        /// <returns>A new VariedLengthBinary with results of the OR operations as its bits.</returns>
        public static VariedLengthBinary operator |(VariedLengthBinary a, VariedLengthBinary b)
        {
            int length = Math.Max(a.Bytes.Length, b.Bytes.Length);
            byte[] bytes_lesser, bytes_greater, bytes = new byte[length];

            if (a.BitLength <= b.BitLength)
            {
                bytes_lesser = a.Bytes; bytes_greater = b.Bytes;
            }
            else
            {
                bytes_lesser = b.Bytes; bytes_greater = a.Bytes;
            }
            

            int i = 0;
            for (; i < bytes_lesser.Length; i++)
                bytes[i] = Convert.ToByte(bytes_lesser[i] | bytes_greater[i]);

            for (; i < bytes.Length; i++)
                bytes[i] = bytes_greater[i];

            return new VariedLengthBinary(bytes, Math.Max(a.BitLength, b.BitLength));
        }
        #endregion

        #region Non-static
        /// <summary>
        /// Overriden implementation for the VariedLengthBinary class.
        /// Converted the stored _bytes to a string, reading from right to left.
        /// </summary>
        /// <returns>A string version of the stored _bytes.</returns>
        public override string ToString()
        {
            return string.Join(" ", _bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray().Reverse());
        }
        #endregion

        #endregion
    }
}
