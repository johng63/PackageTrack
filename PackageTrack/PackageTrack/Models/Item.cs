using System;

namespace PackageTrack.Models
{
    public class Item
    {
        public string _id { get; set; }
        public string BarCode { get; set; }
        public string CheckInUser { get; set; }
        public string CheckOutUser { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Project { get; set; }
        //public string Status { get; set; }
        public string Description { get; set; }
        public string ___v { get; set; }
    }
}