using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_15
    {
        public static Dictionary<int, Dictionary<string, dynamic>> zonas = new Dictionary<int, Dictionary<string, dynamic>> {
            {
                12,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "América del Norte"
                    },
                    {
                        "costo",
                        2
                    }
                }
            },
            {
                15,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "América Central"
                    },
                    {
                        "costo",
                        2.2
                    }
                }
            },
            {
                18,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "América del Sur"
                    },
                    {
                        "costo",
                        4.5
                    }
                }
            },
            {
                19,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "Europa"
                    },
                    {
                        "costo",
                        3.5
                    }
                }
            },
            {
                23,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "Asia"
                    },
                    {
                        "costo",
                        6
                    }
                }
            },
            {
                25,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "África"
                    },
                    {
                        "costo",
                        6
                    }
                }
            },
            {
                29,
                new Dictionary<string, dynamic>
                {
                    {
                        "nombre",
                        "Oceanía"
                    },
                    {
                        "costo",
                        5
                    }
                }
            }
        };

        public static Dictionary<string, dynamic> run()
        {
            Console.WriteLine("ID de Zona: (1 - {0})", zonas.Count);
            int zona = 0;
            while (!int.TryParse(Console.ReadLine(), out zona) && zonas.ContainsKey(zona))
            {
                Console.WriteLine("Por favor ingrese un número real, entre 1 y {0}...", zonas.Count);
            }

            Console.WriteLine("Minutos:");
            double minutos = 0;
            while (!double.TryParse(Console.ReadLine(), out minutos))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double costo = zonas[zona]["costo"] * minutos;

            Console.WriteLine("Zona: {0} - {1}, Minutos: {2}, Costo: {3}", zona, zonas[zona]["nombre"], minutos, costo);

            return new Dictionary<string, dynamic> {
                {
                    "zona",
                    zona
                },
                {
                    "nombre",
                    zonas[zona]["nombre"]
                },
                {
                    "minutos",
                    minutos
                },
                {
                    "costo",
                    costo
                }
            };
        }
    }
}
