using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PackageTrack.Models;
using PackageTrack.Views;
using PackageTrack.ViewModels;
using ZXing.Net.Mobile.Forms;

namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        PropertiesHelper props;
        string barCodeReturned = "";
        string dbOnline;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            props = new PropertiesHelper();
            dbOnline = props.GetPropertyValue("DatabaseOnline");
            viewModel.DBOnline = dbOnline;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (dbOnline.Equals("Online"))
            {
                var scanPage = new ZXingScannerPage();
                barCodeReturned = "";

                scanPage.Disappearing += ScanPage_Disappearing;

                scanPage.OnScanResult += (result) =>
                {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        barCodeReturned = result.Text;
                        Navigation.PopAsync();

                    });
                };
                // await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
                await Navigation.PushAsync(scanPage);
                //  Navigation.PushAsync(new NewItemPage(barCodeReturned));
                ///await  Navigation.PopAsync();
            } else
            {
                //message Can't add when Offline
                await DisplayAlert("Alert", "You are not online", "OK");
            }
        }

        private void ScanPage_Disappearing(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewItemPage(barCodeReturned));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

    
    }
}