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
using C_Minor_Scale.Models;

namespace C_Minor_Scale.Tests.ControllerTests
{
    [TestClass]
    public class BookingApiControllerTest
    {
        //private string username = "test@test.com";
        //private string password = "abc123";

        //[TestMethod]
        //public void getLoginInformation_WithValidRequest_ReceiveValidUser()
        //{
        //    //Arrange
        //    HttpRequestMessage Request = new HttpRequestMessage();
        //    Request.Headers.Add("idesk-auth-username", username);
        //    Request.Headers.Add("idesk-auth-password", password);

        //    //Act
        //    User user = BookingApiController.getLoginInformation(Request);

        //    //Assert
        //    Assert.AreEqual(user.Username, username);
        //    Assert.AreEqual(user.PasswordHash, password);
        //}

        //[TestMethod]
        //public void getLoginInformation_WithMissingUsername_UserWithNullUsername()
        //{
        //    //Arrange
        //    HttpRequestMessage Request = new HttpRequestMessage();
        //    Request.Headers.Add("idesk-auth-password", password);

        //    //Act
        //    User user = BookingApiController.getLoginInformation(Request);

        //    //Assert
        //    Assert.IsNull(user.Username);
        //    Assert.AreEqual(user.PasswordHash, password);
        //}

        //[TestMethod]
        //public void geLoginInformation_WithMissingPassword_UserWithNullPassword()
        //{
        //    //Arrange
        //    HttpRequestMessage Request = new HttpRequestMessage();
        //    Request.Headers.Add("idesk-auth-username", username);

        //    //Act
        //    User user = BookingApiController.getLoginInformation(Request);

        //    //Assert
        //    Assert.IsNull(user.PasswordHash);
        //    Assert.AreEqual(user.Username, username);
        //}

        //[TestMethod]
        //public void getLoginInformation_WithNullRequest_NullUser()
        //{
        //    //Arrange


        //    //Act
        //    User user = BookingApiController.getLoginInformation(null);

        //    //Assert
        //    Assert.IsNull(user);
        //}
    }
}
