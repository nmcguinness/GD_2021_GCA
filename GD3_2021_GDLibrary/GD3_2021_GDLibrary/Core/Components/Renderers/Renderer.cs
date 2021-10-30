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
        protected BoundingBox boundingBox;

        #endregion Fields

        #region Properties

        protected BoundingSphere BoundingSphere { get; }
        protected BoundingBox BoundingBox { get; }

        #endregion Properties

        #region Constructors

        public Renderer() : base()
        {
            boundingSphere = new BoundingSphere();
            boundingBox = new BoundingBox();
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
        public abstract void SetBoundingVolume();

        /// <summary>
        /// Draw the content of the mesh
        /// </summary>
        public abstract void Draw(GraphicsDevice device);

        #endregion Actions - Bounding sphere, Draw

        //TODO - Add sort by material then alpha
        //public override int CompareTo(object obj)
        //{
        //    var renderer = obj as Renderer;
        //    var material = renderer != null ? renderer.Material : null;

        //    if (renderer == null)
        //        return 1;

        //    if (material == null || Material == null)
        //        return base.CompareTo(obj);

        //    if (Material._hasAlpha == material._hasAlpha)
        //        return 0;
        //    else if (Material._hasAlpha && !material._hasAlpha)
        //        return 1;
        //    else
        //        return -1;
        //}
    }
}