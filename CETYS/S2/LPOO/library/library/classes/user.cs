using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.classes
{
    class user
    {
        public int id;
        public string name, first_name, last_name;
        public DateTime date_of_birth;

        public user(string name, string first_name, string last_name, DateTime date_of_birth)
        {
            this.name = name;
            this.first_name = first_name;
            this.last_name = last_name;
            this.date_of_birth = date_of_birth;
        }

        public string get_full_name()
        {
            return $"{name} {first_name} {last_name}";
        }

        public TimeSpan get_age()
        {
            return DateTime.Now.Subtract(date_of_birth);
        }
    }
}
