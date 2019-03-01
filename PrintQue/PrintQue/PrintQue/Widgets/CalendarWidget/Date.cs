using System;

namespace PrintQue.Widgets.CalendarWidget
{
    public class Date
    {
        public DayOfWeek DayOfWeek { get; set; }
        public Month Month{ get; set; }
        public int CalendarDay{ get; set; }
        public int Year{ get; set; }

        public override string ToString()
        {
            return    DayOfWeek.ToString()
                    + ", "
                    + Month.ToString()
                    + " "
                    + CalendarDay.ToString()
                    + ", "
                    + Year.ToString();
        }

        static public DayOfWeek CurrentDayOfWeek { get => (DayOfWeek)DateTime.Now.DayOfWeek; }
        static public Month CurrentMonth { get => (Month)DateTime.Now.Month; }
        static public int CurrentYear { get => DateTime.Now.Year; }
        static public int CurrentDay { get => DateTime.Now.Day; }
        static public Date CurrentDate { get => new Date { DayOfWeek = CurrentDayOfWeek, Month = CurrentMonth, Year = CurrentYear, CalendarDay = CurrentDay }; }
    }
}
