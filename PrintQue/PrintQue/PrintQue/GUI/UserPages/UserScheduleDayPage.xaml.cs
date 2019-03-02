using PrintQue.Widgets.CalendarWidget;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.UserPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserScheduleDayPage : ContentPage
	{
        public Action<Date> OnDateSubmitted { get; set; }

        private CalendarWidget _calendarWidget;
        private Date _selectedDate;

        public UserScheduleDayPage()
		{
			InitializeComponent();

            _calendarWidget = new CalendarWidget(CalendarStackLayout);            
            _calendarWidget.OnDateTapped += OnDateTapped;

            SetSelectedDate(Date.CurrentDate);
        }

        private void OnDateTapped(Date date)
        {
            SetSelectedDate(date);
        }

        private void SetSelectedDate(Date date)        
        {
            _selectedDate = date;
            SelectedDateLabel.Text = "Selected Date:\n" + date.ToString();                             
        }

        private async void SubmitDate_Clicked(object sender, EventArgs e)
        {
            OnDateSubmitted?.Invoke(_selectedDate);
            await Navigation.PopAsync();
        }
    }
}