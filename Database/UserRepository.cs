using EFCoreInMemoryDbDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Database
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers()
        {
            using (var context = new ApiContext())
            {
                var list = context.Users
                    .ToList();
                return list;
            }
        }

        public bool AddUsers(List<User> users)
        {
            using (var context = new ApiContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddUser(User user)
        {
            using (var context = new ApiContext())
            {
                context.Users.AddRange(user);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateUser(User user)
        {
            using (var context = new ApiContext())
            {
                context.Users.Update(user);
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveUser(User user)
        {
            using (var context = new ApiContext())
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveAll()
        {
            using (var context = new ApiContext())
            {
                for(int i = 0;i< this.GetUsers().Count(); i++ )
                {
                    context.Users.Remove(this.GetUsers()[i]);
                }
                
                context.SaveChanges();
                return true;
            }
        }
    }
}
