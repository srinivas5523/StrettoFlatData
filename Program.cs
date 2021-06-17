using System;
using System.Collections.Generic;
using StrettoFlatData.Processor;
using StrettoFlatData.Global;
using System.Linq;

namespace StrettoFlatData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool Process = true;
                string input = string.Empty;
                List<String> options = new List<string>() { Constants.option1, Constants.option2, Constants.option3, Constants.option4 };
                FlatDataprocess objProcess = new FlatDataprocess();

                while (Process)
                {
                    

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine(Messages.welcome);
                        input = Console.ReadLine(); 
                    }

                    objProcess.getFlatData(input);

                    Console.WriteLine(Messages.welcome);
                    input = Console.ReadLine();

                    Process = options.Any(input.Contains);
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
           
            




        }
    }
}
