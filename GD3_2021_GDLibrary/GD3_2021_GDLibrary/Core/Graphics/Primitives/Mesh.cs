using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Graphics
{
    public abstract class Mesh<T> where T : struct, IVertexType
    {
        #region Fields

        protected T[] vertices;
        protected ushort[] indices;
        protected VertexBuffer vertexBuffer;
        protected IndexBuffer indexBuffer;

        #endregion Fields

        #region Constructors

        public T[] Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }

        public ushort[] Indices
        {
            get { return indices; }
            set { indices = value; }
        }

        public VertexBuffer VertexBuffer
        {
            get { return vertexBuffer; }
            protected set { vertexBuffer = value; }
        }

        public IndexBuffer IndexBuffer
        {
            get { return indexBuffer; }
            protected set { indexBuffer = value; }
        }

        #endregion Constructors

        #region Constructors

        public Mesh()
        {
            CreateGeometry();
            CreateBuffers();
        }

        #endregion Constructors

        #region Actions

        protected abstract void CreateGeometry();

        private void CreateBuffers()
        {
            var graphics = Application.GraphicsDevice;

            vertexBuffer = new VertexBuffer(graphics, typeof(T), vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphics, (IndexElementSize)sizeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        #endregion Actions
    }
}