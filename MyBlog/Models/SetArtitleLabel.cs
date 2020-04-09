using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class SetArtitleLabel
    {
        public long ArticleId { get; set; }
        public long LabelId { get; set; }
    }
}
