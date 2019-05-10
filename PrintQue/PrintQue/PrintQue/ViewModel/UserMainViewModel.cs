using PrintQue.GUI.DetailPages;
using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
=======
>>>>>>> parent of 8180791... Incomplete implementation to viewmodel
=======
>>>>>>> parent of 8180791... Incomplete implementation to viewmodel
=======
>>>>>>> parent of 8180791... Incomplete implementation to viewmodel

namespace PrintQue.ViewModel
{
    public class UserMainViewModel
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        public ICommand CreateRequestDetailsCommand { get; private set; }
        public ICommand SelectPrinterCommand { get; private set; }
        private Printer selectedPrinter;

        public ObservableCollection<Printer> Printers { get;
            private set; } = new ObservableCollection<Printer>();
       

        public Printer SelectedPrinter { get => selectedPrinter;
            set => SetValue(ref selectedPrinter, value); }



        public UserMainViewModel(IPageService pageService)
        {
            var temp = Printer.GetAll().Result;
            Printers = new ObservableCollection<Printer>(temp);
            _pageService = pageService;
            CreateRequestDetailsCommand = new Command<Printer>(async vm => await CreateRequest(vm));
            SelectPrinterCommand = new Command<Printer>(async vm => await SelectPrinter(vm));
           
        }
        private readonly IPageService _pageService;
        private async Task CreateRequest(Printer printer)
        {
            Request request = new Request()
            {
                Printer = printer,
                PrinterID = printer.ID,
                UserID = App.LoggedInUserID,
                User = await User.SearchByID(App.LoggedInUserID),
            };

            await _pageService.PushAsync(new RequestDetailPage(request, 1));
        }

        private async Task SelectPrinter(Printer _printer)
        {
            if (_printer == null)
                return;
            string projects = "This is how many projects are queued: " + _printer.ProjectsQueued;
            // This method should display information important to the user
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Printer Details", projects, "Ok");

            //Code for printer information
        }

=======
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

=======
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

>>>>>>> parent of 8180791... Incomplete implementation to viewmodel
=======
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

>>>>>>> parent of 8180791... Incomplete implementation to viewmodel
        public void Navigate(Printer printer)
        {
            Request request = new Request()
            {
                Printer = printer,
                PrinterID = printer.ID,
            };
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RequestDetailPage(request, 1));
        }
>>>>>>> parent of 8180791... Incomplete implementation to viewmodel
    }
}
