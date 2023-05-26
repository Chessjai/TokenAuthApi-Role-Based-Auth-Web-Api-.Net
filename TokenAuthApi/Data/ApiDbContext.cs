using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TokenAuthApi.Models;

namespace TokenAuthApi.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext() : base("dbcs")
        { }
       public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }

}