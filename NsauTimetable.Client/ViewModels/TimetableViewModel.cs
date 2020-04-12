using NsauTimetable.Client.Models;
using NsauTimetable.Client.ViewModels.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NsauTimetable.Client.ViewModels
{
    public class TimetableViewModel : BaseViewModel
    {
        private WeekItem _selectedWeek;
        private DayItem _selectedDay;

        public WeekItem SelectedWeek
        {
            get => _selectedWeek;
            set => SetProperty(ref _selectedWeek, value);
        }

        public DayItem SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        public readonly ObservableCollection<WeekItem> WeekItems = new ObservableCollection<WeekItem>
        {
            new WeekItem { WeekType = WeekType.Even, Title = "Нечетная"},
            new WeekItem { WeekType = WeekType.Odd, Title = "Четная"}
        };

        public readonly ObservableCollection<DayItem> DayItems = new ObservableCollection<DayItem>
        {
            new DayItem { DayOfWeek = DayOfWeek.Monday, Title = "ПН"},
            new DayItem { DayOfWeek = DayOfWeek.Tuesday, Title = "ВТ"},
            new DayItem { DayOfWeek = DayOfWeek.Wednesday, Title = "СР"},
            new DayItem { DayOfWeek = DayOfWeek.Thursday, Title = "ЧТ"},
            new DayItem { DayOfWeek = DayOfWeek.Friday, Title = "ПТ"},
            new DayItem { DayOfWeek = DayOfWeek.Saturday, Title = "СБ"}
        };

        public ObservableCollection<TimetableByDay> TimetableByDays
        {
            get; set;
        }

        public TimetableViewModel()
        {
            Title = "Расписание Title";
            _selectedWeek = WeekItems.First();
            _selectedDay = DayItems.First();

            var list = TimetableService.GetTimetables();
            TimetableByDays = new ObservableCollection<TimetableByDay>(list);
        }
    }
}
