using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Configuration;
using StrettoFlatData.Models;
using StrettoFlatData.Global;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;

namespace StrettoFlatData.Processor
{
    public class FlatDataprocess
    {
        private List<Flat> flatList;

        public FlatDataprocess()
        {
            flatList = extractFlatData();
        }

        

        public void getFlatData(string requestType) 
        {
            bool valid = false;
            try
            {
                if (!String.IsNullOrEmpty(requestType) && requestType.All(Char.IsDigit)) 
                {valid = true;}

                if (valid) 
                {
                    //List<Flat> Flats =  extractFlatData();

                    //int request_type = int.Parse(requestType);
                    switch (requestType)
                    {
                        case Constants.option1: //Get all flats 
                            getAllFlats();
                            break;
                        case Constants.option2: //Get largetflatList 
                            largestFlatList();
                            break;
                        case Constants.option3: //Get all flats 
                            cheapestFlat();
                            break;
                        case Constants.option4: //Get all flats 
                            getFlatPrice();
                            break;
                        default:
                            return;
                    }
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
                   
            
        }

        protected List<Flat> extractFlatData()
        {
            string excelFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.dataFile);
            List<Flat> flatList = new List<Flat>();

            if (!File.Exists(excelFile)) 
            {
                Console.WriteLine(Messages.file_not_exists);
                return flatList;
            }

            using (var reader = new StreamReader(excelFile))
            {

                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    try
                    {
                        csvReader.Read();
                        csvReader.ReadHeader();
                        flatList = csvReader.GetRecords<Flat>().ToList();
                    }
                    catch (CsvHelper.HeaderValidationException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                    return flatList;
                }
            }

        }

        protected void getAllFlats() 
        {
            try
            {
                if (flatList != null && flatList.Count > 0) 
                {
                    formatOutput(Constants.option1,flatList);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        protected void largestFlatList()
        {
            try
            {
                if (flatList != null && flatList.Count > 0)
                {
                    //Filter 
                    var objlist = flatList.GroupBy(x => x.city, (key, y) => y.OrderByDescending(z => z.sq__ft).First());

                    formatOutput(Constants.option2, objlist);
                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        protected void cheapestFlat()
        {
            List<Flat> objList = new List<Flat>();
            try
            {
                if (flatList != null && flatList.Count > 0)
                {
                   
                    var flat = flatList.OrderByDescending(x => x.beds + x.baths).First();
                    if (flat != null) { objList.Add(flat); }                   
                    formatOutput(Constants.option3, objList);                          
                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        protected void getFlatPrice()
        {
            try
            {
                if (flatList != null && flatList.Count > 0)
                {
                     
                    var objlist = flatList.GroupBy(x => x.city, (key, y) => y.OrderByDescending(z => z.price).First());
                    // Loop through the List and show them in Console
                    foreach (var flat in objlist)
                    {
                        //Call APi to get tax code
                        double tax = getCityTaxAsync(flat.city).Result;

                        //Calculate flat price including tax
                        flat.price = flat.price + (int)Math.Round(flat.price * tax / 100);
                    }

                    formatOutput(Constants.option4, objlist);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        protected async Task<double> getCityTaxAsync(string city)
        {
            Double cityTax = 0;  
            try
            {
                if (!string.IsNullOrEmpty(city))
                {
                    string uri = string.Format(Constants.apiUri, city);

                    using (HttpClient client = new HttpClient()) 
                    {
                        string responseBody = await client.GetStringAsync(uri);
                        if (!string.IsNullOrEmpty(responseBody)) { cityTax = Convert.ToDouble(responseBody); }                   
                    }
                                        
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return cityTax;
        }

        protected void formatOutput(string optionType, IEnumerable<Flat> displayList) 
        {
            List<string> outputList = new List<string>();
            try
            {
                foreach (var flat in displayList)
                        {
                            StringBuilder data = new StringBuilder();
                            data.Append(FormatHelper.street);
                            data.Append(flat.street);
                            data.Append("; ") ;
                            data.Append(FormatHelper.city);
                            data.Append(flat.city);
                            data.Append("; ");
                            data.Append(FormatHelper.zip);
                            data.Append(flat.zip);
                            data.Append("; ");
                            data.Append(FormatHelper.state);
                            data.Append(flat.state);
                            data.Append("; ");
                            data.Append(FormatHelper.beds);
                            data.Append(flat.beds);
                            data.Append("; ");
                            data.Append(FormatHelper.baths);
                            data.Append(flat.baths);
                            data.Append("; ");
                            data.Append(FormatHelper.sqft);
                            data.Append(flat.sq__ft);
                            data.Append("; ");
                            data.Append(FormatHelper.type);
                            data.Append(flat.type);
                            data.Append("; ");
                            data.Append(FormatHelper.sale_date);
                            data.Append(flat.sale_date);
                            data.Append("; ");
                            data.Append(FormatHelper.price);
                            data.Append(flat.price);                            

                        if (optionType == Constants.option1) 
                        {
                            data.Append("; ");
                            data.Append(FormatHelper.latitude);
                            data.Append(flat.latitude);
                            data.Append("; ");
                            data.Append(FormatHelper.longitude);
                            data.Append(flat.longitude);                            
                        }
                           data.Append(".");
                    outputList.Add(data.ToString());

                        }

                foreach (string output in outputList) 
                { Console.WriteLine(output); } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
