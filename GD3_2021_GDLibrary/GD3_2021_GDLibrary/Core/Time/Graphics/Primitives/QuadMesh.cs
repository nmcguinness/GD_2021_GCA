using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Graphics
{
    /// <summary>
    /// Defines a textured 1x1 quad, centred on origin, facing +ve Z-axis
    /// </summary>
    public class QuadMesh : Mesh<VertexPositionNormalTexture>
    {
        public QuadMesh(GraphicsDevice device) : base(device)
        {
        }

        protected override void CreateGeometry()
        {
            var positions = new Vector3[4]
           {
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f)
           };

            var uvs = new Vector2[4]
            {
                new Vector2(0.0f, 0.0f),
                new Vector2(0.5f, 0.0f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.0f, 0.5f)
            };

            vertices = new VertexPositionNormalTexture[4];

            for (int i = 0; i < 4; i++)
            {
                vertices[i].Position = positions[i];
                vertices[i].TextureCoordinate = uvs[i];
                vertices[i].Normal = Vector3.Forward;
            }

            indices = new ushort[] { 0, 1, 2, 0, 2, 3 };
        }
    }
}