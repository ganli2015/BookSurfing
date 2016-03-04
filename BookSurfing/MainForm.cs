using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using HtmlAgilityPack;

namespace BookSurfing
{
    public partial class MainForm : Form
    {
        List<Book> _shelf = new List<Book>();
        Book _preferredBook;
        SearchPreferredBooks _surf;
        BookClassifier classifier;

        string _myUrl = "http://book.douban.com/people/remember11/collect";

        public MainForm()
        {
            InitializeComponent();
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            button_Load.Enabled = false;
            button_Surfing.Enabled = false;
            _shelf.Clear();
            listBox_ShowMyBooks.Items.Clear();

            if (textBox_MyUrl.Text == "")
            {
                InitializeListBox(_myUrl);
            }
            else
            {
                InitializeListBox(textBox_MyUrl.Text);
            }
        }

        private void InitializeListBox(string url)
        {
            NavigateWeb(url);
            //TestForHtmlDownload();
        }

        private void NavigateWeb(string url)
        {
            WebBrowser web = new WebBrowser();
            web.Navigate(url);
            web.ScriptErrorsSuppressed = true;
            web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);
        }

        private void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            if (web.ReadyState != WebBrowserReadyState.Complete)
                return;
            if (web == null)
            {
                return;
            }

            HtmlElementCollection li_collection = web.Document.GetElementsByTagName("li");
            List<HtmlElement> info_div = CommonFunction.GetElementsByClass(li_collection, "subject-item");

            foreach (HtmlElement elem in info_div)
            {
                Book book = ParseBookInfo(elem);
                ReadBookWeb(book);
                _shelf.Add(book);
                listBox_ShowMyBooks.Items.Add(book.Title);
            }

            string nextUrl = BookListParser.GetNextPageHref(web);
            web.Dispose();

            if (nextUrl == "")
            {
                MessageBox.Show("Over!");
                button_Load.Enabled = true;
                button_Surfing.Enabled = true;
                InitilizeComboBox();

                return;
            }

            NavigateWeb(nextUrl);
        }

        private Book ParseBookInfo(HtmlElement elem)
        {
            string title = BookListParser.GetTitle(elem);

            string author, press, publishdate;
            List<string> translators;
            double price;
            BookListParser.GetDiscription(elem, out author, out translators, out  press, out publishdate, out price);

            string href = BookListParser.GetHref(elem);
            DateTime date = BookListParser.GetReadingDate(elem);

            Book book = new Book();
            book.Title = title;
            book.Author = author;
            book.Translator = translators;
            book.Press = press;
            book.PublishDate = publishdate;
            book.Price = price;
            book.Rating = 0;
            book.url = href;
            book.ReadingDate = date;

            return book;
        }

        private void ReadBookWeb(Book book)
        {
            string html = CommonFunction.GetHtml(book.url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            book.Pages = BookWebParser.GetPages(html);
            book.SimilarBook = BookWebParser.GetPreferredBooks(doc);
            book.Tags = BookWebParser.GetTags(doc);
        }

        private void listBox_ShowMyBooks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox_ShowMyBooks.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string title = listBox_ShowMyBooks.Items[index] as string;
                Book selectedBook = _shelf.Find(x => x.Title == title);

                string tipStr = title + "\r\n";
                tipStr += selectedBook.Author + "\r\n";
                tipStr += selectedBook.Press + "\r\n";
                tipStr += "页数： " + selectedBook.Pages + "\r\n";
                tipStr += selectedBook.Price + "元" + "\r\n";
                //tipStr += "评分： " + selectedBook.Rating + "\r\n";
                tipStr += "阅读时间： " + selectedBook.ReadingDate.Year + "-" + selectedBook.ReadingDate.Month + "-" + selectedBook.ReadingDate.Day;

                ToolTip tip = new ToolTip();
                tip.SetToolTip(listBox_ShowMyBooks, tipStr);
                tip.Active = true;
            }
        }

        private List<string> RemoveNullString(string[] strings)
        {
            List<string> res = new List<string>();

            foreach (string str in strings)
            {
                if (str != "")
                {
                    res.Add(str);
                }
            }

            return res;
        }

        private void button_Surfing_Click(object sender, EventArgs e)
        {
            listBox_PreferredBook.Items.Clear();

            button_Surfing.Enabled = false;
            button_Load.Enabled = false;

            _surf = new SearchPreferredBooks(_shelf);
            double ratingLowerLimit = Convert.ToDouble(numericUpDown_AboveRating.Value);
            int pagesUpperLimit = Convert.ToInt32(numericUpDown_BelowPages.Value);
            int pagesLowerLimit = Convert.ToInt32(numericUpDown_AbovePages.Value);
            _surf.RatingLowerLimit = ratingLowerLimit;
            _surf.PagesLowerLimit = pagesLowerLimit;
            _surf.PagesUpperLimit = pagesUpperLimit;
            _preferredBook = _surf.SurfingBook();

            listBox_PreferredBook.Items.Clear();
            listBox_PreferredBook.Items.Add(_preferredBook.Title);
            button_Surfing.Enabled = true;
            button_Load.Enabled = true;
            _surf.Dispose();
        }

        private void listBox_PreferredBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox_PreferredBook.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                System.Diagnostics.Process.Start(_preferredBook.url);
            }
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            button_Surfing.Enabled = true;
            button_Load.Enabled = true;
        }

        private SortedDictionary<DateTime, int> GenerateReadingDateStat()
        {
            SortedDictionary<DateTime, int> stat_year_bookNum = new SortedDictionary<DateTime, int>();
            DateTime startDate = MinDate(_shelf);
            InitializeStat(stat_year_bookNum, startDate, DateTime.Now);

            ExcludeStartDate(_shelf, startDate).ForEach(book =>
            {
                DateTime year_month = new DateTime(book.ReadingDate.Year, book.ReadingDate.Month, 1);
                stat_year_bookNum[year_month]++;
            });

            return stat_year_bookNum;
        }

        private void button_OutputTitles_Click(object sender, EventArgs e)
        {
            string filename = @"Titles.txt";

            CommonFunction.OutputBookInfo(_shelf, filename);

        }

        private void InitializeStat(SortedDictionary<DateTime, int> stat, DateTime minDate, DateTime maxDate)
        {
            for (DateTime date = minDate; date <= maxDate; date = date.AddMonths(1))
            {
                DateTime initDate = new DateTime(date.Year, date.Month, 1);
                stat[initDate] = 0;
            }
        }

        private List<Book> ExcludeStartDate(List<Book> rawShelf, DateTime startDate)
        {
            List<Book> res = new List<Book>();
            rawShelf.ForEach(book =>
            {
                if (book.ReadingDate != startDate)
                {
                    res.Add(book);
                }
            });

            return res;
        }

        private DateTime MinDate(List<Book> shelf)
        {
            DateTime minDate = DateTime.Now;
            shelf.ForEach(book =>
            {
                DateTime readingDate = book.ReadingDate;
                if (readingDate < minDate)
                {
                    minDate = readingDate;
                }
            });

            return minDate;
        }

        private void button_OutputStat_Click(object sender, EventArgs e)
        {
            CommonFunction.OutputReadingDates(GenerateReadingDateStat(), @"Stat.csv");
        }

        private void InitilizeComboBox()
        {
            comboBox_Year.Items.Clear();

            DateTime minDate = MinDate(_shelf);
            for (int i = minDate.Year; i <= DateTime.Now.Year; ++i)
            {
                comboBox_Year.Items.Add(i);
            }

            comboBox_Year.Items.Add("全部");

            comboBox_Year.Refresh();
        }

        private void comboBox_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox_Year.SelectedIndex;
            if (index == comboBox_Year.Items.Count - 1)
            {
                listBox_ShowMyBooks.Items.Clear();
                _shelf.ForEach(book =>
                {
                    listBox_ShowMyBooks.Items.Add(book.Title);
                });
            }
            else
            {
                int year = Convert.ToInt32(comboBox_Year.Items[index]);

                listBox_ShowMyBooks.Items.Clear();
                _shelf.ForEach(book =>
                {
                    if (book.ReadingDate.Year == year)
                    {
                        listBox_ShowMyBooks.Items.Add(book.Title);
                    }
                });
            }
        }

        private void TestForHtmlDownload()
        {
            int num = 20;
            string url = "https://www.baidu.com/";

            DateTime start = DateTime.Now;
            for (int i = 0; i < num; ++i)
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.Headers.Clear();
                request.Timeout = 30000;
                request.Method = "GET";
                request.ContentType = "text/html";
                HttpWebResponse response = null;
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch (System.Net.WebException ex)
                {
                    continue;
                }
                string res = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    res = sr.ReadToEnd();
                }
            }
            DateTime end = DateTime.Now;
            double du1 = (end - start).Seconds;

            start = DateTime.Now;
            for (int i = 0; i < num; ++i)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string res = wc.DownloadString(url);
            }
            end = DateTime.Now;
            double du2 = (end - start).Seconds;

        }

        private void button_LoadBookInfo_Click(object sender, EventArgs e)
        {
            _shelf.Clear();

            _shelf = CommonFunction.InputBookInfo(@"Titles.txt");

            CommonFunction.DisplayBookTitles(listBox_ShowMyBooks, _shelf);
            comboBox_Year.Items.Add("全部");
        }

        private void listBox_ShowMyBooks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox_ShowMyBooks.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    string title = listBox_ShowMyBooks.Items[index] as string;
                    Book book = _shelf.Find(x => x.Title == title);

                    BookGraph graph = new BookGraph(_shelf);
                    List<Book> connectedBooks = graph.GetConnectedBooks(book);

                    listBox_ShowMyBooks.Items.Clear();
                    CommonFunction.DisplayBookTitles(listBox_ShowMyBooks, connectedBooks);
                }
            }
        }

        private void button_Classify_Click(object sender, EventArgs e)
        {
            classifier = new BookClassifier(_shelf);
            classifier.Classify();
            classifier.InitClassComboBox(comboBox_Class);

            comboBox_Class.Items.Add("全部");
        }

        private void comboBox_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_ShowMyBooks.Items.Clear();

            string tag = comboBox_Class.Items[comboBox_Class.SelectedIndex] as string;
            if (tag != "全部")
            {
                List<int> classIndex = classifier.GetBookIndexes(tag);
                classIndex.ForEach(i => { listBox_ShowMyBooks.Items.Add(_shelf[i].Title); });
            }
            else
            {
                CommonFunction.DisplayBookTitles(listBox_ShowMyBooks, _shelf);
            }
        }


    }
}