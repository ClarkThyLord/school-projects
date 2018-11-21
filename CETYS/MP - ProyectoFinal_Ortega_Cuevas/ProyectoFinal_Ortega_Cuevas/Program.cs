using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Ortega_Cuevas
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        help();
                        break;
                    case "if":
                        if_menu();
                        break;
                    case "switch":
                        switch_menu();
                        break;
                    case "for":
                        for_menu();
                        break;
                    case "while":
                        while_menu();
                        break;
                    case "array":
                        array_menu();
                        break;
                    case "salir":
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        

        public static void header(ConsoleColor color = ConsoleColor.White)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Projecto Final - Alec Ortega 030247 y Christian Cuevas 029794");
            Console.WriteLine("-------------------------------------------------------------");

            Console.ForegroundColor = color;
        }

        public static void error(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: {0}", msg);

            Console.ReadKey();

            header(color);
        }

        public static void help()
        {
            Console.WriteLine("Bienvenido al Menu Principal");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("?        -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("if       -   abrir submenu de if");
            Console.WriteLine("switch   -   abrir submenu de switch");
            Console.WriteLine("for      -   abrir submenu de for");
            Console.WriteLine("while    -   abrir submenu de while");
            Console.WriteLine("array    -   abrir submenu de array");
            Console.WriteLine("");
            Console.WriteLine("salir    -   salir del programa");
            Console.WriteLine("------------------------------------");
        }

        public static void if_menu()
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué programa de if quieres que se ejecute? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        if_help();
                        break;
                    case "1":
                        if_1();
                        break;
                    case "2":
                        if_2();
                        break;
                    case "3":
                        if_3();
                        break;
                    case "regresar":
                        Console.WriteLine("Regresando a menú principal...");
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void if_help()
        {
            Console.WriteLine("Bienvenido al Menu de If");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("?          -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("1          -   Número válido");
            Console.WriteLine("2          -   Nombre válido");
            Console.WriteLine("3          -   Si el número es positivo o negativo");
            Console.WriteLine("");
            Console.WriteLine("regresar   -   regresar al menú principal");
            Console.WriteLine("--------------------------------------------------");
        }

        public static void if_1()
        {
            Console.WriteLine("Valor mínimo para número:");
            double min = double.Parse(Console.ReadLine());
            Console.WriteLine("Valor máximo para número:");
            double max = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el número para validar:");
            double num = double.Parse(Console.ReadLine());
            if (num >= min && num <= max)
            {
                Console.WriteLine("El número {2} es mayor o igual a {0} y es menor o igual a {1}", min, max, num);
            }
            else
            {
                Console.WriteLine("El número {2} no es mayor o igual a {0} y no es menor o igual a {1}", min, max, num);
            }
        }

        public static void if_2()
        {
            Console.WriteLine("Introduzca el mínimo número de carácteres:");
            int textMin = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca el máximo número de carácteres:");
            int textMax = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el texto para validar:");
            string text = "";
            if (text.Length >= textMin)
            {
                if(text.Length <= textMax)
                {
                    Console.WriteLine("El texto '{1}' cumple con el mínimo de carácteres {0}", textMin, text);
                }
                else
                {
                    Console.WriteLine("El texto '{1}' sobrepasa el máximo de carácteres {0}", textMax, text);
                }
            }
            else
            {
                Console.WriteLine("El texto '{1}' no cumple con el mínimo de carácteres {0}", textMin, text);
            }
        }

        public static void if_3()
        {
            Console.WriteLine("Ingrese un numero:");
            int num1 = int.Parse(Console.ReadLine());
            if (num1 < 0)
            {
                Console.WriteLine("El numero es negativo");
            }
            else
            {
                Console.WriteLine("El numero es positivo");
            }
        }

        public static void switch_menu()
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué programa de switch quieres que se ejecute? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        switch_help();
                        break;
                    case "1":
                        switch_1();
                        break;
                    case "2":
                        switch_2();
                        break;
                    case "3":
                        switch_3();
                        break;
                    case "regresar":
                        Console.WriteLine("Regresando a menú principal...");
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void switch_help()
        {
            Console.WriteLine("Bienvenido al Menu de Switch");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("?          -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("1          -   Elegir opinión");
            Console.WriteLine("2          -   Elige acción");
            Console.WriteLine("3          -   Determine si es una opción válida");
            Console.WriteLine("");
            Console.WriteLine("regresar   -   regresar al menú principal");
            Console.WriteLine("--------------------------------------------------");
        }

        public static void switch_1()
        {
            Console.WriteLine("Bienvenido al Menu de Switch");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1   -   ser presidente es genial");
            Console.WriteLine("2   -   los videojuegos son divertidos");
            Console.WriteLine("3   -   trabajar es muy genial");
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("No es cierto, el ser presidente es horrible");
                    break;
                case 2:
                    Console.WriteLine("Mentira para nada los videojuegos son aburridos");
                    break;
                case 3:
                    Console.WriteLine("El trabajar nunca ha sido genial");
                    break;
            }
        }

        public static void switch_2()
        {
            Console.WriteLine("Bienvenido al Menu de Switch");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1   -   hacer acción sumar");
            Console.WriteLine("2   -   hacer acción resta");
            Console.WriteLine("3   -   hacer acción multiplicación");
            Console.WriteLine("4   -   hacer acción división");
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");

            float a = 0, b = 0;
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("Ingresa la primera cantidad:");
                    a = float.Parse(Console.ReadLine());
                    Console.WriteLine("Ingresa la segunda cantidad:");
                    b = float.Parse(Console.ReadLine());
                    Console.WriteLine("{0} + {1} es igual a: {2}", a, b, a + b);
                    break;
                case 2:
                    Console.WriteLine("Ingresa la primera cantidad:");
                    a = float.Parse(Console.ReadLine());
                    Console.WriteLine("Ingresa la segunda cantidad:");
                    b = float.Parse(Console.ReadLine());
                    Console.WriteLine("{0} - {1} es igual a: {2}", a, b, a - b);
                    break;
                case 3:
                    Console.WriteLine("Ingresa la primera cantidad:");
                    a = float.Parse(Console.ReadLine());
                    Console.WriteLine("Ingresa la segunda cantidad:");
                    b = float.Parse(Console.ReadLine());
                    Console.WriteLine("{0} * {1} es igual a: {2}", a, b, a * b);
                    break;
                case 4:
                    Console.WriteLine("Ingresa la primera cantidad:");
                    a = float.Parse(Console.ReadLine());
                    Console.WriteLine("Ingresa la segunda cantidad:");
                    b = float.Parse(Console.ReadLine());
                    Console.WriteLine("{0} / {1} es igual a: {2}", a, b, a / b);
                    break;
            }
        }

        public static void switch_3()
        {
            Console.WriteLine("Bienvenido al Menu de Switch");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("1   -   ser presidente es genial");
            Console.WriteLine("2   -   los videojuegos no son divertidos");
            Console.WriteLine("3   -   trabajar nunca ha sido genial");
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("No es cierto, el ser presidente es horrible");
                    break;
                case 2:
                    Console.WriteLine("Mentira, los videojuegos son siempre divertidos");
                    break;
                case 3:
                    Console.WriteLine("El trabajar es muy genial");
                    break;
                default:
                    Console.WriteLine("Esta es opción no es valida...");
                    break;
            }
        }

        public static void for_menu()
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué programa de for quieres que se ejecute? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        for_help();
                        break;
                    case "1":
                        for_1();
                        break;
                    case "2":
                        for_2();
                        break;
                    case "3":
                        for_3();
                        break;
                    case "regresar":
                        Console.WriteLine("Regresando a menú principal...");
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void for_help()
        {
            Console.WriteLine("Bienvenido al Menu de For");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("?          -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("1          -   Introduzca x cantidad de números");
            Console.WriteLine("2          -   Iterar sobre x cantidad de números");
            Console.WriteLine("3          -   Operaciones sobre x cantidad de números");
            Console.WriteLine("");
            Console.WriteLine("regresar   -   regresar al menú principal");
            Console.WriteLine("-----------------------------------------------------");
        }

        public static void for_1()
        {
            Console.WriteLine("¿Cuántos números va a ingresar?");
            int numero = int.Parse(Console.ReadLine());
            string resultados = "\n\n-----------------------------------------------------\nNUMEROS INGRESADOS\n-----------------------------------------------------";
            for (int i = 0; i < numero; i++)
            {
                Console.WriteLine("Ingresa un número:");
                resultados += "\n" + int.Parse(Console.ReadLine()).ToString();
            }
            resultados += "\n-----------------------------------------------------";
            Console.WriteLine(resultados);
        }

        public static void for_2()
        {
            int[] numeros = new int[10] {
                1, 3, 5, 3, 5, 8, 9, 6, 7, 3
            };
            for (int i  = 0; i < numeros.Length; i++)
            {
                Console.WriteLine(numeros[i]);
            }
        }

        public static void for_3()
        {
            int[] numeros = new int[10] {
                1, 3, 5, 3, 5, 8, 9, 6, 7, 3
            };
            for (int i = 0; i < numeros.Length; i++)
            {
                Console.WriteLine("{0} + 1 = {1}", numeros[i], numeros[i] + 1);
            }
        }

        public static void while_menu()
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué programa de while quieres que se ejecute? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        while_help();
                        break;
                    case "1":
                        while_1();
                        break;
                    case "2":
                        while_2();
                        break;
                    case "3":
                        while_3();
                        break;
                    case "regresar":
                        Console.WriteLine("Regresando a menú principal...");
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void while_help()
        {
            Console.WriteLine("Bienvenido al Menu de While");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("?          -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("1          -   Ingrese dato mientras elige continuar");
            Console.WriteLine("2          -   Ingrese número válido mientras elige continuar");
            Console.WriteLine("3          -   Ingrese nombre válido mientras elige continuar");
            Console.WriteLine("");
            Console.WriteLine("regresar   -   regresar al menú principal");
            Console.WriteLine("-------------------------------------------------------------");
        }

        public static void while_1()
        {
            string resultados = "\n\n-----------------------------------------------------\nNUMEROS INGRESADOS\n-----------------------------------------------------";
            while (true)
            {
                Console.WriteLine("Ingresa un número:");
                resultados += "\n" + int.Parse(Console.ReadLine()).ToString();

                Console.WriteLine("¿Desea continuar? (y/n)");
                if (Console.ReadLine().ToLower() == "n")
                {
                    break;
                }
            }
            resultados += "\n-----------------------------------------------------";
            Console.WriteLine(resultados);
        }
        
        public static void while_2()
        {
            Console.WriteLine("Valor mínimo para número:");
            double min = double.Parse(Console.ReadLine());
            Console.WriteLine("Valor máximo para número:");
            double max = double.Parse(Console.ReadLine());
            

            string resultados = "\n\n-----------------------------------------------------\nNUMEROS INGRESADOS\n-----------------------------------------------------";
            while (true)
            {
                Console.WriteLine("Ingrese el número valido:");
                double num = double.Parse(Console.ReadLine());
                if (!(num >= min && num <= max))
                {
                    continue;
                }
                
                resultados += "\n" + num;

                Console.WriteLine("¿Desea continuar? (y/n)");
                if (Console.ReadLine().ToLower() == "n")
                {
                    break;
                }
            }
            resultados += "\n-----------------------------------------------------";
            Console.WriteLine(resultados);
        }
        
        public static void while_3()
        {
            Console.WriteLine("Introduzca el mínimo número de carácteres:");
            int textMin = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca el máximo número de carácteres:");
            int textMax = int.Parse(Console.ReadLine());
            

            string resultados = "\n\n-----------------------------------------------------\nNUMEROS INGRESADOS\n-----------------------------------------------------";
            while (true)
            {
                Console.WriteLine("Ingrese el texto para validar:");
                string text = "";
                if (!(text.Length >= textMin && text.Length <= textMax))
                {
                    continue;
                }
                
                resultados += "\n" + text;

                Console.WriteLine("¿Desea continuar? (y/n)");
                if (Console.ReadLine().ToLower() == "n")
                {
                    break;
                }
            }
            resultados += "\n-----------------------------------------------------";
            Console.WriteLine(resultados);
        }

        public static void array_menu()
        {
            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué programa de array quieres que se ejecute? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                header(ConsoleColor.Yellow);
                switch (answer)
                {
                    case "?":
                        array_help();
                        break;
                    case "1":
                        array_1();
                        break;
                    case "2":
                        array_2();
                        break;
                    case "3":
                        array_3();
                        break;
                    case "regresar":
                        Console.WriteLine("Regresando a menú principal...");
                        return;
                    default:
                        header(ConsoleColor.Yellow);
                        error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void array_help()
        {
            Console.WriteLine("Bienvenido al Menu de Array");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("?          -   ayuda");
            Console.WriteLine("");
            Console.WriteLine("1          -   Crear x cantidad de tabajadores");
            Console.WriteLine("2          -   Iterar sobre x cantidad de trabajadores");
            Console.WriteLine("3          -   Dar de baja o de alta a trabajador");
            Console.WriteLine("");
            Console.WriteLine("regresar   -   regresar al menú principal");
            Console.WriteLine("------------------------------------------------------");
        }

        struct trabajador {
            public bool trabajando;
            public string nombre;
            public int id;
        }

        public static void array_1()
        {
            Console.WriteLine("¿Cuántos trabajadores quiéres ingresar?");
            int cantidad = int.Parse(Console.ReadLine());
            trabajador[] trabajadores = new trabajador[cantidad];
            for (int i = 0; i < trabajadores.Length; i++)
            {
                Console.WriteLine("Ingrese el nombre de los trabajadores:");
                string nombre = Console.ReadLine();
                
                trabajadores[i] = new trabajador();
                trabajadores[i].id = i + 1;
                trabajadores[i].nombre = nombre;
                trabajadores[i].trabajando = true;
            }

            Console.WriteLine("Cantidad de trabajadores que hay son: {0}", trabajadores.Length);
        }

        public static void array_2()
        {
            trabajador[] trabajadores = new trabajador[3] {
                new trabajador()
                {
                    id = 1,
                    nombre = "Christian",
                    trabajando = true
                },
                new trabajador()
                {
                    id = 2,
                    nombre = "Alec",
                    trabajando = true
                },
                new trabajador()
                {
                    id = 3,
                    nombre = "Ricardo",
                    trabajando = true
                }
            };

            for (int i = 0; i < trabajadores.Length; i++)
            {
                Console.WriteLine("id: {0} nombre: {1} trabajando: {2}", trabajadores[i].id, trabajadores[i].nombre, trabajadores[i].trabajando);
            }
        }

        public static void array_3()
        {
            trabajador[] trabajadores = new trabajador[3] {
                new trabajador()
                {
                    id = 1,
                    nombre = "Christian",
                    trabajando = true
                },
                new trabajador()
                {
                    id = 2,
                    nombre = "Alec",
                    trabajando = true
                },
                new trabajador()
                {
                    id = 3,
                    nombre = "Ricardo",
                    trabajando = true
                }
            };

            int id = 0;
            while (true)
            {
                Console.WriteLine("¿A qué usuario quisiera dar de alta o de baja? (integre 1 al 3)");
                id = int.Parse(Console.ReadLine());
                if (id < 0 || id > trabajadores.Length)
                {
                    continue;
                }
                break;
            }
            trabajadores[id].trabajando = false;
            for (int i = 0; i < trabajadores.Length; i++)
            {
                Console.WriteLine("id: {0} nombre: {1} trabajando: {2}", trabajadores[i].id, trabajadores[i].nombre, trabajadores[i].trabajando);
            }
        }
    }
}
