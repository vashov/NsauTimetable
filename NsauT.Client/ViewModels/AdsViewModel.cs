using NsauT.Client.Models;
using NsauT.Client.Services.AdsService;
using NsauT.Client.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NsauT.Client.ViewModels
{
    public class AdsViewModel : BaseViewModel
    {
        private IAdsService AdsService { get; set; }

        public ObservableCollection<AdItem> Ads { get; set; }

        public Command ExcecuteHyperlinkCommand { get; private set; }

        public AdsViewModel()
        {
            AdsService = DependencyService.Get<IAdsService>();

            InitAds();
            InitCommands();
        }

        private void InitAds()
        {
            List<AdItem> ads = AdsService.GetAds();
            Ads = new ObservableCollection<AdItem>(ads);
        }

        private void InitCommands()
        {
            ExcecuteHyperlinkCommand = new Command<string>(ExcecuteHyperlink);
        }

        private async void ExcecuteHyperlink(string adId)
        {
            int id = int.Parse(adId);

            AdItem adItem = Ads.First(ad => ad.Id == id);

            await Launcher.OpenAsync(new Uri(adItem.Link));
        }
    }
}
