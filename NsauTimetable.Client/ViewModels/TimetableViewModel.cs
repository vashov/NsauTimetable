using NsauTimetable.Client.Models;
using NsauTimetable.Client.ViewModels.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace NsauTimetable.Client.ViewModels
{
    public class TimetableViewModel : BaseViewModel
    {
        private WeekItem _selectedWeek;
        private DayOfWeek _selectedDay = DayOfWeek.Monday;
        private bool _isEvenWeek = false;

        private int _carouselTimetablePosition;

        public WeekItem SelectedWeek
        {
            get => _selectedWeek;
            set => SetProperty(ref _selectedWeek, value);
        }

        public DayOfWeek SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (SetProperty(ref _selectedDay, value))
                {
                    UpdateCarouselPosition(value);
                }
            }
        }

        public bool IsEvenWeek 
        { 
            get => _isEvenWeek; 
            set => SetProperty(ref _isEvenWeek, value); 
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

        public int CarouselTimetablePosition 
        { 
            get => _carouselTimetablePosition;
            set
            {
                if (SetProperty(ref _carouselTimetablePosition, value))
                {
                    SetSelectedDay(value);
                }
            }
        }

        public ObservableCollection<TimetableByDay> TimetableByDays { get; set; }

        public Command SetIsEvenWeekCommand { get; set; }

        public Command SetSelectedDayCommand { get; set; }

        public TimetableViewModel()
        {
            Title = "Расписание Title";
            _selectedWeek = WeekItems.First();
            //_selectedDay = DayItems.First();

            var list = TimetableService.GetTimetables();
            TimetableByDays = new ObservableCollection<TimetableByDay>(list);
            InitCommands();
        }

        public void InitCommands()
        {
            SetIsEvenWeekCommand = new Command<string>(SetIsEvenWeek);
            SetSelectedDayCommand = new Command<string>(SetSelectedDay);
        }

        private void SetIsEvenWeek(string isEvenWeek)
        {
            IsEvenWeek = bool.Parse(isEvenWeek);
        }

        private void SetSelectedDay(string newDay)
        {
            Enum.TryParse(newDay, ignoreCase: true, out DayOfWeek day);
            SelectedDay = day;
        }

        private void SetSelectedDay(int carouselPosition)
        {
            // Do "+1" because index of carousel position starts from 0
            SelectedDay = (DayOfWeek)(carouselPosition + 1);
        }

        private void UpdateCarouselPosition(DayOfWeek day)
        {
            // Do "-1" because index of carousel position starts from 0
            CarouselTimetablePosition = (int)day - 1;
        }
    }
}
