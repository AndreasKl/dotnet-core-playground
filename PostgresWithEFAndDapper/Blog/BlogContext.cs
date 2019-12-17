using Microsoft.EntityFrameworkCore;

namespace PostgresWithEFAndDapper.Blog
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Blog> Blogs { get; set; }
        public DbSet<Domain.Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Blog>().ToTable("blog");
            modelBuilder.Entity<Domain.Post>().ToTable("post");
        }
    }
}