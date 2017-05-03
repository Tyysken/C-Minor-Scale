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
        [ActionName("DefaultAction")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [ActionName("DefaultAction")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [ActionName("DefaultAction")]
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
            if (!Request.Headers.TryGetValues("idesk-auth-username", out username))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_AUTH_CREDENTIALS_MISSING");
            }

            if (!Request.Headers.TryGetValues("idesk-auth-password", out password))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_NO_PASSWORD");
            }

            User user = new Models.User
            {
                Username = username.First(),
                PasswordHash = password.First()
            };

            return await BookingServices.PostBooking(user, booking);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> Multi([FromBody]CreateBookingMultiRequestObject request)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            IEnumerable<string> username, password;
            if (!Request.Headers.TryGetValues("idesk-auth-username", out username))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_AUTH_CREDENTIALS_MISSING");
            }

            if (!Request.Headers.TryGetValues("idesk-auth-password", out password))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_NO_PASSWORD");
            }

            User user = new User
            {
                Username = username.First(),
                PasswordHash = password.First()
            };

            if (await UserServices.GetUserRole(user) != UserServices.Role.Teacher)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_MISSING_ROLE");
            }

            var bookings = new List<Booking>();
            foreach (long zid in request.Zids)
            {
                Booking booking = new Booking
                {
                    Owner = request.Owner,
                    LastModified = request.LastModified,
                    From = request.From,
                    Until = request.Until,
                    Zid = zid,
                    Subject = request.Subject,
                    Private = request.Private
                };

                bookings.Add(booking);
            }

            var booked = await BookingServices.PostMultipleBookings(user, bookings);

            if (booked.Count == request.Zids.Count)
                return Request.CreateResponse(HttpStatusCode.OK);
            else if (booked.Count == 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "E_MULTI_BOOKING_FAILED");
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "E_MULTI_BOOKING_PARTIALLY_FAILED");
        }

        // PUT api/<controller>/5
        [ActionName("DefaultAction")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [ActionName("DefaultAction")]
        public void Delete(int id)
        {
        }
    }
}