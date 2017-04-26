using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using C_Minor_Scale.Services;
using C_Minor_Scale.Models;

namespace C_Minor_Scale.Tests.Services
{
    [TestClass]
    public class BookingServicesTests
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public async Task PostBooking_SendCorrectBooking_Success()
        {
            //Arrange


            //Act
            var response = await BookingServices.PostBooking(new User
            {
                Email = "henot294@gmail.com",
                PasswordHash = "10c0968211f1f6b5dd4f50d62cbd10e3c9cd95c3bd8cc19c0bcf836fc23c8c07"
            }, new Booking
            {
                Owner = "henot294@gmail.com",
                LastModified = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                From = (long)(DateTime.UtcNow.AddHours(-1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                Until = (long)(DateTime.UtcNow.AddHours(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                Private = false,
                Subject = "Test",
                Zid = 5996382305910784
            });

            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
