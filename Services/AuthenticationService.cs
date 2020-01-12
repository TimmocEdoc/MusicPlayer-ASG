using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASG.Services
{
    class AuthenticationService
    {
        private static string CONTENT_TYPE = "application/json";
        private static string LOGIN_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";

        public async Task<string> LoginTask(string email, string password)
        {
            JObject loginInfo = new JObject();
            loginInfo["email"] = email;
            loginInfo["password"] = password;
            var loginJson = JsonConvert.SerializeObject(loginInfo);
            HttpContent contentToSend = new StringContent(loginJson,
                Encoding.UTF8, CONTENT_TYPE);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(LOGIN_API_URL, contentToSend);
            var stringContent = await response.Content.ReadAsStringAsync();

            //return (string)JObject.Parse(stringContent)["token"];
            Debug.WriteLine(JObject.Parse(stringContent)["error"]);
            return (string)JObject.Parse(stringContent)["token"];
        }
    }
}
