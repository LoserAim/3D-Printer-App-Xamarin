using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PrintQue.ViewModel
{
    public class LoginViewModel
    {
        private User user;
        public User User { get => user;
            set
            {
                user = value;
                OnpropertyChanged("User");
            }
        }
        public LoginCommand loginCommand { get; set; }
        private string email;
        public string Email { get => email;
            set
            {
                email = value;
                user = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                };
                OnpropertyChanged("Email");
            }
        }
        public string Password { get => password;
            set
            {
                password = value;
                user = new User()
                {
                    Email = this.Email,
                    Password = this.Password,
                };
                OnpropertyChanged("Password");
            }
        }

        private string password;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnpropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public LoginViewModel()
        {
            User = new User();
            loginCommand = new LoginCommand(this);
        }
        public async void Login()
        {
            int canLogin = await User.Login(User.Email, user.Password);
            switch (canLogin)
            {
                case 0:
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Try again", "OK");
                    break;
                case 1:
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new AdminTabContainer());
                    break;
                case 2:
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new UserTabContainer());
                    break;
            }
        }
    }
}
