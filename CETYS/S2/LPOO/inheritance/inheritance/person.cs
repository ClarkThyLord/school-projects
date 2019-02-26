using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inheritance
{
    class person
    {
        public string name;
        public string first_name;
        public string last_name;

        public DateTime date_of_birth;

        public person (string name, string first_name, string last_name, DateTime date_of_birth)
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

        public int get_age()
        {
            return DateTime.Now.Year - date_of_birth.Year;
        }
    }
}
