using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___agenda.database
{
    class Contact
    {
        public int id;
        public bool removed = false;
        public string nombre;
        public string telefono_casa;
        public string telefono_celular;
        public string correo_electronico;

        public Contact(int _id = -1, string _nombre = "", string _telefono_casa = "", string _telefono_celular = "", string _correo_electronico = "")
        {
            id = _id;
            nombre = _nombre;
            telefono_casa = _telefono_casa;
            telefono_celular = _telefono_celular;
            correo_electronico = _correo_electronico;
        }
    }
}
