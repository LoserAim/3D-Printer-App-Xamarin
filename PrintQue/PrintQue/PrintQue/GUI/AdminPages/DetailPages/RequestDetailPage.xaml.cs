using PrintQue.GUI.AdminPages.SelectorPages;
using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.DetailPages

{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestDetailPage : ContentPage
	{
        private Request _request;

        private void Update_Request(Request request)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.Update(request);
                


            }
        }
        public RequestDetailPage (Request request)
		{
            if (request == null)
            {
                ToolbarItems.RemoveAt(1);
                ToolbarItems.RemoveAt(2);
            }
            BindingContext = request;
            _request = request;
			InitializeComponent ();


		}
        private void ToolbarItem_Message_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Message Clicked!", "W00t!", "OK");
        }
        private void ToolbarItem_Delete_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Delete Clicked!", "W00t!", "OK");
        }
        private void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            DisplayAlert("Alert!", "Request has been updated!", "OK");
            _request.ProjectName = ent_ProjectName.Text;
            _request.Description = edi_Description.Text;
            
            
        }


        async void Printer_Selector_Tapped(object sender, EventArgs e)
        {
            var page = new PrinterSelectorPage();
            page.PrinterNames.ItemSelected += (source, args) =>
            {
                Printers_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }
        async void User_Selector_Tapped(object sender, EventArgs e)
        {
            var page = new UserSelectorPage();
            page.PrinterNames.ItemSelected += (source, args) =>
            {
                Users_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }


    }
}