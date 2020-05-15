using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LockApi.Models;

namespace LockApi.Models
{
    public class LocksContext : DbContext
    {
        public LocksContext(DbContextOptions<LocksContext> options)
            : base(options) { }
        public DbSet<Lock> Locks { get; set; }
        public LocksContext() { }
        public DbSet<LockApi.Models.UserxLock> UserxLock { get; set; }
    }
}
