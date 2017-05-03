using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using C_Minor_Scale.RequestObjects;
using C_Minor_Scale.Services;
using C_Minor_Scale.Models;
using System.Threading.Tasks;

namespace C_Minor_Scale.Controllers
{
    public class BookingApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody]CreateBookingRequestObject request)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Booking booking = new Booking
            {
                Owner = request.Owner,
                LastModified = request.LastModified,
                From = request.From,
                Until = request.Until,
                Zid = request.Zid,
                Subject = request.Subject,
                Private = request.Private
            };

            IEnumerable<string> username, password;
            if (!Request.Headers.TryGetValues("idesk-auth-username", out username)) {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_AUTH_CREDENTIALS_MISSING");
            }

            if (!Request.Headers.TryGetValues("idesk-auth-password", out password)) {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_NO_PASSWORD");
            }

            User user = new Models.User
            {
                Username = username.First(),
                PasswordHash = password.First()
            };

            return await BookingServices.PostBooking(user, booking);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}