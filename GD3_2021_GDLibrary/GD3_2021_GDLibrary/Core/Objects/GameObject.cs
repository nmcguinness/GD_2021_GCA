﻿using GDLibrary.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    public enum GameObjectType : sbyte //0 - 255
    {
        Camera = 5, Player = 1, NPC = 2, Interactable = 3, Consumable = 4, Architecture = 0
    }

    /// <summary>
    /// Base object in the game scene
    /// </summary>
    public class GameObject : IDisposable
    {
        #region Statics

        private static readonly int DEFAULT_COMPONENT_LIST_SIZE = 4;    //typical minimum number of components added to a GameObject

        #endregion Statics

        #region Fields

        //Type, Tag, LayerMask, ID
        protected GameObjectType gameObjectType;

        public GameObjectType GameObjectType { get; protected set; }

        /// <summary>
        /// Friendly name for the current object
        /// </summary>
        protected string name;

        /// <summary>
        /// Set on first update of the component in SceneManager::Update
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Set in constructor to true. By default all components are enabled on instanciation
        /// </summary>
        private bool isEnabled;

        /// <summary>
        /// Scene to which this game object belongs
        /// </summary>
        protected Scene scene;

        /// <summary>
        /// Stores S, R, T of GameObject to generate the world matrix
        /// </summary>
        protected Transform transform;

        /// <summary>
        /// List of all attached components
        /// </summary>
        protected List<Component> components;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets/sets the game object name
        /// </summary>
        public string Name { get => name; set => name = value.Trim(); }

        /// <summary>
        /// Gets boolean to determine if the object has run in first update cycle
        /// </summary>
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        /// <summary>
        /// Gets/sets boolean to allow object to be disabled (e.g. no update or draw) within a scene
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;

                    //TODO - notify component has been enabled and enable all child components
                }
            }
        }

        /// <summary>
        /// Gets/sets the scene that the current object is used in
        /// </summary>
        public Scene Scene
        {
            get { return scene; }
            set { scene = value; }
        }

        /// <summary>
        /// Gets/sets the transform associated with the current game object
        /// </summary>
        public Transform Transform { get => transform; protected set => transform = value; }

        /// <summary>
        /// Gets a list of all components (e.g. controllers, behaviours, camera) of the current object
        /// </summary>
        public List<Component> Components { get => components; }

        #endregion Properties

        #region Constructors

        public GameObject(string name,
            GameObjectType gameObjectType = GameObjectType.Architecture)
        {
            Name = name;
            this.gameObjectType = gameObjectType;
            if (transform == null)
            {
                components = new List<Component>(DEFAULT_COMPONENT_LIST_SIZE);          //instanciate list
                transform = new Transform();                                            //add default transform
                transform.GameObject = this;                                            //tell transform who it belongs to
                components.Add(transform);                                              //add transform to the list
            }

            isEnabled = true;
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

            //set this as component's parent game object
            component.GameObject = this;

            //perform any initial wake up operations
            component.Awake();

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