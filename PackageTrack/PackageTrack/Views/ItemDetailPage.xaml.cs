using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PackageTrack.Models;
using PackageTrack.ViewModels;

namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                BarCode = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            
            // MessagingCenter.Send(this, "AddItem", Item);
            //await DataStore.AddItemAsync(Item);
            await Navigation.PopAsync();
        }
    }
}