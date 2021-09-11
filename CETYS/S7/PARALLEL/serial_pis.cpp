#include <omp.h>
#include <stdio.h>

static int threads = 4;
static long num_steps = 100'000'000;

double step;

int main()
{
    double pi = 0.0;
    step = 1.0 / num_steps;
#pragma omp parallel num_threads(4)
    {
        double sum = 0.0;
        for (size_t i = 0; i < num_steps / threads; i++)
        {
            double x = (i + 0.5) * step;
            sum += 4.0 / (1.0 + x * x);
        }
        pi += step * sum;
    }
    printf("PI: %f\n", pi);
}