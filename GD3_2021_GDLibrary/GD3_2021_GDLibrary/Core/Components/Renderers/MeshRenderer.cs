using GDLibrary.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Components
{
    public class MeshRenderer : Renderer
    {
        //TODO - generalise for any IVertexType
        protected Mesh<VertexPositionNormalTexture> mesh;

        public override void Draw(GraphicsDevice device)
        {
            device.SetVertexBuffer(mesh.VertexBuffer);
            device.Indices = mesh.IndexBuffer;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, mesh.IndexBuffer.IndexCount / 3);
        }

        public override void SetBoundingSphere()
        {
            //TODO - update bounding sphere
        }
    }
}