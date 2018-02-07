using System;
using System.Collections.Generic;
using System.Text;

namespace PackageTrack
{
    public interface IPinger
    {
        bool PingAddress(string address);
    }
}
