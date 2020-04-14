using NsauTimetable.Client.Models;
using System.Collections.Generic;

namespace NsauTimetable.Client.Services.AdsService
{
    public interface IAdsService
    {
        List<AdItem> GetAds();
    }
}
