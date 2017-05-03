using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Minor_Scale.Models;
using C_Minor_Scale.Services;
using System.Threading.Tasks;

namespace C_Minor_Scale.Tests.Services
{
    [TestClass]
    public class UserServicesTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public async Task GetUser_WithValidUser_SameUserBack()
        {
            //Arrange
            User originalUser = new User
            {
                Username = "henot294@gmail.com",
                PasswordHash = "10c0968211f1f6b5dd4f50d62cbd10e3c9cd95c3bd8cc19c0bcf836fc23c8c07",
                Parent = 5124030458232832
            };

            User userToSend = new User
            {
                Username = "henot294@gmail.com",
                PasswordHash = "10c0968211f1f6b5dd4f50d62cbd10e3c9cd95c3bd8cc19c0bcf836fc23c8c07"
            };

            //Act
            userToSend = await UserServices.GetUser(userToSend);

            //Assert
            Assert.IsNotNull(userToSend);
            Assert.AreEqual(userToSend.Username, originalUser.Username);
            Assert.AreEqual(userToSend.PasswordHash, originalUser.PasswordHash);
            Assert.AreEqual(userToSend.Parent, originalUser.Parent);
        }
    }
}
