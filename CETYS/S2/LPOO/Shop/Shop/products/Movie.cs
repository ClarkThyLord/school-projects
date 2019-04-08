using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    public class Movie : Product
    {
        public double Length
        {
            get;
            set;
        } = 0;
        public string Language
        {
            get;
            set;
        } = "";
        public string Genre
        {
            get;
            set;
        } = "";

        public string Actors
        {
            get;
            set;
        } = "";
        public string Directors
        {
            get;
            set;
        } = "";
        public string Producers
        {
            get;
            set;
        } = "";
        public string Studio
        {
            get;
            set;
        } = "";

        private double rating = 0;
        public double Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }
        
        public Movie(string name="", string description="", double price=0, int amount=0) : base(Types.Movie, name, description, price, amount) {}

        public Movie(string name, string description, double price, int amount, double length, string language, string genre, string actors, string directors, string producers, string studio, double rating) : this(name, description, price, amount)
        {
            Length = length;
            Language = language;
            Genre = genre;
            Actors = actors;
            Directors = directors;
            Producers = producers;
            Studio = studio;
            Rating = rating;
        }
    }
}
