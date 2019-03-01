using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrintQue.Widgets.CalendarWidget
{
    public class CalendarWidget
    {
        public Action<Date> OnDateTapped { get; set; }

        private Grid        _calendarGrid;
        private StackLayout _calendarStackLayout;
        private StackLayout _calendarNavigationStackLayout;

        private readonly Color PrimaryTextColor   = Color.White;
        private readonly Color SecondaryTextColor = Color.Gray;
        private readonly Color BackgroundColor    = Color.Black;

        private readonly double NavigationButtonFontSize     = 20.0;
        private readonly double NavigationButtonDimension    = 60.0;
        private readonly double FontSize                     = 20.0;

        private Button      _previousMonthButton;
        private Button      _nextMonthButton;
        private Label       _currentMonthLabel;  
        private List<Label> _calendarDayLabels;

        private Month _currentSelectedMonth;
        private int   _currentSelectedYear;
        private int   _currentSelectedDay;
        
        public CalendarWidget(StackLayout calendarStackLayout)
        {
            // Scaffold the calendar interface
            _calendarGrid = new Grid()
            {
                BackgroundColor = BackgroundColor,
                ColumnSpacing   = 0.0,
                RowSpacing      = 0.0
            };

            _calendarNavigationStackLayout = new StackLayout()
            {
                BackgroundColor     = BackgroundColor,
                Orientation         = StackOrientation.Horizontal,
                HorizontalOptions   = LayoutOptions.Center,
                VerticalOptions     = LayoutOptions.Center,
                Spacing             = 0.0
            };

            _previousMonthButton = new Button()
            {
                BackgroundColor = BackgroundColor,
                TextColor       = PrimaryTextColor,
                Text            = "<",
                FontSize        = NavigationButtonFontSize,
                HeightRequest   = NavigationButtonDimension,
                WidthRequest    = NavigationButtonDimension              
            };

            _nextMonthButton = new Button()
            {
                BackgroundColor = BackgroundColor,
                TextColor       = PrimaryTextColor,
                Text            = ">",
                FontSize        = NavigationButtonFontSize,
                HeightRequest   = NavigationButtonDimension,
                WidthRequest    = NavigationButtonDimension
            };

            _currentMonthLabel = new Label()
            {
                BackgroundColor         = BackgroundColor,
                TextColor               = PrimaryTextColor,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment   = TextAlignment.Center,
            };

            _previousMonthButton.Clicked += OnPreviousMonthButtonTap;
            _nextMonthButton.Clicked     += OnNextMonthButtonTap;

            _calendarNavigationStackLayout.Children.Add(_previousMonthButton);
            _calendarNavigationStackLayout.Children.Add(_currentMonthLabel);
            _calendarNavigationStackLayout.Children.Add(_nextMonthButton);

            _calendarStackLayout = calendarStackLayout;
            _calendarStackLayout.BackgroundColor = BackgroundColor;
            _calendarStackLayout.Children.Add(_calendarNavigationStackLayout);
            _calendarStackLayout.Children.Add(_calendarGrid);

            // Populate first row with days "Su" through "Sa"
            for (var dayOfWeekNumber = 0; dayOfWeekNumber < 7; ++dayOfWeekNumber)
            {
                Label abbreviatedDayOfWeekLabel = new Label
                {
                    Text                    = ((DayOfWeek)dayOfWeekNumber).ToString().Substring(0, 2),
                    BackgroundColor         = BackgroundColor,
                    TextColor               = PrimaryTextColor,
                    FontSize                = FontSize,
                    HeightRequest           = 20.0,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment   = TextAlignment.Center
                };

                Grid.SetRow(abbreviatedDayOfWeekLabel, 0);
                Grid.SetColumn(abbreviatedDayOfWeekLabel, dayOfWeekNumber);

                _calendarGrid.Children.Add(abbreviatedDayOfWeekLabel);
            }

            // Scaffold calendar days with empty labels
            _calendarDayLabels = new List<Label>();

            for (var row = 0; row < 6; ++row)
            {
                for (var column = 0; column < 7; ++column)
                {                    
                    var calendarDayLabel = new Label()
                    {
                        BackgroundColor         = BackgroundColor,
                        TextColor               = PrimaryTextColor,
                        FontSize                = FontSize,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment   = TextAlignment.Center,
                    };
                                       
                    Grid.SetRow(calendarDayLabel, row + 1);
                    Grid.SetColumn(calendarDayLabel, column);

                    _calendarGrid.Children.Add(calendarDayLabel);
                    _calendarDayLabels.Add(calendarDayLabel);
                }
            }

            NavigateToMonth(Date.CurrentMonth, Date.CurrentYear);
            SelectDay(Date.CurrentDay);
        } 

        private void OnPreviousMonthButtonTap(object sender, EventArgs e)
        {
            if(_currentSelectedMonth == Month.January)
            {
                NavigateToMonth(Month.December, _currentSelectedYear - 1);
            }
            else
            {
                NavigateToMonth(_currentSelectedMonth - 1, _currentSelectedYear);
            }
        }

        private void OnNextMonthButtonTap(object sender, EventArgs e)
        {
            if (_currentSelectedMonth == Month.December)
            {
                NavigateToMonth(Month.January, _currentSelectedYear + 1);
            }
            else
            {
                NavigateToMonth(_currentSelectedMonth + 1, _currentSelectedYear);
            }
        }

        private void NavigateToMonth(Month month, int year)
        {
            _currentSelectedMonth   = month;
            _currentSelectedYear    = year;
            _currentMonthLabel.Text = _currentSelectedMonth.ToString() + ' ' + _currentSelectedYear.ToString();

            ClearDaySelection();

            // Populate the calendar with days 1, 2, 3, ...
            // Days before and after the month are filled with empty days
            var firstOfMonthDateTime    = new DateTime(year, (int)month, 1);
            var daysUntilFirstOfMonth   = (int)firstOfMonthDateTime.DayOfWeek;
            var daysInMonth             = DateTime.DaysInMonth(year, (int)month);

            for (var column = 0; column < 7; ++column)
            {
                for (var row = 0; row < 6; ++row)
                { 
                    var calendarDayIndex = (row * 7) + (column + 1);
                    var calendarDay      = calendarDayIndex - daysUntilFirstOfMonth;
                    var calendarDayLabel = _calendarDayLabels[calendarDayIndex - 1];
                    
                    if (calendarDay >= 1 && calendarDay <= daysInMonth)
                    {
                        calendarDayLabel.Text = calendarDay.ToString();

                        var dayOfWeek = (DayOfWeek)column;

                        calendarDayLabel.GestureRecognizers.Clear();
                        calendarDayLabel.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(() =>
                            OnDateTappedWrapper(new Date()
                            {
                                DayOfWeek   =  dayOfWeek,
                                Month       = _currentSelectedMonth,
                                CalendarDay = calendarDay,
                                Year        = _currentSelectedYear
                            }))
                        });
                    }
                    else
                    {
                        calendarDayLabel.Text = "";
                    }               
                }
            }
        }

        private void ClearDaySelection()
        {
            var dayLabel = _calendarDayLabels.FirstOrDefault(label => label.Text == _currentSelectedDay.ToString());

            if (dayLabel == null) return;

            dayLabel.BackgroundColor  = BackgroundColor;
            dayLabel.TextColor        = PrimaryTextColor;

            _currentSelectedDay = -1;
        }
        
        private void SelectDay(int calendarDay)
        {
            var dayLabel = _calendarDayLabels.FirstOrDefault(label => label.Text == calendarDay.ToString());

            if (dayLabel == null) return;

            dayLabel.BackgroundColor  = PrimaryTextColor;
            dayLabel.TextColor        = BackgroundColor;

            _currentSelectedDay = calendarDay;
        }

        private void OnDateTappedWrapper(Date date)
        {
            if (_currentSelectedDay > 0)
            {
                ClearDaySelection();                
            }
            
            SelectDay(date.CalendarDay);               

            OnDateTapped?.Invoke(date);
        }
    }
}
