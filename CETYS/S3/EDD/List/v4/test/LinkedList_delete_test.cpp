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


BOOST_AUTO_TEST_CASE_TEMPLATE( delete_test, T, test_types )
{
    bitset<2> state;
    LinkedList<T> list{};

    state.reset();
    
    list.push(1);
    list.push(3);
    list.push(5);
    list.push(7);

    list.pop(3);
    if(list.size() == 3)
        state.set(0);
    list.pop(7);
    if(list.size() == 2)
        state.set(1);

    BOOST_CHECK_EQUAL(state.all(),true);

}


BOOST_AUTO_TEST_SUITE_END()
