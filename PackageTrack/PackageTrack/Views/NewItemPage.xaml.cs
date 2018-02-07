using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PackageTrack.Models;
using ZXing.Net.Mobile.Forms;
using PackageTrack.Services;

namespace PackageTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public Item Item { get; set; }

        PropertiesHelper props;

        public NewItemPage()
        {
            InitializeComponent();
            props = new PropertiesHelper();
            Item = new Item
            {
                BarCode = "",
                CheckInUser = props.GetPropertyValue("UserLoggedInUser"),
                Description = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("JFG-newitempage - save_clicked");
           // MessagingCenter.Send(this, "AddItem", Item);
            await DataStore.AddItemAsync(Item);
            await Navigation.PopAsync();
        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    Console.WriteLine("JFG-newpageItem On-Disapearing Un-subscribe");
        //    MessagingCenter.Unsubscribe<NewItemPage, Item>(this, "AddItem");
        //}
    

    async void Scan_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;
                
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();

                    // Item = new Item
                    //{
                    Item.BarCode = result.Text;

                    //};
                    // DisplayAlert("Scanned Barcode", result.Text, "OK");
                    
                });
            };
            //BindingContext = this;
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);

            //MessagingCenter.Send(this, "AddItem", Item);
            //await Navigation.PopModalAsync();
        }
    }
}