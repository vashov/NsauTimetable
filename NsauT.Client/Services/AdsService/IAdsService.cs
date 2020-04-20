using NsauT.Client.Models;
using System.Collections.Generic;

namespace NsauT.Client.Services.AdsService
{
    public interface IAdsService
    {
        List<AdItem> GetAds();
    }
}
