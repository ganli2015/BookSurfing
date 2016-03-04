using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSurfing
{
    class BookNode
    {
        public int Index { get; set; }
        public List<int> Adjacent_Forward { get; set; }
        public List<int> Adjacent_Backward { get; set; }
        public bool Marked;

        public BookNode()
        {
            Adjacent_Forward = new List<int>();
            Adjacent_Backward = new List<int>();
        }
    }

    public class BookGraph
    {
        List<BookNode> _nodes = new List<BookNode>();
        List<Book> _shelf;

        public BookGraph(List<Book> val)
        {
            _shelf = val;
            InitializeNodes(val);
        }

        private void InitializeNodes(List<Book> shelf)
        {
            //Read Nodes
            int i = 0;
            List<List<Book>> similarBooks = new List<List<Book>>();
            shelf.ForEach(book =>
            {
                BookNode node = new BookNode();
                node.Index = i;
                node.Marked = false;
                similarBooks.Add(book.SimilarBook);

                _nodes.Add(node);

                ++i;
            });

            //Node Adjacents
            for (int j = 0; j < similarBooks.Count; ++j)
            {
                similarBooks[j].ForEach(si =>
                {
                    int siIndex = shelf.FindIndex(b => b.Title == si.Title);
                    if (siIndex != -1)
                    {
                        BookNode siNode = _nodes[siIndex];
                        MakeConnection(_nodes[j], siNode);
                    }
                });
            }
        }

        public List<Book> GetConnectedBooks(Book book)
        {
            int index = _shelf.FindIndex(b => b.Title == book.Title);
            List<BookNode> connectedNodes = SearchSubRegion(index);
            List<Book> res = new List<Book>();
            connectedNodes.ForEach(n =>
            {
                res.Add(_shelf[n.Index]);
            });

            return res;
        }

        private void MakeConnection(BookNode from, BookNode to)
        {
            from.Adjacent_Forward.Add(to.Index);
            to.Adjacent_Backward.Add(from.Index);
        }

        private void ResetNodeMark()
        {
            _nodes.ForEach(n => { n.Marked = false; });
        }

        private List<BookNode> SearchSubRegion(int i)
        {
            BookNode node = _nodes[i];
            node.Marked = true;

            MarkForwardAdjacent(node);
            MarkBackwardAdjacent(node);

            List<BookNode> res = new List<BookNode>();
            _nodes.ForEach(n =>
            {
                if (n.Marked)
                {
                    res.Add(n);
                }
            });

            return res;
        }

        private void MarkForwardAdjacent(BookNode node)
        {
            node.Adjacent_Forward.ForEach(index =>
            {
                BookNode adjNode = _nodes[index];
                if (adjNode.Marked == false)
                {
                    adjNode.Marked = true;
                    MarkForwardAdjacent(adjNode);
                    MarkBackwardAdjacent(adjNode);
                }
            });
        }

        private void MarkBackwardAdjacent(BookNode node)
        {
            node.Adjacent_Backward.ForEach(index =>
            {
                BookNode adjNode = _nodes[index];
                if (adjNode.Marked == false)
                {
                    adjNode.Marked = true;
                    MarkForwardAdjacent(adjNode);
                    MarkBackwardAdjacent(adjNode);
                }
            });
        }
    }
}