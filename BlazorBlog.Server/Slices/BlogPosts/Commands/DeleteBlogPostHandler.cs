using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Commands
{
    public class DeleteBlogPostHandler(IBlogPostService blogPostService) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete(Shared.Constants.Routes.Api.BlogPosts.Delete);
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var blogId = Route<Guid>("blogId");
            var id = Route<Guid>("id");

            var isSuccessfil= await blogPostService.DeleteAsync(id,blogId);
            if (!isSuccessfil)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }
            await SendOkAsync(cancellationToken);
        }
    }
}
