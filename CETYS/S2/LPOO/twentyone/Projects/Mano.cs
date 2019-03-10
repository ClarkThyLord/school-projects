using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
