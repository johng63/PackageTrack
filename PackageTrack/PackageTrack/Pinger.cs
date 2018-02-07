using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PackageTrack
{
    class Pinger : IPinger
    {
        IPinger pinger = DependencyService.Get<IPinger>();

        public bool PingAddress(string address)
        {
           return pinger.PingAddress(address);
        }
    }
}
