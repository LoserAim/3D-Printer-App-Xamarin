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

        private void ToolbarItem_Plus_Activated(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Add_Printer_Activated(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Add_Request_Activated(object sender, EventArgs e)
        {

        }
    }
}