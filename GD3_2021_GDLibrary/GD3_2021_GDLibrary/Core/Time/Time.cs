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
        private float elapsedGameTime;            //time between updates
        private float totalGameTime;            //total elapsed time since start
        private float timeScale = 1;        //0-1 scale factor used to slow down time (e.g. slo-mo effects)

        #endregion Fields

        #region Properties

        public float TimeScale { get => timeScale; set => timeScale = value >= 0 ? value : 1; } //use to scale time for visual effects

        //note we can only GET the times below, we cannot set them
        public float UnscaledElapsedGameTime => elapsedGameTime;        //returns unscaled time between updates in ms
        public float UnscaledTotalTime => totalGameTime;        //returns unscaled total time since game started in ms
        public float ElapsedGameTime => elapsedGameTime * timeScale;    //returns scaled time between updates in ms
        public float TotalGameTime => totalGameTime * timeScale;    //returns scaled total time since game started in ms

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

        public override void Update(GameTime gameTime)
        {
            elapsedGameTime = gameTime.ElapsedGameTime.Milliseconds;
            totalGameTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}