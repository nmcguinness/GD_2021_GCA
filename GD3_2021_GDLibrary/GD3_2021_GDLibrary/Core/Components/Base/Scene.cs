using GDLibrary.Components;
using System.Collections.Generic;

namespace GDLibrary.Core.Components.Base
{
    public class Scene : GameObject
    {
        protected List<GameObject> gameObjects;
        protected List<Renderer> renderers;

        public static Scene Current { get; internal set; }

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

        //TODO - Clear, Dispose
    }
}