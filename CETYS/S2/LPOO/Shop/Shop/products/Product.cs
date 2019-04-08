using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public int Type { get; }


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

            Type = (int)type;

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
                    return new Item((string)json.GetValue("Name"), (string)json.GetValue("Description"));
                case (int)Types.Book:
                    return new Book((string)json.GetValue("Name"), (string)json.GetValue("Description"));
                case (int)Types.Movie:
                    return new Movie((string)json.GetValue("Name"), (string)json.GetValue("Description"));
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
