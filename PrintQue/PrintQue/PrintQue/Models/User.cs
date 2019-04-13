using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }



        [MaxLength(50), Unique]
        public string Email { get; set; }


        [MaxLength(50)]
        public string Name { get; set; }
        public int Admin { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public static async Task<int> Insert(User user)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            var rows = await conn.InsertAsync(user);
            
            return rows;
        }
        
        public static async Task<List<User>> GetAll()
        {
            List<User> users = new List<User>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.CreateTableAsync<User>();

            users = await conn.Table<User>().ToListAsync();

            
            return users;
        }
        public static async Task<User> SearchByID(int ID)
        {
            List<User> users = await GetAll();
            return users.FirstOrDefault(u => u.ID == ID);

        }
        public static async Task<User> SearchByEmail(string email)
        {
            List<User> users = await GetAll();
            return users.FirstOrDefault(u => u.Email.Contains(email));

        }

    }
}
