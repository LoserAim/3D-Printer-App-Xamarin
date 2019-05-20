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
        private RequestViewModel _request;
        private bool insert;
        private int _status;
        RequestDetailsViewModel viewModel;
        public RequestDetailPage(RequestViewModel request = null, int Status = 0)
        {
            InitializeComponent();
            _request = request;
            _status = Status;
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
            viewModel = new RequestDetailsViewModel();
            BindingContext = viewModel;
        }
    }
}