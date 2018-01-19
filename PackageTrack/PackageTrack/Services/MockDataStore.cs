using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PackageTrack.Models;

[assembly: Xamarin.Forms.Dependency(typeof(PackageTrack.Services.MockDataStore))]
namespace PackageTrack.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        HttpClient client = new HttpClient();
        string RestUrlHead = "http://10.5.90.209:3000/items";
        List<Item> items;
        //string rawItems;

        public MockDataStore()
        {
            items = new List<Item>();
            client.MaxResponseContentBufferSize = 256000;

            //var restItems = GetItemsFromRestAsync();
            var restItems = GetItemsAsync();






            //var mockItems = new List<Item>
            //{
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "First item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "Second item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "Third item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "Fourth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "Fifth item", Description="This is an item description." },
            //    new Item { Id = Guid.NewGuid().ToString(), BarCode = "Sixth item", Description="This is an item description." },
            //};

            //foreach (var item in restItems)
            //{
            //    items.Add(item);

            //}
        }


        //public async Task<List<Item>> GetItemsFromRestAsync()
        //{
           

        //    var restItems = new List<Item>();
        //    string RestUrl = this.RestUrlHead;
        //    var uri = new Uri(string.Format(RestUrl, string.Empty));

        //    var response = await client.GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        items = JsonConvert.DeserializeObject<List<Item>>(content);
        //    }

        //    return await Task.FromResult(restItems);
        //}

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            var restItems =  GetItemsAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg._id == item._id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg._id == item._id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s._id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var restItems = new List<Item>();
            string RestUrl = this.RestUrlHead;
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Item>>(content);
            }
            return await Task.FromResult(items);
        }
    }
}