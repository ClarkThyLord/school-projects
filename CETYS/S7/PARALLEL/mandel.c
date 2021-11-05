// To Compile:
// g++ -fopenmp <file_path>

#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <omp.h>

#define NPOINTS 1000
#define MAXITER 1000

void testpoint(struct d_complex);

struct d_complex
{
  double r;
  double i;
};

struct d_complex c;
int numoutside = 0;

double area = 0.0;
double error = 0.0;

int main()
{
  int i, j;
  double eps = 1.0e-5;

  //   Loop over grid of points in the complex plane which contains the Mandelbrot set,
  //   testing each point to see whether it is inside or outside the set.
  // Summary:
  // Before we used "pragma omp parallel for default(shared) private(c,eps)" to split our work into multiple threads.
  // However, the way the program was setup made it so that our testpoint function shared the c d_complex variable.
  // This meant that while our work was being done in parallel the info that was being "shared" was really being
  // accessed and overwritten by multiple threads at once. So instead of having it that way we made it so that c d_coplex
  // is shared appropriately, avoiding "oversteps" between threads.
#pragma omp parallel for default(none) private(c, j) firstprivate(eps)
  for (i = 0; i < NPOINTS; i++)
  {
    for (j = 0; j < NPOINTS; j++)
    {
      c.r = -2.0 + 2.5 * (double)(i) / (double)(NPOINTS) + eps;
      c.i = 1.125 * (double)(j) / (double)(NPOINTS) + eps;
      testpoint(c);
    }
  }

  // Calculate area of set and error estimate and output the results

  area = 2.0 * 2.5 * 1.125 * (double)(NPOINTS * NPOINTS - numoutside) / (double)(NPOINTS * NPOINTS);
  error = area / (double)NPOINTS;

  printf("Area of Mandlebrot set = %12.8f +/- %12.8f\n", area, error);
  printf("Correct answer should be around 1.510659\n");
}

void testpoint(struct d_complex c)
{

  // Does the iteration z=z*z+c, until |z| > 2 when point is known to be outside set
  // If loop count reaches MAXITER, point is considered to be inside the set

  struct d_complex z;
  int iter;
  double temp;

  z = c;
  for (iter = 0; iter < MAXITER; iter++)
  {
    temp = (z.r * z.r) - (z.i * z.i) + c.r;
    z.i = z.r * z.i * 2 + c.i;
    z.r = temp;
    if ((z.r * z.r + z.i * z.i) > 4.0)
    {
      numoutside++;
      break;
    }
  }
}
