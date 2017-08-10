using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập địa chỉ Email!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6-50 ký tự")]
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
