using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Submenus
    {
        public long LinkId { get; set; }
        public long MenuId { get; set; }
        public string LinkName { get; set; }
        public string LinkTarget { get; set; }
        public string LinkOpenWay { get; set; }
        public long ParentLinkId { get; set; }
    }
}
