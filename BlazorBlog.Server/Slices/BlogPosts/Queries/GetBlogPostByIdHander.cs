using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Queries
{
    public class GetBlogPostByIdHander(IBlogPostService blogPostService) : EndpointWithoutRequest<BlogPostDto?>
    {
        public override void Configure()
        {
            Get(Shared.Constants.Routes.Api.BlogPosts.GetById);
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var blogId = Route<Guid>("blogId");
            var id = Route<Guid>("id");
            var blogPost = await blogPostService.GetByIdAsync(blogId,id);
            if (blogPost == null)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }


            await SendOkAsync(blogPost, cancellation: cancellationToken);
        }
    }
}
