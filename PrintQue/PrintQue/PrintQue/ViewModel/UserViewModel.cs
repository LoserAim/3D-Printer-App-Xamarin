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
        public string confirmPassword { get; set; }
        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
        public List<RequestViewModel> Requests { get; set; } = new List<RequestViewModel>();

        private static async Task<UserViewModel> GetForeignKeys(UserViewModel userViewModel)
        {

            if (userViewModel.ID != null)
            {
                userViewModel.Messages = await MessageViewModel.SearchByUserID(userViewModel.ID);
                var requests = await RequestViewModel.SearchByUser(userViewModel.ID);
                userViewModel.Requests = requests;
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
                            App.LoggedInUser = user;
                            return 1;
                        }
                        // TODO(VorpW): Assign App.LoggedInUserID when an admin logs in
                    }
                    else
                    {
                        if (user.Password.Contains(password.ToString()))
                        {
                            App.LoggedInUser = user;
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
        private static UserViewModel ReturnUserViewModel(User user)
        {
            var item = new UserViewModel()
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                LatestMessage = user.LatestMessage,
                Admin = user.Admin,
            };
            return item;
        }
        private static User ReturnUser(UserViewModel userviewmodel)
        {
            var item = new User()
            {
                ID = userviewmodel.ID,
                FirstName = userviewmodel.FirstName,
                LastName = userviewmodel.LastName,
                Email = userviewmodel.Email,
                Password = userviewmodel.Password,
                LatestMessage = userviewmodel.LatestMessage,
                Admin = userviewmodel.Admin,
            };
            return item;
        }

        public static async Task Insert(UserViewModel userviewmodel)
        {
            var user = ReturnUser(userviewmodel);
            try
            {
                await App.MobileService.GetTable<User>().InsertAsync(user);
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success", "User was successfully registered!", "OK");
            }
            catch (NullReferenceException nre)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failure", "User failed to be registered", "OK");

            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failure", "User failed to be registered", "OK");

            }
        }

        public static async Task<List<UserViewModel>> GetAll()
        {

            List<UserViewModel> usersviewmodel = new List<UserViewModel>();
            var users = await App.MobileService.GetTable<User>().ToListAsync();
            foreach (var u in users)
            {
                var inser = new UserViewModel()
                {
                    ID = u.ID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    Admin = u.Admin,
                    LatestMessage = u.LatestMessage,

                };
                usersviewmodel.Add(inser);
            }
            //ADD PULL OF FOREIGN KEYS
            return usersviewmodel;
        }
        public static async Task<UserViewModel> SearchByID(string ID)
        {
            User user = (await App.MobileService.GetTable<User>().Where(u => u.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return ReturnUserViewModel(user);

        }
        public static async Task<UserViewModel> SearchByEmail(string email)
        {
            try
            {
                User user = (await App.MobileService.GetTable<User>().Where(u => u.Email.Contains(email)).ToListAsync()).FirstOrDefault();
                return ReturnUserViewModel(user);
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }

        }

    }

}

