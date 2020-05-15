using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LockApi.Models.Contexts
{
    public class UserxLocksContext : DbContext
    {
        public UserxLocksContext(DbContextOptions<UserxLocksContext> options)
            : base(options) { }
        public DbSet<UserxLock> UserxLocks { get; set; }
        public UserxLocksContext() { }
    }
}
