#include <omp.h>
#include <iostream>
#include <chrono>
#include <unistd.h>

using namespace std;

#define THREADS 4
static long num_steps = 100'000'000;

int main()
{
    auto start = chrono::steady_clock::now();

    int at = 0;
    double pi = 0.0;
    double step = 1.0 / num_steps;

    float sums[THREADS] = {0.0};

    omp_set_num_threads(THREADS);
#pragma omp parallel
    {
        for (size_t i = omp_get_thread_num() * (num_steps / omp_get_num_threads()); i < (omp_get_thread_num() + 1) * (num_steps / omp_get_num_threads()); i++)
        {
            double x = (i + 0.5) * step;
            sums[omp_get_thread_num()] += 4.0 / (1.0 + x * x);
        }

        // int steps = num_steps / omp_get_num_threads();
        // at += steps;
        // for (size_t i = at - steps; i < at; i++)
        // {
        //     double x = (i + 0.5) * step;
        //     sums[omp_get_thread_num()] += 4.0 / (1.0 + x * x);
        // }
    }

    for (size_t i = 0; i < THREADS; i++)
    {
        pi += sums[i] * step;
    }

    auto end = chrono::steady_clock::now();

    cout << "PI: "
         << pi << endl;
    cout << "Elapsed Time: "
         << chrono::duration_cast<chrono::seconds>(end - start).count()
         << " sec / "
         << chrono::duration_cast<chrono::milliseconds>(end - start).count()
         << " ms" << endl;
}