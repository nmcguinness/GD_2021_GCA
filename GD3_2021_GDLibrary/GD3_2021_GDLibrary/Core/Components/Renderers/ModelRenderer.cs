﻿using GDLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary.Components
{
    public class ModelRenderer : Renderer
    {
        /// <summary>
        /// Stores vertex, normal, uv data for the model
        /// </summary>
        protected Model model;

        /// <summary>
        /// Stores bone transforms for the model (e.g. each mesh will normally have one bone)
        /// </summary>
        protected Matrix[] boneTransforms;

        public Model Model
        {
            get
            {
                return model;
            }
            set
            {
                if (value != model)
                {
                    model = value;

                    if (model != null)
                    {
                        boneTransforms = new Matrix[model.Bones.Count];
                        model.CopyAbsoluteBoneTransformsTo(boneTransforms);

                        //BUG - on clone
                        //       SetBoundingVolume();
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
                boundingSphere.Radius *= Math.Max(
                    Math.Max(transform.LocalScale.X, transform.LocalScale.Y),
                    transform.LocalScale.Z);
            }
        }

        public override void Draw(GraphicsDevice device)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    device.SetVertexBuffer(meshPart.VertexBuffer);
                    device.Indices = meshPart.IndexBuffer;
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
                }
            }

            //Alternatively we can draw using the BasicEffect
            //foreach (ModelMesh mesh in model.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh.Effects)
            //    {
            //        effect.World = boneTransforms[mesh.ParentBone.Index] * transform.WorldMatrix;
            //        effect.View = camera._viewMatrix;
            //        effect.Projection = camera._projectionMatrix;
            //        effect.EnableDefaultLighting();
            //        effect.CurrentTechnique.Passes[0].Apply();
            //    }
            //    mesh.Draw();
            //}
        }

        #region Actions - Housekeeping

        //TODO - Dispose

        public override object Clone()
        {
            var clone = new ModelRenderer();
            clone.Model = model;
            clone.material = material.Clone() as Material;
            return clone;
        }

        #endregion Actions - Housekeeping
    }
}