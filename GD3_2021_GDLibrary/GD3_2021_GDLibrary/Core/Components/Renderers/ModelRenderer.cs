using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary.Components
{
    public class ModelRenderer : Renderer
    {
        protected Model model;
        protected Matrix[] boneTransforms;

        public Model Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;

                    if (model != null)
                    {
                        boneTransforms = new Matrix[model.Bones.Count];
                        model.CopyAbsoluteBoneTransformsTo(boneTransforms);
                        SetBoundingVolume();
                    }
                }
            }
        }

        public override void SetBoundingVolume()
        {
            if (model != null)
            {
                foreach (ModelMesh mesh in model.Meshes)
                    boundingSphere = BoundingSphere.CreateMerged(BoundingSphere, mesh.BoundingSphere);

                boundingSphere.Center = gameObject.Transform.LocalTranslation;
                boundingSphere.Transform(gameObject.Transform.WorldMatrix);
                boundingSphere.Radius *= Math.Max(Math.Max(transform.LocalScale.X, transform.LocalScale.Y), transform.LocalScale.Z);
            }
        }

        public override void Draw(GraphicsDevice device)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] * transform.WorldMatrix;
                    effect.View = camera._viewMatrix;
                    effect.Projection = camera._projectionMatrix;
                    effect.EnableDefaultLighting();
                    effect.CurrentTechnique.Passes[0].Apply();
                }
                mesh.Draw();
            }
        }
    }
}