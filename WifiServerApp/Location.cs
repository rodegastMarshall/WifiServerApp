using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiServerApp
{
    public class LocationInfo
    {
        public Guid Id { get; set; }
        public String Location { get; set; }
        public String MAC { get; set; }
        public float MaxSignalStrength { get; set; }
        public float MinSignalStrength { get; set; }
    }
}