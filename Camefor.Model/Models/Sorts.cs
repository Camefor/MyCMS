using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class Sorts
    {
        public long SortId { get; set; }
        public string SortName { get; set; }
        public string SortAlias { get; set; }
        public string SortDescription { get; set; }
        public long ParentSortId { get; set; }
    }
}
