using GDLibrary.Core.Scratch;
using NUnit.Framework;

namespace Tests_GD3_2021_GDLibrary
{
    public class TestMath
    {
        [Test]
        public void AddTest()
        {
            var result = MathTest.Add(3, 2);
            Assert.AreEqual(result, -1);
        }
    }
}