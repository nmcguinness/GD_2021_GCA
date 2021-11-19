using Microsoft.Xna.Framework;
using System;

namespace GDLibrary.Components.UI
{
    /// <summary>
    /// Stores the rotation, scale and translation for a 2D texture or text
    /// </summary>
    public sealed class Transform2D : ICloneable
    {
        #region Fields

        private Vector2 localTranslation;
        private Vector2 localScale;
        private float rotationInDegrees;

        #endregion Fields

        #region Properties

        public Vector2 LocalTranslation { get => localTranslation; set => localTranslation = value; }
        public Vector2 LocalScale { get => localScale; set => localScale = value; }
        public float RotationInDegrees { get => rotationInDegrees; set => rotationInDegrees = value; }

        #endregion Properties

        #region Constructors

        public Transform2D(Vector2 localTranslation,
     Vector2 localScale, float rotationInDegrees)
        {
            //TODO - add validation in setters
            LocalTranslation = localTranslation;
            LocalScale = localScale;
            RotationInDegrees = rotationInDegrees;
        }

        #endregion Constructors

        #region Actions - Housekeeping

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion Actions - Housekeeping
    }
}