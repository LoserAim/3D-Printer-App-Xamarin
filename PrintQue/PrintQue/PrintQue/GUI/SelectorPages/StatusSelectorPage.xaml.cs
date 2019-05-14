using PrintQue.Models;
using PrintQue.ViewModel;
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


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in await StatusViewModel.GetAll())
            {
                StringList.Add(p.Name);
                
            }
            Status_ListView.ItemsSource = StringList;
        }
        public ListView StatusNames { get { return Status_ListView; } }

    }
}