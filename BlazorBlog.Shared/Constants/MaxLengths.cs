using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBlog.Shared.Constants
{
    public static class MaxLengths
    {
        public static class Blogs
        {
            public const int Title = 256;
            public const int Description = 512;
        }

        public static class BlogPosts
        {
            public const int Title = 256;
            public const int Content = 5000;
        }
    }
}
