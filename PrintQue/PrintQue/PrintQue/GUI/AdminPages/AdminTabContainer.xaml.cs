
using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SQLiteNetExtensionsAsync.Extensions;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrintQue.GUI.DetailPages;

namespace PrintQue
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTabContainer : TabbedPage
    {



        private List<PrintColor> printColors = new List<PrintColor>()
        {
            new PrintColor()
            {
                Name = "Red",
                HexValue = "#ff0000",
            },
            new PrintColor()
            {
                Name = "Green",
                HexValue = "#008000",
            },
            new PrintColor()
            {
                Name = "Orange",
                HexValue = "#FFA500",
            },
        };

        public List<Status> statuses = new List<Status>()
        {
            new Status()
            {
                Name = "Approved",
            },
            new Status()
            {
                Name = "Denied",
            },
            new Status()
            {
                Name = "nostatus",
            },
            new Status()
            {
                Name = "Busy",
            },
            new Status()
            {
                Name = "Open",
            },
            new Status()
            {
                Name = "Closed",
            },
        };
        private List<Printer> printers = new List<Printer>()
        {
            new Printer() {
                Name = "Demilovato",
            },
            new Printer() {
                Name = "Prince",

            },
            new Printer() {
                Name = "Corpus",

            },
        };
        private List<User> users = new List<User>()
        {
            new User()
            {
                Email = "drew.doser@gmail.com",
                Password = "1234",
                Name = "Andrew",
                Admin = 0,
            },
            new User()
            {
                Email = "Brad.Bergstrom@gmail.com",
                Password = "1234",
                Name = "Brad",
                Admin = 0,
            },

        };

        public AdminTabContainer()
        {
            InitializeComponent();
            attachChildren();

        }

        private void ToolbarItem_Plus_Activated(object sender, EventArgs e)
        {
            var request = new RequestWithChildren();
            request = null;
            Navigation.PushAsync(new RequestDetailPage(request));
        }

        private void ToolbarItem_Add_Printer_Activated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrinterDetailPage());
        }

        private void ToolbarItem_Add_Request_Activated(object sender, EventArgs e)
        {
            var request = new RequestWithChildren();
            request = null;
            Navigation.PushAsync(new RequestDetailPage(request));
        }

        async private void ToolbarItem_Run_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "You are about to logout. Are you sure?", "Yes", "No");
            if (response)
                await Navigation.PopAsync();

        }

        private async void ToolbarItem_Drop_Tables_Activated(object sender, EventArgs e)
        {
            await DropTables();
            await PopulateTables();




        }
        private async Task PopulateStatus()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            var srow = await conn.InsertAllAsync(statuses);

        }
        private async Task PopulatePrintColor()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            var srow = await conn.InsertAllAsync(printColors);

        }
        private async Task PopulateUser()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            var srow = await conn.InsertAllAsync(users);

        }
        private async Task PopulateTables()
        {
            await PopulateStatus();
            await PopulatePrintColor();
            await PopulateUser();

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);
            await conn.InsertAllAsync(printers);
            await conn.UpdateWithChildrenAsync(statuses[3]);
            await conn.UpdateWithChildrenAsync(printColors[0]);
        }
        private void attachChildren()
        {
            foreach (Printer p in printers)
            {
                statuses[3].printers.Add(p);
                printColors[0].printers.Add(p);
            }
        }
        async private Task DropTables()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(App.DatabaseLocation);

            await conn.DropTableAsync<Printer>();
            await conn.DropTableAsync<User>();
            await conn.DropTableAsync<Request>();
            await conn.DropTableAsync<PrintColor>();
            await conn.DropTableAsync<Status>();
            await conn.CreateTableAsync<Printer>();
            await conn.CreateTableAsync<User>();
            await conn.CreateTableAsync<Request>();
            await conn.CreateTableAsync<PrintColor>();
            await conn.CreateTableAsync<Status>();

        }

        async private void ToolbarItem_Add_Color_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrintColorDetailPage());

        }

        async private void ToolbarItem_Add_Status_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusDetailPage());

        }
    }
}