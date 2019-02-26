using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class book
    {
        public int id;
        public string name, author, category;
        public double rating;

        book(string name, string author, string category, double rating)
        {
            this.name = name;
            this.author = author;
            this.category = category;
            this.rating = rating;
        }
    }
}
