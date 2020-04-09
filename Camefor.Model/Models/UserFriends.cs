using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class UserFriends
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserFriendsId { get; set; }
        public string UserNote { get; set; }
        public string UserStatus { get; set; }
    }
}
