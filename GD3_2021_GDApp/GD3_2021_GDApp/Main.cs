using GDLibrary;
using GDLibrary.Components;
using GDLibrary.Core;
using GDLibrary.Graphics;
using GDLibrary.Inputs;
using GDLibrary.Managers;
using GDLibrary.Parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GDApp
{
    public class Main : Game
    {
        private GameObject cObject = null;
        private Curve1D curve1D;

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

        /// <summary>
        /// Quick lookup for all textures used within the game
        /// </summary>
        private Dictionary<string, Texture2D> textureDictionary;

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
            InitializeEngine("My Game Title Goes Here", 1024, 768);

            InitializeDictionaries();

            LoadAssets();

            //level with scenes and game objects
            InitializeLevel();

            //centre the mouse with hardcoded value - remove later
            Input.Mouse.Position = new Vector2(512, 384);

            //DEMO - curve demo
            curve1D = new GDLibrary.Parameters.Curve1D(CurveLoopType.Cycle);
            curve1D.Add(0, 0);
            curve1D.Add(10, 1000);
            curve1D.Add(20, 2000);
            curve1D.Add(40, 4000);
            curve1D.Add(60, 6000);
            // var value = curve1D.Evaluate(500, 2);

            base.Initialize();
        }

        private void InitializeDictionaries()
        {
            textureDictionary = new Dictionary<string, Texture2D>();
        }

        private void LoadAssets()
        {
            LoadTextures();
        }

        private void LoadTextures()
        {
            //debug
            textureDictionary.Add("checkerboard", Content.Load<Texture2D>("Assets/Demo/Textures/checkerboard"));

            //skybox
            textureDictionary.Add("skybox_front", Content.Load<Texture2D>("Assets/Textures/Skybox/front"));
            textureDictionary.Add("skybox_left", Content.Load<Texture2D>("Assets/Textures/Skybox/left"));
            textureDictionary.Add("skybox_right", Content.Load<Texture2D>("Assets/Textures/Skybox/right"));
            textureDictionary.Add("skybox_back", Content.Load<Texture2D>("Assets/Textures/Skybox/back"));
            textureDictionary.Add("skybox_sky", Content.Load<Texture2D>("Assets/Textures/Skybox/sky"));
        }

        private void InitializeLevel()
        {
            //1 - add a scene (e.g. a level of the game)
            Scene levelOne = new Scene("level 1");

            InitializeSkybox(levelOne, 500);

            InitializeCameras(levelOne);

            InitializeCubes(levelOne);

            InitializeModels(levelOne);

            sceneManager.Add(levelOne);

            sceneManager.LoadScene("level 1");
        }

        private void InitializeSkybox(Scene level, float worldScale = 500)
        {
            #region Archetype

            var material = new BasicMaterial("simple diffuse");
            material.Texture = textureDictionary["checkerboard"];
            material.Shader = new BasicShader();

            var archetypalQuad = new GameObject("quad", GameObjectType.Skybox);
            var renderer = new MeshRenderer();
            renderer.Material = material;
            archetypalQuad.AddComponent(renderer);
            renderer.Mesh = new QuadMesh();

            #endregion Archetype

            //back
            GameObject back = archetypalQuad.Clone() as GameObject;
            back.Name = "skybox_back";
            material.Texture = textureDictionary["skybox_back"];
            back.Transform.Translate(0, 0, -worldScale / 2.0f);
            back.Transform.Scale(worldScale, worldScale, null);
            level.Add(back);

            //left
            GameObject left = archetypalQuad.Clone() as GameObject;
            left.Name = "skybox_left";
            material.Texture = textureDictionary["skybox_left"];
            left.Transform.Translate(-worldScale / 2.0f, 0, 0);
            left.Transform.Scale(worldScale, worldScale, null);
            left.Transform.Rotate(0, 90, 0);
            level.Add(left);

            //right
            GameObject right = archetypalQuad.Clone() as GameObject;
            right.Name = "skybox_right";
            material.Texture = textureDictionary["skybox_right"];
            right.Transform.Translate(worldScale / 2.0f, 0, 0);
            right.Transform.Scale(worldScale, worldScale, null);
            right.Transform.Rotate(0, -90, 0);
            level.Add(right);

            //front
            GameObject front = archetypalQuad.Clone() as GameObject;
            front.Name = "skybox_front";
            material.Texture = textureDictionary["skybox_front"];
            front.Transform.Translate(0, 0, worldScale / 2.0f);
            front.Transform.Scale(worldScale, worldScale, null);
            front.Transform.Rotate(0, -180, 0);
            level.Add(front);

            //top
            GameObject top = archetypalQuad.Clone() as GameObject;
            top.Name = "skybox_sky";
            material.Texture = textureDictionary["skybox_sky"];
            top.Transform.Translate(0, worldScale / 2.0f, 0);
            top.Transform.Scale(worldScale, worldScale, null);
            top.Transform.Rotate(90, 0, 0);
            level.Add(top);
        }

        private void InitializeCameras(Scene level)
        {
            var camera = new GameObject("main camera", GameObjectType.Camera);
            camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));

            var controller = new FirstPersonController();
            camera.AddComponent(controller);

            camera.Transform.SetTranslation(0, 0, 15);
            level.Add(camera);
            ////////////////////////////////////////////////////////////////////////////
            //curve camera
            var translationCurve = new Curve3D(CurveLoopType.Oscillate);
            //add points for the curve

            camera = new GameObject("curve camera", GameObjectType.Camera);
            camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
            var curveController = new CurveBehaviour(translationCurve);
            camera.AddComponent(curveController);
            level.Add(camera);
        }

        private void InitializeModels(Scene level)
        {
            #region Archetype

            var material = new BasicMaterial("model material");
            material.Texture = Content.Load<Texture2D>("checkerboard");
            material.Shader = new BasicShader();

            var archetypalSphere = new GameObject("sphere", GameObjectType.Consumable);
            var renderer = new ModelRenderer();
            renderer.Material = material;
            archetypalSphere.AddComponent(renderer);
            renderer.Model = Content.Load<Model>("sphere");

            //downsize the model a little because the sphere is quite large
            archetypalSphere.Transform.SetScale(0.125f, 0.125f, 0.125f);

            #endregion Archetype

            var count = 0;
            for (var i = -8; i <= 8; i += 2)
            {
                var clone = archetypalSphere.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {count++}";
                clone.Transform.SetTranslation(-5, i, 0);
                level.Add(clone);
            }
        }

        private void InitializeCubes(Scene level)
        {
            #region Archetype

            var material = new BasicMaterial("simple diffuse");
            material.Texture = Content.Load<Texture2D>("mona lisa");
            material.Shader = new BasicShader();

            var archetypalCube = new GameObject("cube", GameObjectType.Architecture);
            var renderer = new MeshRenderer();
            renderer.Material = material;
            archetypalCube.AddComponent(renderer);
            renderer.Mesh = new CubeMesh();

            #endregion Archetype

            var count = 0;
            for (var i = 1; i <= 8; i += 2)
            {
                var clone = archetypalCube.Clone() as GameObject;
                clone.Name = $"{clone.Name} - {count++}";
                clone.Transform.SetTranslation(i, 0, 0);
                clone.Transform.SetScale(1, i, 1);
                level.Add(clone);
            }
        }

        //private void InitializeCubes(Scene level)
        //{
        //    //materials define the surface appearance of an object
        //    var material = new BasicMaterial("simple diffuse");  //RE-USE
        //    material.Texture = Content.Load<Texture2D>("mona lisa");  //RE-USE

        //    //shaders draw the object and add lights etc
        //    material.Shader = new BasicShader();  //RE-USE

        //    var count = 1;
        //    for (int i = -20; i <= 20; i += 4)
        //    {
        //        //3 - add demo cube
        //        var cube = new GameObject($"cube{count++}", GameObjectType.Architecture);

        //        cube.Transform.SetTranslation(i, 0, 0);
        //        cube.Transform.Scale(Vector3.One * Math.Abs(i) / 20);

        //        //a renderer draws the object using the model or mesh data
        //        var renderer = new MeshRenderer();
        //        renderer.Material = material;

        //        //add the renderer to the cube or it wont draw anything!
        //        cube.AddComponent(renderer);

        //        //add mesh/model mesh data to the renderer
        //        renderer.Mesh = new CubeMesh();

        //        //add the cube to the level
        //        level.Add(cube);
        //    }
        //}

        //private void InitializeLevel()
        //{
        //    //1 - add a scene (e.g. a level of the game)
        //    Scene levelOne = new Scene("level 1");

        //    #region Camera

        //    //2 - add camera
        //    var camera = new GameObject("main camera");
        //    camera.AddComponent(new Camera(_graphics.GraphicsDevice.Viewport));
        //    var moveKeys = new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D };
        //    var turnKeys = new Keys[] { Keys.J, Keys.L };
        //    camera.AddComponent(new FirstPersonCameraController(moveKeys, turnKeys));
        //    camera.Transform.SetTranslation(0, 0, 4);
        //    levelOne.Add(camera);

        //    #endregion Camera

        //    #region Cube using mesh data

        //    //3 - add demo cube
        //    var cube = new GameObject("cube");

        //    //a renderer draws the object using the model or mesh data
        //    var renderer = new MeshRenderer();

        //    //materials define the surface appearance of an object
        //    var material = new BasicMaterial("simple diffuse");
        //    material.Texture = Content.Load<Texture2D>("mona lisa");

        //    //shaders draw the object and add lights etc
        //    material.Shader = new BasicShader();
        //    renderer.Material = material;

        //    //add the renderer to the cube or it wont draw anything!
        //    cube.AddComponent(renderer);

        //    //add mesh/model mesh data to the renderer
        //    renderer.Mesh = new CubeMesh();

        //    //add the cube to the level
        //    levelOne.Add(cube);

        //    #endregion Cube using mesh data

        //    //4 - repeat step 3 for all game objects

        //    //5 - add scene to scene manager
        //    sceneManager.Add(levelOne);

        //    //6 - repeat steps 1 - 5 for each new scene (note, each scene does not need its own camera, we can reuse)

        //    //7 - very important - set the active scene
        //    sceneManager.LoadScene("level 1");
        //}

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
            var value = curve1D.Evaluate(
                gameTime.TotalGameTime.TotalMilliseconds, 2);
            System.Diagnostics.Debug.WriteLine(
                $"At time {gameTime.TotalGameTime.TotalMilliseconds}" +
                $"the curve evaluates to {value}");

#endif
        }

#if DEBUG

        private void DemoFind()
        {
            //lets look for an object - note - we can ONLY look for object AFTER SceneManager::Update has been called
            if (cObject == null)
                cObject = sceneManager.Find(gameObject => gameObject.Name.Equals("Clone - cube - 2"));

            //the ? is short for (if cObject != null) then...
            cObject?.Transform.Rotate(0, 45 / 60.0f, 0);
        }

#endif

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //render every renderable game object
            renderManager.Render(sceneManager.ActiveScene);

            base.Draw(gameTime);
        }

        #endregion Update & Draw
    }
}