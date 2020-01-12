using ASG.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASG.Services
{
    class SongService
    {
        private static string CONTENT_TYPE = "application/json";
        private static string GETSONG_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public ObservableCollection<Song> LoadSongs()
        {
            ObservableCollection<Song> list = new ObservableCollection<Song>();
            JObject songInfo = new JObject();
            
            var getSongJson = JsonConvert.SerializeObject(songInfo);
            
            
            return list;
        }
    }
}
