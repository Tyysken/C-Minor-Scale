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

        public static async Task<HttpResponseMessage> PostBooking(User user, Booking booking)
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
            client.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-username", user.Email);
            client.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-password", user.PasswordHash);
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.idesk-v5+json");
        }
    }
}