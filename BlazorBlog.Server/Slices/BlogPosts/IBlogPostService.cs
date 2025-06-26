using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.BlogPosts
{
    public interface IBlogPostService
    {
        Task<List<BlogPostDto>> GetAllAsync(Guid blogId);
        Task<BlogPostDto?> GetByIdAsync(Guid blogId, Guid id);
        Task<Guid> AddAsync(AddOrUpdateBlogPostCommand command, Guid blogId);
        Task<bool> UpdateAsync(AddOrUpdateBlogPostCommand command, Guid blogId);
        Task<bool> DeleteAsync(Guid id, Guid blogId);
    }
}
