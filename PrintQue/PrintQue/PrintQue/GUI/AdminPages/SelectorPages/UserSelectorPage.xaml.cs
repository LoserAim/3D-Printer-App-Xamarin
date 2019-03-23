using PrintQue.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.SelectorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSelectorPage : ContentPage
	{
        public UserSelectorPage()
        {
            InitializeComponent();
        }
        List<User> GetUsers()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();


                return conn.Table<User>().ToList();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetUsers())
            {
                StringList.Add(p.Email);
                User_ListView.ItemsSource = StringList;
            }
        }
        public ListView UserNames { get { return User_ListView; } }
    }
}