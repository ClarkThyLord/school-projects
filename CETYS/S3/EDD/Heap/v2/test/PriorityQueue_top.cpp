#define BOOST_TEST_MODULE "Priority queue test"
#define BOOST_TEST_MAIN

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>
#include <string>
#include <bitset>
#include "Heap.hpp"

using namespace std;


BOOST_AUTO_TEST_SUITE(LLTEST)


typedef boost::mpl::list<int,long> test_types;

/* Recall, you must edit the hpp header file and implement 
the class methods labeled TODO. For argument I/O requirements
see comments in template hpp file */

BOOST_AUTO_TEST_CASE_TEMPLATE( empty_heap_test, T, test_types )
{
    bitset<2> state;
    Heap<T> min_heap{}; // TODO: implement constructor in hpp
    T sum{};

    state.reset();

    if(min_heap.top() == -1) //TODO: implement top in hpp
        state.set(0);
    
    min_heap.push(2); // TODO: implement push in hpp
    min_heap.push(3);
    min_heap.push(0);

    if(min_heap.top() == 2) // TODO: mean_heap.top() == 0
        state.set(1);

    BOOST_CHECK_EQUAL(state.all(),true);  
}

BOOST_AUTO_TEST_SUITE_END()
