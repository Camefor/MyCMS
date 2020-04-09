using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Posts
    {
        public long PostId { get; set; }
        public long ForumId { get; set; }
        public long UserId { get; set; }
        public string PostTitle { get; set; }
        public long PostViews { get; set; }
        public string PostContent { get; set; }
        public DateTime? PostDate { get; set; }
        public string PostStatus { get; set; }
        public long PostCommentCount { get; set; }
    }
}
