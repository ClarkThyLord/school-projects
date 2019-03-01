using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class worker : user
    {
        public int access = 0;

        public worker(int id, string name, string first_name, string last_name, DateTime date_of_birth, int access) : base(id, name, first_name, last_name, date_of_birth)
        {
            this.access = access;
        }

        public override string ToString()
        {
            return $"ID: {this.id} | Full Name: {get_full_name()}, DoB: {date_of_birth} | ACCESS: {this.access}";
        }
    }
}
