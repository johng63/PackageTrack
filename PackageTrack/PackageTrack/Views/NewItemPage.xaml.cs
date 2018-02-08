using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PackageTrack.Models;
using ZXing.Net.Mobile.Forms;
using PackageTrack.Services;
using PackageTrack.ViewModels;
using System.Collections.ObjectModel;

namespace PackageTrack.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public ObservableCollection<Item> Items { get; set; }
        NewItemModel newItemModel;
  
        public Item Item { get; set; }

        PropertiesHelper props;
       // private string barCode = "";

        public NewItemPage(string barCode)
        {
            InitializeComponent();
            props = new PropertiesHelper();
            Items = new ObservableCollection<Item>();
            BindingContext = newItemModel = new NewItemModel();

            Item = new Item
            {
                BarCode = barCode,
                CheckInUser = props.GetPropertyValue("UserLoggedInUser"),
                Description = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("JFG-newitempage - save_clicked");
           // MessagingCenter.Send(this, "AddItem", Item);
            await newItemModel.DataStore.AddItemAsync(Item);
            Items.Add(Item);
            await Navigation.PopAsync();
        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    Console.WriteLine("JFG-newpageItem On-Disapearing Un-subscribe");
        //    MessagingCenter.Unsubscribe<NewItemPage, Item>(this, "AddItem");
        //}
    

    //async void Scan_Clicked(object sender, EventArgs e)
    //    {
    //        var scanPage = new ZXingScannerPage();

    //        scanPage.OnScanResult += (result) => {
    //            // Stop scanning
    //            scanPage.IsScanning = false;
                
    //            // Pop the page and show the result
    //            Device.BeginInvokeOnMainThread(() =>
    //            {


    //                // Item = new Item
    //                //{
    //                barCode = result.Text;


    //                Navigation.PopAsync();
    //                //};
    //                // DisplayAlert("Scanned Barcode", result.Text, "OK");

    //            });
    //        };
    //        //BindingContext = this;
    //        // Navigate to our scanner page
    //       await  Navigation.PushAsync(scanPage);
    //        Item.BarCode = barCode;
    //        newItemModel.BarCode = barCode;
    //        //BindingContext = this;
    //        //MessagingCenter.Send(this, "AddItem", Item);
    //        //await Navigation.PopModalAsync();
    //    }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    Item.BarCode = barCode;
        //    newItemModel.BarCode = barCode;
        //    BindingContext = this;

        //}
    }
}