#define BOOST_TEST_MODULE BinarySearchTreeTest

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>

#include <osrbtree.hpp>

BOOST_AUTO_TEST_CASE(osrbt_crud)
{
    OSRBTree<int> osrbt{ 13 };
    int values[]{ 5, 4, 6, 15, 14, 16, 11 };

    for(int i = 0; i < 7; i++)
        osrbt.insert(values[i]); 

    BOOST_REQUIRE(osrbt.root->size == 8);
    BOOST_REQUIRE(osrbt.root->right->size == 6);
    BOOST_REQUIRE(osrbt.root->right->left->size == 2);
}

