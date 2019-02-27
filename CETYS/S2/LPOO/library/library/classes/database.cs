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

        public client get_client(int id)
        {
            return null;
        }

        public client add_client(string name, string first_name, string last_name, DateTime date_of_birth, worker _worker)
        {
            if (_worker.access < 2) return null;

            client _client = new client(USERS_COUNT, name, first_name, last_name, date_of_birth);

            USERS.Add(_client);

            USERS_COUNT++;

            return _client;
        }

        public client modify_client(int id, Dictionary<string, dynamic> modify, worker _worker)
        {
            if (_worker.access < 2) return null;

            return null;
        }

        public client remove_client(int id, worker _worker)
        {
            if (_worker.access < 2) return null;

            return null;
        }

        public worker get_worker(int id)
        {
            return WORKERS.Find(_worker => _worker.id == id);
        }

        public worker add_worker(string name, string first_name, string last_name, DateTime date_of_birth, int access, worker _worker)
        {
            if (_worker.access < 2) return null;

            worker __worker = new worker(WORKERS_COUNT, name, first_name, last_name, date_of_birth, access);

            WORKERS.Add(__worker);

            WORKERS_COUNT++;

            return __worker;
        }

        public worker modify_worker(int id, Dictionary<string, dynamic> modify, worker _worker)
        {
            if (_worker.access < 2) return null;

            worker __worker = null;
            for (int i = 0; i < WORKERS.Count; i++)
            {
                if (WORKERS[i].id == id)
                {
                    if (modify.ContainsKey("name")) WORKERS[i].name = modify["name"];
                    if (modify.ContainsKey("first_name")) WORKERS[i].name = modify["first_name"];
                    if (modify.ContainsKey("last_name")) WORKERS[i].name = modify["last_name"];
                    if (modify.ContainsKey("date_of_birth")) WORKERS[i].name = modify["date_of_birth"];
                    if (modify.ContainsKey("access") && WORKERS[i].access < _worker.access) WORKERS[i].name = modify["access"];

                    __worker = WORKERS[i];
                    break;
                }
            }

            return __worker;
        }

        public worker remove_worker(int id, worker _worker)
        {
            if (_worker.access < 2) return null;

            worker __worker = get_worker(id);

            if (_worker.access > __worker.access) return null;

            WORKERS.Remove(__worker);

            return __worker;
        }

        public book get_book(int id)
        {
            return BOOKS.Find(_book => _book.id == id);
        }

        public book add_book(string name, string author, string category, double rating, worker _worker)
        {
            if (_worker.access < 1) return null;

            book _book = new book(BOOKS_COUNT, name, author, category, rating);

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
