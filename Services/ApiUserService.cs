using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ASG.Interfaces;
using ASG.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASG.Services
{
    class ApiUserService : IUserService
    {
        private static string REGISTER_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members";
        private static string CONTENT_TYPE = "application/json";
        public async Task<User> Create(User user)
        {
                var studentJson = JsonConvert.SerializeObject(user);
                HttpContent contentToSend = new StringContent(studentJson,
                    Encoding.UTF8, CONTENT_TYPE);
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.PostAsync(REGISTER_API_URL, contentToSend);
                var stringContent = await response.Content.ReadAsStringAsync();
                var returnUser = JsonConvert.DeserializeObject<User>(stringContent);
                Debug.WriteLine("ID: "+JObject.Parse(stringContent)["id"]);
                Debug.WriteLine("Status: "+JObject.Parse(stringContent)["status"]);
                Debug.WriteLine("Status: "+JObject.Parse(stringContent)["error"]);
                return returnUser;
        }

        public Task<bool> Delete(string rollNumber)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetDetail(string rollNumber)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
