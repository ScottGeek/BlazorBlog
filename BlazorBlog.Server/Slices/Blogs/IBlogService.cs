using BlazorBlog.Shared.Blogs.Commands;
namespace BlazorBlog.Server.Slices.Blogs
{
    public interface IBlogService
    {
        public Task<List<BlogDto>> GetAllAsync();
        public Task<BlogDto?> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(AddOrUpdateBlogCommand command);
        public Task<bool> UpdateAsync(AddOrUpdateBlogCommand command);
        public Task<bool> DeleteAsync(Guid id);
    }
}
