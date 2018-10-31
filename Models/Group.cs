using System;
using System.Collections.Generic;

namespace toy.Models
{
    public class Group
    {
        public string Name { get; set; }
        public string Gid { get; set; }
        public List<User> Members { get; set; }
    }
}
