using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockApi.Models
{
    public class Lock
    {
        public long Id { get; set; }
        public string LockCode{ get; set; }
        public char Status { get; set; }
    }
}
