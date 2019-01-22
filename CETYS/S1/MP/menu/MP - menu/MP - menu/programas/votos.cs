using System;
using System.Collections.Generic;

namespace MP___menu.programas.votos
{
    public class votos
    {
        public static int[] resultados = new int[] {
            1,
            3,
            1,
            4,
            2,
            2,
            1,
            4,
            1,
            1,
            1,
            2,
            1,
            3,
            1,
            4
        };

        public static Dictionary<int, double> run()
        {
            Dictionary<int, double> candidatos = new Dictionary<int, double> {
                {
                    1,
                    0
                },
                {
                    2,
                    0
                },
                {
                    3,
                    0
                },
                {
                    4,
                    0
                }
            };

            foreach (int resultado in resultados)
            {
                candidatos[resultado] += 1;
            }

            Console.WriteLine("Resultados: {0}", string.Join(", ", resultados));
            foreach (KeyValuePair<int, double> candidato in candidatos)
            {
                Console.WriteLine("Candidato #{0}: {1} | {2}%", candidato.Key, candidato.Value, (candidato.Value / resultados.Length) * 100);
            }

            return candidatos;
        }
    }
}
