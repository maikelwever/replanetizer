﻿using System;
using static RatchetEdit.DataFunctions;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RatchetEdit.LevelObjects
{
    public class Cuboid : MatrixObject
    {
        public const int ELEMENTSIZE = 0x80;
        public int id;
        public Matrix4 mat1;
        public Matrix4 mat2;

		int IBO;
		int VBO;
		// Try to refactor this away at some point
		private readonly float originalM44;

		static readonly float[] cube = {
            -1.0f, -1.0f,  1.0f,
			1.0f, -1.0f,  1.0f,
			1.0f,  1.0f,  1.0f,
			-1.0f,  1.0f,  1.0f,
            // back
            -1.0f, -1.0f, -1.0f,
			1.0f, -1.0f, -1.0f,
			1.0f,  1.0f, -1.0f,
			-1.0f,  1.0f, -1.0f
		};

		public static readonly ushort[] cubeElements = { 
            0, 1, 2, 
            2, 3, 0, 
            1, 5, 6, 
            6, 2, 1, 
            7, 6, 5, 
            5, 4, 7, 
            4, 0, 3, 
            3, 7, 4, 
            4, 5, 1, 
            1, 0, 4, 
            3, 2, 6, 
            6, 7, 3 
        };

		public Cuboid(byte[] block, int index)
        {
            id = index;
            int offset = index * ELEMENTSIZE;

            mat1 = new Matrix4(
                ReadFloat(block, offset + 0x00),
                ReadFloat(block, offset + 0x04),
                ReadFloat(block, offset + 0x08),
                ReadFloat(block, offset + 0x0C),

                ReadFloat(block, offset + 0x10),
                ReadFloat(block, offset + 0x14),
                ReadFloat(block, offset + 0x18),
                ReadFloat(block, offset + 0x1C),

                ReadFloat(block, offset + 0x20),
                ReadFloat(block, offset + 0x24),
                ReadFloat(block, offset + 0x28),
                ReadFloat(block, offset + 0x2C),

                ReadFloat(block, offset + 0x30),
                ReadFloat(block, offset + 0x34),
                ReadFloat(block, offset + 0x38),
                1.0f
                );

            mat2 = new Matrix4(
                ReadFloat(block, offset + 0x40),
                ReadFloat(block, offset + 0x44),
                ReadFloat(block, offset + 0x48),
                ReadFloat(block, offset + 0x4C),

                ReadFloat(block, offset + 0x50),
                ReadFloat(block, offset + 0x54),
                ReadFloat(block, offset + 0x58),
                ReadFloat(block, offset + 0x5C),

                ReadFloat(block, offset + 0x60),
                ReadFloat(block, offset + 0x64),
                ReadFloat(block, offset + 0x68),
                ReadFloat(block, offset + 0x6C),

                ReadFloat(block, offset + 0x70),
                ReadFloat(block, offset + 0x74),
                ReadFloat(block, offset + 0x78),
                ReadFloat(block, offset + 0x7C)
            );

			originalM44 = ReadFloat(block, offset + 0x3C);

            modelMatrix = mat1;
            _rotation = modelMatrix.ExtractRotation().Xyz * 2.2f;
            _position = modelMatrix.ExtractTranslation();
            _scale = modelMatrix.ExtractScale();

            GetVBO();
            GetIBO();
		}

        public void GetVBO() {
            //Get the vertex buffer object, or create one if one doesn't exist
            if (VBO == 0) {
                GL.GenBuffers(1, out VBO);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, cube.Length * sizeof(float), cube, BufferUsageHint.StaticDraw);

            }
            else {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            }
        }

        public void GetIBO() {
            if (IBO == 0) {
                GL.GenBuffers(1, out IBO);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, cubeElements.Length * sizeof(ushort), cubeElements, BufferUsageHint.StaticDraw);
            }
            else {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO);
            }
        }

        public override LevelObject Clone() {
            throw new NotImplementedException();
        }

        public override byte[] ToByteArray()
        {
            byte[] bytes = new byte[0x80];

			// mat1
			WriteFloat(ref bytes, 0x00, modelMatrix.M11);
			WriteFloat(ref bytes, 0x04, modelMatrix.M12);
			WriteFloat(ref bytes, 0x08, modelMatrix.M13);
			WriteFloat(ref bytes, 0x0C, modelMatrix.M14);

			WriteFloat(ref bytes, 0x10, modelMatrix.M21);
			WriteFloat(ref bytes, 0x14, modelMatrix.M22);
			WriteFloat(ref bytes, 0x18, modelMatrix.M23);
			WriteFloat(ref bytes, 0x1C, modelMatrix.M24);

			WriteFloat(ref bytes, 0x20, modelMatrix.M31);
			WriteFloat(ref bytes, 0x24, modelMatrix.M32);
			WriteFloat(ref bytes, 0x28, modelMatrix.M33);
			WriteFloat(ref bytes, 0x2C, modelMatrix.M34);

			WriteFloat(ref bytes, 0x30, modelMatrix.M41);
			WriteFloat(ref bytes, 0x34, modelMatrix.M42);
			WriteFloat(ref bytes, 0x38, modelMatrix.M43);
			WriteFloat(ref bytes, 0x3C, originalM44);

			// mat2
			WriteFloat(ref bytes, 0x40, mat2.M11);
            WriteFloat(ref bytes, 0x44, mat2.M12);
            WriteFloat(ref bytes, 0x48, mat2.M13);
            WriteFloat(ref bytes, 0x4C, mat2.M14);

            WriteFloat(ref bytes, 0x50, mat2.M21);
            WriteFloat(ref bytes, 0x54, mat2.M22);
            WriteFloat(ref bytes, 0x58, mat2.M23);
            WriteFloat(ref bytes, 0x5C, mat2.M24);

            WriteFloat(ref bytes, 0x60, mat2.M31);
            WriteFloat(ref bytes, 0x64, mat2.M32);
            WriteFloat(ref bytes, 0x68, mat2.M33);
            WriteFloat(ref bytes, 0x6C, mat2.M34);

            WriteFloat(ref bytes, 0x70, mat2.M41);
            WriteFloat(ref bytes, 0x74, mat2.M42);
            WriteFloat(ref bytes, 0x78, mat2.M43);
            WriteFloat(ref bytes, 0x7C, mat2.M44);

            return bytes;
        }

		public override void Render(RenderManager glControl, bool selected = false)
		{
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
				Matrix4 mvp = modelMatrix * glControl.worldView;
				GL.UniformMatrix4(glControl.matrixID, false, ref mvp);
				GL.Uniform4(glControl.colorID, selected ? selectedColor : normalColor);

				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO);

				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

				GL.DrawElements(PrimitiveType.Triangles, cubeElements.Length, DrawElementsType.UnsignedShort, 0);
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
		}
	}
}
