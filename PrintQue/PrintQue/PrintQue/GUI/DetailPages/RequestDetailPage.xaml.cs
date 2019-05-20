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
        RequestDetailsViewModel viewModel;
        public RequestDetailPage(RequestViewModel request = null, int Status = 0)
        {
            InitializeComponent();
            viewModel = new RequestDetailsViewModel(request, Status);
            BindingContext = viewModel;
        }
    }
}