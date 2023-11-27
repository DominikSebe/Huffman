using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    /// <summary>
    /// Represents a binary value of variable bit-length.
    /// </summary>
    internal class VariedLengthBinary
    {
        #region Members
        private byte[] bytes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value of the bit stored at the index.
        /// </summary>
        /// <param name="index">A 0 based index of the bit to get.</param>
        /// <returns>True if the bit is 1, False if 0.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index points outside of the array of bits.</exception>
        public bool this[int index]
        {
            get { return Convert.ToBoolean(this.bytes[index / 8] & 0b1 << index % 8); }
        }
        /// <summary>
        /// Gets a copy of the stored bytes.
        /// </summary>
        public byte[] Bytes 
        { 
            get { return this.bytes.Clone() as byte[]; }
        }
        /// <summary>
        /// Gets the integer value of the stored bits.
        /// </summary>
        public int Value
        {
            get{ return this; }
        }
        /// <summary>
        /// Gets the index of the most significant bit (the largest bit with a value of 1).
        /// </summary>
        public int BitLength
        {
            get
            {
                int i = 7;
                while (i >= 0 && (this.bytes[this.bytes.Length - 1] & 0b1 << i) < 1) i--;

                return (this.bytes.Length - 1) * 8 + i + 1;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new VariedLengthBinary object.
        /// </summary>
        /// <param name="value">An integer value to convert to binary.</param>
        public VariedLengthBinary(int value)
        {
            int byteLength = Math.Max((int)Math.Ceiling(Math.Log(value, 2) / 8), 1);
            this.bytes = new byte[byteLength];
            for (int i = 0; i < byteLength; i++)
            {
                bytes[i] = Convert.ToByte(value >> i * 8 & 0b1111_1111);
            }
        }
        /// <summary>
        /// Initializes a new VariedLengthBinary object.
        /// </summary>
        /// <param name="bytes">An array of bytes used to build the object.</param>
        public VariedLengthBinary(params byte[] bytes)
        {
            this.bytes = bytes;
        }
        #endregion

        #region Functions

        #region Static
        /// <summary>
        /// Implicitly converts an integer value to a VariedLengthbinary object.
        /// </summary>
        /// <param name="value">The integer valued to convert.</param>
        public static implicit operator VariedLengthBinary(int value)
        {
            int byteLength = Math.Max((int)Math.Ceiling(Math.Log(value, 2) / 8), 1);
            byte[] bytes = new byte[byteLength];
            for (int i = 0;  i < byteLength; i++) {
                bytes[i] = Convert.ToByte(value >> i * 8 & 0b1111_1111);
            }

            return new VariedLengthBinary(bytes);
        }
        /// <summary>
        /// Implicitly convert a VariedLengthBinary object to an integer value.
        /// </summary>
        /// <param name="binary">The VariedLengthBinary object to convert.</param>
        public static implicit operator int(VariedLengthBinary binary)
        {
            return binary.Bytes.Select((b, i) => (int)(b << i*8)).ToArray().Sum();
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
            if (shift == 0) return binary;

            int reqBytes = (binary.BitLength + shift) / 8 + ((binary.BitLength + shift) % 8 > 0 ? 1 : 0);
            int byteShift = shift / 8, bitShift = shift % 8;
            byte[] bytes = new byte[reqBytes];

            int i = reqBytes - 1;

            if (byteShift > 0)
            {
                bytes[i] = (byte)(binary.Bytes[i - byteShift - 1] >> (8 - bitShift));
                i--;
            }

            for (; i - byteShift - 1 >= 0; i--)
                bytes[i] = (byte)((binary.Bytes[i - byteShift] << bitShift) | (binary.Bytes[i - byteShift - 1] >> (8 - bitShift)));
                

            bytes[i] = (byte)(binary.Bytes[i - byteShift] << bitShift);

            return new VariedLengthBinary(bytes);

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
            if (shift == 0) return binary;

            int reqBytes = (binary.BitLength - shift) / 8 + ((binary.BitLength - shift) % 8 > 0 ? 1 : 0);
            int byteshift = shift / 8, bitShift = shift % 8;
            byte[] bytes = new byte[reqBytes];

            int i = 0;
            for (; i + byteshift + 1 < binary.Bytes.Length; i++)
                bytes[i] = (byte)((binary.Bytes[i + byteshift] >> bitShift) | binary.Bytes[i + byteshift + 1] << (8 - bitShift));

            bytes[i] = (byte)(binary.Bytes[i + byteshift] >> bitShift);

            return new VariedLengthBinary(bytes);

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
            byte[] bytes_less, bytes_more, bytes;

            if (a.BitLength <= b.BitLength) {
                bytes_less = a.bytes;
                bytes_more = b.bytes;
            }
            else
            {
                bytes_less = b.bytes;
                bytes_more = a.bytes;
            }

            bytes = new byte[bytes_less.Length];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(bytes_less[i] & bytes_more[i]);

            return new VariedLengthBinary(bytes);
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
            byte[] bytes_less, bytes_more, bytes;

            if (a.BitLength <= b.BitLength)
            {
                bytes_less = a.bytes;
                bytes_more = b.bytes;
            }
            else
            {
                bytes_less = b.bytes;
                bytes_more = a.bytes;
            }

            int i;
            bytes = new byte[bytes_more.Length];

            
            for (i = 0; i < bytes_less.Length; i++)
                bytes[i] = Convert.ToByte(bytes_less[i] | bytes_more[i]);

            for (; i < bytes.Length; i++)
            {
                bytes[i] = bytes_more[i];
            }

            return new VariedLengthBinary(bytes);
        }
        #endregion

        #region Non-static
        /// <summary>
        /// Overriden implementation for the VariedLengthBinary class.
        /// Converted the stored bytes to a string, reading from right to left.
        /// </summary>
        /// <returns>A string version of the stored bytes.</returns>
        public override string ToString()
        {
            return string.Join(" ", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray().Reverse());
        }
        #endregion

        #endregion
    }
}
