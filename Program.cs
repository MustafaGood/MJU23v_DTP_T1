using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MJU23v_DTP_T1
{
    public class Language
    {
        public string Family { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string Area { get; set; }
        public string Link { get; set; }

        public Language(string line)
        {
            string[] fields = line.Split("|");
            if (fields.Length != 6) // FIXME: Handle incorrect number of fields
            {
                Console.WriteLine($"Incorrect number of fields in input line: {line}");
                return;
            }
            Family = fields[0];
            Group = fields[1];
            Name = fields[2];
            Population = (int)double.Parse(fields[3], CultureInfo.InvariantCulture);
            Area = fields[4];
            Link = fields[5];
        }

        public void Print()
        {
            Console.WriteLine($"Language {Name}:");
            Console.Write($"  family: {Family}");
            if (!string.IsNullOrEmpty(Group))
            {
                Console.Write($">{Group}");
            }
            Console.WriteLine($"\n  population: {Population}");
            Console.WriteLine($"  area: {Area}");
        }
    }

    public class Program
    {
        static string dir = @"..\..\..";
        static List<Language> languages = new List<Language>();

        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader($"{dir}\\lang.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Language lang = new Language(line);
                        if (lang != null)
                        {
                            languages.Add(lang);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading input file: {ex.Message}");
                return;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing population: {ex.Message}");
                return;
            }

            Console.WriteLine("==== Languages in Spain ====");
            foreach (Language lang in languages)
            {
                if (lang.Area.Contains("Spain"))
                {
                    lang.Print();
                }
            }

            Console.WriteLine("==== Baltic Languages ====");
            foreach (Language lang in languages)
            {
                if (lang.Group.Contains("Baltic"))
                {
                    lang.Print();
                }
            }

            Console.WriteLine("==== Population larger than 50 million ====");
            foreach (Language lang in languages)
            {
                if (lang.Population >= 50_000_000)
                {
                    lang.Print();
                }
            }

            Console.WriteLine("==== Number of Germanics ====");
            int germanicPopulation = GetPopulationByGroup("Germanic");
            Console.WriteLine($"Germanic speaking population: {germanicPopulation}");

            Console.WriteLine("==== Number of Romance ====");
            int romancePopulation = GetPopulationByGroup("Romance");
            Console.WriteLine($"Romance speaking population: {romancePopulation}");
        }

        static int GetPopulationByGroup(string group)
        {
            int population = 0;
            foreach (Language language in languages)
            {
                if (language.Group.Contains(group))
                {
                    population += language.Population;
                }
            }
            return population;
        }
    }
}
