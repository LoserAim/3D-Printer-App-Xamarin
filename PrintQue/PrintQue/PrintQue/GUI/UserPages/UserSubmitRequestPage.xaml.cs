using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using PrintQue.GUI.UserPages;
using PrintQue.Models;
using PrintQue.Widgets.CalendarWidget;
using SQLite;
using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.UserPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSubmitRequestPage : ContentPage
	{
        private Date _dateRequestSet;

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
            var page = new UserScheduleDayPage();
            page.OnDateSubmitted += OnDateSubmitted;
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
                    DateRequested  = new DateTime(_dateRequestSet.Year, (int)_dateRequestSet.Month, _dateRequestSet.CalendarDay)
                });                
            }

            await Navigation.PopAsync();
        }
        
        private void OnDateSubmitted(Date date)
        {
            _dateRequestSet = date;
            PrintTimeLabel.Text = "Print Time: " + date.ToString();
        }
    }
}