﻿using BlazorBlog.Shared.Blogs.Commands;
using FastEndpoints;


namespace BlazorBlog.Server.Slices.Blogs.Commands
{
    public class DeleteBlogHandler(IBlogService blogService) : EndpointWithoutRequest
        {

        public override void Configure()
        {
            Delete(Shared.Constants.Routes.Api.Blogs.Delete);
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var id = Route<Guid>("id");
            var isSuccessfil = await blogService.DeleteAsync(id);

            if (!isSuccessfil)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            await SendOkAsync(cancellationToken);
        }
    }
}
