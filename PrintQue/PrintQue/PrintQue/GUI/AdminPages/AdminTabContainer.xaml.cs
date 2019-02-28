using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminTabContainer : TabbedPage
	{
		public AdminTabContainer ()
		{
			InitializeComponent ();

        }

        private void Toolbar_Plus_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Activated", "ToolbarItem Plus Activated", "OK");
        }
        private void Toolbar_Add_Printer_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Activated", "ToolbarItem Printer Activated", "OK");
        }
        private void Toolbar_Add_Request_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Activated", "ToolbarItem Printer Activated", "OK");
        }
    }
}