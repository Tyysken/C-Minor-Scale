using C_Minor_Scale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using C_Minor_Scale.Json;
using Newtonsoft.Json.Linq;

namespace C_Minor_Scale.Services
{
    public static class BookingServices
    {
        private const string ApiBaseUrl = "https://stage-booking.intelligentdesk.com/booking";
        private const long teacherId = 6570433172733952;
        private const long studentId = 5124030458232832;

        /// <summary>
        /// Books one zone
        /// </summary>
        /// <param name="user">The user doing the booking</param>
        /// <param name="booking">The booking</param>
        /// <returns>The response message from the Rol API</returns>
        public static async Task<HttpResponseMessage> PostBooking(User user, Booking booking)
        {
            if (await UserServices.GetUserRole(user) == UserServices.Role.Teacher)
            {
                var bookings = GetBookingsByZone(user, booking.Zid, booking.From, booking.Until); 

                // If booker is student cancel the booking

            }

            return await SendBookingToRol(user, booking);
        }

        /// <summary>
        /// Book multiple zones at once. The user must be a teacher
        /// </summary>
        /// <param name="user">The user doing the booking</param>
        /// <param name="bookings">The list of bookings to do</param>
        /// <returns>The list of response messages received from the Rol API</returns>
        public static async Task<List<long>> PostMultipleBookings(User user, List<Booking> bookings)
        {
            List<long> successfulBookings = new List<long>();

            foreach (var booking in bookings)
            {
                var response = await PostBooking(user, booking);
                if (response.IsSuccessStatusCode)
                {
                    successfulBookings.Add(booking.Zid);
                }
            }
            
            return successfulBookings;
        }

        public static async Task<HttpResponseMessage> GetBookings(User user, string uri)
        {
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                response = await httpClient.GetAsync(ApiBaseUrl + uri);
            }

            if (!response.IsSuccessStatusCode)
                return response;

            string content = await response.Content.ReadAsStringAsync();
            // Deserialize to json object
            var jArr = JArray.Parse(content);

            //string js = jArr[0].ToString();
            //var json = JsonConvert.DeserializeObject<BookingJson>(jArr[0].ToString());
            var json = jArr[0].ToObject<BookingJson>();
            // For each booking
                // Get owner
                // Get parent
                // Insert parent
            // Serialize json to string

            response.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

            return response;
        }

        /// <summary>
        /// Send request to delete booking to Rol API
        /// </summary>
        /// <param name="user">The user sending the delete request</param>
        /// <param name="bid">The id of the booking to delete</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CancelBooking(User user, long bid)
        {
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                response = await httpClient.DeleteAsync(ApiBaseUrl + bid);
            }

            return response;
        }

        private static async Task<Booking> GetBooking(User user, long bid)
        {
            Booking booking = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                var response = await httpClient.GetAsync(ApiBaseUrl + bid);

                booking = JsonConvert.DeserializeObject<Booking>(await response.Content.ReadAsStringAsync());
            }

            return booking;
        }

        private static async Task<List<Booking>> GetBookingsByZone(User user, long zid, long from, long until)
        {
            List<Booking> bookings;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                var response = await httpClient.GetAsync(ApiBaseUrl + "?zid=" + zid + "&from=" + from + "&until=" + until);

                bookings = JsonConvert.DeserializeObject<List<Booking>>(await response.Content.ReadAsStringAsync());
            }

            return bookings;
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