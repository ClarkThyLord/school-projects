using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    public class Product
    {
        private static int _id = 0;

        [JsonIgnore]
        public int id { get; }

        public enum Types
        {
            Item,
            Book,
            Movie
        };
        public Types Type { get; }


        public string Name {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        } = 0;

        public int Amount
        {
            get;
            set;
        } = 0;

        public Product(Types type, string name=null, string description=null)
        {
            id = _id;
            _id++;

            Type = type;

            Name = name == null ? $"New {(Types)Type} #{id}" : name;
            Description = description == null ? $"{(Types)Type}'s description" : description;
        }

        public virtual string to_json()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Product from_json(JObject json)
        {
            switch ((int)json.GetValue("Type"))
            {
                case (int)Types.Item:
                    return new Item((string)json.GetValue("Name"), (string)json.GetValue("Description"), (double)json.GetValue("Weight"), (string)json.GetValue("Dimensions"), (string)json.GetValue("Usage"), (string)json.GetValue("Producers"));
                case (int)Types.Book:
                    return new Book((string)json.GetValue("Name"), (string)json.GetValue("Description"), (int)json.GetValue("Pages"), (string)json.GetValue("Language"), (string)json.GetValue("Genre"), (string)json.GetValue("Authors"), (string)json.GetValue("Editors"), (string)json.GetValue("Publishers"), (double)json.GetValue("Rating"));
                case (int)Types.Movie:
                    return new Movie((string)json.GetValue("Name"), (string)json.GetValue("Description"), (double)json.GetValue("Lenght"), (string)json.GetValue("Language"), (string)json.GetValue("Genre"), (string)json.GetValue("Actors"), (string)json.GetValue("Directors"), (string)json.GetValue("Producers"), (string)json.GetValue("Studio"), (double)json.GetValue("Rating"));
                default:
                    return new Product((Types)(int)json.GetValue("Type"), (string)json.GetValue("Name"), (string)json.GetValue("Description"));
            }
        }

        public override string ToString()
        {
            return $"ID: {id} - {Name} : {Price} x {Amount} : {(Description.Length > 20 ? Description.Substring(0, 20) + "..." : Description)}";
        }
    }
}
