// To Compile:
// g++ -fopenmp <file_path>

#include <omp.h>
#include <iostream>
#include <chrono>
#include <unistd.h>

using namespace std;

#define PAD 8

static long num_steps = 1'000'000'000;

int main()
{
    for (int THREADS = 1; THREADS < 7; THREADS++)
    {
        auto start = chrono::steady_clock::now();

        int i = 0;
        int nthreads = 0;
        double pi = 0.0;

        double sums[THREADS][PAD] = {0.0};
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
                sums[id][0] += 4.0 / (1.0 + x * x);
            }
        }

        for (i = 0; i < nthreads; i++)
            pi += sums[i][0] * step;

        auto end = chrono::steady_clock::now();

        cout << "THREADS: "
             << nthreads << " / " << THREADS << endl;
        cout << "PI: "
             << pi << endl;
        cout << "Elapsed Time: "
             << chrono::duration_cast<chrono::seconds>(end - start).count()
             << " sec / "
             << chrono::duration_cast<chrono::milliseconds>(end - start).count()
             << " ms" << endl;
    }

    return 0;
}