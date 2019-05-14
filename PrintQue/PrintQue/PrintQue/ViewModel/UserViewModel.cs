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

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
        public List<RequestViewModel> Requests { get; set; } = new List<RequestViewModel>();

        private static async Task<UserViewModel> GetForeignKeys(UserViewModel userViewModel)
        {

            if (userViewModel.ID != null)
            {
                userViewModel.Messages = await MessageViewModel.SearchByUserID(userViewModel.ID);
                var requests = await RequestViewModel.GetAll();
                userViewModel.Requests = requests.Where(r => r.UserID.Contains(userViewModel.ID)).ToList();
            }


            return userViewModel;
        }
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

                var user = await SearchByEmail(email);

                if (user != null)
                {
                    //admin
                    
                    if (user.Admin == 1)
                    {
                        if (user.Password.Contains(password))
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
                ID = userviewmodel.ID,
                FirstName   = userviewmodel.FirstName,
                LastName    = userviewmodel.LastName,
                Email       = userviewmodel.Email,
                Password    = userviewmodel.Password,
                LatestMessage = userviewmodel.LatestMessage,
                Admin = userviewmodel.Admin,
            };
            await App.MobileService.GetTable<User>().InsertAsync(user);
        }

        public static async Task<List<UserViewModel>> GetAll()
        {
            
            List<UserViewModel> usersviewmodel = new List<UserViewModel>();
            var users =await App.MobileService.GetTable<User>().ToListAsync();
            foreach(var u in users)
            {
                var inser = new UserViewModel()
                {
                    ID          = u.ID,
                    FirstName   = u.FirstName,
                    LastName    = u.LastName,
                    Email       = u.Email,
                    Password    = u.Password,
                    Admin       = u.Admin,
                    LatestMessage = u.LatestMessage,

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

