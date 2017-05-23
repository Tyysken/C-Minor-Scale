using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Minor_Scale.Models;
using C_Minor_Scale.Services;
using System.Threading.Tasks;

namespace C_Minor_Scale.Tests.ControllerTests
{
    [TestClass]
    public class UserServicesTest
    {
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

        [TestMethod]
        public async Task GetUserRole_WithStudent_GetStudentRole()
        {
            //Arrange
            User userToSend = new User
            {
                Username = "henot294@gmail.com",
                PasswordHash = "10c0968211f1f6b5dd4f50d62cbd10e3c9cd95c3bd8cc19c0bcf836fc23c8c07"
            };

            //Act
            var role = await UserServices.GetUserRole(userToSend);

            //Assert
            Assert.IsNotNull(role);
            Assert.AreEqual(role, UserServices.Role.Student);
        }

        [TestMethod]
        public async Task GetUserRole_WithTeacher_GetTeacherRole()
        {
            //Arrange
            User userToSend = new User
            {
                Username = "othe1492@student.ju.se",
                PasswordHash = "198feca450b1e906ae42d8057c1769d90ea2bbdf68cc733f104bc85bc52af965"
            };

            //Act
            var role = await UserServices.GetUserRole(userToSend);

            //Assert
            Assert.IsNotNull(role);
            Assert.AreEqual(role, UserServices.Role.Teacher);
        }
    }
}
