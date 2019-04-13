using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.SelectorPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColorSelectorPage : ContentPage
	{
		public ColorSelectorPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in await PrintColor.GetAll())
            {
                StringList.Add(p.Name);
                
            }
            Color_ListView.ItemsSource = StringList;
        }
        public ListView ColorNames { get { return Color_ListView; } }
    }
}