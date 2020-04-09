using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Comments
    {
        public long CommentId { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }
        public long CommentLikeCount { get; set; }
        public DateTime? CommentDate { get; set; }
        public string CommentContent { get; set; }
        public long ParentCommentId { get; set; }
    }
}
