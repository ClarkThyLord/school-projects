using System;

namespace EXAMONE
{
    class Program
    {
        // 1) Abstracción, encapsulación, herencia, polimorfismo
        // 2) Microsoft Visual Studio es un entorno de desarrollo integrado de Microsoft. Se utiliza para desarrollar programas informáticos, así como sitios web, aplicaciones web, servicios web y aplicaciones móviles.
        // 3) public class nombre_del_padre { ... } -> public class nombre_del_niño: nombre_del_padre { ... }
        // 4) base: cuando se hereda, la base se usa para referirse al padre; por ejemplo, para llamar al constructor del padre desde el niño

        //    
        //    
        //    
        static int[] game = new int[9];

        //  XO
        // OOX
        // XOX
        //static int[] game = new int[9] {
        //    0,
        //    1,
        //    2,
        //    2,
        //    2,
        //    1,
        //    1,
        //    2,
        //    1
        //};

        static void Main(string[] args)
        {
            while (true)
            {
                int winner = 0;
                Console.Clear();
                for (int turn = 0; turn < 9; turn++)
                {
                    Console.Clear();
                    render_guide();
                    if (turn > 0)
                    {
                        Console.Write("\n");
                        render_game();
                    }
                    
                    Console.Write($"Turn #{turn + 1}\nJugadora #{((turn + 1) % 2 == 0 ? "2 (X)" : "1 (O)")}: ");

                    int action = 0;
                    while (!int.TryParse(Console.ReadLine(), out action) || !((action >= 0 && action <= 8) && game[action] == 0))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Elija una posición correcta!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    game[action] = ((turn + 1) % 2) + 1;

                    if (check_victory(((turn + 1) % 2) + 1))
                    {
                        winner = ((turn + 1) % 2) + 1;
                        break;
                    }
                }
                Console.Clear();
                render_game();

                if (winner > 0) Console.WriteLine($"Juego terminado, ganador jugadora #{(winner % 2 == 0 ? "1 (O)" : "2 (X)")}!");
                else Console.WriteLine("Juego terminado, nadie ganó!");

                Console.ReadKey();
                game = new int[9];
            }
        }

        static void render_guide()
        {
            for (int i = 0; i < game.Length; i++)
            {
                Console.Write(i);

                //if ((i + 1) % 3 == 0) Console.Write("\n");
                if ((i + 1) % 3 == 0) Console.Write("\n-+-+-\n");
                else Console.Write('|');
            }
        }

        static void render_game()
        {
            for (int i = 0; i < game.Length; i++)
            {
                switch (game[i])
                {
                    case 1:
                        Console.Write("X");
                        break;
                    case 2:
                        Console.Write("O");
                        break;
                    default:
                        Console.Write(" ");
                        break;
                }

                //if ((i + 1) % 3 == 0) Console.Write("\n");
                if ((i + 1) % 3 == 0) Console.Write("\n-+-+-\n");
                else Console.Write('|');
            }
        }

        static bool check_victory(int player)
        {
            bool victory = false;

            // 012
            // 345
            // 678

            if ((game[0] == player && game[1] == player && game[2] == player) || (game[3] == player && game[4] == player && game[5] == player) || (game[6] == player && game[7] == player && game[8] == player)) victory = true;
            else if ((game[0] == player && game[3] == player && game[6] == player) || (game[1] == player && game[4] == player && game[7] == player) || (game[2] == player && game[5] == player && game[8] == player)) victory = true;
            else if ((game[0] == player && game[4] == player && game[8] == player) || (game[2] == player && game[4] == player && game[6] == player)) victory = true;

            return victory;
        }
    }
}
