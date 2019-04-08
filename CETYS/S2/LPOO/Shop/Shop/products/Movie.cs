using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    internal class Movie : Product
    {
        public int Length
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
        
        public Movie(string name = null, string description = null) : base(Types.Movie, name, description) {}
    }
}
