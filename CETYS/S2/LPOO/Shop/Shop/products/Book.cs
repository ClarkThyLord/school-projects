﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    internal class Book : Product
    {
        public int Pages
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

        public string Author
        {
            get;
            set;
        } = "";
        public string Editors
        {
            get;
            set;
        } = "";
        public string Publisher
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

        public Book(string name = null, string description = null) : base(Types.Book, name, description) { }

        public Book(string name, string description, int pages, string language, string genre, string author, string editors, string publisher, double rating) : this(name, description)
        {
            Pages = pages;
            Language = language;
            Genre = genre;
            Author = author;
            Editors = editors;
            Publisher = publisher;
            Rating = rating;
        }
    }
}