using BlazorBlog.Shared.Constants;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorBlog.Server.Persistence.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(MaxLengths.Blogs.Title);

            builder.Property(b => b.Description)
                            .IsRequired()
                            .HasMaxLength(MaxLengths.Blogs.Description);

            builder.ToTable("Blogs");
        }
    }
}
