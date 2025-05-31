using System;
using Csv;

namespace DynamicResourceLocaleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "locales.csv");
            if (!File.Exists(path))
            {
                Console.WriteLine("No locales found. Press any key to exit.");
                Console.ReadKey();
                return;
            }
            
            string csv = File.ReadAllText("locales.csv");
            IEnumerable<ICsvLine> lines = CsvReader.ReadFromText(csv);

            foreach (ICsvLine line in lines)
            {
                Console.WriteLine(line.Raw);
            }
        }
    }
}