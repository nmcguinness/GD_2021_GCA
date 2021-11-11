using System;

namespace GDLibrary.Components
{
    /// <summary>
    /// Increases/decreases camera FOV based on mouse scroll wheel direction
    /// </summary>
    public class FOVOnScrollController : Controller
    {
        private Camera camera;
        public override void Awake()
        {
            camera = GetComponent<Camera>();

            if (camera == null)
                throw new Exception("No camera attached to this game object.");
        }

        public override void Update()
        {
            HandleInputs();
            //   base.Update(); //nothing happens so dont call this
        }

        protected override void HandleInputs()
        {
            HandleMouseInput();
        }

        protected override void HandleMouseInput()
        {
            var scrollDelta = Input.Mouse.GetDeltaFromScrollWheel();
            System.Diagnostics.Debug.WriteLine(scrollDelta);

            if (scrollDelta > 0)
                camera.FieldOfView += 1;
            else if (scrollDelta < 0)
                camera.FieldOfView -= 1;
        }

        #region Unused

        protected override void HandleGamepadInput()
        {
        }

        protected override void HandleKeyboardInput()
        {
        }

        #endregion Unused
    }
}