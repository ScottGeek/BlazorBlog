namespace BlazorBlog.Server.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; } = null!; // Navigation property to the Blog entity
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

    }
}
