using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    class Product
    {
        private static int _id = 0;

        [JsonIgnore]
        public int id { get; }

        public string name;
        public string description;
        public int amount = 0;
        public double price = 0;

        public enum Types
        {
            Item,
            Book,
            Movie
        };
        public int type { get; }

        public Product(Types type, string name=null, string description=null)
        {
            this.id = _id;
            _id++;

            this.type = (int)type;

            this.name = name == null ? $"New {(Types)this.type} #{this.id}" : name;
            this.description = description == null ? $"{(Types)this.type}'s description" : description;
        }

        public virtual string to_json()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Product from_json(JObject json)
        {
            switch ((int)json.GetValue("type"))
            {
                case (int)Types.Item:
                    return new Item((Types)(int)json.GetValue("type"), (string)json.GetValue("name"), (string)json.GetValue("description"));
                case (int)Types.Book:
                    return new Book((Types)(int)json.GetValue("type"), (string)json.GetValue("name"), (string)json.GetValue("description"));
                case (int)Types.Movie:
                    return new Movie((Types)(int)json.GetValue("type"), (string)json.GetValue("name"), (string)json.GetValue("description"));
                default:
                    return new Product((Types)(int)json.GetValue("type"), (string)json.GetValue("name"), (string)json.GetValue("description"));
            }
        }

        public override string ToString()
        {
            return $"ID: {this.id} - {this.name} : {this.amount} : {(this.description.Length > 20 ? this.description.Substring(0, 20) + "..." : this.description)}";
        }
    }
}
