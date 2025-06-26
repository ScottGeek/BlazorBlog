using BlazorBlog.Server.Slices.BlogPosts.Queries;
using FastEndpoints;
using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.BlogPosts.Commands
{
    public class AddBlogPostHandler(IBlogPostService blogPostService) : Endpoint<AddOrUpdateBlogPostCommand>
    {

        public override void Configure()
        {
            Post(Shared.Constants.Routes.Api.BlogPosts.Add);
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddOrUpdateBlogPostCommand command, CancellationToken cancellationToken)
        {
            var blogId = Route<Guid>("blogId");
            var id = await blogPostService.AddAsync(command, blogId);
            await SendCreatedAtAsync<GetBlogPostByIdHander>(id, new { blogId, id }, cancellation: cancellationToken);

        }

    }
}
