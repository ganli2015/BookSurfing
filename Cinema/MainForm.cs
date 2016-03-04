using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Cinema
{
    public partial class MainForm : Form
    {
        string url = "http://movie.douban.com/people/remember11/collect?start=0&sort=time&rating=all&filter=all&mode=grid";

        List<Film> _films = new List<Film>();

        public MainForm()
        {
            InitializeComponent();

            LoadWeb(url);
            OutputStat();
        }

        private void LoadWeb(string url)
        {
            string nextUrl = url;
            do 
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                string html = wc.DownloadString(nextUrl);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                HtmlAgilityPack.HtmlNodeCollection infoNodes = doc.DocumentNode.SelectNodes("//div[@class='info']");
                foreach (HtmlAgilityPack.HtmlNode node in infoNodes)
                {
                    var infoNode=node.SelectSingleNode(".//li//a//em");
                    if(infoNode==null)
                    {
                        continue;
                    }
                    string title=infoNode.InnerText;
                    string dateStr = node.SelectSingleNode(".//span[@class='date']").InnerText;

                    Film film = new Film();
                    film.Title = title;
                    film.Date = DateTime.Parse(dateStr);
                    _films.Add(film);

                    listBox_Titles.Items.Add(title);
                }

                HtmlAgilityPack.HtmlNode nextNode = doc.DocumentNode.SelectSingleNode("//span[@class='next']//a");
                if (nextNode == null)
                {
                    nextUrl = "";
                }
                else
                {
                    nextUrl = nextNode.Attributes["href"].Value;
                }

            } while (nextUrl!="");
        }

        private void OutputStat()
        {
            SortedDictionary<DateTime, int> stat = new SortedDictionary<DateTime, int>();
            DateTime minDate=GetMinDate(_films);
            for (DateTime i = minDate; i < DateTime.Now; i = i.AddMonths(1))
            {
                DateTime date = new DateTime(i.Date.Year, i.Date.Month, 1);
                stat[date] = 0;
            }

            _films.ForEach(film =>
            {
                DateTime date = new DateTime(film.Date.Year, film.Date.Month, 1);
                stat[date]++;
            });

            FileStream fs = new FileStream(@"stat.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (KeyValuePair<DateTime, int> pair in stat)
            {
                sw.Write(pair.Key.Year + "_" + pair.Key.Month);
                sw.Write(",");
                sw.Write(pair.Value);
                sw.Write("\r\n");
            }

            sw.Flush();
            fs.Close();
        }

        DateTime GetMinDate(List<Film> films)
        {
            DateTime minDate = DateTime.Now;
            films.ForEach(film =>
            {
                if (film.Date < minDate)
                {
                    minDate = film.Date;
                }
            });

            return minDate;
        }
    }
}
