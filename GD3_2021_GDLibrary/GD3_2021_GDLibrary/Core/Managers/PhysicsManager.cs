using BEPUphysics;
using Microsoft.Xna.Framework;

namespace GDLibrary.Managers
{
    public class PhysicsManager : GameComponent
    {
        private Space space;

        public Space Space
        {
            get { return space; }
        }

        public PhysicsManager(Game game) : base(game)
        {
            //instanciate a physics space
            space = new Space();

            //set gravity
            space.ForceUpdater.Gravity = new BEPUutilities.Vector3(0, -9.81f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            space.Update();
        }
    }
}