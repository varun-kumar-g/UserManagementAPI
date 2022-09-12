using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Data
{
    public class Users
    {
        public User[] GetUsers()
        {
            return new[]
               {
                   new User { Id = 1, FirstName="James", LastName = "Smith", DateOfBirth = "1/2/1981"},
                   new User { Id = 2, FirstName="Christopher", LastName = "Anderson", DateOfBirth = "2/3/1981"  },
                   new User { Id = 3, FirstName="Ronald", LastName = "Clark", DateOfBirth = "Clark"},
                   new User { Id = 4, FirstName="Mary", LastName = "Wright", DateOfBirth = "4/5/1981"},
                   new User { Id = 5, FirstName="Lisa", LastName = "Mitchell", DateOfBirth = "5/6/1981"},
                   new User { Id = 6, FirstName="Michelle", LastName = "Johnson", DateOfBirth = "6/7/1981"},
                   new User { Id = 7, FirstName="John", LastName = "Thomas", DateOfBirth = "7/8/1981"},
                   new User { Id = 8, FirstName="Daniel", LastName = "Rodriguez", DateOfBirth = "8/9/1981"},
                   new User { Id = 9, FirstName="Anthony", LastName = "Lopez", DateOfBirth = "9/10/1981"},
                   new User { Id = 10, FirstName="Patricia", LastName = "Perez", DateOfBirth = "1/10/1975"}
                };
        }
    }
}
