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
        public async Task PostBooking_SendCorrectBooking_Success()
        {
            // Är detta äns möjligt att testa?
            //Arrange
            

            //Act
            var response = await BookingServices.PostBooking(new User
            {
                Email = "",
                PasswordHash = ""
            }, new Booking
            {
                Owner = "henot294@gmail.com",
                LastModified = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                From = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                Until = (long)(DateTime.UtcNow.AddHours(2).Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000,
                Private = false,
                Subject = "Test",
                Zid = 6669852505276416
            });

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
