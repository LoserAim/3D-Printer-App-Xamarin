using PrintQue.Helper;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PrintQue.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private UserViewModel user;
        ApiHelper apiHelper;
        public UserViewModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }


        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                User = new UserViewModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    confirmPassword = this.confirmPassword,
                };
                OnPropertyChanged("LastName");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                User = new UserViewModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    confirmPassword = this.confirmPassword,
                };
                OnPropertyChanged("FirstName");
            }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new UserViewModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    confirmPassword = this.confirmPassword,
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        private string confirmpassword;
        private string firstName;
        private string lastName;





        //Important for viewmodels. Notifies Commands of new changes
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        




        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new UserViewModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    confirmPassword = this.confirmPassword,
                };
                OnPropertyChanged("Password");
            }

        }
        public string confirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                User = new UserViewModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password,
                    confirmPassword = this.confirmPassword, 
                };
                OnPropertyChanged("confirmPassword");
            }

        }




        public RegisterViewModel()
        {
            User = new UserViewModel();
            RegisterCommand = new RegisterCommand(this);
            apiHelper = new ApiHelper();
        }

        public async void Register()
        {
            IsBusy = true;
            var response = await apiHelper.RegisterAsync(User);

            if (response)
            {
                var aspuser = (await App.MobileService.GetTable<AspNetUsers>().Where(u => u.Email.Contains(User.Email)).ToListAsync()).FirstOrDefault();
                User.ID = aspuser.ID;
                await UserViewModel.Insert(User);
                IsBusy = false;
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success!", "You have successfully Registered!", "OK");
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
            }

            else
            {
                IsBusy = false;
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Try again", "OK");
            }



        }
    }
}
