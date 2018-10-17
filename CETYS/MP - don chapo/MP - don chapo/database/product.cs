using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___don_chapo.database
{
    class product
    {
        public int id;
        public int count;
        public bool removed = false;

        public product(int _id = 0, int _count = 0)
        {
            id = _id;
            count = _count;
        }
    }
}
