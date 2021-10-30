﻿using GDLibrary.Components;
using GDLibrary.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary.Managers
{
    public class RenderManager : IDisposable
    {
        #region Fields

        protected GraphicsDevice graphicsDevice;

        //temps
        private Renderer renderer;

        private Material material;
        private Shader shader;

        #endregion Fields

        #region Constructors

        public RenderManager()
        {
            //cache this de-reference to save us some CPU cycles since its called often in Render below
            graphicsDevice = Application.GraphicsDevice;
        }

        #endregion Constructors

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion Initialization

        #region Actions - Render

        public virtual void Render(Scene scene)
        {
            //set depth and blend state

            //render sky box

            //render game objects
            var length = scene.Renderers.Count;

            for (var i = 0; i < length; i++)
            {
                renderer = scene.Renderers[i];
                material = renderer.Material;

                if (material == null)
                    throw new NullReferenceException("This game object has no material set for its renderer!");

                //access the shader (e.g. BasicEffect) for this rendered game object
                shader = material.Shader;

                //TODO - set Camera.Main in Main or SceneManager
                //Set View and Projection
                shader.PrePass(Camera.Main);

                //Set World matrix
                shader.Pass(renderer);

                renderer.Draw(graphicsDevice);
            }

            //render post processing

            //render ui
        }

        #endregion Actions - Render

        #region Actions - Housekeeping

        //TODO - add dispose code
        public virtual void Dispose()
        {
        }

        #endregion Actions - Housekeeping
    }
}