using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class Comment
    {
        public int Id { get; set; }     
        public string Cmt { get; set; }
        public DateTime CmtDate { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
