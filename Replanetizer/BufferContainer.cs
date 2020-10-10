using LibReplanetizer.Models;
using LibReplanetizer.LevelObjects;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RatchetEdit
{
    /*
     * A very simple container to store IBO and VBO references for a Model
     */
    public class BufferContainer
    {
        public int ibo = 0;
        public int vbo = 0;

        public BufferContainer() { }

        public static BufferContainer FromRenderable(IRenderable renderable)
        {
            BufferContainer container = new BufferContainer();

            // IBO
            ushort[] iboData = renderable.GetIndices();
            GL.GenBuffers(1, out container.ibo);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, container.ibo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, iboData.Length * sizeof(ushort), iboData, BufferUsageHint.StaticDraw);

            // VBO 
            float[] vboData = renderable.GetVertices();
            GL.GenBuffers(1, out container.vbo);
            GL.BindBuffer(BufferTarget.ArrayBuffer, container.vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vboData.Length * sizeof(float), vboData, BufferUsageHint.StaticDraw);

            return container;
        }

        public void Bind()
        {
            // IBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ibo);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vbo);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 8, 0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, sizeof(float) * 8, sizeof(float) * 6);
        }
    }
}
