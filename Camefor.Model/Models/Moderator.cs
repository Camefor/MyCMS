using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class Moderator
    {
        public long ModeratorId { get; set; }
        public long ForumId { get; set; }
        public string ModeratorLevel { get; set; }
    }
}
