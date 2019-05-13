using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class UserViewModel : User
    {

        public List<Message> Messages { get; set; } = new List<Message>();
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
        public static async Task Insert(UserViewModel userviewmodel)
        {
            var user = new User()
            {
                FirstName   = userviewmodel.FirstName,
                LastName    = userviewmodel.LastName,
                Email       = userviewmodel.Email,
                Password    = userviewmodel.Password,

            };
            await App.MobileService.GetTable<User>().InsertAsync(user);
        }

        public static async Task<List<UserViewModel>> GetAll()
        {
            List<User> users = new List<User>();
            List<UserViewModel> usersviewmodel = new List<UserViewModel>();
            users =await App.MobileService.GetTable<User>().ToListAsync();
            foreach(var u in users)
            {
                var inser = new UserViewModel()
                {
                    FirstName   = u.FirstName,
                    LastName    = u.LastName,
                    Email       = u.Email,
                    Password    = u.Password,
                };
                usersviewmodel.Add(inser);
            }
            //ADD PULL OF FOREIGN KEYS
            return usersviewmodel;
        }
        public static async Task<UserViewModel> SearchByID(string ID)
        {
            List<UserViewModel> users = await GetAll();
            return users.FirstOrDefault(u => u.ID == ID);

        }
        public static async Task<UserViewModel> SearchByEmail(string email)
        {
            List<UserViewModel> users = await GetAll();
            return users.FirstOrDefault(u => u.Email.Contains(email));

        }

    }

}

