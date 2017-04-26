using C_Minor_Scale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace C_Minor_Scale.Services
{
    public static class BookingServices
    {
        public static async Task<HttpResponseMessage> PostBooking(User user, Booking booking)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-method", "up");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-username", user.Email);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("idesk-auth-password", user.PasswordHash);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                    var formContent = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "Owner", booking.Owner },
                        { "From", booking.From.ToString() },
                        { "Until", booking.Until.ToString() },
                        { "LastModified", booking.LastModified.ToString() },
                        { "Private", booking.Private.ToString() },
                        { "Subject", booking.Subject.ToString() },
                        { "Zid", booking.Zid.ToString() }
                    });

                    //response = await httpClient.PostAsJsonAsync("https://stage-booking.intelligentdesk.com/booking/", booking);
                    //response = await httpClient.PostAsync("https://stage-booking.intelligentdesk.com/booking/", formContent);
                }
            }
            catch (Exception e)
            {
                // Returns null
            }

            return response;
        }
    }
}