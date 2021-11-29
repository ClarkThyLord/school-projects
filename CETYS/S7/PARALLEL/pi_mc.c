// To Compile:
// gcc -fopenmp <file_path>

#include <stdio.h>
#include <omp.h>
#include "random.h"

//
// The monte carlo pi program
//

static long num_trials = 10000;

int main()
{
    for (int THREADS = 1; THREADS < 7; THREADS++)
    {
        omp_set_num_threads(THREADS);
        double runtime = omp_get_wtime();

        long i;
        long Ncirc = 0;
        double pi, x, y, test;
        double r = 1.0; // radius of circle. Side of squrare is 2*r

        // Este problema fue interesante y revelador, ya que tuvimos que usar una interfaz en nuestro trabajo,
        // algo que nunca habíamos hecho. Es interesante ver cómo podemos hacer uso de openmp para dividir el
        // trabajo no solo para nuestro trabajo definido, sino también para otras bibliotecas. Por supuesto,
        // aún necesitaría diseñar y asegurarse de que todo funcione sin problemas.
#pragma omp parallel
        {
            seed(-r, r); // The circle and square are centered at the origin
                         // Al compartir qué y el tipo de datos que manejará, puede evitar la sobreescritura por
                         // múltiples subprocesos
#pragma omp for reduction(+ \
                          : Ncirc) private(x, y, test)
            for (i = 0; i < num_trials; i++)
            {
                for (i = 0; i < num_trials; i++)
                {
                    x = drandom();
                    y = drandom();

                    test = x * x + y * y;

                    if (test <= r * r)
                        Ncirc++;
                }

                pi = 4.0 * ((double)Ncirc / (double)num_trials);

                runtime = omp_get_wtime() - runtime;

                // Al igual que los problemas anteriores que estaban paralizados, notamos una
                // disminución significativa en el tiempo de ejecución después de dos subprocesos,
                // alrededor de 5-6 subprocesos comenzamos a ver una meseta.
                printf("In %f seconds with %d / %d threads, with %ld trials and pi is %lf \n",
                       runtime, omp_get_num_threads(), THREADS, num_trials, pi);
            }
            return 0;
        }