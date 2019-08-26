#ifndef __min_heap__
#define __min_heap__

#include <iostream>
#include <vector>

/* 
    
In this assigment, you must implement a minheap. For interface usage see test cases. For ease of implementation use std::vector for storing the data. See https://en.cppreference.com/w/cpp/container/vector for use of std::vector. Method argument requirements are defined in each method comments. For compilation purposes, placeholder code is added in order to pass all test cases. You must remove the place holder code. 

*/

using namespace std;

template <typename T>
class Heap
{
private:
    std::vector<T> data;   // the data container
    int sz = 0;            // the logical size
    int buffer_size = 100; // size of the allocation buffer
    int num_realloc = 1;   // Number of time the buffer has been reallocated

    void swap(int i, int j)
    {
        T temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    void min_heapify(int index)
    {
        int smallest = index;
        if (child_li(index) > 0 && child_li(index) < sz && data[index] > data[child_li(index)])
            smallest = child_li(index);
        if (child_ri(index) > 0 && child_ri(index) < sz && data[child_ri(index)] < data[smallest])
            smallest = child_ri(index);

        if (smallest != index)
        {
            swap(index, smallest);
            min_heapify(smallest);
        }
    }

public:
    Heap() : sz{0}
    {
        data.resize(buffer_size, -1); // allocates a vector of size 100 with all values initiated to -1.
    };

    int parent_i(int index) { return (index + 1) / 2; }

    int child_li(int index) { return (2 * index) + 1; }

    int child_ri(int index) { return (2 * index) + 2; }

    /*
        Binary tree insert. Structural and order properties must be maintained. 

        Input : reference to const value T
        Output: void
    */
    void push(const T &val)
    {
        data[sz] = val;
        sz++;

        for (int index = sz - 1; sz > 0; index = parent_i(index))
        {
            if (data[index] < data[parent_i(index)])
                swap(index, parent_i(index));
            else
                break;
        }
    }

    /* 
        Deletes the top element of the heap

        Output: true if element deleted, false otherwise
    */
    bool pop()
    {
        if (sz >= 1)
        {
            data.erase(data.begin());
            sz--;

            swap(0, sz);
            min_heapify(0);

            return true;
        }
        else
            return false;
    }

    /*
        Returns the logical size of the heap

        Out: the size of the heap
    */
    int size() { return sz; }

    bool empty() { return (sz == 0) ? true : false; }

    T top() { return data[0]; }

    void imprime()
    {
        for (int i = 0; i < sz; i++)
            std::cout << data[i] << std::endl;
    }
};

#endif
