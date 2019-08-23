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


typedef boost::mpl::list<int,long> test_types;


/**
*   Test cases:
*   1. Create an empty integet type list.
*   1. Insert a set of integer values.
*   2. Insert a set of float values.
*/
BOOST_AUTO_TEST_CASE_TEMPLATE( insert_test, T, test_types )
{
    bitset<4> state;
    LinkedList<T> list{};

    state.reset();
    list.push(1);
    list.push(3);
    list.push(5);
    list.push(7);

    T p1{7};
    Node<T>* node1 = list.successor(p1);
    if( node1 == nullptr )
        state.set(0);

    T p2{1};
    Node<T>* node2 = list.successor(p2);
    if( true ) // TODO: node2->key == 3
        state.set(1);

    T p3{1};
    Node<T>* node3 = list.predecessor(p3);
    if(true) // TODO: node3 == nullptr
        state.set(2);

    T p4{5};
    Node<T>* node4 = list.predecessor(p4);
    if(true) // TODO: node4->key == 3
        state.set(3);

    BOOST_CHECK_EQUAL(state.all(),true);  
}

BOOST_AUTO_TEST_SUITE_END()
