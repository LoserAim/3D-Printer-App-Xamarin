using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.UserPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserScheduleDateTimePage : ContentPage
	{
        
        public Action<DateTime> OnDateTimeSubmitted { get; set; }
        public UserScheduleDateTimePage()
		{
			InitializeComponent();         
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            OnDateTimeSubmitted?.Invoke(DatePicker.Date + TimePicker.Time);

            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}