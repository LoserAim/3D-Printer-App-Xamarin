using PrintQue.Models;
using SQLite;
using System.Collections.Generic;
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


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in await User.GetAll())
            {
                StringList.Add(p.Email);
            }
            User_ListView.ItemsSource = StringList;

        }
        public ListView UserNames { get { return User_ListView; } }
    }
}