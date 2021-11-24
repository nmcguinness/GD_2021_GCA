using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Physics;
using Microsoft.Xna.Framework;

namespace GDLibrary.Components
{
    /// <summary>
    /// Provides physics behaviour for the game object
    /// </summary>
    public class ColliderBehaviour : Behaviour
    {
        #region Fields

        /// <summary>
        /// Holds the mass, position, angular momentum, velocity of the collidable object
        /// </summary>
        private Body body;

        /// <summary>
        /// Holds the primitive (e.g. sphere, capsule, box) which tests for collisions
        /// </summary>
        private CollisionSkin collision;

        #endregion Fields

        #region Properties

        public Body Body { get => body; protected set => body = value; }
        public CollisionSkin Collision { get => collision; protected set => collision = value; }

        #endregion Properties

        #region Actions - Physics setup related

        /// <summary>
        /// Called after the component is constructed in the SceneManager to setup physics stuff
        /// </summary>
        public override void Awake()
        {
            //instanciate a new body
            Body = new Body();
            //set the parent gam object to be the attached drawn object (used when collisions occur)
            Body.ExternalData = gameObject;
            //instanciate a collision skin (which will have a primitive added e.g. sphere, capsule, trianglemesh)
            Collision = new CollisionSkin(Body);
            //set the skin as belonging to the body
            Body.CollisionSkin = Collision;
        }

        /// <summary>
        /// Used to add a collision primitive to the collider behaviour
        /// </summary>
        /// <param name="primitive">Primitive</param>
        /// <param name="materialProperties">MaterialProperties</param>
        public virtual void AddPrimitive(Primitive primitive, MaterialProperties materialProperties)
        {
            Collision?.AddPrimitive(primitive, materialProperties);
        }

        /// <summary>
        /// Must be called after we instanciate the new collider behaviour in order for the body to participate in the physics system
        /// </summary>
        /// <param name="bImmovable"></param>
        /// <param name="mass"></param>
        public virtual void Enable(bool bImmovable, float mass)
        {
            //set whether the object can move
            Body.Immovable = bImmovable;
            //calculate the centre of mass
            Vector3 com = SetMass(mass);
            //adjust skin so that it corresponds to the 3D mesh as drawn on screen
            Body.MoveTo(gameObject.Transform.LocalTranslation, Matrix.Identity);
            //set the centre of mass
            Collision.ApplyLocalTransform(new JigLibX.Math.Transform(-com, Matrix.Identity));
            //enable so that any applied forces (e.g. gravity) will affect the object
            Body.EnableBody();
        }

        /// <summary>
        /// Sets the physics mass of the body
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        protected Vector3 SetMass(float mass)
        {
            float junk;
            Vector3 com;
            Matrix it, itCoM;
            PrimitiveProperties primitiveProperties = new PrimitiveProperties(PrimitiveProperties.MassDistributionEnum.Solid, PrimitiveProperties.MassTypeEnum.Density, mass);
            Collision.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCoM);
            Body.BodyInertia = itCoM;
            Body.Mass = junk;
            return com;
        }

        #endregion Actions - Physics setup related
    }
}