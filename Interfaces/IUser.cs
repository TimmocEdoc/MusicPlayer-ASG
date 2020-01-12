using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASG.Interfaces
{
    interface IUser
    {
        long id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string password { get; set; }
        string address { get; set; }
        string introduction { get; set; }
        string phone { get; set; }
        string avatar { get; set; }
        int gender { get; set; }
        string email { get; set; }
        string birthday { get; set; }
        Dictionary<string, string> CheckValidate();
    }
}
