
using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SQLiteNetExtensionsAsync.Extensions;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrintQue.GUI.DetailPages;
using PrintQue.ViewModel;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTabContainer : TabbedPage
    {

        public AdminTabContainer()
        {
            InitializeComponent();
            

        }

        private void ToolbarItem_Plus_Activated(object sender, EventArgs e)
        {
            var request = new RequestViewModel();
            request = null;
            Navigation.PushAsync(new RequestDetailPage(request));
        }

        private void ToolbarItem_Add_Printer_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrinterDetailPage());
        }

        private void ToolbarItem_Add_Request_Activated(object sender, EventArgs e)
        {
            var request = new RequestViewModel();
            request = null;
            Navigation.PushAsync(new RequestDetailPage(request));
        }

        async private void ToolbarItem_Run_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "You are about to logout. Are you sure?", "Yes", "No");
            if (response)

            {
                App.LoggedInUserID = null;

                await Navigation.PopAsync();
            }
        }




        async private void ToolbarItem_Add_Color_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrintColorDetailPage());

        }

        async private void ToolbarItem_Add_Status_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusDetailPage());

        }
    }
}