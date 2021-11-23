using Microsoft.Xna.Framework;

namespace GDLibrary.Core.Physics
{
    public class PhysicsDebugDrawer : DrawableGameComponent
    {
        private Matrix world;
        private Matrix view;
        private Matrix projection;

        public PhysicsDebugDrawer(Game game) : base(game)
        {
        }
    }
}