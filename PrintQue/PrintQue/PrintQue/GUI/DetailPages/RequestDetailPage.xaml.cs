using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.SelectorPages;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using PrintQue.Widgets.CalendarWidget;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestDetailPage : ContentPage
    {
        private DateTime _dateTimeRequestSet;
        private Request _request;
        private bool insert;

        public RequestDetailPage(Request request=null, int status =0)
        {

            InitializeComponent();
            /*
             status:
             0 Admin insert
             1 User insert
             2 Admin Update
             3 User Update
             
             */

            switch (status)
            {
                case 0:

                    insert = true;
                    ToolbarItems.RemoveAt(1);
                    ToolbarItems.RemoveAt(1);
                    break;
                case 1:
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    insert = true;
                    ToolbarItems.RemoveAt(1);
                    ToolbarItems.RemoveAt(1);
                    break;
                case 2:
                    insert = false;
                    break;
                case 3:
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    insert = false;
                    break;

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
           
        }
        private async void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var user = await User.SearchByEmail(Users_Picker.Text);
            var printer = await Printer.SearchByName(Printers_Picker.Text);
            var status = await Status.SearchByName(Status_Picker.Text);
            var request = new Request()
            {
                
                ProjectName = ent_ProjectName.Text,
                //File = _request.File,
                DateRequested = new DateTime(_dateTimeRequestSet.Year, _dateTimeRequestSet.Month, _dateTimeRequestSet.Day),
                Duration = Convert.ToInt32(lbl_sli_duration.Text),
                DateMade = DateTime.Now,
                UserID = user.ID,
                User = user,
                PrinterID = printer.ID,
                Printer = printer,
                StatusID = status.ID,
                Status = status,
                Personal = PersonalUse_Picker.Text,
                Description = edi_Description.Text,
            };
            var exists = await Request.SearchProjectNameByUser(request);
            if (exists == null && insert == true)
            {
                await Request.Insert(request);
                await Navigation.PopAsync();
            }
            else if (!insert)
            {
                request.ID = _request.ID;
                await Request.Update(request);
                await Navigation.PopAsync();

            }
            else if (exists != null && insert == true)
            {

                await DisplayAlert("ERROR", "Project Name already Used. Please choose another", "OK");

            }
            else
            {
                await DisplayAlert("ERROR", "Could not Update Request", "OK");

            }
        }
        private void OnDateTimeSubmitted(DateTime datetime)
        {
            _dateTimeRequestSet = datetime;
            PrintTimeLabel.Text = "Print Time: " + datetime.ToString();
        }
        private async void ScheduleDay_Clicked(object sender, EventArgs e)
        {
            var page = new UserScheduleDateTimePage();

            page.OnDateTimeSubmitted += OnDateTimeSubmitted;

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