using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Components
{
    /// <summary>
    /// Base class for all renderers (i.e. an object to render a mesh, model, animated model) used by the engine
    /// </summary>
    public abstract class Renderer : Component
    {
        #region Fields

        protected BoundingSphere boundingSphere;

        #endregion Fields

        #region Constructors

        public Renderer()
        {
            SetBoundingSphere();
        }

        #endregion Constructors

        #region Update

        public override void Update()
        {
            //TODO - add if() for static objects
            //update the bounding sphere if the game object which this component is attached to moves
            boundingSphere.Center = transform.LocalTranslation;
        }

        #endregion Update

        #region Actions - Bounding sphere, Draw

        /// <summary>
        /// Compute the bounding sphere of mesh used for culling objects out of the camera frustum
        /// </summary>
        public abstract void SetBoundingSphere();

        /// <summary>
        /// Draw the content of the mesh
        /// </summary>
        public abstract void Draw(GraphicsDevice device);

        #endregion Actions - Bounding sphere, Draw
    }
}