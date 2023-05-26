using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthApi.Data;
using TokenAuthApi.Models;

namespace TokenAuthApi.UserRepository
{
    public class UserRepo : IDisposable
    {
        ApiDbContext apiDbContext = new ApiDbContext();

       

        public User ValidateUser(string username,string password)
        {
            return apiDbContext.Users.FirstOrDefault(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
        }
        public void Dispose()
        {
            apiDbContext.Dispose();
        }
    }
}