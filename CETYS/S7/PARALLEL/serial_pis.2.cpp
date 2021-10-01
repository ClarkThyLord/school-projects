// To Compile:
// g++ -fopenmp ./helloworlds.cpp

#include <omp.h>
#include <iostream>
#include <chrono>
#include <unistd.h>

using namespace std;

static long num_steps = 100'000;
#define THREADS 2

int main()
{
    auto start = chrono::steady_clock::now();

    int i = 0;
    int nthreads = 0;
    double pi = 0.0;
    double sums[THREADS] = {0.0};
    double step = 1.0 / num_steps;

    omp_set_num_threads(THREADS);
#pragma omp parallel
    {
        int i = 0;
        int id = 0;
        int nthrds = 0;
        double x = 0.0;

        id = omp_get_thread_num();
        nthrds = omp_get_num_threads();
        if (id == 0)
            nthreads = nthrds;
        for (int i = id; i < num_steps; i = i + nthrds)
        {
            x = (i + 0.5) * step;
            sums[id] += 4.0 / (1.0 + x * x);
        }
    }
    for (i = 0; i < nthreads; i++)
        pi += sums[i] * step;

    auto end = chrono::steady_clock::now();

    cout << "PI: "
         << pi << endl;
    cout << "Elapsed Time: "
         << chrono::duration_cast<chrono::seconds>(end - start).count()
         << " sec / "
         << chrono::duration_cast<chrono::milliseconds>(end - start).count()
         << " ms" << endl;

    return 0;
}