#ifndef __linked_list_
#define __linked_list_

#include <iostream>
#include <vector>

/* 
    
In this assigment, you must implement a minheap. For interface usage see test cases. For ease of implementation use std::vector for storing the data. See https://en.cppreference.com/w/cpp/container/vector for use of std::vector. Method argument requirements are defined in each method comments. For compilation purposes, placeholder code is added in order to pass all test cases. You must remove the place holder code. 

*/

using namespace std;


template<typename T>
class Heap{
    private:
        std::vector<T> data;        // the data container 
        int sz;                     // the logical size
        int buffer_size = 100;      // size of the allocation buffer
    
        // TODO: implement private method, i.e. min_heapify, a swaping function, binary tree insert 

    public:
        Heap(): sz{0}{
            data.resize(buffer_size,-1); // allocates a vector of size 100 with all values initiated to -1. 
        };           
    
    /*
        TODO: implement
        Binary tree insert. Structural and order properties must be maintained. 

        Input : reference to const value T
        Output: void
    */
    void push(const T& val) {
        // TODO: implement binary tree insert.
        data.push_back(val); // Example of vector insert
        sz++;
    }

    
    /* 
        TODO: implement 
        Deletes the top element of the heap

        Output: true if element deleted, false otherwise
    */
    bool pop( ) {
        // This is how you delete the first element of the vector
        data.erase(c.begin());
        // you must swap first and last element, for example
        T temp = data[c.begin()+sz]; // First element is in position 0
        data[c.begin()+sz] = data[0];
        data[0] = temp;
        // now restore the order property.
    }
    
    
    /*
        Returns the logical size of the heap

        Out: the size of the heap
    */
    int size(){ return sz; }

    
    /*
        TODO: implement

        Specifies if the heap is empty or not
        Out: true if sz == 0, otherwise false
    */
    bool empty() {
        // TODO: implement logic
        return false;
    }

};

#endif
