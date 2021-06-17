using System;
using System.Collections.Generic;
using System.Text;

namespace StrettoFlatData.Global
{
    public class Messages
    {
        public static string welcome = string.Format("\n{0} \n {1}\n {2}\n {3}\n {4}\n {5}\n {6}", "Please select from below options for flats information.."," 1 -To display all Flats information", " 2 -Larget residential flat in each city ", " 3 -Cheapest Flat with exists with Maximun number of rooms(including Bed & bath rooms)", " 4 -Expensive Flat of each City", " **Exit by entering any other key", "Please enter input..");
        public static string file_not_exists = "Data file not exists/missing at specified path. Please check."; 
    }

    public static class Constants
    {
        public const string option1 = "1";
        public const string option2 = "2";
        public const string option3 = "3";
        public const string option4 = "4";

        public const string apiUri = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/taxes?city={0}";
        //public const string dataFile = @"C:\Users\srini\source\repos\Data\Flats.csv";
        public const string dataFile = @"Data\Flats.csv";

    }

    public static class FormatHelper
    {
        public const string street = "Street:";
        public const string city = "City:";
        public const string zip = "Zip:";
        public const string state = "State:";
        public const string beds = "Beds:";
        public const string baths = "Baths:";
        public const string sqft = "Sqft:";
        public const string type = "Type:";
        public const string sale_date = "SaleDate:";
        public const string price = "Price:";
        public const string latitude = "latitude:";
        public const string longitude = "Longitude:";
    }
}
