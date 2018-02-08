using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using PackageTrack.Models;
using PackageTrack.Views;

namespace PackageTrack.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        PropertiesHelper props;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            props = new PropertiesHelper();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    //var _item = item as Item;
            //    //Items.Add(_item);
            //    //Console.WriteLine("JFG-itemview subscribe");
            //    //await DataStore.AddItemAsync(_item);
            //});
        }
        string isDBOnline = string.Empty;
        public string DBOnline
        {
            get { return isDBOnline; }
            set { SetProperty(ref isDBOnline, value) ; }
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                isDBOnline = props.GetPropertyValue("DatabaseOnline");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    //protected  void OnDisappearing()
    //{
    //        Console.WriteLine("JFG-itemviewModel On-Disapearing Un-subscribe");
    //        MessagingCenter.Unsubscribe<NewItemPage, Item>(this, "AddItem");
    //    }
    }


}