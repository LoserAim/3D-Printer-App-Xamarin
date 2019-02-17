using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50), Unique]
        public string UserName { get; set; }
        public int Admin { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        
        public string Name { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; }

    }
}
