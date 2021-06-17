using System;
using System.Collections.Generic;
using System.Text;

namespace StrettoFlatData.Models
{
    public class Flat
    {
        public string street { get; set; }
        public string city { get; set; }
        public int zip { get; set; }
        public string state { get; set; }
        public int beds { get; set; }
        public int baths { get; set; }
        public int sq__ft { get; set; }
        public string type { get; set; }
        public string sale_date { get; set; }
        public int price { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }        
    }
}
