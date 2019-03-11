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
	public partial class PersonalUseSelector : ContentPage
	{
		public PersonalUseSelector ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>()
            {
                "true",
                "false",
            };
            PersonalUse_ListView.ItemsSource = StringList;

        }
        public ListView PersonalUse { get { return PersonalUse_ListView; } }
    }
}