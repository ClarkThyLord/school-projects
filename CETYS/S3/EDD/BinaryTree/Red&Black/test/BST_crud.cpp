// https://www.cs.usfca.edu/~galles/visualization/BST.html

#define BOOST_TEST_MODULE BinarySearchTreeTest

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>

#include <tree.hpp>

BOOST_AUTO_TEST_CASE(bst_crud)
{
    Tree<int> t{ 13 };
    int values[]{ 5, 4, 6, 15, 14, 16, 11 };

    for(int i = 0; i < 7; i++)
        t.insert(values[i]);

    BOOST_REQUIRE(t.root->left->data == 5);
    BOOST_REQUIRE(t.root->right->left->data == 14);

    auto res1 = t.search(4);
    auto res2 = t.search(11);
    auto res3 = t.search(3);

    BOOST_REQUIRE(res1->data == 4);
    BOOST_REQUIRE(res1->data == 11);
    BOOST_REQUIRE(res3 == nullptr);
    
    t.remove(14);
    t.remove(11);
    t.remove(5);
    t.remove(13);

    BOOST_REQUIRE(t.root->data == 6);
    BOOST_REQUIRE(t.root->right->data == 15);
    BOOST_REQUIRE(t.root->left->data == 4);
}
