namespace BlazorBlog.Server.Persistence
{
    public class BlogDataContext(DbContextOptions<BlogDataContext> options) : DbContext(options)
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
