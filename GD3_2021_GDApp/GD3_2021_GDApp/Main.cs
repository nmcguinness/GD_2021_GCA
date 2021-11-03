using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Graphics;
using GDLibrary.Inputs;
using GDLibrary.Managers;
using GDLibrary.Time;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        /// <summary>
        /// Renders all game objects with an attached and enabled renderer
        /// </summary>
        private RenderManager renderManager;

        private GameObject cObject;

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
            InitializeEngine("My Game Title Goes Here", 1920, 1080);

            //level with scenes and game objects
            InitializeLevel();

            base.Initialize();
        }

        private void InitializeLevel()
        {
            //1 - add a scene (e.g. a level of the game)
            Scene levelOne = new Scene("level 1");

            #region Camera

            //2 - add camera
            var camera = new GameObject("main camera");
            camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
            var moveKeys = new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D };
            var turnKeys = new Keys[] { Keys.J, Keys.L };
            camera.AddComponent(new FirstPersonCameraController(moveKeys, turnKeys));
            camera.Transform.SetTranslation(0, 0, 4);
            levelOne.Add(camera);

            #endregion Camera

            #region Cube using mesh data

            //3 - add demo cube
            var cube = new GameObject("cube");

            //a renderer draws the object using the model or mesh data
            var renderer = new MeshRenderer();

            //materials define the surface appearance of an object
            var material = new BasicMaterial("simple diffuse");
            material.Texture = Content.Load<Texture2D>("mona lisa");

            //shaders draw the object and add lights etc
            material.Shader = new BasicShader();
            renderer.Material = material;

            //add the renderer to the cube or it wont draw anything!
            cube.AddComponent(renderer);

            //add mesh/model mesh data to the renderer
            renderer.Mesh = new CubeMesh();

            //add the cube to the level
            levelOne.Add(cube);

            #endregion Cube using mesh data

            //4 - repeat step 3 for all game objects

            //5 - add scene to scene manager
            sceneManager.Add(levelOne);

            //6 - repeat steps 1 - 5 for each new scene (note, each scene does not need its own camera, we can reuse)

            //7 - very important - set the active scene
            sceneManager.LoadScene("level 1");
        }

        /// <summary>
        /// Set application data, input, title and scene manager
        /// </summary>
        private void InitializeEngine(string gameTitle, int width, int height)
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

            //instanciate render manager to render all drawn game objects
            renderManager = new RenderManager();

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

            //update every updateable game object
            //Q. what would happen is we commented out this line?
            sceneManager.Update();

#if DEBUG
            DemoFind();
#endif
        }

        private void DemoFind()
        {
            //lets look for an object - note - we can ONLY look for object AFTER SceneManager::Update has been called
            if (cObject == null)
                cObject = sceneManager.Find(gameObject => gameObject.Name.Equals("cube"));

            //the ? is short for (if cObject != null) then...
            cObject?.Transform.Rotate(0, 45 / 60.0f, 0);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render every renderable game object
            renderManager.Render(sceneManager.ActiveScene);

            //var cam = camera.GetComponent<Camera>();
            //var meshRenderer = cube.GetComponent<Renderer>() as MeshRenderer;

            //effect.World = meshRenderer.GameObject.Transform.WorldMatrix;
            //effect.View = cam.ViewMatrix;
            //effect.Projection = cam.ProjectionMatrix;
            //effect.CurrentTechnique.Passes[0].Apply();

            //meshRenderer.Draw(_graphics.GraphicsDevice);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}