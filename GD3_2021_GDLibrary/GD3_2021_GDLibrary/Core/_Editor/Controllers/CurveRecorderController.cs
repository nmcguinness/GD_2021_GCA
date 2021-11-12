using GDLibrary.Components;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GDLibrary.Editor
{
    public class CurveRecorderController : Controller
    {
        private static readonly int DEFAULT_MIN_SIZE = 10;
        private Camera camera;
        private List<Transform> keyTransforms;

        public CurveRecorderController()
        {
            keyTransforms = new List<Transform>(DEFAULT_MIN_SIZE);
        }

        public override void Awake()
        {
            camera = Camera.Main;
            if (camera is null)
                throw new NullReferenceException("Scene does not have a main camera");
        }

        public override void Update()
        {
            HandleInputs();
        }
        protected override void HandleInputs()
        {
            HandleMouseInput();
        }
        protected override void HandleMouseInput()
        {
            //if we right clicked then add to list
            if (Input.Mouse.WasJustClicked(Inputs.MouseButton.Right))
                keyTransforms.Add(camera.transform);
        }

        protected override void HandleGamepadInput()
        {
        }

        protected override void HandleKeyboardInput()
        {
            if (Input.Keys.WasJustPressed(Keys.F1))
                keyTransforms.Clear();
            else if (Input.Keys.WasJustPressed(Keys.F2))
            {
                keyTransforms.RemoveAt(keyTransforms.Count - 1);
            }
            else if (Input.Keys.WasJustPressed(Keys.F5))
            {
                //Serialization
            }
        }
    }
}