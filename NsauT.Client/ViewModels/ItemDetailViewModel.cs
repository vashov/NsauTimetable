using NsauT.Client.Models;
using NsauT.Client.ViewModels.BaseViewModels;

namespace NsauT.Client.ViewModels
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
