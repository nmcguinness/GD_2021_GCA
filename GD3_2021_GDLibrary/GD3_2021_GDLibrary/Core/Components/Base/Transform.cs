using GDLibrary.Components;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Store and manage transform operations e.g. translation, rotation and scale
    /// </summary>
    public class Transform : Component
    {
        #region Fields

        private Matrix worldMatrix = Matrix.Identity;   //used to draw the game object that this transform is associated with
        private Matrix rotationMatrix = Matrix.Identity;
        private Vector3 localScale;
        private Vector3 localRotation;
        private Vector3 localTranslation;
        private bool isWorldDirty = true;                    //set to true so the worldMatrix is calculated the first time we create a transform

        #endregion Fields

        #region Properties

        public Vector3 LocalScale
        {
            get
            {
                return localScale;
            }
        }
        public Vector3 LocalRotation
        {
            get
            {
                return localRotation;
            }
        }
        public Vector3 LocalTranslation
        {
            get
            {
                return localTranslation;
            }
        }

        //BUG - 28/10/21 - Does this need to be called before WorldMatrix?
        public Matrix RotationMatrix
        {
            get
            {
                return rotationMatrix;
            }
        }

        public Matrix WorldMatrix
        {
            get
            {
                if (isWorldDirty)
                {
                    rotationMatrix = Matrix.CreateFromYawPitchRoll(localRotation.Y, localRotation.X, localRotation.Z);

                    worldMatrix = Matrix.Identity
                        * Matrix.CreateScale(localScale)
                            * rotationMatrix
                                * Matrix.CreateTranslation(localTranslation);
                    isWorldDirty = false;
                }

                return worldMatrix;
            }
        }

        public Vector3 Forward => worldMatrix.Forward;
        public Vector3 Backward => worldMatrix.Backward;
        public Vector3 Right => worldMatrix.Right;
        public Vector3 Left => worldMatrix.Left;
        public Vector3 Up => worldMatrix.Up;
        public Vector3 Down => worldMatrix.Down;

        #endregion Properties

        #region Constructors

        public Transform(Vector3? scale, Vector3? rotation, Vector3? translation)
        {
            localScale = scale.HasValue ? scale.Value : Vector3.One;
            localRotation = rotation.HasValue ? rotation.Value : Vector3.Zero;
            localTranslation = translation.HasValue ? translation.Value : Vector3.Zero;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <see cref="GameObject()"/>
        public Transform() : this(null, null, null)
        {
        }

        #endregion Constructors

        #region Actions - Modify Scale, Rotation, Translation

        public void Scale(float? x, float? y, float? z)
        {
            localScale.Add(x, y, z);
            isWorldDirty = true;
        }

        public void Scale(Vector3 delta)
        {
            localScale.Add(ref delta);
            isWorldDirty = true;
        }

        public void Scale(ref Vector3 delta)
        {
            localScale.Add(ref delta);
            isWorldDirty = true;
        }

        public void SetScale(float? x, float? y, float? z)
        {
            localScale.Set(x, y, z);
            isWorldDirty = true;
        }

        public void SetScale(Vector3 scale)
        {
            localScale.Set(scale);
            isWorldDirty = true;
        }

        public void SetScale(ref Vector3 scale)
        {
            localScale.Set(ref scale);
            isWorldDirty = true;
        }

        public void Rotate(float? x, float? y, float? z)
        {
            localRotation.Add(x, y, z);
            isWorldDirty = true;
        }

        public void Rotate(ref Vector3 delta)
        {
            localRotation.Add(ref delta);
            isWorldDirty = true;
        }

        public void Rotate(Vector3 delta)
        {
            localRotation.Add(ref delta);
            isWorldDirty = true;
        }

        public void SetRotation(float? x, float? y, float? z)
        {
            localRotation.Set(x, y, z);
            isWorldDirty = true;
        }

        public void SetRotation(Vector3 rotation)
        {
            localRotation.Set(rotation);
            isWorldDirty = true;
        }

        public void SetRotation(ref Vector3 rotation)
        {
            localRotation.Set(ref rotation);
            isWorldDirty = true;
        }

        public void SetRotation(Matrix matrix)
        {
            var quaternion = Quaternion.CreateFromRotationMatrix(matrix);
            var rotation = quaternion.ToEuler();
            localRotation.Set(rotation.X, rotation.Y, rotation.Z);
            isWorldDirty = true;
        }

        public void Translate(float? x, float? y, float? z)
        {
            localTranslation.Add(x, y, z);
            isWorldDirty = true;
        }

        public void Translate(Vector3 delta)
        {
            localTranslation.Add(delta);
            isWorldDirty = true;
        }

        public void Translate(ref Vector3 delta)
        {
            localTranslation.Add(ref delta);
            isWorldDirty = true;
        }

        public void SetTranslation(float? x, float? y, float? z)
        {
            localTranslation.Set(x, y, z);
            isWorldDirty = true;
        }

        public void SetTranslation(Vector3 translation)
        {
            localTranslation.Set(translation);
            isWorldDirty = true;
        }

        public void SetTranslation(ref Vector3 translation)
        {
            localTranslation.Set(ref translation);
            isWorldDirty = true;
        }

        #endregion Actions - Modify Scale, Rotation, Translation
    }
}