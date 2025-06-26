using BlazorBlog.Server.Extensions;
using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.BlogPosts
{
    public class BlogPostService(ILogger<BlogPostService> logger, BlogDataContext blogDataContext) : IBlogPostService
    {
        public async Task<List<BlogPostDto>> GetAllAsync(Guid blogId)
        {

            try
            {
                logger.LogInformation("Fetching all blog posts for blog {BlogId}", blogId);
                var posts = await blogDataContext.BlogPosts
                    .Where(x => x.BlogId == blogId)
                    .ProjectToDtos()
                    .ToListAsync();
                logger.LogInformation("Fetched {PostCount} posts for blog {BlogId}", posts.Count, blogId);
                return posts;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An error occurred while fetching blog posts for blog {BlogId}", blogId);
                return [];
            }

        }
        public async Task<BlogPostDto?> GetByIdAsync(Guid blogId, Guid id)
        {
            try
            {
                logger.LogInformation("Fetching blog post {PostId} for blog {BlogId}", id, blogId);
                var post = await blogDataContext.BlogPosts
                    .Where(x => x.BlogId == blogId && x.Id == id)
                    .ProjectToDtos()
                    .FirstOrDefaultAsync();

                if (post == null)
                {
                    logger.LogWarning("Blog post {PostId} not found for blog {BlogId}", id, blogId);
                    return null;
                }

                logger.LogInformation("Fetched blog post {PostId} for blog {BlogId}", id, blogId);
                return post;

            }
            catch (Exception ex)
            {
                
                logger.LogError(ex, "An error occurred while fetching blog post {PostId} for blog {BlogId}", id, blogId);
                return null;
            }

        }
        public async Task<Guid> AddAsync(AddOrUpdateBlogPostCommand command, Guid blogId)
        {

            try
            {
                logger.LogInformation("Adding new blog post for blog {BlogId}", blogId);
                if (command == null)
                {
                    logger.LogError("AddOrUpdateBlogPostCommand is null");
                    //throw new ArgumentNullException(nameof(command), "Blog post cannot be null");
                }
                var post = command!.MapToNewBlogPost();
                post.Id = Guid.NewGuid();
                post.BlogId = blogId;
                post.CreatedAt = DateTimeOffset.UtcNow;
                post.UpdatedAt = post.CreatedAt;
                blogDataContext.BlogPosts.Add(post);
                await blogDataContext.SaveChangesAsync();
                logger.LogInformation("Added new blog post with ID {PostId} for blog {BlogId}", post.Id, blogId);
                return post.Id;

            }
            catch (Exception ex)
            {
                // Log the exception and rethrow or handle it as needed              
                logger.LogError(ex, "An error occurred in the AddAsync method for blog {BlogId}", blogId);
            }

            return Guid.Empty; // Return an empty GUID or handle as appropriate
        }
        public async Task<bool> UpdateAsync(AddOrUpdateBlogPostCommand command, Guid blogId)
        {

            try
            {
                logger.LogInformation("Updating blog post {PostId} for blog {BlogId}", command.Id, blogId);
                var existingPost = await blogDataContext.BlogPosts
                    .FirstOrDefaultAsync(p => p.Id == command.Id && p.BlogId == blogId);
                if (existingPost == null)
                {
                    logger.LogWarning("Blog post {PostId} not found for blog {BlogId}", command.Id, blogId);
                    return false;
                }
                command.MapToExistingBlogPost(existingPost);
                existingPost.UpdatedAt = DateTimeOffset.UtcNow;
                await blogDataContext.SaveChangesAsync();
                logger.LogInformation("Updated blog post {PostId} for blog {BlogId}", command.Id, blogId);
                return true;

            }
            catch (Exception ex)
            {
                // Log the exception and return false or handle it as needed              
                logger.LogError(ex, "An error occurred while updating blog post {PostId} for blog {BlogId}", command.Id, blogId);
            }
            // Log the exception and return false or handle it as needed
            logger.LogError("An error occurred while updating blog post {PostId} for blog {BlogId}", command.Id, blogId);
            return false; // Return false if the update fails

        }
        public async Task<bool> DeleteAsync(Guid id, Guid blogId)
        {

            try
            {
                logger.LogInformation("Deleting blog post {PostId} for blog {BlogId}", id, blogId);
                var post = await blogDataContext.BlogPosts
                    .FirstOrDefaultAsync(p => p.Id == id && p.BlogId == blogId);
                if (post != null)
                {
                    blogDataContext.BlogPosts.Remove(post);
                    await blogDataContext.SaveChangesAsync();
                    logger.LogInformation("Deleted blog post {PostId} for blog {BlogId}", id, blogId);
                    return true;
                }
                logger.LogWarning("Blog post {PostId} not found for blog {BlogId}- Not Deleted.", id, blogId);
                return false;

            }
            catch (Exception ex)
            {               
                logger.LogError(ex, "An error occurred while deleting blog post {PostId} for blog {BlogId}- Not Deleted.", id, blogId);   
            }
            return false; // Return false if the deletion fails
        }

    }
}
