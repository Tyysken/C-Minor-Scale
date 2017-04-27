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
using System.Threading.Tasks;
using System.Net;
using System.Web.Http.Controllers;

namespace C_Minor_Scale.Tests.Controllers
{
    [TestClass]
    public class BookingApiControllerTest
    {
        [TestMethod]
        public async Task Post_CreateBooking_RecieveSuccess()
        {
            // Arrange
            BookingApiController controller = new BookingApiController();
            RequestObjects.CreateBookingRequestObject requestObject = new RequestObjects.CreateBookingRequestObject
            {
                Owner = "test@test.com",
                LastModified = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                From = (long)DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                Until = (long)(DateTimeOffset.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + 60000),
                Zid = 1234,
                Subject = "Test booking",
                Private = true
            };
            var request = new HttpRequestMessage();
            request.Headers.Add("idesk-auth-method", "up");
            request.Headers.Add("idesk-auth-username", "aa.gpost@gmail.com");
            request.Headers.Add("idesk-auth-password", "c4a6fe62602ccec204e1afd053dc00d7d18dc92d0ceb2ed0f477f3cc6d310be3");
            request.Headers.Add("Content-Type", "application/vnd.idesk-v5+json");
            request.Headers.Add("Accept", "application/vnd.idesk-v5+json");
            var controllerContext = new HttpControllerContext();
            controllerContext.Request = request;

            // Act
            controller.ControllerContext = controllerContext;
            HttpResponseMessage result = await controller.Post(requestObject);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public async Task Post_CreateBooking_RecieveError()
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
            };
            // Invalid Zid and no headers

            // Act
            HttpResponseMessage result = await controller.Post(request);

            // Assert
            Assert.AreNotEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
