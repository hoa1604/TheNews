using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public List<Post> Posts { get; set; }
    }
}
