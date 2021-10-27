using GDLibrary.Components;
using GDLibrary.Type;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary.Components
{
    public class Camera : Component
    {
        #region Fields

        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        private Viewport viewPort;
        private CameraProjectionType projectionType;
        private float fieldOfView, aspectRatio, nearClipPlane, farClipPlane;
        private Vector3 up;
        private BoundingFrustum boundingFrustrum;
        private int depth;
        private bool isViewDirty = true, isProjectionDirty = true, isFrustumDirty = true;

        #endregion Fields

        #region Properties

        public static Camera Main { get; set; }

        public Matrix ViewMatrix
        {
            get
            {
                if (isViewDirty)
                {
                    //TODO - Optimize this call
                    var transform = gameObject.Transform;
                    var target = transform.Translation + Vector3.Transform(Vector3.Forward, transform.RotationMatrix);
                    viewMatrix = Matrix.CreateLookAt(transform.Translation, target, up);
                    isViewDirty = false;
                    isFrustumDirty = true;
                }

                return viewMatrix;
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                if (isProjectionDirty)
                {
                    projectionMatrix =
                        projectionType == CameraProjectionType.Perspective
                        ? Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane)
                        : Matrix.CreateOrthographic(viewPort.Width, viewPort.Height, nearClipPlane, farClipPlane);

                    isProjectionDirty = false;
                    isFrustumDirty = true;
                }

                return projectionMatrix;
            }
        }

        public BoundingFrustum BoundingFrustum
        {
            get
            {
                if (isFrustumDirty)
                {
                    boundingFrustrum = new BoundingFrustum(viewMatrix * projectionMatrix);
                    isFrustumDirty = false;
                }

                return boundingFrustrum;
            }
        }

        public CameraProjectionType ProjectionType
        {
            get => projectionType;
            set
            {
                if (projectionType != value)
                {
                    projectionType = value;
                    isProjectionDirty = true;
                }
            }
        }

        public float FieldOfView
        {
            get => fieldOfView;
            set
            {
                if (fieldOfView != value)
                {
                    fieldOfView = value;
                    isProjectionDirty = true;
                }
            }
        }

        public float AspectRatio
        {
            get => aspectRatio;
            set
            {
                if (aspectRatio != value)
                {
                    aspectRatio = value;
                    isProjectionDirty = true;
                }
            }
        }

        public float NearClipPlane
        {
            get => nearClipPlane;
            set
            {
                if (nearClipPlane != value)
                {
                    nearClipPlane = value >= 0 ? value : 1;
                    isProjectionDirty = true;
                }
            }
        }

        public float FarClipPlane
        {
            get => farClipPlane;
            set
            {
                if (farClipPlane != value)
                {
                    farClipPlane = value >= 1 ? value : 1000;
                    isProjectionDirty = true;
                }
            }
        }

        public Vector3 Forward => viewMatrix.Forward;
        public Vector3 Backward => viewMatrix.Backward;
        public Vector3 Right => viewMatrix.Right;
        public Vector3 Left => viewMatrix.Left;
        public Vector3 Up => viewMatrix.Up;
        public Vector3 Down => viewMatrix.Down;

        #endregion Properties

        #region Constructors

        public Camera(Viewport viewPort)
        {
            this.viewPort = viewPort;
            projectionType = CameraProjectionType.Perspective;
            fieldOfView = MathHelper.ToRadians(45);
            aspectRatio = (float)viewPort.Width / viewPort.Height;
            nearClipPlane = 1.0f;
            farClipPlane = 1000.0f;
            up = Vector3.Up;
            depth = 0;
        }

        #endregion Constructors
    }
}