using NUnit.Framework;
using RemoteTestRunner.Subject;

namespace RemoteTestRunner.RunningTests
{
    public class SimpleMathTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Pow3To3Degree_Returns_27()
        {
            // arrange
            var math = new SimpleMath();

            // act
            var number = 3;
            var degree = 3;

            // assert
            Assert.AreEqual(math.Pow(number, degree), 27);
        }
    }
}