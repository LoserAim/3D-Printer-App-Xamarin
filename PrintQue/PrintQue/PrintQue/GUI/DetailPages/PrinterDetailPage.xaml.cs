using PrintQue.GUI.SelectorPages;
using PrintQue.Models;
using PrintQue.ViewModel;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrinterDetailPage : ContentPage
    {

        public PrinterDetailPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            
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
        async void Color_Selector_Tapped(object sender, EventArgs e)
        {
            var page = new ColorSelectorPage();
            page.ColorNames.ItemSelected += (source, args) =>
            {
                Color_Picker.Text = args.SelectedItem.ToString();
                Navigation.PopAsync();
            };
            await Navigation.PushAsync(page);
        }
        async private void ToolbarItem_Save_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "Are you sure you want to Create this Printer?", "Yes", "No");
            if (response)
            {
                var exists = await PrinterViewModel.SearchByName(ent_Name.Text);
                if (exists != null)
                {
                    await DisplayAlert("ERROR", "Name already Used. Please choose another", "OK");
                }
                else
                {
                    var status = await StatusViewModel.SearchByName(Status_Picker.Text);
                    var printColor = await PrintColorViewModel.SearchByName(Color_Picker.Text);
                    var printer = new PrinterViewModel()
                    {
                        Name = ent_Name.Text,
                        StatusID = status.ID,
                        ColorID = printColor.ID,
                        Status = status,
                        PrintColor = printColor,
                        ProjectsQueued = 0,
                    };

                    await PrinterViewModel.Insert(printer);

                }


            }

        }
    }
}