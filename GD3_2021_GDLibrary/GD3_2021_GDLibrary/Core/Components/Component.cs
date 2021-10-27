using Microsoft.Xna.Framework;

namespace GDLibrary.Components
{
    /// <summary>
    /// A part of a game object e.g. Mesh, MeshRenderer, Camera, FirstPersonController
    /// </summary>
    public abstract class Component
    {
        #region Fields

        protected GameObject gameObject;

        #endregion Fields

        #region Properties

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        #endregion Properties

        #region Update

        public virtual void Update()
        {
            //Overridden in child class
        }

        #endregion Update

        #region Add & Get Components

        /// <summary>
        /// Adds a component to the parent GameObject
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>Instance of the new component of type T</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint"/>
        public T AddComponent<T>() where T : Component, new()
        {
            return gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Gets the first component in the parent GameObject of type T
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>First instance of the component  of type T</returns>
        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        /// <summary>
        /// Gets an array of all components in the parent GameObject of type T
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>Array of instanced of the component of type T</returns>
        public T[] GetComponents<T>() where T : Component
        {
            return gameObject.GetComponents<T>();
        }

        #endregion Add & Get Components
    }
}