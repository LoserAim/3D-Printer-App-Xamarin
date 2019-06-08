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

using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestDetailPage : ContentPage
    {
        private DateTime _dateTimeRequestSet;

        RequestDetailsViewModel viewModel;

        public RequestDetailPage(RequestViewModel request =null, int Status =0)
        {

            InitializeComponent();

            viewModel = new RequestDetailsViewModel(request);
            
            BindingContext = viewModel;
            _dateTimeRequestSet = DateTime.Now;
            switch (Status)
            {
                case 0:
                    //Admin insert
                    viewModel.insert = true;
                    ToolbarItems.RemoveAt(1);

                    break;
                case 1:
                    //User insert
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    RequestDetails.Root.Remove(Duration_Slider);
                    viewModel.insert = true;
                    ToolbarItems.RemoveAt(1);

                    break;
                case 2:
                    //Admin Edit
                    viewModel.insert = false;

                    break;
                case 3:
                    //User Edit
                    RequestDetails.Root.Remove(StatusEditor);
                    RequestDetails.Root.Remove(Duration_Slider);
                    RequestDetails.Root.Remove(UserSelectorSection);
                    viewModel.insert = false;
                    break;

            }


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