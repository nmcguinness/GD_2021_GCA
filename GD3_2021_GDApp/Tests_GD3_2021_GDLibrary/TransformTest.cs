using GDLibrary.Components;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests_GD3_2021_GDLibrary
{
    public class TransformTest
    {
        [Test]
        public void TransformClonePass()
        {
            var transform = new Transform();
            var clone = transform.Clone() as Transform;

            Assert.NotNull(clone);

            //different memory address
            Assert.AreNotSame(clone, transform);

            //deep copies
            clone.SetTranslation(1, 2, 3);
            Assert.AreEqual(clone.LocalTranslation,
                                new Vector3(1, 2, 3));

            Assert.AreNotEqual(clone.LocalTranslation,
                            transform.LocalTranslation);
        }
    }
}