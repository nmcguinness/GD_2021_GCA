using System;

namespace GDLibrary.Components
{
    /// <summary>
    /// A part of a game object e.g. Mesh, MeshRenderer, Camera, FirstPersonController
    /// </summary>
    public abstract class Component : IDisposable
    {
        #region Fields

        private bool isStarted;
        private bool isEnabled;
        protected GameObject gameObject;
        protected Transform transform;

        #endregion Fields

        #region Properties

        public bool IsStarted
        {
            get
            {
                return isStarted;
            }
            protected set
            {
                isStarted = value;
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;

                    if (isEnabled)
                        OnEnabled();
                    else
                        OnDisabled();

                    //TODO - notify event enable/disable
                }
            }
        }

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        #endregion Properties

        #region Constructors

        public Component()
        {
            IsStarted = false;
            IsEnabled = true;
        }

        #endregion Constructors

        #region Update

        public virtual void Update()
        {
            //Overridden in child class
        }

        #endregion Update

        #region Actions - Activation

        /// <summary>
        /// Called when the component is first instanciated
        /// </summary>
        public virtual void Awake()
        {
            if (transform == null)
                ///Cache the transform so that we can access in child components without double de-reference e.g. transform.LocalTranslation not gameObject.Transform.LocalTranslation
                transform = gameObject.Transform;
        }

        /// <summary>
        /// Called when the component runs on the first update
        /// </summary>
        public virtual void Start()
        {
            IsStarted = true;
        }

        /// <summary>
        /// Called when the component is enabled (e.g. enable animation on a crane arm in a lab)
        /// </summary>
        protected void OnEnabled()
        {
            //Overridden in child class
        }

        /// <summary>
        /// Called when the component is disabled (e.g. disable rotation on a pickup)
        /// </summary>
        protected virtual void OnDisabled()
        {
            //Overridden in child class
        }

        #endregion Actions - Activation

        #region Actions - Components

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

        #endregion Actions - Components

        #region Housekeeping

        public virtual void Dispose()
        {
            //Overridden in child class
        }

        #endregion Housekeeping

        //TODO - add virtual reset, clone(memberwise) with no code
    }
}