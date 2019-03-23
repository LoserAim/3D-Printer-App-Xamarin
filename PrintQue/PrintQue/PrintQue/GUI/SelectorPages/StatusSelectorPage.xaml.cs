using PrintQue.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.SelectorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatusSelectorPage : ContentPage
	{
		public StatusSelectorPage ()
		{
			InitializeComponent ();
		}
        List<Status> GetStatuses()
        {
            var status = new List<Status>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {



                status =conn.Table<Status>().ToList();
            }
            return status;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetStatuses())
            {
                StringList.Add(p.Name);
                Status_ListView.ItemsSource = StringList;
            }
        }
        public ListView StatusNames { get { return Status_ListView; } }

    }
}