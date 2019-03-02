using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.AdminPages.SelectorPages;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using PrintQue.Widgets.CalendarWidget;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestDetailPage : ContentPage
	{
        private Date _dateRequestSet;
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

			InitializeComponent ();
            if (request == null)
            {
                ToolbarItems.RemoveAt(1);
                ToolbarItems.RemoveAt(1);
            }
            BindingContext = request;
            _request = request;


        }
        private async void SelectFile_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();

                // User cancelled file selection
                if (fileData == null)
                    return;

                SelectedFileLabel.Text = fileData.FileName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception choosing file: " + ex.ToString());
            }
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

            
            
        }
        private void OnDateSubmitted(Date date)
        {
            _dateRequestSet = date;
            PrintTimeLabel.Text = "Print Time: " + date.ToString();
        }
        private async void ScheduleDay_Clicked(object sender, EventArgs e)
        {
            var page = new UserScheduleDayPage();
            page.OnDateSubmitted += OnDateSubmitted;
            await Navigation.PushAsync(page);
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
            page.UserNames.ItemSelected += (source, args) =>
            {
                Users_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }
        async void Status_Selector_Tapped(object sender, EventArgs e)
        {
            var page = new StatusSelectorPage();
            page.StatusNames.ItemSelected += (source, args) =>
            {
                Status_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }
        
        async void PersonalUse_Selector_Tapped(object sender, EventArgs e)
        {
            var page = new PersonalUseSelector();
            page.PersonalUse.ItemSelected += (source, args) =>
            {
                PersonalUse_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }
    }
}