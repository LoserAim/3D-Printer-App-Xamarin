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

        public PrinterDetailPage()
        {
            InitializeComponent();

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
                var color = Color_Picker.Text;
                var status = Status_Picker.Text;
                var printer = new Printer()
                {
                    Name = ent_Name.Text,
                    ProjectsQueued = 0,
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Printer>();
                    conn.CreateTable<Status>();
                    conn.CreateTable<PrintColor>();
                    var liststatus = conn.Table<Status>().ToList();
                    var listprintcolor = conn.Table<PrintColor>().ToList();
                    var foundstatus = liststatus.SingleOrDefault(s => s.Name.Contains(status));
                    var foundprintcolor = listprintcolor.SingleOrDefault(pc => pc.Name.Contains(color));
                    var rows = conn.Insert(printer);
                    if(rows > 0)
                    {
                        await DisplayAlert("Success", "Printer was Successfully created!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Failure", "Uh oh! Looks like someone failed to create a printer!", "OK");
                    }
                    foundstatus.Printers.Add(printer);
                    foundprintcolor.Printers.Add(printer);
                    conn.UpdateWithChildren(foundprintcolor);
                    conn.UpdateWithChildren(foundstatus);
                }
                
            }

        }
    }
}