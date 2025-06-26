using BlazorBlog.Server.Extensions;
using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.Blogs
{
    public class BlogService(ILogger<BlogService> logger, BlogDataContext blogDataContext) : IBlogService
    {

        public async Task<List<BlogDto>> GetAllAsync()
        {

            try
            {
                logger.LogInformation("Fetching all blogs");
                var blogs = await blogDataContext.Blogs
                    .ProjectToDtos()
                    .ToListAsync();
                logger.LogInformation("Fetched {BlogCount} blogs", blogs.Count);
                return blogs;

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An error occurred while fetching all blogs");
                return [];
            }

        }

        public async Task<BlogDto?> GetByIdAsync(Guid id)
        {
            var blog = await blogDataContext.Blogs
                .ProjectToDtos()
                .FirstOrDefaultAsync(b => b.Id == id);
            return blog;
        }

        public async Task<Guid> AddAsync(AddOrUpdateBlogCommand command)
        {

            var blog = command.MapToNewBlog();
            blog.Id = Guid.NewGuid();
            blogDataContext.Blogs.Add(blog);
            await blogDataContext.SaveChangesAsync();

            return blog.Id;

        }

        public async Task<bool> UpdateAsync(AddOrUpdateBlogCommand command)
        {
            var existingBlog = await blogDataContext.Blogs.FindAsync(command.Id);
            if (existingBlog == null)
            {
                return false;
            }
            command.MapToExistingBlog(existingBlog);
            await blogDataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blog = await blogDataContext.Blogs.FindAsync(id);
            if (blog != null)
            {
                blogDataContext.Blogs.Remove(blog);
                await blogDataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
