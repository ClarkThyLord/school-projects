using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    class main
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda )");

                string commando = Console.ReadLine().ToLower();
                switch (commando)
                {
                    case "?":
                        Console.WriteLine("Ayuda:");
                        Console.WriteLine("");
                        Console.WriteLine("estadistico   -   corre programa");
                        Console.WriteLine("votos         -   corre programa");
                        Console.WriteLine("estadio       -   corre programa");
                        Console.WriteLine("");
                        Console.WriteLine("?             -   ayuda");
                        Console.WriteLine("salir         -   salir del programa");
                        break;
                    case "salir":
                        return;
                    case "estadistico":
                        MP___menu.programas.estadistico.estadistico.run();
                        break;
                    case "estadio":
                        MP___menu.programas.estadio.estadio.run();
                        break;
                    case "votos":
                        MP___menu.programas.votos.votos.run();
                        break;
                    default:
                        Console.WriteLine("No entendí del todo, intente de nuevo...");
                        break;
                }
            }
        }
    }
}
