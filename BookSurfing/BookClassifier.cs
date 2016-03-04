using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSurfing
{
    public class BookClassifier
    {
        struct BookClassInfo
        {
            public string tag;
            public List<int> indexes;
        }

        List<BookClassInfo> _classified = new List<BookClassInfo>();
        List<Book> _shelf = new List<Book>();

        public BookClassifier(List<Book> shelf)
        {
            _shelf = shelf;
        }

        public void Classify()
        {
            _classified.Clear();

            Book[] books = new Book[_shelf.Count];
            _shelf.CopyTo(books);
            List<Book> shelf_copy = new List<Book>(books);

            List<BookClassInfo> initClassified = InitClassify(shelf_copy);
            List<BookClassInfo> merged = MergeSameTags(initClassified);
            _classified = LetIndividualFindHome(merged);

            //             List<BookClassInfo> initClassified = InitClassify(shelf_copy);
            //             _classified = MergeSameTags(initClassified);

            //Sort _classified
            _classified.Sort((left, right) =>
            {
                return left.indexes.Count <= right.indexes.Count ? 1 : -1;
            });
        }

        public void InitClassComboBox(System.Windows.Forms.ComboBox box)
        {
            box.Items.Clear();
            int size = _classified.Count;
            for (int i = 0; i < size; ++i)
            {
                box.Items.Add(_classified[i].tag);
            }
        }

        public List<int> GetBookIndexes(string tag)
        {
            return _classified.Find(info => info.tag == tag).indexes;
        }

        private string FindMostCommonTag(List<Book> books)
        {
            Dictionary<string, int> tag_count = new Dictionary<string, int>();

            books.ForEach(b =>
            {
                b.Tags.ForEach(tag =>
                {
                    if (!tag_count.ContainsKey(tag))
                    {
                        tag_count[tag] = 1;
                    }
                    else
                    {
                        tag_count[tag]++;
                    }
                });
            });

            int maxCount = -1;
            string maxTag = "";
            foreach (KeyValuePair<string, int> pair in tag_count)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    maxTag = pair.Key;
                }
            }

            return maxTag;

        }

        private List<BookClassInfo> InitClassify(List<Book> shelf_copy)
        {
            List<BookClassInfo> classified = new List<BookClassInfo>();

            //Classify by finding sub connected region of books!
            while (shelf_copy.Count != 0)
            {
                BookGraph graph = new BookGraph(shelf_copy);
                List<Book> connectedBooks = graph.GetConnectedBooks(shelf_copy[0]);

                List<int> classIndex = new List<int>();
                connectedBooks.ForEach(book =>
                {
                    int index = _shelf.FindIndex(x => x.Title == book.Title);
                    classIndex.Add(index);

                    shelf_copy.Remove(book);
                });

                BookClassInfo classInfo = new BookClassInfo();
                classInfo.tag = FindMostCommonTag(connectedBooks);
                classInfo.indexes = classIndex;
                classified.Add(classInfo);
            }

            return classified;
        }

        private List<BookClassInfo> MergeSameTags(List<BookClassInfo> initClassified)
        {
            List<BookClassInfo> newClassified = new List<BookClassInfo>();

            while (initClassified.Count != 0)
            {
                string tag = initClassified[0].tag;
                List<BookClassInfo> sameTag = initClassified.FindAll(cl => cl.tag == tag);
                BookClassInfo newBookInfo = new BookClassInfo();
                newBookInfo.indexes = new List<int>();
                newBookInfo.tag = tag;
                sameTag.ForEach(s => { newBookInfo.indexes.AddRange(s.indexes); });
                newClassified.Add(newBookInfo);

                sameTag.ForEach(s => initClassified.Remove(s));
            }

            return newClassified;
        }

        private List<BookClassInfo> LetIndividualFindHome(List<BookClassInfo> initClassified)
        {

            //Select classes whose count of book is above averageCountPerClass
            double averageCountPerClass = (double)_shelf.Count / initClassified.Count;
            Dictionary<string, BookClassInfo> tag_BookInfo = GenerateMainTags(averageCountPerClass, initClassified);
            List<BookClassInfo> newClassified = SynchronousNewClassified(tag_BookInfo);

            //Merge classes whose count of book is below averageCountPerClass to classes above
            initClassified.ForEach(cl =>
            {
                if (cl.indexes.Count <= averageCountPerClass)
                {
                    TryMergeClass(tag_BookInfo, cl);
                }
            });

            return newClassified;
        }

        private Dictionary<string, BookClassInfo> GenerateMainTags(double averageCountPerClass, List<BookClassInfo> initClassified)
        {
            Dictionary<string, BookClassInfo> tag_BookInfo = new Dictionary<string, BookClassInfo>();
            initClassified.ForEach(cl =>
            {
                if (cl.indexes.Count > averageCountPerClass)
                {
                    tag_BookInfo[cl.tag] = cl;
                }
            });
            BookClassInfo other = new BookClassInfo();
            other.tag = "其他";
            other.indexes = new List<int>();
            tag_BookInfo["其他"] = other;

            return tag_BookInfo;
        }

        private List<BookClassInfo> SynchronousNewClassified(Dictionary<string, BookClassInfo> tag_BookInfo)
        {
            List<BookClassInfo> newClassified = new List<BookClassInfo>();

            foreach (KeyValuePair<string, BookClassInfo> pair in tag_BookInfo)
            {
                newClassified.Add(pair.Value);
            }

            return newClassified;
        }

        private void TryMergeClass(Dictionary<string, BookClassInfo> tag_BookInfo, BookClassInfo cl)
        {
            List<int> indexToRemove = new List<int>();
            cl.indexes.ForEach(i =>
            {
                string commonTag = GetCommonTag(_shelf[i], tag_BookInfo);
                if (commonTag != "")//if commonTag exists,than merge and remove 
                {
                    tag_BookInfo[commonTag].indexes.Add(i);
                    indexToRemove.Add(i);
                }
            });

            indexToRemove.ForEach(i => cl.indexes.Remove(i));

            if (cl.indexes.Count != 0)
            {
                tag_BookInfo["其他"].indexes.AddRange(cl.indexes);
            }
        }

        private string GetCommonTag(Book book, Dictionary<string, BookClassInfo> tag_BookInfo)
        {
            string res = "";
            book.Tags.ForEach(tag =>
            {
                if (tag_BookInfo.ContainsKey(tag))
                {
                    res = tag;
                }
            });

            return res;
        }
    }
}