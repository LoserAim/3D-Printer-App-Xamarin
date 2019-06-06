using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintQue.Helper;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using Xamarin.Essentials;

namespace PrintQue.ViewModel
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool rememberMe;
        public bool RememberMe
        {
            get { return rememberMe; }
            set
            {
                rememberMe = value;
                OnPropertyChanged("RememberMe");
            }
        }
        private UserViewModel user;

        public UserViewModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public LoginCommand LoginCommand { get; set; }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new UserViewModel()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new UserViewModel()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public LoginViewModel()
        {
            User = new UserViewModel();
            LoginCommand = new LoginCommand(this);
        }
        private async void RememberUser()
        {
            if (RememberMe)
            {
                Preferences.Set("RememberMe", true);
                try
                {
                    await SecureStorage.SetAsync("User_Email", Email);
                    await SecureStorage.SetAsync("User_Password", Password);
                }
                catch (Exception)
                {
                    // Possible that device doesn't support secure storage on device.
                }
            }
            else
            {
                Preferences.Set("RememberMe", false);
                SecureStorage.Remove("User_Email");
                SecureStorage.Remove("User_Password");
            }
        }
        public async void Login()
        {
            IsLoading = true;

            if(Preferences.Get("RememberMe", false))
            {
                Email = await SecureStorage.GetAsync("User_Email");
                Password = await SecureStorage.GetAsync("User_Password");
            }
            int canLogin = await UserViewModel.Login(Email, Password);

            switch (canLogin)
            {
                case 0:
                    IsLoading = false;
                    break;
                case 1:
                    RememberUser();
                    IsLoading = false;
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new UserTabContainer());
                    //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new AdminTabContainer());
                    break;
                case 2:
                    RememberUser();
                    IsLoading = false;
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new UserTabContainer());
                    break;
            }
        }
    }
}
