using System;
using System.Collections.Generic;
using System.Text;

namespace PackageTrack
{
    public class PropertiesHelper
    {

        public PropertiesHelper()
        {

        }

        public string GetPropertyValue(string property)
        {
            string value = "";
            if (App.Current.Properties.ContainsKey(property))
            {
                value = App.Current.Properties[property] as string;
            }

            return value;
        }

        public void SetPropertyValue(string property, string value)
        {
            if (App.Current.Properties.ContainsKey(property))
                App.Current.Properties[property] = value;
            else
                App.Current.Properties.Add(property, value);

        }
    }
}
