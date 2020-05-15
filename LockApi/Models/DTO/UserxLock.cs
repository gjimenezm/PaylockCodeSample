using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockApi.Models
{
    public class UserxLock
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LockId { get; set; }
        public char Status { get; set; }
    }
}
