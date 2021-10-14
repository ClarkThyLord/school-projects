// To Compile:
// g++ <file_path>

#include <omp.h>
#include <stdio.h>

int main()
{
#pragma omp parallel
    {
        int ID = 0;
        printf("hello(%d)", ID);
        printf("world(%d)", ID);
    }
    printf("\n");
}