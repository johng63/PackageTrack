using System;

using PackageTrack.Models;

namespace PackageTrack.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.BarCode;
            Item = item;
        }
    }
}
