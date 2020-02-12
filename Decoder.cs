using OpenTK;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RatchetEdit
{
    public class Decoder
    {
        [StructLayout(LayoutKind.Explicit)]
        struct FloatUnion
        {
            [FieldOffset(0)]
            public byte byte0;
            [FieldOffset(1)]
            public byte byte1;
            [FieldOffset(2)]
            public byte byte2;
            [FieldOffset(3)]
            public byte byte3;

            [FieldOffset(0)]
            public float value;
        }

        static FloatUnion flt;

        public bool littleEndian;

        public Decoder(bool littleEndian) {
            this.littleEndian = littleEndian;
        }

        public float Float(byte[] buf, int offset)
        {
            if (littleEndian) {
                return FloatLE(buf, offset);
            } else {
                return FloatBE(buf, offset);
            }
        }

        public float FloatBE(byte[] buf, int offset)
        {
            flt.byte0 = buf[offset + 3];
            flt.byte1 = buf[offset + 2];
            flt.byte2 = buf[offset + 1];
            flt.byte3 = buf[offset];
            return flt.value;
        }

        public float FloatLE(byte[] buf, int offset)
        {
            flt.byte0 = buf[offset];
            flt.byte1 = buf[offset + 1];
            flt.byte2 = buf[offset + 2];
            flt.byte3 = buf[offset + 3];
            return flt.value;
        }

        public int Int(byte[] buf, int offset) 
        {
            if (littleEndian) {
                return IntLE(buf, offset);
            } else {
                return IntBE(buf, offset);
            }
        }

        public int IntBE(byte[] buf, int offset)
        {
            return buf[offset + 0] << 24 | buf[offset + 1] << 16 | buf[offset + 2] << 8 | buf[offset + 3];
        }

        public int IntLE(byte[] buf, int offset)
        {
            return buf[offset + 3] << 24 | buf[offset + 2] << 16 | buf[offset + 1] << 8 | buf[offset];
        }

        public short Short(byte[] buf, int offset)
        {
            if (littleEndian) {
                return ShortLE(buf, offset);
            } else {
                return ShortBE(buf, offset);
            }
        }

        public short ShortBE(byte[] buf, int offset)
        {
            return (short)(buf[offset + 0] << 8 | buf[offset + 1]);
        }

        public short ShortLE(byte[] buf, int offset)
        {
            return (short)(buf[offset + 1] << 8 | buf[offset + 0]);
        }

        public uint Uint(byte[] buf, int offset)
        {
            if (littleEndian) {
                return UintLE(buf, offset);
            } else {
                return UintBE(buf, offset);
            }
        }

        public uint UintBE(byte[] buf, int offset)
        {
            return (uint)(buf[offset + 0] << 24 | buf[offset + 1] << 16 | buf[offset + 2] << 8 | buf[offset + 3]);
        }

        public uint UintLE(byte[] buf, int offset)
        {
            return (uint)(buf[offset + 3] << 24 | buf[offset + 2] << 16 | buf[offset + 1] << 8 | buf[offset + 0]);
        }

        public ushort Ushort(byte[] buf, int offset)
        {
            if (littleEndian) {
                return UshortLE(buf, offset);
            } else {
                return UshortBE(buf, offset);
            }
        }

        public ushort UshortBE(byte[] buf, int offset)
        {
            return (ushort)(buf[offset + 0] << 8 | buf[offset + 1]);
        }

        public ushort UshortLE(byte[] buf, int offset)
        {
            return (ushort)(buf[offset + 1] << 8 | buf[offset + 0]);
        }

        public Matrix4 Matrix4(byte[] buf, int offset)
        {
            return new Matrix4(
                Float(buf, offset + 0x00),
                Float(buf, offset + 0x04),
                Float(buf, offset + 0x08),
                Float(buf, offset + 0x0C),

                Float(buf, offset + 0x10),
                Float(buf, offset + 0x14),
                Float(buf, offset + 0x18),
                Float(buf, offset + 0x1C),

                Float(buf, offset + 0x20),
                Float(buf, offset + 0x24),
                Float(buf, offset + 0x28),
                Float(buf, offset + 0x2C),

                Float(buf, offset + 0x30),
                Float(buf, offset + 0x34),
                Float(buf, offset + 0x38),
                Float(buf, offset + 0x3C)
                );
        }

        public byte[] Block(FileStream fs, long offset, int length)
        {
            if (length > 0)
            {
                fs.Seek(offset, SeekOrigin.Begin);
                byte[] returnBytes = new byte[length];
                fs.Read(returnBytes, 0, length);
                return returnBytes;
            }
            else
            {
                byte[] returnBytes = new byte[0x10];
                return returnBytes;
            }
        }

        public byte[] BlockNopad(FileStream fs, long offset, int length)
        {
            if (length > 0)
            {
                fs.Seek(offset, SeekOrigin.Begin);
                byte[] returnBytes = new byte[length];
                fs.Read(returnBytes, 0, length);
                return returnBytes;
            }
            return new byte[0];
        }

        public String String(FileStream fs, int offset)
        {
            String output = "";
            fs.Seek(offset, SeekOrigin.Begin);
            int pos = offset;

            byte[] buffer = new byte[4];
            do 
            {
                fs.Read(buffer, 0, 4);
                output += System.Text.Encoding.ASCII.GetString(buffer);
            }
            while (buffer[3] != '\0');

            return output.Substring(0, output.IndexOf('\0'));
        }
    }
}
