using System;

namespace Projects
{
    class Baraja
    {
        Random r = new Random();
        public Carta [] Cartas { get; }
        
        public Baraja()
        {
            string[] palos = { "clubs", "hearts", "spades", "diamonds" };
            string[] valores = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "ace", "jack", "queen", "king" };
            Cartas = new Carta[52];

            for(int i = 0; i< 52; i++)
            {
                int valor = 0;
                if (i%13 == 9)
                {
                    valor = 11;
                }
                else if (i%13 > 9)
                {
                    valor = 10;
                }
                else
                {
                    valor = (i % 13)+2;
                }
                Carta c = new Carta(@"assets\cards\"+valores[i % 13] + "_of_" + palos[i / 13]+".png",valor);
                Cartas[i] = c;
            }
        }

        public Carta ReparteCarta()
        {
            return  Cartas[r.Next(0, 52)];
        }
    }
}
