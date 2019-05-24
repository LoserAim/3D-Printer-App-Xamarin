using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class AspNetUsers
    {
        public string   ID { get; set; }
        public string   UserName { get; set; }
        public string   Email { get; set; }
        public string   NormalizedUserName { get; set; }
        public string   NormalizedEmail { get; set; }
        public bool     EmailConfirmed { get; set; }
        public string   PasswordHash { get; set; }
        public string   SecurityStamp { get; set; }
        public string   ConcurrencyStamp { get; set; }
        public string   PhoneNumber { get; set; }
        public bool     PhoneNumberConfirmed { get; set; }
        public bool     TwoFactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool     LockoutEnabled { get; set; }
        public string   First_Name { get; set; }
        public string   Last_Name { get; set; }
        public int      AccessFailedCount { get; set; }
        public DateTime LatestMessage { get; set; }
    }
}
