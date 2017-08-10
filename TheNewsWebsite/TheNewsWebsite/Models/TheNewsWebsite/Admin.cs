using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class Admin
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập!")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-50 ký tự")]
        public string Password { get; set; }
        public string Name { get; set; }
        public int Authority { get; set; }
        
        public bool Status { get; set; }
        public List<Post> Post { get; set; }
        // public virtual ICollection<Post> Publishes { get; set; }
        // public virtual ICollection<Post> Modifies { get; set; }
        //public List<Post> Posts1 { get; set; }
        // public List<Post> ModifiedByAd { get; set; }


    }
}
