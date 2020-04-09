using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Articles
    {
        public long ArticleId { get; set; }
        public long UserId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public long ArticleViews { get; set; }
        public long ArticleCommentCount { get; set; }
        public DateTime? ArticleDate { get; set; }
        public long ArticleLikeCount { get; set; }

        public virtual Users User { get; set; }
    }
}
