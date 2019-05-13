using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using SQLiteNetExtensionsAsync.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PrintQue.Models
{
    public class User 
    {
        [PrimaryKey]
        public string ID { get; set; }

        [MaxLength(50), Unique]
        public string Email { get; set; }



        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public int Admin { get; set; }



        public string Password { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Message> Messages { get; set; } = new List<Message>();


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Request> Requests { get; set; } = new List<Request>();


        public static async Task<int> Login(string email, string password)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);
            if (isUsernameEmpty || isPasswordEmpty)
            {
                //then show error
                return 0;
            }
            else
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    //admin
                    if (user.Admin == 1)
                    {
                        if (user.Password.Contains(password.ToString()))
                        {
                            App.LoggedInUserID = user.ID;
                            return 1;
                        }
                        // TODO(VorpW): Assign App.LoggedInUserID when an admin logs in

                    }
                    else
                    {

                        if (user.Password.Contains(password.ToString()))
                        {
                            App.LoggedInUserID = user.ID;
                            return 2;
                        }




                    }
                }

            }
            return 0;
        }

        public static async Task<int> Register(string email, string password, string firstname, string lastname)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);
            bool isFirstNameEmpty = string.IsNullOrEmpty(firstname);
            bool isLastNameEmpty = string.IsNullOrEmpty(lastname);
            if (isUsernameEmpty || isPasswordEmpty || isFirstNameEmpty || isLastNameEmpty)
            {
                //then show error
                return 0;
            }
            else
            {
                if (email.Contains("@") || email.Contains(".com") || email.Contains(".edu"))
                {
                    var user = await SearchByEmail(email.ToString());
                    if (user == null)
                    {
                        return 1;
                    }
                }
   
                return 0;

            }
        }
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
            users = await conn.GetAllWithChildrenAsync<User>();
            return users;
        }
        public static async Task<User> SearchByID(string ID)
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
