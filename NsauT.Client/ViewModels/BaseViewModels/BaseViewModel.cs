﻿using NsauT.Client.Models;
using NsauT.Client.Services;
using NsauT.Client.Services.TimetableService;
using Xamarin.Forms;

namespace NsauT.Client.ViewModels.BaseViewModels
{
    public class BaseViewModel : BaseNotifier
    {
        public IDataStore<TimetableItem> DataStore => DependencyService.Get<IDataStore<TimetableItem>>();
        public ITimetableService TimetableService => DependencyService.Get<ITimetableService>();

        bool isBusy = false;
        string title = string.Empty;

        public BaseViewModel()
        {
            //TimetableService = DependencyService.Get<ITimetableService>();
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
