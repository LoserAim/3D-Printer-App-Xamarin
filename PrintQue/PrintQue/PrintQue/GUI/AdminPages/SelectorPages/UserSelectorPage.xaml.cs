using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.SelectorPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSelectorPage : ContentPage
	{
        List<User> GetUsers()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();


                return conn.Table<User>().ToList();
            }
        }
        public UserSelectorPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetUsers())
            {
                StringList.Add(p.Name);
                User_ListView.ItemsSource = StringList;
            }
        }
        public ListView PrinterNames { get { return User_ListView; } }
    }
}