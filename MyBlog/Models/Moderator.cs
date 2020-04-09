using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Moderator
    {
        public long ModeratorId { get; set; }
        public long ForumId { get; set; }
        public string ModeratorLevel { get; set; }
    }
}
