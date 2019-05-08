using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PrintQue.ViewModel
{
    public class UserMainViewModel
    {
        public NavigationCommand NavCommand { get; set; }
        private Printer printer;

        public Printer Printer
        {
            get { return printer; }
            set
            {
                printer = value;
                OnPropertyChanged("User");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserMainViewModel()
        {
            NavCommand = new NavigationCommand(this);
        }

        public void Navigate(Printer printer)
        {
            Request request = new Request()
            {
                Printer = printer,
                PrinterID = printer.ID,
            };
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RequestDetailPage(request, 1));
        }
    }
}
