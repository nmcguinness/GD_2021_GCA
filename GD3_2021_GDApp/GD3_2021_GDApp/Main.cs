using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Graphics;
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

        private GameObject camera;
        private GameObject cube;
        private BasicEffect effect;
        private Texture2D texture;

        #endregion Fields

        #region Constructors

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion Constructors

        #region Initialization - Scene manager, Application data, Screen, Input, Scenes, Game Objects

        protected override void Initialize()
        {
            //data, input, scene manager
            InitialGameCore("My Game Title Goes Here", 1024, 768);

            //level with scenes and game objects
            InitializeLevel();

            effect = new BasicEffect(Application.GraphicsDevice);
            texture = Content.Load<Texture2D>("mona lisa");
            effect.Texture = texture;
            effect.TextureEnabled = true;
            effect.LightingEnabled = true;
            effect.EnableDefaultLighting();

            base.Initialize();
        }

        private void InitializeLevel()
        {
            //1 - add a scene (e.g. a level of the game)
            Scene levelOne = new Scene();

            //2 - add camera
            camera = new GameObject();
            camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
            camera.Transform.SetTranslation(0, 0, 4);
            levelOne.Add(camera);

            //3 - add demo cube
            cube = new GameObject();
            var meshRenderer = new MeshRenderer();
            cube.AddComponent(meshRenderer);
            meshRenderer.Mesh = new CubeMesh();
            levelOne.Add(cube);

            //4 - repeat step 3 for all game objects

            //5 - add scene to scene manager
            sceneManager.Add(levelOne);

            //6 - repeat steps 1 - 5 for each new scene (note, each scene does not need its own camera, we can reuse)
        }

        /// <summary>
        /// Set application data, input, title and scene manager
        /// </summary>
        private void InitialGameCore(string gameTitle, int width, int height)
        {
            //set game title
            Window.Title = gameTitle;

            //instanciate scene manager to store all scenes
            sceneManager = new SceneManager();

            //initialize global application data
            Application.Main = this;
            Application.Content = Content;
            Application.GraphicsDevice = _graphics.GraphicsDevice; //TODO - is this necessary?
            Application.GraphicsDeviceManager = _graphics;
            Application.SceneManager = sceneManager;

            //instanciate screen (singleton) and set resolution etc
            Screen.GetInstance().Set(width, height, true, false);

            //instanciate input components and store reference in Input for global access
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

        #endregion Initialization - Scene manager, Application data, Screen, Input, Scenes, Game Objects

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

            //camera.Update();
            //cube.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var cam = camera.GetComponent<Camera>();
            var meshRenderer = cube.GetComponent<Renderer>() as MeshRenderer;

            effect.World = meshRenderer.GameObject.Transform.WorldMatrix;
            effect.View = cam.ViewMatrix;
            effect.Projection = cam.ProjectionMatrix;
            effect.CurrentTechnique.Passes[0].Apply();

            meshRenderer.Draw(_graphics.GraphicsDevice);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}