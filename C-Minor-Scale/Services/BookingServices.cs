using C_Minor_Scale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace C_Minor_Scale.Services
{
    public static class BookingServices
    {
        private const string ApiBaseUrl = "https://stage-booking.intelligentdesk.com/booking/";
        private const long teacherId = 6570433172733952;
        private const long studentId = 5124030458232832;

        public static async Task<HttpResponseMessage> PostBooking(User user, Booking booking)
        {
            // TODO: Check if user is high prio, check if zone is booked. Cancel booking if user is of higher prio
            return await SendBookingToRol(user, booking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bookings"></param>
        /// <returns>
        /// Success when all bookings worked
        /// Partial when some of the bookings worked
        /// Failed when none of the bookings worked
        /// </returns>
        public static async Task<string> PostMultipleBookings(User user, List<Booking> bookings)
        {
            // TODO: For each booking in the list run PostBooking
            return "Not yet implemented";
        }

        private static async Task<HttpResponseMessage> CancelBookingAtRol(User user)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.NotImplemented;
            return response;
        }

        private static async Task<HttpResponseMessage> SendBookingToRol(User user, Booking booking)
        {
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(booking),
                    UTF8Encoding.UTF8,
                    "application/vnd.idesk-v5+json"
                    );

                response = await httpClient.PostAsync(ApiBaseUrl, content);
            }

            return response;
        }

        private static void PrepareHttpClient(HttpClient client, User user)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-method", "up");
            client.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-username", user.Username);
            client.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-password", user.PasswordHash);
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.idesk-v5+json");
        }
    }
}