using PrintQue.Models;
using PrintQue.ViewModel;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.SelectorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSelectorPage : ContentPage
	{
        public UserSelectorPage()
        {
            InitializeComponent();
        }
        private async void RefreshListView(string searchtext = null)
        {
            var StringList = new List<string>();
            foreach (var p in await UserViewModel.GetAll())
            {
                StringList.Add(p.Email);
            }
            if (searchtext != null)
                StringList = StringList.Where(e => e.Contains(searchtext)).ToList();

            User_ListView.ItemsSource = StringList;


        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListView(e.NewTextValue);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in await UserViewModel.GetAll())
            {
                StringList.Add(p.Email);
            }
            User_ListView.ItemsSource = StringList;

        }
        public ListView UserNames { get { return User_ListView; } }
    }
}