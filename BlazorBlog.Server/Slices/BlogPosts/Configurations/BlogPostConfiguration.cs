using BlazorBlog.Shared.Constants;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorBlog.Server.Persistence.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(MaxLengths.BlogPosts.Title);

            builder.Property(b => b.Content)
                            .IsRequired()
                            .HasMaxLength(MaxLengths.BlogPosts.Content);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.UpdatedAt)
                .IsRequired();

            builder.HasOne(x => x.Blog)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.BlogId)
                .IsRequired();

            builder.ToTable("BlogPosts");
        }
    }
}
