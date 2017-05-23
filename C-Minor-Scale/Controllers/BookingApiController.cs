using C_Minor_Scale.Models;
using C_Minor_Scale.RequestObjects;
using C_Minor_Scale.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace C_Minor_Scale.Controllers
{
    public class BookingApiController : ApiController
    {
        [ActionName("DefaultAction")]
        public async Task<HttpResponseMessage> Get()
        {
            return await BookingServices.GetBookings(getLoginInformation(Request), Request.RequestUri.Query);
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

            User user = getLoginInformation(Request);

            return await BookingServices.PostBooking(user, booking);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> Multi([FromBody]CreateBookingMultiRequestObject request)
        {
            User user = getLoginInformation(Request);

            if (await UserServices.GetUserRole(user) != UserServices.Role.Teacher)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "E_MISSING_ROLE");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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

        public static User getLoginInformation(HttpRequestMessage Request)
        {
            if(Request == null)
            {
                return null;
            }

            User user = new User
            {
                Username = null,
                PasswordHash = null
            };

            IEnumerable<string> username, password;
            if (Request.Headers.TryGetValues("idesk-auth-username", out username))
            {
                user.Username = username.First();
            }

            if (Request.Headers.TryGetValues("idesk-auth-password", out password))
            {
                user.PasswordHash = password.First();
            }

            return user;
        }
    }
}