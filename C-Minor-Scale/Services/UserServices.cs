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
    public class UserServices
    {
        private const string ApiBaseUrl = "https://stage-core.intelligentdesk.com/v3/user/";

        public static async Task<User> GetUser(User user)
        {
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                string apiUrl = ApiBaseUrl + user.Username;
                response = await httpClient.GetAsync(apiUrl);
                var contents = await response.Content.ReadAsStringAsync();
            }

            return user;
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