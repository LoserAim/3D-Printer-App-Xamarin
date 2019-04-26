using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrintQue
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static int    LoggedInUserID   = -1;
        public static MobileServiceClient MobileService =new MobileServiceClient("https://3dprintqueue.azurewebsites.net");
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
