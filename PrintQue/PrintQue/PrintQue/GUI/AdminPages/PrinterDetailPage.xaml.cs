using PrintQue.GUI.AdminPages.SelectorPages;
using PrintQue.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrinterDetailPage : ContentPage
    {
        private List<Status> currentStatus;
        private List<PrintColor> currentColors;
        void GetLists()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Status>();
                conn.CreateTable<PrintColor>();
                currentColors = conn.Table<PrintColor>().ToList();
                currentStatus = conn.Table<Status>().ToList();

            }
        }
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
                var printer = new Printer()
                {
                    Name = ent_Name.Text,
                    ProjectsQueued = 0,
                };
                GetLists();
                var foundstatus = currentStatus.SingleOrDefault(s => s.Name.Contains(Status_Picker.Text));
                var foundprintcolor = currentColors.SingleOrDefault(pc => pc.Name.Contains(Color_Picker.Text));
                //
                if (foundstatus.Printers == null)
                    foundstatus.Printers = new List<Printer>() { printer };
                else
                    foundstatus.Printers.Add(printer);
                //
                if (foundprintcolor.Printers == null)
                    foundprintcolor.Printers = new List<Printer>() { printer };
                else
                    foundprintcolor.Printers.Add(printer);
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.Insert(printer);
                    conn.UpdateWithChildren(foundprintcolor);
                    conn.UpdateWithChildren(foundstatus);
                }
                await Navigation.PopAsync();


            }

        }
    }
}