// To Compile:
// g++ -fopenmp <file_path>

#include <omp.h>
#include <chrono>
#include <iostream>
#include <unistd.h>

using namespace std;

static long num_steps = 1'000'000'000;
double step;

int main()
{
    auto start = chrono::steady_clock::now();

    int i;
    double x, pi, sum = 0.0;

    step = 1.0 / num_steps;
#pragma omp parallel for reduction(+ \
                                   : pi)
    for (size_t i = 0; i < num_steps; i++)
    {
        if (i == 0)
            cout << "THREADS: "
                 << omp_get_num_threads() << endl;

        x = (i + 0.5) * step;
        pi += step * (4.0 / (1.0 + x * x));
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