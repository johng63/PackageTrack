﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PackageTrack.Models;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(PackageTrack.Services.MockDataStore))]
namespace PackageTrack.Services
{

    public class MockDataStore : IDataStore<Item>
    {

        HttpClient client = new HttpClient();
        //string RestUrlHead = "http://10.5.90.209:3000/items";    //KLC Server
        string RestUrlHead;
        //string RestUrlHead = "http://192.168.10.109:3000/items";

        List<Item> items;
        PropertiesHelper props;
        string server;
        
        //string rawItems;
        FileEngine fileEngine = new FileEngine();

        public MockDataStore()
        {
            items = new List<Item>();
            client.MaxResponseContentBufferSize = 256000;
            props = new PropertiesHelper();
            server = props.GetPropertyValue("Server");

            if (server == null || server.Length == 0)
            {
                 //DisplayAlert("Alert", "You are not online", "OK");
            }
            else
            {

                GetRestURL(server);
                var restItems = GetItemsAsync();
            }

        }

        private void GetRestURL(string server)
        {
            RestUrlHead = "http://" + server + ":3000/items";
        }

        public async Task<bool> AddItemAsync(Item item)
        {

            ItemAdd addItem = item;
            server = props.GetPropertyValue("Server");
            GetRestURL(server);

            Dictionary<string, string> dict = ConvertToDictionary(addItem);
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, RestUrlHead) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);
           // Console.WriteLine("JFG-additemasync - mockdatastore");
            var items = GetItemsAsync();


            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            //Add Item update to DB
            //var _item = items.Where((Item arg) => arg._id == item._id).FirstOrDefault();
            //items.Remove(_item);
            //items.Add(item);
            server = props.GetPropertyValue("Server");
            GetRestURL(server);
            ItemAdd addItem = item;
            Dictionary<string, string> dict = ConvertToDictionaryUpdate(addItem);
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Put, RestUrlHead + "/" + item._id) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);

            return await Task.FromResult(true);
        }
        //public bool CheckServerConnection()
        //{
        //    //Ping pingSender = new Ping();
        //    /////IPAddress address = IPAddress.
        //    //PingReply reply = pingSender.Send("192.168.63.60");
        //    bool retVal = false;

        //    //if (reply.Status == IPStatus.Success)
        //    //{
        //    //    retVal = true;
        //    //    //Console.WriteLine("Address: {0}", reply.Address.ToString());
        //    //    //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
        //    //    //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
        //    //    //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
        //    //    //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
        //    //}
        //    //else
        //    //{
        //    //    //Console.WriteLine(reply.Status);
        //    //    retVal = false;
        //    //}

        //    return retVal;
        //}

        public async Task<bool> DeleteItemAsync(Item item)
        {
            //Add Item remove from DB
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
            server = props.GetPropertyValue("Server");
            GetRestURL(server);

            if (props.GetPropertyValue("DatabaseOnline").Equals("Online"))
            {
                try
                {
                    using (var client = new HttpClient
                    {
                        Timeout = TimeSpan.FromMilliseconds(30000)
                    })
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string RestUrl = this.RestUrlHead;

                        var response = await client.GetAsync(new Uri(string.Format(RestUrl, string.Empty)));

                        HttpStatusCode statusCode = response.StatusCode;

                        var content = await response.Content.ReadAsStringAsync();

                        props.SetPropertyValue("DatabaseOnline", "Online");

                        await fileEngine.WriteTextAsync("PackageData.json", content);

                        restItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(content);

                    }

                }
                catch (TaskCanceledException tcex)
                {
                    props.SetPropertyValue("DatabaseOnline", "Offline");
                    restItems = await GetOfflineData(restItems);
                    Console.WriteLine("taskcanceled: " + tcex.Message);
                }
                catch (Exception ex)
                {
                    props.SetPropertyValue("DatabaseOnline", "Offline");
                    restItems = await GetOfflineData(restItems);
                    Console.WriteLine("taskexception: " + ex.Message);
                }
            }
            else
            {
                props.SetPropertyValue("DatabaseOnline", "Offline");
                restItems = await GetOfflineData(restItems);

            }


            return restItems;
        }

        private async Task<List<Item>> GetOfflineData(List<Item> restItems)
        {
            //props.SetPropertyValue("DatabaseOnline", "Offline");
            var content = await fileEngine.ReadTextAsync("PackageData.json");
            restItems = JsonConvert.DeserializeObject<List<Item>>(content);
            return restItems;
        }

        private static Dictionary<string, string> ConvertToDictionary(ItemAdd item)
        {
            var dict = new Dictionary<string, string>
            {
                { "barcode", item.BarCode },
                { "checkInUser", item.CheckInUser },
                { "checkOutUser", item.CheckOutUser },
                { "project", item.Project },
                { "description", item.Description }
            };

            return dict;
        }

        private static Dictionary<string, string> ConvertToDictionaryUpdate(ItemAdd item)
        {
            var dict = new Dictionary<string, string>
            {
                { "project", item.Project },
                { "description", item.Description }
            };

            return dict;
        }
    }
}