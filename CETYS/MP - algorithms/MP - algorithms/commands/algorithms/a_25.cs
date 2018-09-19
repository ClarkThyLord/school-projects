using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_25
    {
        public static Dictionary<string, dynamic> run()
        {
            // PSEUDOCODIGO
            // Inicio
            // Ingresa los precios para cada zona
            // Mientras hay clientes
            //  Pide de que zona(s) quiere sus boletos
            //  Pide la cuantidad de boletos que quiere comprar
            // Por canda venta
            //  Imprimir clave, cantidad y el importe total de los boletos
            // Imprimir total en ventas
            // Fin

            double ganancia_total = 0;
            List<Dictionary<string, dynamic>> zonas = new List<Dictionary<string, dynamic>>();
            List<Dictionary<string, dynamic>> clientes = new List<Dictionary<string, dynamic>>();

            Console.WriteLine("-------------------\nVendedor de Boletos\n-------------------");
            
            for (int i = 0; i < 5; i++)
            {
                zonas.Add(new Dictionary<string, dynamic>
                {
                    {
                        "costo",
                        0
                    },
                    {
                        "ventas_quantidad",
                        0
                    },
                    {
                        "ventas_total",
                        0
                    }
                });

                Console.WriteLine("¿Cuánto cuestan los boletos de la Zona #{0}?", i + 1);
                double costo = 0;
                while (!double.TryParse(Console.ReadLine(), out costo))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                zonas[i]["costo"] = costo;
            }
            
            SortedDictionary<int, int> compras = new SortedDictionary<int, int>();
            Console.WriteLine("\n-------------------\nVendedor de Boletos ~ Abierto\n-------------------\n");
            while (true)
            {
                Console.WriteLine("Cliente #{0}!:", clientes.Count + 1);

                Console.WriteLine("¿De qué zona quieres comprar boletos?");
                int zona = 0;
                while (!int.TryParse(Console.ReadLine(), out zona) && zona >= 0 && zona < 5)
                {
                    Console.WriteLine("Por favor ingrese un número real, entre 0 y 5...");
                }

                Console.WriteLine("¿Cuántos boletos te gustaría comprar?");
                int boletos = 0;
                while (!int.TryParse(Console.ReadLine(), out boletos))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                if (compras.ContainsKey(zona))
                {
                    compras[zona] += boletos;
                } else
                {
                    compras[zona] = boletos;
                }

                Console.WriteLine("¿Seguir comprando? y/n");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    continue;
                } else
                {
                    foreach (KeyValuePair<int, int> compra in compras)
                    {
                        zonas[compra.Key]["ventas_total"] += compra.Value;
                        ganancia_total += compra.Value * zonas[compra.Key]["costo"];
                        zonas[compra.Key]["ventas_quantidad"] += compra.Value * zonas[compra.Key]["costo"];

                        Console.WriteLine("Zona #{0} : {1} Boletos ~ ${2}", compra.Key, compra.Value, compra.Value * zonas[compra.Key]["costo"]);
                    }

                    clientes.Add(new Dictionary<string, dynamic>
                    {
                        {
                            "compras",
                            compras
                        },
                        {
                            "total",
                            ganancia_total
                        }
                    });

                    compras = new SortedDictionary<int, int>();

                    Console.WriteLine("¿Cerrar Vendedor de Boletos? y/n");
                    ans = Console.ReadLine().ToLower();
                    if (ans == "y")
                    {
                        break;
                    } else
                    {
                        continue;
                    }
                }
            }
            Console.WriteLine("\n-------------------\nVendedor de Boletos ~ Cerrado\n-------------------");

            Console.WriteLine("\n--------------------------------\nResumen de Vendedor de Boletos\n--------------------------------");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Zona #{0} : {1} Boletos ~ ${2}", i + 1, zonas[i]["ventas_total"], zonas[i]["ventas_quantidad"]);
            }
            Console.WriteLine("Ganancia Total: ${0}", ganancia_total);

                return new Dictionary<string, dynamic>();
        }
    }
}
