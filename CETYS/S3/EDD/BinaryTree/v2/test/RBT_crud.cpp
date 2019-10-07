// https://www.cs.usfca.edu/~galles/visualization/RedBlack.html

#define BOOST_TEST_MODULE RedBlackTreeTest 

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>

#include <rbtree.hpp>

BOOST_AUTO_TEST_CASE(rbt_crud)
{
    RBTree<int> rbt{ 13 };
    int values[]{ 5, 4, 6, 15, 14, 16, 11 };

    for(int i = 0; i < 7; i++)
        rbt.insert(values[i]);

    BOOST_REQUIRE(rbt.root->data == 5);
    BOOST_REQUIRE(rbt.root->left->data == 4);
    BOOST_REQUIRE(rbt.root->right->left->data == 6);

    rbt.remove(5);
    rbt.remove(6);
    rbt.remove(4);

    BOOST_REQUIRE(rbt.root->data == 13);
    BOOST_REQUIRE(rbt.root->left->data == 11);
    BOOST_REQUIRE(rbt.root->right->left->data == 14);
}
