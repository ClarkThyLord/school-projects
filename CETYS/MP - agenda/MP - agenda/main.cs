using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___agenda
{
    class main
    {
        public static int _id = 0;
        public static MP___agenda.database.Contact[] db = new MP___agenda.database.Contact[100];

        static void Main(string[] args)
        {
            Console.WriteLine("Christian Moises Cuevas Larin 029794");
            Console.WriteLine("====================================");
            Console.WriteLine("Agenda V1.0");
            Console.WriteLine("************************************");

            Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda )");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                string answer = Console.ReadLine().ToLower();
                
                switch (answer)
                {
                    case "?":
                        ayuda();
                        break;
                    case "salir":
                        return;
                    case "a":
                        agregar();
                        break;
                    case "r":
                        quitar();
                        break;
                    case "s":
                        buscar();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("No entendí del todo, intente de nuevo...");
                        continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\nPulse cualquier tecla para continuar...");
                Console.ReadKey();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("¿Qué te gustaría que haga ahora? (? : ayuda )");
            }
        }

        static void ayuda()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("*************");
            Console.WriteLine("Menú de Ayuda");
            Console.WriteLine("*************");
            Console.WriteLine("?       :   ayuda");
            Console.WriteLine("");
            Console.WriteLine("a       :   añadir personas a la agenda");
            Console.WriteLine("r       :   remover a la gente a la agenda");
            Console.WriteLine("s       :   buscar personas en la agenda");
            Console.WriteLine("");
            Console.WriteLine("salir   :   salir del programa");
        }

        static void agregar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            if (_id >= 99)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡Lo siento Agenda ha alcanzado su capacidad!");
                return;
            }

            db[_id] = new MP___agenda.database.Contact();

            db[_id].id = _id;

            string valor = "";
            while (true)
            {
                Console.WriteLine("Nombre de contacto:");
                valor = Console.ReadLine();

                Console.WriteLine("{0} ¿estás seguro? (y/n)", valor);
                string ans = Console.ReadLine();
                if (!(ans == "n"))
                {
                    break;
                }
            }
            db[_id].nombre = valor;

            Console.Clear();

            valor = "";
            while (true)
            {
                Console.WriteLine("Telefono casa de contacto:");
                valor = Console.ReadLine();

                Console.WriteLine("{0} ¿estás seguro? (y/n)", valor);
                string ans = Console.ReadLine();
                if (!(ans == "n"))
                {
                    break;
                }
            }
            db[_id].telefono_casa = valor;

            Console.Clear();

            valor = "";
            while (true)
            {
                Console.WriteLine("Telefono celular de contacto:");
                valor = Console.ReadLine();

                Console.WriteLine("{0} ¿estás seguro? (y/n)", valor);
                string ans = Console.ReadLine();
                if (!(ans == "n"))
                {
                    break;
                }
            }
            db[_id].telefono_celular = valor;

            Console.Clear();

            valor = "";
            while (true)
            {
                Console.WriteLine("Correo electronico de contacto:");
                valor = Console.ReadLine();

                Console.WriteLine("{0} ¿estás seguro? (y/n)", valor);
                string ans = Console.ReadLine();
                if (!(ans == "n"))
                {
                    break;
                }
            }
            db[_id].correo_electronico = valor;

            Console.Clear();

            Console.WriteLine("ID: {0}", db[_id].id);
            Console.WriteLine("Nombre: {0}", db[_id].nombre);
            Console.WriteLine("Telefono de Casa: {0}", db[_id].telefono_casa);
            Console.WriteLine("Telefono Celular: {0}", db[_id].telefono_celular);
            Console.WriteLine("Correo Electronico: {0}", db[_id].correo_electronico);
        }

        static void quitar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            
            if (_id == 0 )
            {
                Console.WriteLine("No hay contactos para eliminar...");
                return;
            }

            int id = 0;
            Console.WriteLine("ID del usuario que desea eliminar: (0 - 99)");
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Por favor ingrese un ID real, entre 0-99...");
                }

                if ((id < 0 && id > 99) || db[id].id != -1)
                {
                    Console.WriteLine("No es un ID válido...");
                    continue;
                }
                break;
            }

            if (db[id].id != -1)
            {
                Console.WriteLine("{0} ~ {1} ¿estás seguro? (y/n)", id, db[id].nombre);
                string ans = Console.ReadLine();
                if (!(ans == "n"))
                {
                    Console.WriteLine("Volviendo al menú principal ...");
                    return;
                }

                db[id].removed = false;
            }
        }

        static void buscar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

        }
    }
}
