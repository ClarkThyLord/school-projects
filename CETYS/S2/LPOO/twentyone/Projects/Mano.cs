using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projects
{
    class Mano
    {
        public List<Carta> mano { get; set; }

        public Mano()
        {
            mano = new List<Carta>();
        }

        public void RecibiriCarta(Carta c)
        {
            mano.Add(c);
        }

        public int SumaMano()
        {
            int suma = 0;
            int ases = 0;
            foreach (Carta c in mano)
            {
                if (c.Valor == 11)
                {
                    ases++;
                }
                suma += c.Valor;
            }

            while( suma > 21 && ases > 0)
            {
                suma -= 10;
                ases--;
            }

            return suma;
        }
        
        public void Dibujate(Canvas c, int posX, int posY, double scale=1)
        {
            posX -= (75 + (int)(mano.Count * (15 * scale))) / 2;

            foreach (Carta carta in mano)
            {
                carta.Dibujate(c, posX, posY, scale);
                posX += (int)(15 * scale);
            }
        }
    }
}
