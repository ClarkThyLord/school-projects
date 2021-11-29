// To Compile:
// gcc -fopenmp <file_path>

#include <omp.h>
#ifdef APPLE
#include <stdlib.h>
#else
#include <malloc.h>
#endif
#include <stdio.h>

#define N 10000

/* Some random number constants from numerical recipies */
#define SEED 2531
#define RAND_MULT 1366
#define RAND_ADD 150889
#define RAND_MOD 714025
int randy = SEED;

/* function to fill an array with random numbers */
void fill_rand(int length, double *a)
{
    int i;
    for (i = 0; i < length; i++)
    {
        randy = (RAND_MULT * randy + RAND_ADD) % RAND_MOD;
        *(a + i) = ((double)randy) / ((double)RAND_MOD);
    }
}

/* function to sum the elements of an array */
double Sum_array(int length, double *a)
{
    int i;
    double sum = 0.0;
    for (i = 0; i < length; i++)
        sum += *(a + i);
    return sum;
}

int main()
{
    for (int THREADS = 1; THREADS < 7; THREADS++)
    {
        omp_set_num_threads(THREADS);

        double *A, sum, runtime;
        int flag = 0;

        A = (double *)malloc(N * sizeof(double));

        runtime = omp_get_wtime();

        // Esta sincronización por pares entre hilos es muy diferente de lo que
        // hemos visto hasta ahora, ya que antes siempre vinculamos nuestro trabajo
        // compartido mediante estructuras iterativas, como whiles y fors. Sin
        // embargo, esta forma de seccionar es una construcción de trabajo
        // compartido no iterativo que contiene un conjunto de bloques estructurados
        // que deben ser distribuidos y ejecutados por los subprocesos de un equipo.
        // Cada bloque estructurado es ejecutado una vez por uno de los subprocesos
        // del equipo en el contexto de su tarea implícita.
#pragma omp parallel
        {
#pragma omp sections
            {
#pragma omp section
                {
                    fill_rand(N, A); // Producer: fill an array of data
#pragma omp flush
                    flag = 1;
#pragma omp flush(flag)
                }
#pragma omp section
                {
#pragma omp flush(flag)
                    while (flag != 1)
                    {
#pragma omp flush(flag)
                    }

#pragma omp flush
                    sum = Sum_array(N, A); // Consumer: sum the array
                }
            }
        }

        runtime = omp_get_wtime() - runtime;

        // Al igual que los problemas anteriores que estaban paralizados, notamos una
        // disminución significativa en el tiempo de ejecución después de dos subprocesos,
        // alrededor de 5-6 subprocesos comenzamos a ver una meseta.
        printf("In %f seconds with %d / %d threads, the sum is %f \n",
               runtime, omp_get_num_threads(), THREADS, sum);
    }
}
