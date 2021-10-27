using GDLibrary.Time;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDApp
{
    public class Main : Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #endregion Fields

        #region Constructors

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #endregion Constructors

        #region Initialization

        protected override void Initialize()
        {
            InitializeScreen();
            InitializeInput();
            InitializeTime();
            InitializeScene();
            base.Initialize();
        }

        /// <summary>
        /// Initialize the game objects within the current scene including cameras, players, enemies, pickups etc
        /// </summary>
        private void InitializeScene()
        {
        }

        /// <summary>
        /// Initialize screen dimensions, mouse visibility, anti-aliasing etc
        /// </summary>
        private void InitializeScreen()
        {
        }

        /// <summary>
        /// Initialize entities to read from input devices
        /// </summary>
        private void InitializeInput()
        {
        }

        /// <summary>
        /// Creates Time instance which can be used by game objects that want to slow down, or speed up, time during the game
        /// </summary>
        private void InitializeTime()
        {
            //add time instance to the components list so that it will be updated
            Components.Add(Time.GetInstance(this));
        }

        #endregion Initialization

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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