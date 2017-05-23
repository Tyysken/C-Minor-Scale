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
    public static class UserServices
    {
        public enum Role { Teacher, Student, None };
        private const string ApiBaseUrl = "https://stage-core.intelligentdesk.com/v3/user/";
        private const long teacherId = 6570433172733952;
        private const long studentId = 5124030458232832;

        public static async Task<Role> GetUserRole(User user)
        {
            if(user.Parent == 0)
            {
                user = await GetUser(user);
            }

            switch(user.Parent)
            {
                case teacherId:
                    return Role.Teacher;
                case studentId:
                    return Role.Student;
                default:
                    return Role.None;
            }
        }

        public static async Task<User> GetUser(User user)
        {
            return await getUser(user, user.Username);
        }

        public static async Task<long> GetParent(User user, string username)
        {
            return (await getUser(user, username)).Parent;
        }

        private static async Task<User> getUser(User user, string username)
        {
            HttpResponseMessage response = null;

            using (var httpClient = new HttpClient())
            {
                PrepareHttpClient(httpClient, user);
                string apiUrl = ApiBaseUrl + username;
                response = await httpClient.GetAsync(apiUrl);
                user.Parent = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync()).Parent;
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