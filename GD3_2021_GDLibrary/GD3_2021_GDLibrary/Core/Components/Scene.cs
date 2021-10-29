using System.Collections.Generic;

namespace GDLibrary.Components
{
    public class Scene : GameObject
    {
        private static Scene current;
        protected string name;
        protected List<GameObject> gameObjects;
        protected List<Renderer> renderers;

        public static Scene Current { get => current; set => current = value; }
        public string Name { get => name; set => name = value; }

        public Scene()
        {
            //TODO - add default readonly variables instead of magic numbers!
            gameObjects = new List<GameObject>(10);
            renderers = new List<Renderer>(10);
        }

        public override void Update()
        {
            base.Update();

            //TODO - Add remove of destroyed objects

            for (int i = 0; i < gameObjects.Count; i++)
            {
                //TODO - test for enabled
                gameObjects[i].Update();
            }
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
            //TODO - AddComponent clear for all other lists
        }

        //TODO - Clear, Dispose
    }
}