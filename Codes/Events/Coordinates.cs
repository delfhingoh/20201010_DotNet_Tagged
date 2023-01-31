using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT365_A01_33836223.Events
{
    public class Coordinates
    {
        private double lat;
        private double longi;

        public double GetLat() { return lat; }
        public double GetLongi() { return longi; }

        public Coordinates(double _lat, double _longi)
        {
            lat = _lat;
            longi = _longi;
        }
    }
}
