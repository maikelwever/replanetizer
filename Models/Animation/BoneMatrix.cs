﻿using OpenTK;
using static RatchetEdit.DataFunctions;

namespace RatchetEdit.Models.Animations
{ 
    public class BoneMatrix
    {
        public Matrix4 mat1;
        public short bb;

        public BoneMatrix(byte[] boneBlock, int num)
        {
            int offset = num * 0x40;
            mat1 = ReadMatrix4(boneBlock, offset);
            
            mat1.M14 = 0;
            mat1.M24 = 0;
            mat1.M34 = 0;
            mat1.M44 = 1;

            /*
            mat1.M11 = 1;
            mat1.M12 = 0;
            mat1.M13 = 0;

            mat1.M21 = 0;
            mat1.M22 = 1;
            mat1.M23 = 0;

            mat1.M31 = 0;
            mat1.M32 = 0;
            mat1.M33 = 1;
            */

            bb = ReadShort(boneBlock, offset + 0x3E);
            //mat1.Transpose();
            //mat1.Invert();
        }

        public byte[] Serialize()
        {
            byte[] outBytes = new byte[0x40];

            WriteMatrix4(outBytes, 0, mat1);

            return outBytes;
        }
    }
}
