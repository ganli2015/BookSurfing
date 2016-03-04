using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Net;

namespace BookSurfing
{
    public class SearchPreferredBooks
    {
        List<Book> _shelf = new List<Book>();
        List<Book> _preferredBooks = new List<Book>();
        Book _preferredBook;
        public bool SurfingFinish { get; private set; }
        List<string> _searchedUrls = new List<string>();
        WebBrowser _bookWeb;
        WebBrowser _preferWeb;


        bool _condition_rating = false;
        bool _condition_pages = false;
        public double RatingLowerLimit
        {
            get;
            set;
        }
        public int PagesLowerLimit
        {
            get;
            set;
        }
        public int PagesUpperLimit
        {
            get;
            set;
        }

        //         bool _preferredBookChecked = false;
        //         bool _satifyCondition = false;
        //         bool _goon = false;

        public SearchPreferredBooks(List<Book> shelf)
        {
            _shelf = shelf;
            SurfingFinish = false;
        }

        public void Dispose()
        {
            if (_bookWeb != null)
                _bookWeb.Dispose();
            if (_preferWeb != null)
                _preferWeb.Dispose();
        }

        public Book GetPreferredBook()
        {
            return _preferredBook;
        }

        public Book SurfingBook()
        {
            CheckConditional();

            Random ran = new Random();

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            for (int i = 0; ; i++)
            {
                //LoadBookWeb(_shelf[ranIndex]);

                int ranIndex = ran.Next(0, _shelf.Count - 1);
                string html = wc.DownloadString(_shelf[ranIndex].url);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                List<Book> preferBooks = BookWebParser.GetPreferredBooks(doc);
                foreach (Book book in preferBooks)
                {
                    Book findBook = _preferredBooks.Find(x => x.Title == book.Title);
                    if (findBook == null)
                    {
                        _preferredBooks.Add(book);
                    }
                    else
                    {
                        if (CommonFunction.IsInShelf(_shelf, findBook))
                        {
                            continue;
                        }

                        if (BookSatisfyCondition(findBook))
                        {
                            return findBook;
                        }
                    }
                }
            }

        }

        private void CheckConditional()
        {
            if (RatingLowerLimit != 0)
            {
                _condition_rating = true;
            }

            if ((PagesLowerLimit != 0 || PagesUpperLimit != 0) && PagesUpperLimit >= PagesLowerLimit)
            {
                _condition_pages = true;
            }
        }

        private bool BookSatisfyCondition(Book book)
        {
            if (!_condition_pages && !_condition_rating)
            {
                _preferredBook = book;
                SurfingFinish = true;
                return true;
            }

            WebClient wc = new WebClient();
            string html = wc.DownloadString(book.url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            int pages = BookWebParser.GetPages(html);
            if (_condition_pages)
            {
                if (pages > PagesUpperLimit || pages < PagesLowerLimit)
                {
                    return false;
                }
            }

            double rating = BookWebParser.GetRating(doc);
            if (_condition_rating)
            {
                if (rating < RatingLowerLimit)//如果不满足再重新搜索
                {
                    return false;
                }
            }


            return true;
        }
    }
}