using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___don_chapo
{
    class main
    {
        public static Random random = new Random();
        public static MP___don_chapo.database.product[] db = new MP___don_chapo.database.product[random.Next(1, 50)];

        static void Main(string[] args)
        {
            header();

            // CREATE PRODUCT
            int[] stocks = new int[db.Length];
            for (int i = 0; i < db.Length; ++i)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Generando inventario {0}/{1}...", i + 1, db.Length);

                int stock = 0;
                while (true)
                {
                    stock = random.Next(0, 100);

                    if (!stocks.Contains(stock))
                    {
                        stocks[i] = stock;
                        break;
                    }
                }

                db[i] = new MP___don_chapo.database.product(i, stock);
            }

            Console.ReadKey();

            while (true)
            {
                header();
                Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda )");

                Console.ForegroundColor = ConsoleColor.Blue;
                string answer = Console.ReadLine().ToLower();

                switch (answer)
                {
                    case "?":
                        ayuda();
                        break;
                    case "salir":
                        return;
                    case "lista":
                        lista();
                        break;
                    case "max":
                        max();
                        break;
                    case "repoblar":
                        repoblar();
                        break;
                    case "eliminar":
                        eliminar();
                        break;
                    default:
                        error("No entendí del todo, intente de nuevo...");
                        Console.ReadKey();
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

        static void error(string error, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        static void header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Christian Moises Cuevas Larin 029794");
            Console.WriteLine("====================================");
            Console.WriteLine("DON CHAPO V1.0");
            Console.WriteLine("************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        static void ayuda()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("*************");
            Console.WriteLine("Menú de Ayuda");
            Console.WriteLine("*************");
            Console.WriteLine("?          :   ayuda");
            Console.WriteLine("");
            Console.WriteLine("lista      :   lista de productos");
            Console.WriteLine("max        :   producto con el maximo producto");
            Console.WriteLine("repoblar   :   lista de productos para repoblar");
            Console.WriteLine("eliminar   :   eliminar mercancía con cero producto");
            Console.WriteLine("");
            Console.WriteLine("salir      :   salir del programa");
        }

        static void lista()
        {
            header();
            Console.WriteLine("******************");
            Console.WriteLine("LISTA DE PRODUCTOS");
            Console.WriteLine("******************");

            foreach (MP___don_chapo.database.product product in db)
            {
                if (product is MP___don_chapo.database.product && !product.removed)
                {
                    Console.WriteLine("ID: {0} : COUNT: {1}", product.id, product.count);
                }
            }
        }

        static void max()
        {
            header();
            Console.WriteLine("***************");
            Console.WriteLine("PRODUCTO MAXIMO");
            Console.WriteLine("***************");

            MP___don_chapo.database.product max_product = new MP___don_chapo.database.product();
            foreach (MP___don_chapo.database.product product in db)
            {
                if (max_product is MP___don_chapo.database.product && product is MP___don_chapo.database.product)
                {
                    if (product.count > max_product.count && !product.removed)
                    {
                        max_product = product;
                    }
                }
            }

            Console.WriteLine("ID: {0} : COUNT: {1}", max_product.id, max_product.count);
        }

        static void repoblar()
        {
            header();
            Console.WriteLine("********************************");
            Console.WriteLine("LISTA DE PRODUCTOS PARA REPOBLAR");
            Console.WriteLine("********************************");

            foreach (MP___don_chapo.database.product product in db)
            {
                if (product is MP___don_chapo.database.product)
                {
                    if (product.count >= 1 && product.count < 50 && !product.removed)
                    {
                        Console.WriteLine("REPOBLA: ID: {0} : COUNT: {1}", product.id, product.count);
                    }
                }
            }
        }

        static void eliminar()
        {
            header();
            Console.WriteLine("********************************");
            Console.WriteLine("LISTA DE PRODUCTOS PARA ELIMINAR");
            Console.WriteLine("********************************");

            foreach (MP___don_chapo.database.product product in db)
            {
                if (product is MP___don_chapo.database.product)
                {
                    if (product.count == 0 && !product.removed)
                    {
                        product.removed = true;
                        Console.WriteLine("SIENDO ELIMINADO: ID: {0} : COUNT: {1}", product.id, product.count);
                    }
                }
            }
        }
    }
}
