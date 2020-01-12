using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASG.Objects;

namespace ASG.Interfaces
{
    interface IUserService
    {
        Task<User> Create(User user);
        Task<List<User>> GetList();
        Task<User> GetDetail(string rollNumber);
        Task<User> Update(User user);
        Task<bool> Delete(string rollNumber);
    }
}
