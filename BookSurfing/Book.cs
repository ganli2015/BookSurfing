using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSurfing
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Translator { get; set; }
        public string Press { get; set; }
        public string PublishDate { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public int Pages { get; set; }
        public string url { get; set; }
        public DateTime ReadingDate { get; set; }
        public List<Book> SimilarBook { get; set; }
        public List<string> Tags { get; set; }

        public Book()
        {
            Translator = new List<string>();
            SimilarBook = new List<Book>();
            Tags = new List<string>();
        }
    }
}