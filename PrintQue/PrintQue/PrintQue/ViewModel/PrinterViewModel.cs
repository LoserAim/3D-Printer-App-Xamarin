using PrintQue.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.ViewModel
{
    public class PrinterViewModel : BaseViewModel
    {
        private int _iD;
        private string _name;
        private int _statusID;
        private int _colorID;
        private int _projectsQueued;

        public int ID { get => _iD; set => _iD = value; }
        public string Name { get => _name; set => SetValue(ref _name, value); }
        public int StatusID { get => _statusID; set => SetValue(ref _statusID, value); }
        public int ColorID { get => _colorID; set => SetValue(ref _colorID, value); }
        public int ProjectsQueued { get => Requests.Count; set => SetValue(ref _projectsQueued, value); }


        public Status Status { get; set; }


        public PrintColor PrintColor { get; set; }

        public List<Request> Requests { get; set; }
    }
}
