using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrintQue.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrintQue
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static int    LoggedInUserID   = -1;
        public static MobileServiceClient MobileService =new MobileServiceClient("https://3dprintqueue.azurewebsites.net");
        public static IMobileServiceSyncTable<Request> requestsTable;
        public static IMobileServiceSyncTable<Printer> printersTable;
        public static IMobileServiceSyncTable<Status> statusesTable;
        public static IMobileServiceSyncTable<PrintColor> printColorsTable;



        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new MainPage();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new MainPage();
            DatabaseLocation = databaseLocation;
            var store = new MobileServiceSQLiteStore(databaseLocation);
            store.DefineTable<Request>();
            store.DefineTable<PrintColor>();
            store.DefineTable<Printer>();
            store.DefineTable<Status>();

            MobileService.SyncContext.InitializeAsync(store);

            requestsTable = MobileService.GetSyncTable<Request>();
            printersTable = MobileService.GetSyncTable<Printer>();
            statusesTable = MobileService.GetSyncTable<Status>();
            printColorsTable = MobileService.GetSyncTable<PrintColor>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
