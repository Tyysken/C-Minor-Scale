using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using C_Minor_Scale.Services;
using C_Minor_Scale.Models;
using System.Net.Http;

namespace C_Minor_Scale.Tests.ServiceTests
{
    [TestClass]
    public class BookingServicesTests
    {
        [TestMethod]
        public async Task GetBookings_ForAllTime_ShouldHaveResponse()
        {
            var user = new User()
            {
                Username = "aa.gpost@gmail.com",
                PasswordHash = "c4a6fe62602ccec204e1afd053dc00d7d18dc92d0ceb2ed0f477f3cc6d310be3"
            };
            string uri = "?from=1495404000000&until=1497218400000&realm=true";
            var response = await Services.BookingServices.GetBookings(user, uri);
            var content = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(response.StatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual("", content);
            Assert.IsTrue(content.Contains("OwnerParent"));
        }
    }
}
