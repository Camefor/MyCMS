using Camefor.Model;
using System;
using System.Collections.Generic;

namespace Camefor.Models
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
        }

        public long UserId { get; set; }
        public string UserIp { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserProfilePhoto { get; set; }
        public string UserLevel { get; set; }
        public string UserRights { get; set; }
        public DateTime? UserRegistrationTime { get; set; }
        public DateTime? UserBirthday { get; set; }
        public byte? UserAge { get; set; }
        public int UserTelephoneNumber { get; set; }
        public string UserNickname { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
    }
}
