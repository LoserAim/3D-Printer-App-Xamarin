using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using SQLite;
using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.UserPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSubmitRequestPage : ContentPage
	{
        private DateTime _scheduledDateTime = DateTime.Now;

        public UserSubmitRequestPage()
		{
			InitializeComponent();
		}
        
        private async void SelectFile_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();

                // User cancelled file selection
                if (fileData == null)
                    return; 

                SelectedFileLabel.Text = fileData.FileName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        private async void ScheduleDay_Clicked(object sender, EventArgs e)
        {
            var page = new UserScheduleDateTimePage();
            page.OnDateTimeSubmitted += OnDateTimeSubmitted;
            await Navigation.PushAsync(page);
        }

        private async void SubmitRequest_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Request>();

                conn.Insert(new Request()
                {
                    ProjectName     = ProjectName.Text,
                    Description     = ProjectDescription.Text,
                    DateMade = DateTime.Now,
                    DateRequested  = _scheduledDateTime

                });                
            }

            await Navigation.PopAsync();
        }
        
        private void OnDateTimeSubmitted(DateTime datetime)
        {
            _scheduledDateTime = datetime;
            PrintDateTimeLabel.Text = "Print Time: " + datetime.ToString("f", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}