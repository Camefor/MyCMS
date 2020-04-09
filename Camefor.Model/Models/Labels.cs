using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class Labels
    {
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        public string LabelAlias { get; set; }
        public string LabelDescription { get; set; }
    }
}
