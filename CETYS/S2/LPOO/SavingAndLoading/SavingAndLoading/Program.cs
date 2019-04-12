using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SavingAndLoading
{
    class Program
    {
        public static string xmlDir = $@"{Directory.GetCurrentDirectory()}\xml";
        public static string jsonDir = $@"{Directory.GetCurrentDirectory()}\json\";

        public static List<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();

        static void Main(string[] args)
        {
            Console.WriteLine("Load files? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                int FileCount = 0;
                Console.WriteLine("Load which files? (xml/json)");
                string FileType = Console.ReadLine().ToLower() == "xml" ? "xml" : "json";

                Console.Clear();
                Console.WriteLine($"Loading {FileType}(s)...");

                switch (FileType)
                {
                    case "xml":
                        var xmlFiles = Directory.GetFiles(xmlDir, "*.xml");
                        foreach (var xmlFile in xmlFiles)
                        {
                            Console.Write($"\rLoading file {FileCount + 1}...");
                            ProgrammingLanguages.Add(ProgrammingLanguage.FromXML(File.ReadAllText(xmlFile)));
                            FileCount++;
                        }
                        break;
                    default:
                        var jsonFiles = Directory.GetFiles(jsonDir, "*.json");
                        foreach (var jsonFile in jsonFiles)
                        {
                            Console.Write($"\rLoading file {FileCount + 1}...");
                            ProgrammingLanguages.Add(ProgrammingLanguage.FromJSON(File.ReadAllText(jsonFile)));
                            FileCount++;
                        }
                        break;
                }
                
                Console.Write($"\rLoaded {FileCount} {FileType} files!\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Creating new database...");

                ProgrammingLanguages.Add(new ProgrammingLanguage("C#", "Microsoft", "Structured, imperative, object-oriented, event-driven, task-driven, functional, generic, reflective, concurrent", 2000));
                ProgrammingLanguages.Add(new ProgrammingLanguage("C", "	Dennis Ritchie & Bell Labs (creators); ANSI X3J11 (ANSI C); ISO/IEC JTC1/SC22/WG14 (ISO C)", "Imperative (procedural), structured", 1972));

                Console.WriteLine($"Created new database with {ProgrammingLanguages.Count} objects!");
            }

            Console.WriteLine("***---***");

            foreach (ProgrammingLanguage programmingLanguage in ProgrammingLanguages)
            {
                Console.WriteLine(programmingLanguage.ToXML());
                Console.WriteLine(programmingLanguage.ToJSON());
                Console.WriteLine(programmingLanguage.ToString());

                System.IO.Directory.CreateDirectory(xmlDir);
                File.WriteAllText($@"{xmlDir}\{programmingLanguage.Name}.xml", programmingLanguage.ToXML());

                System.IO.Directory.CreateDirectory(jsonDir);
                File.WriteAllText($@"{jsonDir}\{programmingLanguage.Name}.json", programmingLanguage.ToJSON());

                Console.WriteLine("---");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    [XmlRoot("ProgrammingLanguage")]
    public class ProgrammingLanguage
    {
        [XmlElement("Name")]
        public string Name;
        [XmlElement("Developers")]
        public string Developers;
        [XmlElement("Paradigmas")]
        public string Paradigmas;

        [XmlElement("YearIntroduced")]
        public int YearIntroduced;

        private static XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProgrammingLanguage));

        public ProgrammingLanguage() { }

        public ProgrammingLanguage(string name, string developers, string paradigmas, int year_introduced) : base() {
            Name = name;
            Developers = developers;
            Paradigmas = paradigmas;
            YearIntroduced = year_introduced;
        }

        public string ToXML()
        {
            string xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, this);
                    xml = sww.ToString();
                }
            }

            return xml;
        }

        public static ProgrammingLanguage FromXML(string xml)
        {
            ProgrammingLanguage programmingLanguage;

            using (TextReader reader = new StringReader(xml))
            {
                programmingLanguage = (ProgrammingLanguage)xmlSerializer.Deserialize(reader);
            }

            return programmingLanguage;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ProgrammingLanguage FromJSON(string json) {
            return JsonConvert.DeserializeObject<ProgrammingLanguage>(json);
        }

        public override string ToString()
        {
            return $"{Name} | {YearIntroduced} | {Developers} | {Paradigmas}";
        }
    }
}
