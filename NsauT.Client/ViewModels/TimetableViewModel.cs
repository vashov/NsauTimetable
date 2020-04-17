using NsauT.Client.Models;
using NsauT.Client.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace NsauT.Client.ViewModels
{
    public class TimetableViewModel : BaseViewModel
    {
        private const string DefaultSelectedGroup = "Не выбрано";
        private string _selectedGroup;

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

        public string SelectedGroup 
        { 
            get => _selectedGroup; 
            set => SetProperty(ref _selectedGroup, value); 
        }

        public ObservableCollection<WeekItem> WeekItems { get; } = new ObservableCollection<WeekItem>
        {
            new WeekItem { WeekType = WeekType.Even, Title = "Нечетная"},
            new WeekItem { WeekType = WeekType.Odd, Title = "Четная"}
        };

        public ObservableCollection<DayItem> DayItems { get; } = new ObservableCollection<DayItem>
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
        public ObservableCollection<string> Groups { get; set; }

        public Command SetIsEvenWeekCommand { get; private set; }
        public Command SetSelectedDayCommand { get; private set; }

        public TimetableViewModel()
        {
            Title = "Расписание Title";
            _selectedWeek = WeekItems.First();
            //_selectedDay = DayItems.First();

            InitTimetable();
            InitGroups();

            InitCommands();
        }

        private void InitCommands()
        {
            SetIsEvenWeekCommand = new Command<string>(SetIsEvenWeek);
            SetSelectedDayCommand = new Command<string>(SetSelectedDay);
        }

        private void InitTimetable()
        {
            List<TimetableByDay> list = TimetableService.GetTimetables();
            TimetableByDays = new ObservableCollection<TimetableByDay>(list);
        }

        private void InitGroups()
        {
            List<string> groups = TimetableService.GetGroups();
            groups.Insert(0, DefaultSelectedGroup);

            Groups = new ObservableCollection<string>(groups);
            SelectedGroup = groups.First();
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
