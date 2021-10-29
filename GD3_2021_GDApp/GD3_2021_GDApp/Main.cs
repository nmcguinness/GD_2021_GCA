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

        /// <summary>
        /// Stores all scenes (which means all game objects i.e. players, cameras, pickups, behaviours, controllers)
        /// </summary>
        private SceneManager sceneManager;

        #endregion Fields

        #region Constructors - Scene manager, Application data, Screen

        public Main()
        {
            Window.Title = "My Game Name";
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion Constructors - Scene manager, Application data, Screen

        #region Initialization - Input, Scenes, Game Objects

        protected override void Initialize()
        {
            //instanciate scene manager to store all scenes
            sceneManager = new SceneManager();

            //initialize global application data
            Application.Main = this;
            Application.Content = Content;
            Application.GraphicsDevice = _graphics.GraphicsDevice;
            Application.GraphicsDeviceManager = _graphics;
            Application.SceneManager = sceneManager;

            //initialize screen
            var screen = Screen.GetInstance();
            screen.Set(1280, 720, true, false);

            //initialize input components
            InitializeInput();

            //add scene

            //add game object(s) to scene, repeat for all game objects

            //add scene to scenemanager, repeat for all scenes

            base.Initialize();
        }

        /// <summary>
        /// Instanciate input objects to allow us to access from any controller object in the game and store reference to input objects to allow us to access from any controller object in the game
        /// </summary>
        private void InitializeInput()
        {
            Input.Keys = new KeyboardComponent(this);
            Input.Mouse = new MouseComponent(this);
            Input.Gamepad = new GamepadComponent(this);

            //add all input components to component list so that they will be updated
            //Q. what would happen is we commented out these lines?
            Components.Add(Input.Keys);
            Components.Add(Input.Mouse);
            Components.Add(Input.Gamepad);
            Components.Add(Time.GetInstance(this));
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
            //allow the system to update first
            base.Update(gameTime);

            //then update everything in the game
            //Q. what would happen is we commented out this line?
            sceneManager.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}