using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PrintQue.ViewModel
{
    public class RequestDetailsViewModel : INotifyPropertyChanged
    {
        private bool insert;
        private int statusSwitch;
        
        private UserViewModel _user;
        public UserViewModel user
        {
            get { return _user; }
            set
            {
                _user = value;
                request = new RequestViewModel()
                {
                    User = this.user,
                    UserID = this.user.ID,
                    DateMade = DateTime.Now,
                };
                OnPropertyChanged("user");
            }
        }

        private RequestViewModel _request;
        public RequestViewModel request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged("request");
            }
        }

        private string _projectName;
        public string projectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                request = new RequestViewModel()
                {
                    ProjectName = this.projectName,
                };
                OnPropertyChanged("projectName");
            }
        }

        private string _projectFilePath;
        public string projectFilePath
        {
            get { return _projectFilePath; }
            set
            {
                _projectFilePath = value;
                request = new RequestViewModel()
                {
                    ProjectFilePath = this.projectFilePath,
                };
                OnPropertyChanged("projectFilePath");
            }
        }

        private DateTime _dateTimeRequestSet;
        public DateTime dateTimeRequestSet
        {
            get { return _dateTimeRequestSet; }
            set
            {
                _dateTimeRequestSet = value;
                request = new RequestViewModel()
                {
                    DateRequested = new DateTime(dateTimeRequestSet.Year, dateTimeRequestSet.Month, dateTimeRequestSet.Day),
                };
                OnPropertyChanged("dateTimeRequestSet");
            }
        }

        private Label _lbl_sli_duration;
        public Label lbl_sli_duration
        {
            get { return _lbl_sli_duration; }
            set
            {
                _lbl_sli_duration = value;
                request = new RequestViewModel()
                {
                    Duration = Convert.ToInt32(lbl_sli_duration.Text),
                };
                OnPropertyChanged("lbl_sli_duration");
            }
        }

        private PrinterViewModel _printer;
        public PrinterViewModel printer
        {
            get { return _printer; }
            set
            {
                _printer = value;
                request = new RequestViewModel()
                {
                    Printer = printer,
                    PrinterId = printer.ID,
                };
                OnPropertyChanged("printer");
            }
        }

        private StatusViewModel _status;
        public StatusViewModel status
        {
            get { return _status; }
            set
            {
                _status = value;
                request = new RequestViewModel()
                {
                    Status = status,
                    StatusId = status.ID,
                };
                OnPropertyChanged("status");
            }
        }

        private bool _personalUse;
        public bool personalUse
        {
            get { return _personalUse; }
            set
            {
                _personalUse = value;
                request = new RequestViewModel()
                {
                    PersonalUse = personalUse,
                };
                OnPropertyChanged("personalUse");
            }
        }

        private string _projectDescript;
        public string projectDescript
        {
            get { return _projectDescript; }
            set
            {
                _projectDescript = value;
                request = new RequestViewModel()
                {
                    ProjectDescript = projectDescript,
                };
                OnPropertyChanged("projectDescript");
            }
        }

        public RequestDetailsViewModel(RequestViewModel request = null, int Status = 0)
        {
            _request = request;
            statusSwitch = Status;
            switch (statusSwitch)
            {
                case 0:
                    //Admin insert
                    insert = true;
                    Application.Current.MainPage.ToolbarItems.RemoveAt(1);
                    Application.Current.MainPage.ToolbarItems.RemoveAt(1);
                    break;
                case 1:
                    //User insert
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    insert = true;
                    Application.Current.MainPage.ToolbarItems.RemoveAt(1);
                    Application.Current.MainPage.ToolbarItems.RemoveAt(1);
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
            Application.Current.MainPage.DisplayAlert("Message Clicked!", "W00t!", "OK");
        }
        private async void ToolbarItem_Delete_Activated(object sender, EventArgs e)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("ALERT", "Are you sure you would like to delete this request?", "OK", "Cancel");
            if (answer)
            {
                await RequestViewModel.Remove(_request);

                await Application.Current.MainPage.DisplayAlert("ALERT", "Request Deleted", "OK");

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
                UserID = user.ID,
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
