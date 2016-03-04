using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookSurfing
{
    public class BookListParser
    {
        static public string GetTitle(HtmlElement elem)
        {
            HtmlElementCollection a_collection = elem.GetElementsByTagName("a");
            HtmlElement titleElem = a_collection[1];

            if (titleElem == null)
            {
                return "";
            }
            else
            {
                return titleElem.InnerText.Trim();
            }

        }

        static public double GetRating(HtmlElement elem)
        {
            HtmlElementCollection div_collection = elem.GetElementsByTagName("div");
            HtmlElement div_rating = CommonFunction.GetFirstElementByClass(div_collection, "rating");
            if (div_rating == null)//评分人数不够
            {
                return -1;
            }

            HtmlElementCollection span_collection = div_rating.GetElementsByTagName("span");
            HtmlElement span_rating = CommonFunction.GetFirstElementByClass(span_collection, "rating_nums");

            return Convert.ToDouble(span_rating.InnerText);
        }

        static public string GetHref(HtmlElement elem)
        {
            HtmlElementCollection a_collection = elem.GetElementsByTagName("a");
            HtmlElement titleElem = a_collection[1];

            if (titleElem == null)
            {
                return "";
            }
            else
            {
                return titleElem.GetAttribute("href");
            }
        }

        static public bool GetDiscription(HtmlElement elem, out string author, out List<string> translator, out string press, out string publishDate, out double price)
        {
            try
            {
                HtmlElementCollection div_collection = elem.GetElementsByTagName("div");
                HtmlElement div_pub = CommonFunction.GetFirstElementByClass(div_collection, "pub");
                string desc = div_pub.InnerText;
                string[] desc_elems = desc.Split('/');

                int size = desc_elems.Count();

                //处理价格
                int priceIndex = -1;
                price = 0;
                for (int i = desc_elems.Length - 1; i >= 0; --i)
                {
                    if (ParsePrice(desc_elems[i], out price))
                    {
                        priceIndex = i;
                        break;
                    }
                }

                //处理出版日期
                int publishDateIndex = -1;
                DateTime dateTime;
                publishDate = "";
                for (int i = 0; i < desc_elems.Length; ++i)
                {
                    if (DateTime.TryParse(desc_elems[i], out dateTime))
                    {
                        publishDateIndex = i;
                        publishDate = desc_elems[i];
                        break;
                    }
                }

                //处理出版社
                int pressIndex = -1;
                if (publishDateIndex != -1 && priceIndex != -1)
                {
                    pressIndex = Math.Min(publishDateIndex, priceIndex) - 1;
                }
                else if (publishDateIndex == -1 && priceIndex == -1)
                {
                    pressIndex = size - 1;
                }
                else if (publishDateIndex != -1)
                {
                    pressIndex = publishDateIndex - 1;
                }
                else
                {
                    pressIndex = priceIndex - 1;
                }
                press = desc_elems[pressIndex];

                author = desc_elems[0];

                //剩余的字符串都是译者
                translator = new List<string>();
                for (int i = 1; i < pressIndex; ++i)
                {
                    translator.Add(desc_elems[i]);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                author = "";
                translator = new List<string>();
                press = "";
                publishDate = "";
                price = 0;

                return false;
            }
        }

        static public int GetPages(HtmlElement elem)
        {
            throw new NotImplementedException();
        }

        static public int GetPages(string html)
        {
            throw new NotImplementedException();
        }

        static bool ParsePrice(string priceStr, out double price)
        {

            if (!Double.TryParse(priceStr, out price))
            {
                string priceTrimed = "";
                if (ContainPriceUnit(priceStr))
                {
                    foreach (char ch in priceStr)
                    {
                        if (ch >= 48 && ch <= 58)
                        {
                            priceTrimed += ch;
                        }
                        if (ch == '.')
                        {
                            priceTrimed += ch;
                        }
                    }
                    price = Convert.ToDouble(priceTrimed);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        static bool ContainPriceUnit(string priceStr)
        {
            if (priceStr.Contains("元") || priceStr.Contains("CNY") || priceStr.Contains("NT") || priceStr.Contains("TWD"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public string GetNextPageHref(WebBrowser web)
        {
            HtmlElementCollection span_collection = web.Document.GetElementsByTagName("span");
            HtmlElement next_span = null;
            foreach (HtmlElement elem in span_collection)
            {
                if (elem.GetAttribute("classname") == "next")
                {
                    next_span = elem;
                    break;
                }
            }

            HtmlElementCollection a_collection = next_span.GetElementsByTagName("a");
            foreach (HtmlElement elem in a_collection)
            {
                return elem.GetAttribute("href");
            }

            return "";
        }

        static public DateTime GetReadingDate(HtmlElement elem)
        {
            HtmlElementCollection span_collection = elem.GetElementsByTagName("span");
            HtmlElement date_span = CommonFunction.GetFirstElementByClass(span_collection, "date");

            string innerText = date_span.InnerText;
            string dateStr = innerText.Substring(0, innerText.IndexOf("读"));
            return DateTime.Parse(dateStr);
        }
    }
}