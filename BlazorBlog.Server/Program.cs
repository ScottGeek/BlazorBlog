global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.SqlServer;
global using BlazorBlog.Server.Domain;
global using BlazorBlog.Shared.Blogs;
global using BlazorBlog.Server.Persistence;
using FluentValidation;
using BlazorBlog.Shared.Blogs.Validators;
using BlazorBlog.Server.Slices.Blogs;
using BlazorBlog.Server.Slices.BlogPosts;
using FastEndpoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFastEndpoints(options =>
{
    options.IncludeAbstractValidators = true;
});

builder.Services.AddOpenApi();
builder.Services.AddDbContext<BlogDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDataContext")));
builder.Services.AddValidatorsFromAssemblyContaining<AddOrUpdateBlogValidator>();

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.UseFastEndpoints();

app.Run();
