#define BOOST_TEST_MODULE "LLTEST"
#define BOOST_TEST_MAIN

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>
#include <string>
#include <bitset>
#include "LinkedList.hpp"

using namespace std;

BOOST_AUTO_TEST_SUITE(LLTEST)

typedef boost::mpl::list<int, long> test_types;

/**
*   Test cases:
*   1. Create an empty integet type list.
*   1. Insert a set of integer values.
*   2. Insert a set of float values.
*/
BOOST_AUTO_TEST_CASE_TEMPLATE(insert_test, T, test_types)
{
    bitset<3> state;
    LinkedList<T> list{};

    state.reset();
    list.push(1);
    list.push(3);
    list.push(5);
    list.push(7);

    T val1 = list.minimum();
    std::cout << val1 << std::endl;
    if (val1 == 1)
        state.set(0);

    list.pop(1);
    T val2 = list.minimum();
    std::cout << val2 << std::endl;
    if (val2 == 3) // TODO: Cambien el valor a 3
        state.set(1);

    T val3 = list.maximum();
    std::cout << val3 << std::endl;
    if (val3 == 7)
        state.set(2);

    BOOST_CHECK_EQUAL(state.all(), true);
}

BOOST_AUTO_TEST_SUITE_END()
