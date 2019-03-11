using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    class Jugardor
    {
        public bool terminado = false;
        public Mano mano = new Mano();

        public void sacar_carta(Baraja baraja)
        {
            mano.RecibiriCarta(baraja.ReparteCarta());

            if (mano.SumaMano() > 21)
            {

            }
        }
    }
}
