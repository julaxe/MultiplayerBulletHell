using NUnit.Framework;
using UnityEditor.VersionControl;

namespace Tests
{
    public class TestTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FirstTestEver()
        {
            // ARRANGE
            int a = 2;
            int b = 4;

            //ACT
            int total = a + b;

            //ASSERT
            Assert.AreEqual(6, total);
        }

        [Test]
        public void SecondTestEver()
        {
            //Arrange
            int a = 2;
            int b = 4;
            
            //Act
            int total = a * b;
            
            //Assert
            Assert.AreEqual(3, total);
        } 
    }
}
