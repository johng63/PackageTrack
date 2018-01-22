using System;

namespace PackageTrack.Models
{
    public class ItemAdd
    {

        public string BarCode { get; set; }
        public string CheckInUser { get; set; }
        public string CheckOutUser { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }

        public static implicit operator ItemAdd(Item v)
        {
            ItemAdd i = new ItemAdd();
            i.BarCode = v.BarCode;
            i.CheckInUser = v.CheckInUser;
            i.CheckOutUser = v.CheckOutUser;
            i.Project = v.Project;
            i.Description = v.Description;

            return i;
        }
    }
}