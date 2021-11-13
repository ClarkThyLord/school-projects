// To Compile:
// gcc -fopenmp <file_path>

#include <stdlib.h>
#include <stdio.h>
#include "omp.h"

#define N 5
#define FS 38
#define NMAX 10

struct node
{
   int data;
   int fibdata;
   struct node *next;
};

int fib(int n)
{
   int x, y;
   if (n < 2)
   {
      return (n);
   }
   else
   {
      x = fib(n - 1);
      y = fib(n - 2);
      return (x + y);
   }
}

void processwork(struct node *p)
{
   int n;
   n = p->data;
   p->fibdata = fib(n);
}

struct node *init_list(struct node *p)
{
   int i;
   struct node *head = NULL;
   struct node *temp = NULL;

   head = malloc(sizeof(struct node));
   p = head;
   p->data = FS;
   p->fibdata = 0;
   for (i = 0; i < N; i++)
   {
      temp = malloc(sizeof(struct node));
      p->next = temp;
      p = temp;
      p->data = FS + i + 1;
      p->fibdata = i + 1;
   }
   p->next = NULL;
   return head;
}

int main(int argc, char *argv[])
{
   double start, end;
   struct node *p = NULL;
   struct node *temp = NULL;
   struct node *head = NULL;
   struct node *parr[NMAX];
   int i, count = 0;

   printf("Process linked list\n");
   printf("  Each linked list node will be processed by function 'processwork()'\n");
   printf("  Each linked list node will compute %d fibonacci numbers beginning with %d\n", N, FS);

   p = init_list(p);
   head = p;

   start = omp_get_wtime();
   {
      while (p != NULL)
      {
         processwork(p);
         p = p->next;
      }
   }

   end = omp_get_wtime();

   printf("Serial Compute Time: %f seconds\n", end - start);

   p = head;

   // omp_set_num_threads(1);
   // omp_set_num_threads(2);
   // omp_set_num_threads(3);
   // omp_set_num_threads(4);
   // omp_set_num_threads(5);
   // omp_set_num_threads(6);
   start = omp_get_wtime();
   {
      // Before we had our "while" do all of our logic, but now we just
      // count the number of items in the list with this while. And with
      // this we now know the number of elements, which allows us to use
      // the "for" to divide the work via openmp threads.
      while (p != NULL)
      {
         p = p->next;
         count++;
      }

      // Now knowing the number of elements we can go through the list
      // storing pointers into our array.
      p = head;
      for (i = 0; i < count; i++)
      {
         parr[i] = p;
         p = p->next;
      }

      // As mentioned before, we used a "while" to do all of our work
      // before because we didn't know how many elements we had, but
      // now counting the number of elements we can divide our main
      // work via openmp threads.
#pragma omp parallel
      {
#pragma omp single
         printf(" %d threads \n", omp_get_num_threads());
#pragma omp for schedule(static, 1)
         for (i = 0; i < count; i++)
            processwork(parr[i]);
      }
   }

   end = omp_get_wtime();
   p = head;
   while (p != NULL)
   {
      printf("%d : %d\n", p->data, p->fibdata);
      temp = p->next;
      free(p);
      p = temp;
   }
   free(p);

   printf("Compute Time: %f seconds\n", end - start);

   return 0;
}
