
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

typedef boost::mpl::list<int, long> test_types;

/* Recall, you must edit the hpp header file and implement 
the class methods labeled TODO. For argument I/O requirements
see comments in template hpp file */

BOOST_AUTO_TEST_CASE_TEMPLATE(empty_heap_test, T, test_types)
{
    bitset<3> state;
    Heap<T> min_heap{}; // TODO: implement constructor in hpp
    T sum{};

    state.reset();

    if (min_heap.size() == 0) // TODO: implement size in hpp
        state.set(0);

    for (int i = 1; i <= 10; i++)
        min_heap.push(i);      // TODO: implement push in hpp
    if (min_heap.size() == 10) // TODO: implement size in hpp
        state.set(1);

    min_heap.pop(); // TODO: implement pop in hpp
    if (min_heap.size() == 9)
        state.set(2);

    BOOST_CHECK_EQUAL(state.all(), true);
}

BOOST_AUTO_TEST_SUITE_END()
