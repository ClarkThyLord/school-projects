#ifndef __ordenamiento_hpp_
#define __ordenamiento_hpp_

#include <vector>
#include <algorithm>
#include <iostream>
// Falta incluir iterator

using namespace std;

template <typename T>
class ordenamiento
{
private:
    void my_sorting_heuristic() { std::cout << "My sorting method" << std::endl; }

public:
    /*
            Input:
                Address of a first element in the vector
                Address of a second element in the vector
        */
    static void ordena(typename std::vector<T>::iterator &&b, typename std::vector<T>::iterator &&e)
    {

        //ordenamiento<T>::getInstance();
        ordenamiento<T> *instance = new ordenamiento<T>();

        int start = 0;
        int end = -1;
        for (auto p = b; p != e; p++)
            end++;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            for (int index = start; index < end; ++index)
            {
                if (b[index] > b[index + 1])
                {
                    swap(b[index], b[index + 1]);
                    swapped = true;
                }
            }

            if (!swapped)
                break;

            --end;

            for (int index = end - 1; index >= start; --index)
            {
                if (b[index] > b[index + 1])
                {
                    swap(b[index], b[index + 1]);
                    swapped = true;
                }
            }

            ++start;
        }

        instance->my_sorting_heuristic();

        for (auto p = b; p != e; p++)
            std::cout << " -- " << *p;
        std::cout << std::endl;
    }

    /*
            Input:
                Address of a first element in the vector
                Address of a second element in the vector
                Pointer to a comparator function
        */
    static void ordena(typename std::vector<T>::iterator &&b, typename std::vector<T>::iterator &&e,
                       bool (*fp)(T &i, T &j))
    {
        int start = 0;
        int end = -1;
        for (auto p = b; p != e; p++)
            end++;
        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            for (int index = start; index < end; ++index)
            {
                if ((*fp)(b[index + 1], b[index]))
                {
                    swap(b[index], b[index + 1]);
                    swapped = true;
                }
            }

            if (!swapped)
                break;

            --end;

            for (int index = end - 1; index >= start; --index)
            {
                if ((*fp)(b[index + 1], b[index]))
                {
                    swap(b[index], b[index + 1]);
                    swapped = true;
                }
            }

            ++start;
        }

        T x{1}; // The following code illustrates how to use the function pointer
        T y{2};
        std::cout << "First call to callback function: " << (*fp)(x, y) << std::endl;
        std::cout << "Second call to callback function: " << fp(x, y) << std::endl;
    }
};

#endif
