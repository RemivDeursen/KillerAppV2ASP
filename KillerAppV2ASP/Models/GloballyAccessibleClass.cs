using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class GloballyAccessibleClass
    {
        private GloballyAccessibleClass() { }
        public int testint { get; set; }

        public EventSystem EventsSystem { get; set; }

        public static GloballyAccessibleClass Instance
        {
            get
            {
                IDictionary items = HttpContext.Current.Items;
                if (!items.Contains("TheInstance"))
                {
                    items["TheInstance"] = new GloballyAccessibleClass();
                }
                return items["TheInstance"] as GloballyAccessibleClass;
            }
        }
    }
}