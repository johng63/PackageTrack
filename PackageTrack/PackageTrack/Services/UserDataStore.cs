using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PackageTrack.Models;

[assembly: Xamarin.Forms.Dependency(typeof(PackageTrack.Services.UserDataStore))]
namespace PackageTrack.Services
{
    public class UserDataStore : IDataStore<User>
    {
        HttpClient client = new HttpClient();
        //string RestUrlHead = "http://10.5.90.209:3100/users";    //KLC Server
        string RestUrlHead = "http://192.168.63.60:3100/users";  //Laptop
        List<User> users;
        //string rawItems;

        public UserDataStore()
        {
            users = new List<User>();
            client.MaxResponseContentBufferSize = 256000;

           // var restItems = GetItemsAsync();

        }
        //public bool CheckServerConnection()
        //{
        //    bool retVal = false;

        //    //var pinger = new SimplePing("www.apple.com");

        //    //pinger.Started += (sender, e) => {
        //    //    var endpoint = e.EndPoint;
        //    //    pinger.SendPing(null);
        //    //};

        //    //pinger.ResponseRecieved += (sender, e) => {
        //    //    var seq = e.SequenceNumber;
        //    //    var packet = e.Packet;
        //    //};

        //    //pinger.Start();
        //    //Ping pingSender = new Ping();
        //    /////IPAddress address = IPAddress.
        //    //PingReply reply = pingSender.Send("192.168.63.60");


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

        public async Task<bool> AddItemAsync(User user)
        {

            UserAdd addUser = user;

            Dictionary<string, string> dict = ConvertToDictionary(addUser);
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, RestUrlHead) { Content = new FormUrlEncodedContent(dict) };
            var res = await client.SendAsync(req);

            //var restItems = GetItemsAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User user)
        {
            var _user = users.Where((User arg) => arg.UserName == user.UserName).FirstOrDefault();
            users.Remove(_user);
            users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(User user)
        {
            var _user = users.Where((User arg) => arg.UserName == user.UserName).FirstOrDefault();
            users.Remove(_user);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(string username)
        {
            var restItems = new List<User>();
            User user = new User();
            string RestUrl = this.RestUrlHead + "/" + username;
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(content);
                users.Add(user);
            }
            // return await Task.FromResult(users.FirstOrDefault(s => s.UserName == username));
            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            var restItems = new List<User>();
            string RestUrl = this.RestUrlHead;
            //var uri = new Uri(string.Format(RestUrl, string.Empty));

            //var response = await client.GetAsync(uri);
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    users = JsonConvert.DeserializeObject<List<User>>(content);
            //}
            return await Task.FromResult(users);
        }

        private static Dictionary<string, string> ConvertToDictionary(UserAdd user)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("userName", user.UserName);
            dict.Add("password", user.Password);

            return dict;
        }
    }
}