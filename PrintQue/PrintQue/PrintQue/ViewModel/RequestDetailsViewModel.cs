using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PrintQue.ViewModel
{
    public class RequestDetailsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _projectName;
        private string _projectFilePath;
        private DateTime _dateRequested;
        private DateTime _dateMade;
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
        public RequestDetailsViewModel(RequestViewModel request)
        {
            DateMade = request.DateMade;
            DateRequested = request.DateRequested;
            Duration = request.Duration;
            ProjectName = request.ProjectName;
            ProjectDescript = request.ProjectDescript;
            ProjectFilePath = request.ProjectFilePath;
            PersonalUse = request.PersonalUse;
            SaveOrUpdateCommand = new SaveOrUpdateCommand(this);
        }
    }
}
