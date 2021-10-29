using GDLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary.Components
{
    public class MeshRenderer : Renderer
    {
        //TODO - generalise for any IVertexType
        protected Mesh<VertexPositionNormalTexture> mesh;

        public Mesh<VertexPositionNormalTexture> Mesh
        {
            get
            {
                return mesh;
            }

            set
            {
                if (value != mesh && value != null)
                {
                    mesh = value;
                    SetBoundingVolume();
                }
            }
        }

        public override void Draw(GraphicsDevice device)
        {
            device.SetVertexBuffer(mesh.VertexBuffer);
            device.Indices = mesh.IndexBuffer;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, mesh.IndexBuffer.IndexCount / 3);
        }

        public override void SetBoundingVolume()
        {
            if (mesh == null)
                return;

            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var vertices = mesh.GetVertices(VertexType.Position);

            for (int i = 0, l = vertices.Length; i < l; i++)
            {
                min.X = Math.Min(vertices[i].X, min.X);
                min.Y = Math.Min(vertices[i].Y, min.Y);
                min.Z = Math.Min(vertices[i].Z, min.Z);
                max.X = Math.Max(vertices[i].X, max.X);
                max.Y = Math.Max(vertices[i].Y, max.Y);
                max.Z = Math.Max(vertices[i].Z, max.Z);
            }

            boundingBox.Min = min;
            boundingBox.Max = max;

            var dx = max.X - min.X;
            var dy = max.Y - min.Y;
            var dz = max.Z - min.Z;

            boundingSphere.Radius = (float)Math.Max(Math.Max(dx, dy), dz) / 2.0f;
            boundingSphere.Center = transform.LocalTranslation;
        }
    }
}