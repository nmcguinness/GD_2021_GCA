using GDLibrary.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    /// <summary>
    /// Base object in the game scene
    /// </summary>
    public class GameObject : IDisposable
    {
        #region Fields

        private static readonly int DEFAULT_COMPONENT_LIST_SIZE = 4;    //typical minimum number of components added to a GameObject
        protected Transform transform;                                  //stores S, R, T of GameObject to generate the world matrix
        protected List<Component> components;                           //a list of all attached components
        private bool isInitialized;

        #endregion Fields

        #region Properties

        public Transform Transform { get => transform; protected set => transform = value; }
        public List<Component> Components { get => components; }

        #endregion Properties

        #region Constructors

        public GameObject()
        {
            if (transform == null)
            {
                components = new List<Component>(DEFAULT_COMPONENT_LIST_SIZE);          //instanciate list
                transform = new Transform();                                            //add default transform
                transform.GameObject = this;                                            //tell transform who it belongs to
                components.Add(transform);                                              //add transform to the list
            }
        }

        #endregion Constructors

        #region Initialization

        /// <summary>
        /// Called when the game object is run in the first update
        /// </summary>
        public virtual void Initialize()
        {
            if (!isInitialized)
            {
                isInitialized = true;

                //TODO - Add sort IComparable in each component
                components.Sort();

                for (int i = 0; i < components.Count; i++)
                    components[i].Start();
            }
        }

        #endregion Initialization

        #region Update

        /// <summary>
        /// Called each update to call an update on all components of the game object
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Update();
        }

        #endregion Update

        #region Add & Get Components

        /// <summary>
        /// Adds a sppecific instance of a component
        /// Note - We cannot add a second Transform to any GameObject
        /// </summary>
        /// <typeparam name="T">Instance of a Component</typeparam>
        /// <returns>Instance of the component</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint"/>
        public Component AddComponent(Component component)
        {
            if (component == null || component is Transform)
                return null;

            component.GameObject = this;

            //TODO - prevent duplicate components? Component::Equals and GetHashCode need to be implemented
            components.Add(component);

            return component;
        }

        /// <summary>
        /// Adds a component of type T
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>Instance of the new component of type T</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint"/>
        public T AddComponent<T>() where T : Component, new()
        {
            var component = new T();

            return (T)AddComponent(component);
        }

        /// <summary>
        /// Gets the first component of type T
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>First instance of the component  of type T</returns>
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                    return components[i] as T;
            }

            return null;
        }

        /// <summary>
        /// Gets an array of all components of type T
        /// </summary>
        /// <typeparam name="T">Instance of type Component</typeparam>
        /// <returns>Array of instanced of the component of type T</returns>
        public T[] GetComponents<T>() where T : Component
        {
            List<T> componentList = new List<T>();

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                    componentList.Add(components[i] as T);
            }

            return componentList.ToArray();
        }

        #endregion Add & Get Components

        #region Housekeeping

        public virtual void Dispose()
        {
            foreach (Component component in components)
                component.Dispose();
        }

        #endregion Housekeeping
    }
}