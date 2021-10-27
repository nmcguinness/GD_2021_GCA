using GDLibrary.Components;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Graphics
{
    public abstract class Mesh<T> : Component where T : struct, IVertexType
    {
        protected T[] vertices;
        protected ushort[] indices;
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;

        public Mesh(GraphicsDevice device)
        {
            CreateGeometry();
            CreateBuffers(device);
        }
        protected abstract void CreateGeometry();

        private void CreateBuffers(GraphicsDevice device)
        {
            vertexBuffer = new VertexBuffer(device, typeof(T), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(device, (IndexElementSize)sizeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }
    }
}