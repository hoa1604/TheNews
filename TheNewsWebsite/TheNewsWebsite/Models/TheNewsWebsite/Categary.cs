using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class Categary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<CategaryChild> CategaryChilds { get; set; }
        public List<Post> Posts { get; set; }
    }
}
