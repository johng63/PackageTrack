using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PackageTrack.Models;

[assembly: Xamarin.Forms.Dependency(typeof(PackageTrack.Services.MockDataStore))]
namespace PackageTrack.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        HttpClient client = new HttpClient();
        //string RestUrlHead = "http://10.5.90.209:3000/items";    //KLC Server
        string RestUrlHead = "http://192.168.63.60:3000/items";  //Laptop
        List<Item> items;
        //string rawItems;

        public MockDataStore()
        {
            items = new List<Item>();
            client.MaxResponseContentBufferSize = 256000;

            //var restItems = GetItemsFromRestAsync();
            var restItems = GetItemsAsync();

        }

        public async Task<bool> AddItemAsync(Item item)
        {

            ItemAdd addItem = item;

            Dictionary<string, string> dict = ConvertToDictionary(addItem);
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, RestUrlHead) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);

            var restItems = GetItemsAsync();

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

        private static Dictionary<string, string> ConvertToDictionary(ItemAdd item)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("barcode", item.BarCode);
            dict.Add("checkInUser", item.CheckInUser);
            dict.Add("checkOutUser", item.CheckOutUser);
            dict.Add("project", item.Project);
            dict.Add("description", item.Description);

            return dict;
        }
    }
}