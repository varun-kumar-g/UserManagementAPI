using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Database
{
    public interface IUserRepository
    {   
        public List<User> GetUsers();

        public bool AddUsers(List<User> users);

        public bool AddUser(User user);

        public bool UpdateUser(User user);

        public bool RemoveUser(User user);

        public bool RemoveAll();
    }
}
