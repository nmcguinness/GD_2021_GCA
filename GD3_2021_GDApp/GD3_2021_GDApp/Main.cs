using GDLibrary;
using GDLibrary.Core;
using GDLibrary.Inputs;
using GDLibrary.Time;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDApp
{
    public class Main : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #endregion Fields

        #region Constructors - Scene manager, Application data, Screen

        public Main() : this("My Game Name", 640, 480)
        {
        }

        /// <summary>
        /// Creates the game by initializing graphics, scene manager, data, screen
        /// </summary>
        /// <param name="name">Name of the game</param>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        /// <param name="isFullScreen">Fullscreen on/off</param>
        /// <param name="isMouseVisible">Mouse visibible on/off</param>
        public Main(string name, int width = 640, int height = 480, bool isFullScreen = false, bool isMouseVisible = true)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = isMouseVisible;

            //initialize scene manager and global application data
            Application.Content = Content;
            Application.GraphicsDevice = GraphicsDevice;
            Application.GraphicsDeviceManager = _graphics;
            Application.SceneManager = new SceneManager();

            //initialize screen
        }

        #endregion Constructors - Scene manager, Application data, Screen

        #region Initialization - Input, Scenes, Game Objects

        protected override void Initialize()
        {
            //initialize input
            InitializeInput();

            //add all components to component list so that they will be updated
            InitializeCoreComponents();

            //add scene

            //add game object(s) to scene, repeat for all game objects

            //add scene to scenemanager, repeat for all scenes

            base.Initialize();
        }

        private void InitializeCoreComponents()
        {
            Components.Add(Input.Keys);
            Components.Add(Input.Mouse);
            Components.Add(Input.Gamepad);
            Components.Add(Time.GetInstance(this));
        }

        private void InitializeInput()
        {
            Input.Keys = new KeyboardComponent(this);
            Input.Mouse = new MouseComponent(this);
            Input.Gamepad = new GamepadComponent(this);
        }

        #endregion Initialization - Input, Scenes, Game Objects

        #region Load & Unload Assets

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion Load & Unload Assets

        #region Update & Draw

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}