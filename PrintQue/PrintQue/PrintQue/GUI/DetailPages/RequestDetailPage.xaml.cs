using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.SelectorPages;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using PrintQue.ViewModel;
using PrintQue.Widgets.CalendarWidget;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestDetailPage : ContentPage
    {
        private DateTime _dateTimeRequestSet;
        private RequestViewModel _request;
        private bool insert;
        private int _status;


        public RequestDetailPage(RequestViewModel request =null, int Status =0)
        {

            InitializeComponent();
            BindingContext = request;
            _request = request;
            _status = Status;
            _dateTimeRequestSet = DateTime.Now;
            switch (_status)
            {
                case 0:
                    //Admin insert
                    insert = true;
                    ToolbarItems.RemoveAt(1);
                    ToolbarItems.RemoveAt(1);
                    break;
                case 1:
                    //User insert
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    insert = true;
                    ToolbarItems.RemoveAt(1);
                    ToolbarItems.RemoveAt(1);
                    break;
                case 2:
                    //Admin Edit
                    insert = false;

                    break;
                case 3:
                    //User Edit
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    insert = false;
                    break;

            }


        }

        private async void SelectFile_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if(!fileData.FileName.Contains(".stl"))
                {
                    await DisplayAlert("Error!","You can only submit .stl files! Please pick a file type of .stl!", "OK");
                }
                string text = File.ReadAllText(fileData.FilePath);
                // User cancelled file selection
                if (fileData == null)
                    return;
                _request.ProjectFilePath = text;
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
        private async void ToolbarItem_Delete_Activated(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("ALERT", "Are you sure you would like to delete this request?", "OK", "Cancel");
            if(answer)
            {
                await RequestViewModel.Remove(_request);

                await DisplayAlert("ALERT", "Request Deleted", "OK");
 
            }
        }
        private async void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var user = await UserViewModel.SearchByEmail(Users_Picker.Text);
            var printer = await PrinterViewModel.SearchByName(Printers_Picker.Text);
            var status = await StatusViewModel.SearchByName(Status_Picker.Text);
            var request = new RequestViewModel()
            {

                ProjectName = ent_ProjectName.Text,
                ProjectFilePath = _request.ProjectFilePath,
                DateRequested = new DateTime(_dateTimeRequestSet.Year, _dateTimeRequestSet.Month, _dateTimeRequestSet.Day),
                Duration = Convert.ToInt32(lbl_sli_duration.Text),
                DateMade = DateTime.Now,
                ApplicationUserId = user.ID,
                User = user,
                PrinterId = printer.ID,
                Printer = printer,
                StatusId = status.ID,
                Status = status,
                PersonalUse = Convert.ToBoolean(PersonalUse_Picker.Text),
                ProjectDescript = edi_Description.Text,
            };
            var exists = await RequestViewModel.SearchProjectNameByUser(request);
            if (exists == null && insert == true)
            {
                await RequestViewModel.Insert(request);
                await Navigation.PopAsync();

            }
            else if (!insert)
            {
                request.ID = exists.ID;
                await RequestViewModel.Update(request);
                await Navigation.PopAsync();

            }
            else if (exists != null && insert == true)
            {

                await DisplayAlert("ERROR", "Project Name already Used. Please choose another", "OK");

            }
            else
            {
                await DisplayAlert("ERROR", "Could not save details of Request", "OK");

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