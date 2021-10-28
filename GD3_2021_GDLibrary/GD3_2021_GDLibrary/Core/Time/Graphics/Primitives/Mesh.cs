using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Graphics
{
    public abstract class Mesh<T> where T : struct, IVertexType
    {
        protected T[] vertices;
        protected ushort[] indices;
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;

        public Mesh()
        {
            CreateGeometry();
            CreateBuffers();
        }
        protected abstract void CreateGeometry();

        private void CreateBuffers()
        {
            var graphics = Application.GraphicsDevice;

            vertexBuffer = new VertexBuffer(graphics, typeof(T), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphics, (IndexElementSize)sizeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }
    }
}