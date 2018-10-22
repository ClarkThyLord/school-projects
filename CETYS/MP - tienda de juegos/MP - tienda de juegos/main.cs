using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___tienda_de_juegos
{
    class main
    {
        public static string[] names;
        public static string[] descriptions;

        static void Main(string[] args)
        {
            // LOAD JSONS
            using (System.IO.StreamReader r = new StreamReader("./db/products/names.json"))
            {
                string json = r.ReadToEnd();
                names = JsonConvert.DeserializeObject<string[]>(json);
            }
            using (System.IO.StreamReader r = new StreamReader("./db/products/descriptions.json"))
            {
                string json = r.ReadToEnd();
                descriptions = JsonConvert.DeserializeObject<string[]>(json);
            }

            Console.WriteLine(names[0]);
            Console.ReadKey();

            Console.WriteLine(descriptions[0]);
            Console.ReadKey();
        }
    }
}
