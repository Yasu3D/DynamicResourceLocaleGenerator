using System;
using System.Text;
using Csv;

namespace DynamicResourceLocaleGenerator
{
    internal class Locale
    {
        public string Key = "";
        public Dictionary<string, string> Strings = new();
    }
    
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "locales.csv");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("locales.csv not found! "); 
            }
            
            string csv = File.ReadAllText("locales.csv");
            List<ICsvLine> lines = CsvReader.ReadFromText(csv).ToList();
            
            List<Locale> locales = GetLocales(lines);
            Dictionary<string, string> files = new();

            CreateFiles(files, locales);
            WriteFiles(files);
        }

        internal static List<Locale> GetLocales(List<ICsvLine> lines)
        {
            List<Locale> locales = [];
            
            for (int i = 0; i < lines.Count; i++)
            {
                ICsvLine line = lines[i];
                
                // Create locales
                if (i == 0)
                {
                    for (int j = 1; j < line.Values.Length; j++)
                    {
                        Locale locale = new() { Key = line.Headers[j] };
                        locales.Add(locale);
                    }
                }

                string key = line.Values[0];
                for (int j = 1; j < line.Values.Length; j++)
                {
                    locales[j - 1].Strings[key] = line.Values[j];
                }
            }

            return locales;
        }

        internal static void CreateFiles(Dictionary<string, string> files, List<Locale> locales)
        {
            foreach (Locale locale in locales)
            {
                string result = "<!-- Auto-Generated with DynamicResourceLocaleGenerator.exe -->\n" +
                                "<ResourceDictionary xmlns=\"https://github.com/avaloniaui\"\n" +
                                "                    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\n" +
                                "                    xmlns:system=\"clr-namespace:System;assembly=System.Runtime\">\n";

                foreach (KeyValuePair<string, string> pair in locale.Strings)
                {
                    result += $"    <system:String x:Key=\"{pair.Key}\">{pair.Value}</system:String>\n";
                }
                
                result += "</ResourceDictionary>";

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{locale.Key}.axaml");
                files.Add(path, result);
            }
        }

        internal static void WriteFiles(Dictionary<string, string> files)
        {
            foreach (KeyValuePair<string, string> pair in files)
            {
                File.WriteAllText(pair.Key, pair.Value);
            }
        }
    }
}