using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerAppV2ASP.Models
{
    public class StoryItems
    {
        public string Text { get; set; }
        public string Button1Text { get; set; }
        public string Button2Text { get; set; }

        public StoryItems(string text, string button1, string button2)
        {
            Text = text;
            Button1Text = button1;
            Button2Text = button2;
        }
    }
}