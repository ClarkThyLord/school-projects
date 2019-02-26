using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class database
    {
        int USERS_COUNT = 1;
        List<user> USERS = new List<user>();

        int WORKERS_COUNT = 1;
        List<worker> WORKERS = new List<worker>();

        int BOOKS_COUNT = 1;
        List<book> BOOKS = new List<book>();

        public book get_book(int id)
        {
            return BOOKS.Find(_book => _book.id == id);
        }

        public book add_book(book _book, worker _worker)
        {
            if (_worker.access < 1) return null;

            _book.id = BOOKS_COUNT;

            BOOKS.Add(_book);

            BOOKS_COUNT++;

            return _book;
        }

        public book modify_book(int id, Dictionary<string, dynamic> modify, worker _worker)
        {
            if (_worker.access < 1) return null;

            book _book = null;
            for (int i = 0; i < BOOKS.Count; i++)
            {
                if (BOOKS[i].id == id)
                {
                    if (modify.ContainsKey("name")) BOOKS[i].name = modify["name"];
                    if (modify.ContainsKey("author")) BOOKS[i].name = modify["author"];
                    if (modify.ContainsKey("category")) BOOKS[i].name = modify["category"];
                    if (modify.ContainsKey("rating")) BOOKS[i].name = modify["rating"];

                    _book = BOOKS[i];
                    break;
                }
            }
                

            return _book;
        }

        public book remove_book(int id, worker _worker)
        {
            if (_worker.access < 1) return null;

            book _book = get_book(id);

            BOOKS.Remove(_book);

            return _book;
        }

        public List<book> search_book(string name, string author = "", string category="", int min_raiting=0)
        {
            name = name.ToLower();
            author = author.ToLower();
            category = category.ToLower();

            List<book> results = new List<book>();

            foreach (book _book in BOOKS)
            {
                bool valid = true;
                if (_book.name.IndexOf(name) == -1) valid = false;
                else if (author.Length > 0 && _book.author.IndexOf(author) == -1) valid = false;
                else if (category.Length > 0 && _book.category != category) valid = false;
                else if (_book.rating < min_raiting) valid = false;

                if (valid) results.Add(_book);
            }

            return results;
        }
    }
}
