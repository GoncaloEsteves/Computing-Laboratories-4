using System;
using System.Collections.Generic;
using System.Text;

namespace PGB.Models
{
    public class Notification
    {
        public string Text { get; set; }

        public Notification(string text)
        {
            Text = text;
        }
    }
}
