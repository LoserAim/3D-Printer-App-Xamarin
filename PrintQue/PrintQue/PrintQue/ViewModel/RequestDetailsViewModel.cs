using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.UserPages;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PrintQue.ViewModel
{
    public class RequestDetailsViewModel : INotifyPropertyChanged
    {
        private DateTime _dateTimeRequestSet;
        public bool insert;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _projectName;
        private string _projectFilePath;
        private DateTime _dateRequested;
        private DateTime _dateMade;
        private string _selectedFileText;


        private string _projectDescript;
        private bool _personalUse;
        private double _duration;
        private UserViewModel _user;
        private PrinterViewModel _printer;
        private StatusViewModel _status;
        private RequestViewModel _request;
        public UserViewModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public PrinterViewModel Printer
        {
            get { return _printer; }
            set
            {
                _printer = value;
                OnPropertyChanged("Printer");
            }
        }
        public StatusViewModel Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        public string SelectedFileText
        {
            get { return _selectedFileText; }
            set
            {
                _selectedFileText = value;
                OnPropertyChanged("SelectedFileText");
            }
        }
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged("ProjectName");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public string ProjectFilePath
        {
            get { return _projectFilePath; }
            set
            {
                _projectFilePath = value;
                OnPropertyChanged("ProjectFilePath");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public DateTime DateRequested
        {
            get { return _dateRequested; }
            set
            {
                _dateRequested = value;
                OnPropertyChanged("DateRequested");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public DateTime DateMade
        {
            get { return _dateMade; }
            set
            {
                _dateMade = value;
                OnPropertyChanged("DateMade");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public string ProjectDescript
        {
            get { return _projectDescript; }
            set
            {
                _projectDescript = value;
                OnPropertyChanged("ProjectDescript");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public bool PersonalUse
        {
            get { return _personalUse; }
            set
            {
                _personalUse = value;
                OnPropertyChanged("PersonalUse");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }
        public double Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
                Request = new RequestViewModel()
                {
                    DateMade = this.DateMade,
                    DateRequested = this.DateRequested,
                    Duration = this.Duration,
                    ProjectName = this.ProjectName,
                    ProjectDescript = this.ProjectDescript,
                    ProjectFilePath = this.ProjectFilePath,
                    PersonalUse = this.PersonalUse,
                    User = this.User,
                    Printer = this.Printer,
                    Status = this.Status,
                };
            }
        }




        public RequestViewModel Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged("Request");
            }
        }
        public SaveOrUpdateCommand SaveOrUpdateCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }
        public PushChatPagesCommand PushChatPagesCommand { get; set; }
        public FilePickerCommand FilePickerCommand { get; set; }
        public SetDateCommand SetDateCommand { get; set; }
        public RequestDetailsViewModel(RequestViewModel request)
        {
            
            DateMade = request.DateMade;
            DateRequested = request.DateRequested;
            Duration = request.Duration;
            ProjectName = request.ProjectName;
            ProjectDescript = request.ProjectDescript;
            ProjectFilePath = request.ProjectFilePath;
            PersonalUse = request.PersonalUse;
            Request = request;
            Status = request.Status;
            Printer = request.Printer;
            User = request.User;

            PrintTimeLabel = "Print Time: " + DateMade.ToString();
            SaveOrUpdateCommand = new SaveOrUpdateCommand(this);
            DeleteCommand = new DeleteCommand(this);
            PushChatPagesCommand = new PushChatPagesCommand(this);
            FilePickerCommand = new FilePickerCommand(this);
            SetDateCommand = new SetDateCommand(this);
        }
        public async void SaveData()
        {
            var user = await UserViewModel.SearchByEmail(User.Email);
            var printer = await PrinterViewModel.SearchByName(Printer.Name);
            var status = await StatusViewModel.SearchByName(Status.Name);
            var request = Request;
            request.User = user;
            request.Printer = printer;
            request.Status = status;
            request.ApplicationUserId = user.ID;
            request.PrinterId = printer.ID;
            request.StatusId = status.ID;
            var exists = await RequestViewModel.SearchProjectNameByUser(request);
            if (exists == null && insert == true)
            {
                await RequestViewModel.Insert(request);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

            }
            else if (!insert)
            {
                request.ID = exists.ID;
                await RequestViewModel.Update(request);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

            }
            else if (exists != null && insert == true)
            {

                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", "Project Name already Used. Please choose another", "OK");

            }
            else
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", "Could not save details of Request", "OK");

            }
        }
        public void DeleteData()
        {

        }
        internal async void ExecutFilePicker()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (!fileData.FileName.Contains(".stl"))
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error!", "You can only submit .stl files! Please pick a file type of .stl!", "OK");
                    return;
                }
                
                // User cancelled file selection
                if (fileData == null)
                    return;
                string text = File.ReadAllText(fileData.FilePath);
                ProjectFilePath = text;
                SelectedFileText = fileData.FileName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
        internal async void OpenDatePicker()
        {
            var page = new UserScheduleDateTimePage();

            page.OnDateTimeSubmitted += OnDateTimeSubmitted;

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(page);
        }
        private void OnDateTimeSubmitted(DateTime datetime)
        {
            DateMade = datetime;
            PrintTimeLabel = "Print Time: " + datetime.ToString();
        }
        private string _printTimeLabel;
        public string PrintTimeLabel
        {
            get { return _printTimeLabel; }
            set
            {
                _printTimeLabel = value;
                OnPropertyChanged("PrintTimeLabel");
            }
        }
    }
}
