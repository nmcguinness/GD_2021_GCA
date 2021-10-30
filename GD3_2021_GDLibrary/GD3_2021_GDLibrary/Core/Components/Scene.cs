using System.Collections.Generic;

namespace GDLibrary.Components
{
    /// <summary>
    /// Actions that we can apply to a game object in a list
    /// </summary>
    public enum ComponentChangeType
    {
        Add, Update, Remove
    }

    /// <summary>
    /// Stores and updates all game objects for a level or part of a level
    /// </summary>
    public class Scene : GameObject
    {
        #region Statics

        private static Scene current;

        #endregion Statics

        #region Fields

        protected string name;
        protected List<GameObject> gameObjects;
        protected List<Renderer> renderers;
        protected List<Camera> cameras;

        #endregion Fields

        #region Properties

        public static Scene Current { get => current; set => current = value; }
        public string Name { get => name; set => name = value; }

        #endregion Properties

        #region Constructors

        public Scene()
        {
            //TODO - add default readonly variables instead of magic numbers!
            gameObjects = new List<GameObject>(10);
            renderers = new List<Renderer>(10);
            cameras = new List<Camera>(4);
        }

        #endregion Constructors

        #region Update

        public override void Update()
        {
            base.Update();

            //TODO - Add remove of destroyed objects

            for (int i = 0; i < gameObjects.Count; i++)
            {
                //do not update something that is disabled (e.g. a controller for a door that wont be enabled until X time or we have Y key)
                if (gameObjects[i].IsEnabled)
                    gameObjects[i].Update();
            }
        }

        #endregion Update

        #region Actions

        public void Add(GameObject gameObject)
        {
            //add to the list of objects in the scene
            gameObjects.Add(gameObject);

            //set the scene that object belongs to
            gameObject.Scene = this;

            //TODO - add root transform set and notify change
            //gameObject.Transform.Root = _transform;

            //if object enabled then add to appropriate list
            if (gameObject.IsEnabled)
                CheckComponents(gameObject, ComponentChangeType.Add);

            //if object isn't initialized then init
            if (!gameObject.IsInitialized)
                gameObject.Initialize();
        }

        protected void CheckComponents(GameObject gameObject, ComponentChangeType type)
        {
            for (int i = 0; i < gameObject.Components.Count; i++)
            {
                var component = gameObject.Components[i];

                if (component is Renderer)
                {
                    var renderable = component as Renderer;

                    if (type == ComponentChangeType.Add)
                        AddRenderer(renderable);
                    else if (type == ComponentChangeType.Remove)
                        RemoveRenderer(renderable);
                }
                else if (component is Camera)
                {
                    var camera = component as Camera;

                    if (type == ComponentChangeType.Add && !cameras.Contains(camera))
                        AddCamera(camera);
                    else if (type == ComponentChangeType.Remove)
                        RemoveCamera(camera);
                }
            }
        }

        protected int AddCamera(Camera camera)
        {
            var index = cameras.IndexOf(camera);

            if (index == -1)
            {
                cameras.Add(camera);
                cameras.Sort();
                index = cameras.Count - 1;

                if (Camera.Main == null)
                    Camera.Main = camera;
            }

            return index;
        }

        protected void RemoveCamera(Camera camera)
        {
            if (cameras.Contains(camera))
                cameras.Remove(camera);
        }

        protected void AddRenderer(Renderer renderer)
        {
            if (renderers.Contains(renderer))
                return;

            renderers.Add(renderer);
            renderers.Sort();
        }

        protected void RemoveRenderer(Renderer renderable)
        {
            if (renderers.Contains(renderable))
                renderers.Remove(renderable);
        }

        public virtual void Unload()
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.Dispose();

            //TODO - add Dispose for materials, behaviours etc

            Clear();
        }

        protected void Clear()
        {
            gameObjects.Clear();
            renderers.Clear();
            cameras.Clear();
        }

        #endregion Actions

        //TODO - Clear, Dispose
    }
}