using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(PackageTrack.Droid.Pinger))]
namespace PackageTrack.Droid
{
    class Pinger : IPinger
    {
        public bool PingAddress(string address)
        {
            Ping pingSender = new Ping();
            ///IPAddress address = IPAddress.
            PingReply reply = pingSender.Send(address);
            bool retVal = false;

            if (reply.Status == IPStatus.Success)
            {
                retVal = true;
                //Console.WriteLine("Address: {0}", reply.Address.ToString());
                //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
            else
            {
                //Console.WriteLine(reply.Status);
                retVal = false;
            }
            return retVal;
        }
    }


}