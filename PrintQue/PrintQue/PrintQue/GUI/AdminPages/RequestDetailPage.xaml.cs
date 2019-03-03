using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.AdminPages.SelectorPages;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using PrintQue.Widgets.CalendarWidget;
using SQLite;
using SQLiteNetExtensions.Attributes;
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
        private List<User>    userRequest;
        private List<Printer> printerRequest;
        private List<Status>  statusRequest;
        private void Update_RequestWithChildren(Request request)
        {
            GetChildren();
            var user = userRequest.SingleOrDefault(
                    su => su.Email.Contains(Users_Picker.Text));
            var printer = printerRequest.SingleOrDefault(
                    sp => sp.Name.Contains(Printers_Picker.Text));
             var status = statusRequest.SingleOrDefault(
                    ss => ss.Name.Contains(Status_Picker.Text));
            if (user.Requests == null)
                user.Requests = new List<Request>() { request };
            else
                user.Requests.Add(request);
            if(printer.Requests == null)
                printer.Requests = new List<Request>() { request };
            else
                printer.Requests.Add(request);
            if(status.Requests == null)
                printer.Requests = new List<Request>() { request };
            else
                status.Requests.Add(request);


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Request>();
                conn.Update(request);
                conn.UpdateWithChildren(user);
                conn.UpdateWithChildren(printer);
                conn.UpdateWithChildren(status);
            }
        }
        private void GetChildren()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                conn.CreateTable<Printer>();
                conn.CreateTable<Status>();
                userRequest = conn.Table<User>().ToList();
                printerRequest = conn.Table<Printer>().ToList();
                statusRequest = conn.Table<Status>().ToList();
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
                _request.File = JsonConvert.SerializeObject(fileData);
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
            var request = new Request()
            {
                ProjectName = ent_ProjectName.Text,
                //File = _request.File,
                DateRequested = new DateTime(_dateRequestSet.Year, (int)_dateRequestSet.Month, _dateRequestSet.CalendarDay),
                Duration = Convert.ToInt32(lbl_sli_duration.Text),
                DateMade = DateTime.Now,
                Personal = PersonalUse_Picker.Text,
                Description = edi_Description.Text,
            };

            
            Update_RequestWithChildren(request);

            Navigation.PopAsync();
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