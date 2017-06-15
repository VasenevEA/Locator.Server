using System;

namespace Locator.API
{
    public class State
    {
        public int UserID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Heading { get; set; }
        public double Accuracy { get; set; }

        public double BatteryLevel { get; set; }

        public DateTime DateTime { get; set; }
    }
}
