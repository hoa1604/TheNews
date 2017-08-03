using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNewsWebsite.Models.TheNewsWebsite
{
    public class TheNewsContext : DbContext
    {
        public TheNewsContext()
        {

        }
        public TheNewsContext(DbContextOptions<TheNewsContext> options)
            : base(options)
        { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Categary> Categaries { get; set; }
        public DbSet<CategaryChild> CategaryChilds { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }
     
    }
}
