using PrintQue.Helper;
using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.ViewModel
{
    public class UserViewModel : AspNetUsers
    {
        public string Password { get; set; }
        public string confirmPassword { get; set; }
        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
        public List<RequestViewModel> Requests { get; set; } = new List<RequestViewModel>();
        public int Admin { get; set; }
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
        public static async Task<bool> IsAdmin(UserViewModel user)
        {
            var admin = (await App.MobileService.GetTable<AspNetUserClaims>().Where(c => c.UserId == user.ID).ToListAsync()).FirstOrDefault();
            if(admin != null)
                if (admin.ClaimValue.Contains("Admin"))
                    return true;
            return false;
        }
        public static async Task<int> Login(string email, string password)
        {
            var logger = new UserViewModel()
            {
                Email = email,
            };
            bool canLogin = await ApiHelper.LoginAsync(email, password); // Not Working
            

            if (canLogin)
            {
                var user = await SearchByEmail(email);
                if (user == null)
                    return 0;
                var admin = await IsAdmin(user);
                //admin
                if (admin)
                {

                    App.LoggedInUser = user;
                    App.LoggedInUser.Admin = 1;
                    return 1;
                    
                }
                else
                {

                        App.LoggedInUser = user;
                        return 2;
                }
            }


            
            return 0;
        }

        public static async Task<int> Register(string email, string password, string First_Name, string Last_Name)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);
            bool isFirst_NameEmpty = string.IsNullOrEmpty(First_Name);
            bool isLast_NameEmpty = string.IsNullOrEmpty(Last_Name);
            if (isUsernameEmpty || isPasswordEmpty || isFirst_NameEmpty || isLast_NameEmpty)
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
        private static UserViewModel ReturnUserViewModel(AspNetUsers user)
        {
            var item = new UserViewModel()
            {
                ID = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName,
                NormalizedEmail = user.NormalizedEmail,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                ConcurrencyStamp = user.ConcurrencyStamp,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                AccessFailedCount = user.AccessFailedCount,
                LatestMessage = user.LatestMessage,
            };
            return item;
        }
        private static AspNetUsers ReturnUser(UserViewModel userviewmodel)
        {
            var item = new AspNetUsers()
            {
                ID = userviewmodel.ID,
                UserName = userviewmodel.UserName,
                Email = userviewmodel.Email,
                NormalizedUserName = userviewmodel.NormalizedUserName,
                NormalizedEmail = userviewmodel.NormalizedEmail,
                EmailConfirmed = userviewmodel.EmailConfirmed,
                PasswordHash = userviewmodel.PasswordHash,
                SecurityStamp = userviewmodel.SecurityStamp,
                ConcurrencyStamp = userviewmodel.ConcurrencyStamp,
                PhoneNumber = userviewmodel.PhoneNumber,
                PhoneNumberConfirmed = userviewmodel.PhoneNumberConfirmed,
                TwoFactorEnabled = userviewmodel.TwoFactorEnabled,
                LockoutEnd = userviewmodel.LockoutEnd,
                LockoutEnabled = userviewmodel.LockoutEnabled,
                First_Name = userviewmodel.First_Name,
                Last_Name = userviewmodel.Last_Name,
                AccessFailedCount = userviewmodel.AccessFailedCount,
                LatestMessage = userviewmodel.LatestMessage,
            };
            return item;
        }

        public static async Task UpdateUser(UserViewModel user)
        {
            var us = ReturnUser(user);

                await App.MobileService.GetTable<AspNetUsers>().UpdateAsync(us);


        }

        public static async Task<List<UserViewModel>> GetAll()
        {

            List<UserViewModel> usersviewmodel = new List<UserViewModel>();
            var users = await App.MobileService.GetTable<AspNetUsers>().ToListAsync();
            foreach (var u in users)
            {
                usersviewmodel.Add(ReturnUserViewModel(u));
            }
            //ADD PULL OF FOREIGN KEYS
            return usersviewmodel;
        }
        public static async Task<UserViewModel> SearchByID(string ID)
        {
            var user = (await App.MobileService.GetTable<AspNetUsers>().Where(u => u.ID.Contains(ID)).ToListAsync()).FirstOrDefault();
            return (await GetForeignKeys(ReturnUserViewModel(user)));

        }
        public static async Task<UserViewModel> SearchByEmail(string email)
        {
            try
            {
                AspNetUsers user = (await App.MobileService.GetTable<AspNetUsers>().Where(u => u.Email.Contains(email)).ToListAsync()).FirstOrDefault();
                return ReturnUserViewModel(user);
            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;

            }

        }

    }

}

