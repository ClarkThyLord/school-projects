using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class client : user
    {
        public List<book> owns = new List<book>();

        public client(int id, string name, string first_name, string last_name, DateTime date_of_birth) : base(id, name, first_name, last_name, date_of_birth)
        {
        }
    }
}
