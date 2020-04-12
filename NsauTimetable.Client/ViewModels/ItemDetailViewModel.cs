﻿using NsauTimetable.Client.Models;
using NsauTimetable.Client.ViewModels.BaseViewModels;

namespace NsauTimetable.Client.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public TimetableItem Item { get; set; }
        public ItemDetailViewModel(TimetableItem item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
