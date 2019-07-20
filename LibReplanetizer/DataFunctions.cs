﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace LibReplanetizer
{
    public static class DataFunctions
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

        public static float ReadFloat(byte[] buf, int offset)
        {
            flt.byte0 = buf[offset + 3];
            flt.byte1 = buf[offset + 2];
            flt.byte2 = buf[offset + 1];
            flt.byte3 = buf[offset];
            return flt.value;
        }

        public static int ReadInt(byte[] buf, int offset)
        {
            return (buf[offset + 0] << 24) | (buf[offset + 1] << 16) | (buf[offset + 2] << 8) | (buf[offset + 3]);
        }

        public static short ReadShort(byte[] buf, int offset)
        {
            return (short)((buf[offset + 0] << 8) | (buf[offset + 1]));
        }

        public static uint ReadUint(byte[] buf, int offset)
        {
            return (uint)((buf[offset + 0] << 24) | (buf[offset + 1] << 16) | (buf[offset + 2] << 8) | (buf[offset + 3]));
        }

        public static ushort ReadUshort(byte[] buf, int offset)
        {
            return (ushort)((buf[offset + 0] << 8) | (buf[offset + 1]));
        }

        public static Matrix4 ReadMatrix4(byte[] buf, int offset)
        {
            return new Matrix4(
                ReadFloat(buf, offset + 0x00),
                ReadFloat(buf, offset + 0x04),
                ReadFloat(buf, offset + 0x08),
                ReadFloat(buf, offset + 0x0C),

                ReadFloat(buf, offset + 0x10),
                ReadFloat(buf, offset + 0x14),
                ReadFloat(buf, offset + 0x18),
                ReadFloat(buf, offset + 0x1C),

                ReadFloat(buf, offset + 0x20),
                ReadFloat(buf, offset + 0x24),
                ReadFloat(buf, offset + 0x28),
                ReadFloat(buf, offset + 0x2C),

                ReadFloat(buf, offset + 0x30),
                ReadFloat(buf, offset + 0x34),
                ReadFloat(buf, offset + 0x38),
                ReadFloat(buf, offset + 0x3C)
                );
        }

        public static byte[] ReadBlock(FileStream fs, int offset, int length)
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

        public static String ReadString(FileStream fs, int offset)
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

        public static void WriteUint(ref byte[] byteArr, int offset, uint input)
        {
            byte[] byt = BitConverter.GetBytes(input);
            byteArr[offset + 0] = byt[3];
            byteArr[offset + 1] = byt[2];
            byteArr[offset + 2] = byt[1];
            byteArr[offset + 3] = byt[0];
        }

        public static void WriteInt(ref byte[] byteArr, int offset, int input)
        {
            byte[] byt = BitConverter.GetBytes(input);
            byteArr[offset + 0] = byt[3];
            byteArr[offset + 1] = byt[2];
            byteArr[offset + 2] = byt[1];
            byteArr[offset + 3] = byt[0];
        }

        public static void WriteFloat(ref byte[] byteArr, int offset, float input)
        {
            byte[] byt = BitConverter.GetBytes(input);
            byteArr[offset + 0] = byt[3];
            byteArr[offset + 1] = byt[2];
            byteArr[offset + 2] = byt[1];
            byteArr[offset + 3] = byt[0];
        }

        public static void WriteShort(ref byte[] byteArr, int offset, short input)
        {
            byte[] byt = BitConverter.GetBytes(input);
            byteArr[offset + 0] = byt[1];
            byteArr[offset + 1] = byt[0];
        }

        public static void WriteUshort(ref byte[] byteArr, int offset, ushort input)
        {
            byte[] byt = BitConverter.GetBytes(input);
            byteArr[offset + 0] = byt[1];
            byteArr[offset + 1] = byt[0];
        }

        public static void WriteMatrix4(ref byte[] byteArray, int offset, Matrix4 input)
        {
            WriteFloat(ref byteArray, offset + 0x00, input.M11);
            WriteFloat(ref byteArray, offset + 0x04, input.M12);
            WriteFloat(ref byteArray, offset + 0x08, input.M13);
            WriteFloat(ref byteArray, offset + 0x0C, input.M14);

            WriteFloat(ref byteArray, offset + 0x10, input.M21);
            WriteFloat(ref byteArray, offset + 0x14, input.M22);
            WriteFloat(ref byteArray, offset + 0x18, input.M23);
            WriteFloat(ref byteArray, offset + 0x1C, input.M24);

            WriteFloat(ref byteArray, offset + 0x20, input.M31);
            WriteFloat(ref byteArray, offset + 0x24, input.M32);
            WriteFloat(ref byteArray, offset + 0x28, input.M33);
            WriteFloat(ref byteArray, offset + 0x2C, input.M34);

            WriteFloat(ref byteArray, offset + 0x30, input.M41);
            WriteFloat(ref byteArray, offset + 0x34, input.M42);
            WriteFloat(ref byteArray, offset + 0x38, input.M43);
            WriteFloat(ref byteArray, offset + 0x3C, input.M44);
        }

        public static byte[] getBytes(byte[] array, int ind, int length)
        {
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = array[ind + i];
            }
            return data;
        }

        public static int GetLength(int length)
        {
            while (length % 0x10 != 0)
            {
                length++;
            }
            return length;
        }

        public static int GetLength100(int length)
        {
            while (length % 0x100 != 0)
            {
                length++;
            }
            return length;
        }
    }
}
