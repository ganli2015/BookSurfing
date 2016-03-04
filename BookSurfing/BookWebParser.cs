using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookSurfing
{
    public class BookWebParser
    {
        static public double GetRating(HtmlElement elem)
        {
            HtmlElement strong = CommonFunction.GetFirstElementByClass(elem.GetElementsByTagName("strong"), "ll rating_num ");
            return Convert.ToDouble(strong.InnerText);
        }

        static public double GetRating(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//strong[@class='ll rating_num ']");
            return Convert.ToDouble(node.InnerText.Trim());
        }

        static public int GetPages(HtmlElement elem)
        {
            string text = elem.InnerText;

            string tagBegin = "页数:";
            string tagEnd = "\r";
            return CommonFunction.ExtraceIntBetweenTags(text, tagBegin, tagEnd);
        }

        static public int GetPages(string html)
        {
            string tagBegin = "页数:</span> ";
            string tagEnd = "<br/>";

            return CommonFunction.ExtraceIntBetweenTags(html, tagBegin, tagEnd);
        }

        static public List<Book> GetPreferredBooks(HtmlElement element)
        {
            List<Book> books = new List<Book>();

            HtmlElementCollection dd_collection = element.GetElementsByTagName("dd");
            //读入当前的prefer书
            foreach (HtmlElement elem in dd_collection)
            {
                HtmlElementCollection a_collection = elem.GetElementsByTagName("a");
                HtmlElement a = a_collection[0];
                Book book = new Book();
                book.Title = a.InnerText.Trim();
                book.url = a.GetAttribute("href");
                books.Add(book);
            }

            return books;
        }

        static public List<Book> GetPreferredBooks(HtmlAgilityPack.HtmlDocument doc)
        {
            List<Book> books = new List<Book>();

            var a_collection = doc.DocumentNode.SelectNodes("//dd/a");
            if (a_collection == null)
            {
                return books;
            }
            foreach (HtmlAgilityPack.HtmlNode elem in a_collection)
            {
                Book book = new Book();
                book.Title = elem.InnerText.Trim("\n    ".ToCharArray());
                book.url = elem.Attributes["href"].Value;
                books.Add(book);
            }

            return books;
        }

        static public List<string> GetTags(HtmlAgilityPack.HtmlDocument doc)
        {
            List<string> res = new List<string>();

            var a_collection = doc.DocumentNode.SelectNodes("//a[@class='  tag']");
            if (a_collection == null)
            {
                return res;
            }
            foreach (HtmlAgilityPack.HtmlNode elem in a_collection)
            {
                res.Add(elem.InnerText.Trim());
            }

            return res;
        }
    }
}