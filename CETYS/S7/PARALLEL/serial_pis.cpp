#include <omp.h>
#include <stdio.h>

static int threads = 4;
static long num_steps = 100'000'000;

double step;

int main()
{
    double pi = 0.0;
    step = 1.0 / num_steps;
    int at = 0;
#pragma omp parallel num_threads(4)
    {
        double sum = 0.0;
        int range = num_steps / threads;
        for (size_t i = at; i < at + range; i++)
        {
            double x = (i + 0.5) * step;
            sum += 4.0 / (1.0 + x * x);
        }
        pi += step * sum;
        at += range;
    }
    printf("PI: %f\n", pi);
}