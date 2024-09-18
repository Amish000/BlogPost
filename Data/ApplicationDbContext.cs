using BlogPost.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogPost.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BlogPosts> BlogPosts { get; set; }
    }
}