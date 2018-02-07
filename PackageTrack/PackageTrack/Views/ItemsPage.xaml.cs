﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PackageTrack.Models;
using PackageTrack.Views;
using PackageTrack.ViewModels;

namespace PackageTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        PropertiesHelper prop;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            prop = new PropertiesHelper();
            viewModel.DBOnline = prop.GetPropertyValue("DatabaseOnline");

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
            // await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    Console.WriteLine("JFG-ItemPage.cs On-Disapearing Un-subscribe");
        //    MessagingCenter.Unsubscribe<NewItemPage, Item>(this, "AddItem");
        //}
    
}
}