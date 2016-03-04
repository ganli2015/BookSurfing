using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace BookSurfing
{
    public class CommonFunction
    {
        static public HtmlElement GetFirstElementByClass(HtmlElementCollection collection, string classname)
        {
            foreach (HtmlElement subelem in collection)
            {
                if (subelem.GetAttribute("classname") == classname)
                {
                    return subelem;
                }
            }

            return null;
        }

        static public List<HtmlElement> GetElementsByClass(HtmlElementCollection collection, string classname)
        {
            List<HtmlElement> res = new List<HtmlElement>();
            foreach (HtmlElement subelem in collection)
            {
                if (subelem.GetAttribute("classname") == classname)
                {
                    res.Add(subelem);
                }
            }

            return res;
        }

        static public int CompareByRating(Book left, Book right)
        {
            if (left.Rating > right.Rating)
            {
                return -1;
            }
            else if (left.Rating == right.Rating)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        static public bool IsInShelf(List<Book> shelf, Book book)
        {
            Book findBook = shelf.Find(x => x.Title == book.Title);
            if (findBook == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public int ExtraceIntBetweenTags(string rawStr, string tagBegin, string tagEnd)
        {
            int startIndex = rawStr.IndexOf(tagBegin) + tagBegin.Length;
            if (startIndex == -1 + tagBegin.Length)
            {
                return -1;
            }
            int endIndex = startIndex;
            do
            {
                endIndex++;
            } while (rawStr[endIndex] != tagEnd[0]);

            string pages = rawStr.Substring(startIndex, endIndex - startIndex);

            string num = "";
            foreach (char item in pages)
            {
                if (item >= 48 && item <= 58)
                {
                    num += item;
                }
            }

            return Convert.ToInt32(num);
        }

        static public string GetHtml(string url)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string res = wc.DownloadString(url);

            return res;
        }

        static public void DisplayBookTitles(ListBox box, List<Book> books)
        {
            box.Items.Clear();

            books.ForEach(b =>
            {
                box.Items.Add(b.Title);
            });
        }

        static public Book FindFirstBook(List<Book> shelf, string title)
        {
            return shelf.Find(x => x.Title == title);
        }

        static public void OutputReadingDates(SortedDictionary<DateTime, int> stat_year_bookNum, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            foreach (KeyValuePair<DateTime, int> pair in stat_year_bookNum)
            {
                string year_month = "";
                int year = pair.Key.Year;
                int month = pair.Key.Month;
                year_month += Convert.ToString(year) + "_" + Convert.ToString(month);
                sw.Write(year_month);
                sw.Write(',');
                sw.Write(pair.Value);
                sw.Write("\r\n");
            }

            sw.Flush();
            fs.Close();
        }

        static public void OutputBookInfo(List<Book> shelf, string filename)
        {
            char mainTag = '!';
            char similarTag = '@';
            char tagTag = '#';

            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            shelf.ForEach(book =>
            {
                sw.Write(book.Title);
                sw.Write(mainTag);

                sw.Write(book.Pages);
                sw.Write(mainTag);

                book.SimilarBook.ForEach(simi =>
                {
                    sw.Write(simi.Title);
                    sw.Write(similarTag);
                });
                sw.Write(mainTag);

                book.Tags.ForEach(tag =>
                {
                    sw.Write(tag);
                    sw.Write(tagTag);
                });
                sw.Write(mainTag);

                sw.Write(book.url);

                sw.Write("\r\n");
            });

            sw.Flush();
            fs.Close();
        }

        static public List<Book> InputBookInfo(string filename)
        {
            List<Book> res = new List<Book>();

            char mainTag = '!';
            char similarTag = '@';
            char tagTag = '#';

            FileStream fs = new FileStream(@"Titles.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            //Read Titles
            List<string> similarStr = new List<string>();
            while (!sr.EndOfStream)
            {
                Book book = new Book();

                string line = sr.ReadLine();
                string[] segment = line.Split(mainTag);

                book.Title = segment[0];
                book.Pages = Convert.ToInt32(segment[1]);

                similarStr.Add(segment[2]);

                string[] tags = segment[3].Split(tagTag);
                foreach (string tag in tags)
                {
                    if (tag != "")
                    {
                        book.Tags.Add(tag);
                    }
                }

                book.url = segment[4];

                res.Add(book);
            }
            fs.Close();

            //Read Similar Books
            for (int i = 0; i < similarStr.Count; ++i)
            {
                if (similarStr[i] == "")
                {
                    continue;
                }

                Book book = res[i];
                string[] similarArray = similarStr[i].Split(similarTag);
                foreach (string str in similarArray)
                {
                    if (str != "")
                    {
                        Book similarBook = new Book();
                        similarBook.Title = str;
                        book.SimilarBook.Add(similarBook);
                    }
                }
            }

            return res;
        }
    }
}