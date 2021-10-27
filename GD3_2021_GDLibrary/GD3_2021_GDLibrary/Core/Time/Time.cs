using Microsoft.Xna.Framework;
using System;

namespace GDLibrary.Time
{
    /// <summary>
    /// Replaces GameTime for any game objects that want to slow down, or speed up, time in the game
    /// </summary>
    /// <example>
    ///     var time = Time.GetInstance(this);  //In Main.cs we create the first and only instance
    ///     Components.Add(time);               //Add to the component list so that it will be updated
    /// </example>
    /// <seealso cref="https://refactoring.guru/design-patterns/singleton/csharp/example"/>
    public class Time : GameComponent
    {
        #region Fields

        private static Time instance;       //singleton instance to allow global accessibility
        private float deltaTime;            //time between updates
        private float totalGameTime;            //total elapsed time since start
        private float timeScale = 1;        //0-1 scale factor used to slow down time (e.g. slo-mo effects)
        private long frameCount = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Scale at which time passes (0-1)
        /// </summary>
        public float TimeScale { get => timeScale; set => timeScale = value >= 0 ? value : 1; }

        /// <summary>
        /// Unscaled interval in time from the last frame to the current one
        /// </summary>
        public float UnscaledDeltaTime => deltaTime;

        /// <summary>
        /// Scaled interval in time from the last frame to the current one
        /// </summary>
        public float DeltaTime => deltaTime * timeScale;

        /// <summary>
        /// Unscaled time since the game started
        /// </summary>
        public float UnscaledTotalTime => totalGameTime;

        /// <summary>
        /// Scaled time since the game started
        /// </summary>
        public float TotalGameTime => totalGameTime * timeScale;

        /// <summary>
        /// Count of frames since the game started
        /// </summary>
        public long FrameCount => frameCount;

        public static Time Instance
        {
            get
            {
                if (instance == null)
                    throw new NullReferenceException("Instance is null. Has GetInstance been called at startup?");

                return instance;
            }
        }

        #endregion Properties

        #region Constructors

        public static Time GetInstance(Game game)
        {
            if (instance == null)
                instance = new Time(game);

            return instance;
        }

        private Time(Game game) : base(game)
        {
        }

        #endregion Constructors

        #region Update

        public override void Update(GameTime gameTime)
        {
            frameCount++;
            deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            totalGameTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        }

        #endregion Update
    }
}