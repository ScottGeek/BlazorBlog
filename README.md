# BlazorBlog

A modern blogging application built with Blazor WebAssembly and ASP.NET Core 9, showcasing Vertical Slice Architecture.

## Architecture Overview

BlazorBlog is structured using **Vertical Slice Architecture**, organizing code around business features rather than technical layers. This approach enhances maintainability, testability, and development productivity.

### Project Structure

The solution consists of three main projects:

1. **BlazorBlog.Client**
   - Blazor WebAssembly frontend application
   - Organized by feature areas in Pages folder
   - Communicates with server via HTTP client

2. **BlazorBlog.Server**
   - ASP.NET Core backend API
   - Implements Vertical Slice Architecture with feature slices
   - Uses FastEndpoints for lightweight API endpoints
   - EntityFrameworkCore for data persistence

3. **BlazorBlog.Shared**
   - Shared DTOs, commands, and validation rules
   - Used by both client and server projects

### Vertical Slice Architecture Implementation

Each feature "slice" contains all components required for that feature:
```plaintext
BlazorBlog.Server/
├── Slices/
│   ├── Blogs/
│   │   ├── Commands/           # Command handlers for blog operations
│   │   │   ├── AddBlogHandler.cs
│   │   │   ├── UpdateBlogHandler.cs
│   │   │   └── DeleteBlogHandler.cs
│   │   ├── Queries/            # Query handlers for blog data retrieval
│   │   │   ├── GetAllBlogsHandler.cs
│   │   │   └── GetBlogByIdHandler.cs
│   │   ├── Entities/           # Domain models
│   │   │   └── Blog.cs
│   │   ├── Configurations/     # Entity configurations
│   │   │   └── BlogConfiguration.cs
│   │   ├── BlogService.cs      # Service implementing business logic
│   │   └── IBlogService.cs     # Service interface
│   │
│   └── BlogPosts/
│       ├── Commands/
│       ├── Queries/
│       ├── Entities/
│       └── etc...
```plaintext
### Key Technical Components

1. **CQRS Pattern**
   - Commands and Queries are separated
   - Each handler is focused on a single operation

2. **FastEndpoints**
   - Lightweight alternative to MVC controllers
   - Enables focused, single-responsibility endpoints

3. **Entity Framework Core**
   - SQL Server database
   - Code-first approach with migrations
   - Entity configurations for clean domain models

4. **Mapperly**
   - Source generator-based object mapping
   - Type-safe mapping between entities and DTOs

5. **FluentValidation**
   - Request validation for commands
   - Client and server-side validation

## Domain Model

The application manages two main entities:

1. **Blog**
   - Core entity representing a blog
   - Contains title, description
   - One-to-many relationship with BlogPosts

2. **BlogPost**
   - Represents individual posts within a blog
   - Contains title, content, timestamps
   - Many-to-one relationship with Blog

## Features

- Create, read, update, and delete blogs
- Create, read, update, and delete blog posts within blogs
- Client-side form validation
- Responsive Blazor WebAssembly UI

## Benefits of This Architecture

1. **High Cohesion**
   - Related code is grouped together by feature
   - Easier to understand, maintain, and modify

2. **Low Coupling**
   - Features are isolated from each other
   - Changes to one feature minimally impact others

3. **Developer Productivity**
   - Working on a feature means staying in one directory
   - Less context switching between layers

4. **Testability**
   - Each slice can be tested independently
   - Clear boundaries make writing tests easier

5. **Scalability**
   - New features can be added without modifying existing code
   - Team members can work on different features with minimal conflicts

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (or LocalDB)

### Setup
1. Clone the repository
2. Update the connection string in `appsettings.json` if needed
3. Run Entity Framework migrations:dotnet ef database update --project BlazorBlog.Server4. Run the application:dotnet run --project BlazorBlog.Server
## Technology Stack

- **Frontend**: Blazor WebAssembly (.NET 9)
- **Backend**: ASP.NET Core (.NET 9)
- **API**: FastEndpoints
- **ORM**: Entity Framework Core 9
- **Database**: SQL Server
- **Validation**: FluentValidation
- **Object Mapping**: Mapperly