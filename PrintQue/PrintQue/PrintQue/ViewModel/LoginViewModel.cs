using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrintQue.Models;
using PrintQue.ViewModel.Commands;




namespace PrintQue.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
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
                User = new User()
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
                User = new User()
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
            User = new User();
            LoginCommand = new LoginCommand(this);
        }
        public async void Login()
        {
            int canLogin = await UserViewModel.Login(User.Email, user.Password);
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
