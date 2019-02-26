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

        worker(string name, string first_name, string last_name, DateTime date_of_birth, int access = 0) : base(name, first_name, last_name, date_of_birth)
        {
            this.access = access;
        }
    }
}
