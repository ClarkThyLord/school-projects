using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class book
    {
        public int id { get { return id; } set { } }
        public string name, author, category;
        public double rating;

        public book(int id, string name, string author, string category, double rating)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.category = category;
            this.rating = rating;
        }
    }
}
