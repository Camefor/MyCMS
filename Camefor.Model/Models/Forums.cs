using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class Forums
    {
        public long ForumId { get; set; }
        public string ForumName { get; set; }
        public string ForumDescription { get; set; }
        public string ForumLogo { get; set; }
        public long ForumPostCount { get; set; }
        public long ParentForumId { get; set; }
    }
}
