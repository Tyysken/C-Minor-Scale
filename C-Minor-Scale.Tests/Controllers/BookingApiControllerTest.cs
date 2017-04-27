using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_Minor_Scale;
using C_Minor_Scale.Controllers;

namespace C_Minor_Scale.Tests.Controllers
{
    [TestClass]
    public class BookingApiControllerTest
    {
        [TestMethod]
        public void Post_CreateBooking_RecieveSuccess()
        {
            // Arrange
            BookingApiController controller = new BookingApiController();
            RequestObjects.CreateBookingRequestObject request = new RequestObjects.CreateBookingRequestObject
            {
                Owner = "test@test.com",
                LastModified = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                From = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                Until = (long)(DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + 60000),
                Zid = 1234,
                Subject = "Test booking",
                Private = true
            };

            // Act

            IHttpActionResult result = controller.Post(request);

            // Assert
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public void Post_CreateBooking_Recieve4XX()
        {
            // Arrange
            BookingApiController controller = new BookingApiController();
            RequestObjects.CreateBookingRequestObject request = new RequestObjects.CreateBookingRequestObject
            {
                Owner = "test@test.com",
                LastModified = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                From = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                Until = (long)(DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + 60000),
                Zid = -1,
                Subject = "Test booking",
                Private = true
            }; // Invalid Zid

            // Act

            IHttpActionResult result = controller.Post(request);
            Assert.AreNotEqual(typeof(OkResult), result.GetType());
        }
    }
}
