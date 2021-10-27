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
            InitializeTime();
            base.Initialize();
        }

        /// <summary>
        /// Creates Time instance which can be used by game objects that want to slow down, or speed up, time
        /// </summary>
        private void InitializeTime()
        {
            //add time instance to the components list so that it will be updated
            var time = Time.GetInstance(this);
            Components.Add(time);
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

            DemoTime(gameTime);

            base.Update(gameTime);
        }

        private void DemoTime(GameTime gameTime)
        {
            //we can change the scale at any point in the game to modify how time passed
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                Time.Instance.TimeScale = 2;
            else if (Keyboard.GetState().IsKeyDown(Keys.R))
                Time.Instance.TimeScale = 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                Time.Instance.TimeScale = 0.5f;

            System.Diagnostics.Debug.WriteLine("Original:" + gameTime.ElapsedGameTime.Milliseconds + ", Scaled:" + Time.Instance.ElapsedGameTime);

            if (gameTime.ElapsedGameTime.Milliseconds < Time.Instance.ElapsedGameTime)
                System.Diagnostics.Debug.WriteLine("Time is running fast!");
            else if (gameTime.ElapsedGameTime.Milliseconds == Time.Instance.ElapsedGameTime)
                System.Diagnostics.Debug.WriteLine("Time is normal!");
            else
                System.Diagnostics.Debug.WriteLine("Time is running slowly!");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}