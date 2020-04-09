using System;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public partial class Floors
    {
        public long FloorId { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string FloorContent { get; set; }
        public DateTime? FloorDate { get; set; }
        public long ParentFloorId { get; set; }
    }
}
