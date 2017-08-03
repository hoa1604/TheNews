using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class Post
    {
        public int Id { get; set; }       
        [Required(ErrorMessage = "Bạn phải nhập tiêu đề của bản tin!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập mô tả ngắn của bản tin!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập nội dung của bản tin!")]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DatePublish { get; set; }           
        public int NumView { get; set; }
        public string Keyword { get; set; }
        public int Status { get; set; }
        
        public int CategaryChildId { get; set; }
        public CategaryChild CategaryChild { get; set; }
        public int PublishBy { get; set; }
        public Admin Admin { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
