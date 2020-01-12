using ASG.Objects;
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
    class AddSongService
    {
        private static string ADDSONG_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private static string CONTENT_TYPE = "application/json";
        public async Task<Song> Create(Song song)
        {
            var studentJson = JsonConvert.SerializeObject(song);
            HttpContent contentToSend = new StringContent(studentJson,
                Encoding.UTF8, CONTENT_TYPE);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(ADDSONG_API_URL, contentToSend);
            var stringContent = await response.Content.ReadAsStringAsync();
            var returnSong = JsonConvert.DeserializeObject<Song>(stringContent);
            Debug.WriteLine("Status: " + JObject.Parse(stringContent)["status"]);
            Debug.WriteLine("Status: " + JObject.Parse(stringContent)["error"]);
            return returnSong;
        }
    }
}
