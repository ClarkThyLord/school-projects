#ifndef __OSRBTREE__
#define __OSRBTREE__

#include <rbtree.hpp>
#include <osrbnode.hpp>

template <typename T>
class OSRBTree : public RBTree<T>
{
public:
    OSRBNode<T>* root;

    OSRBTree(const T key);
};

template <typename T>
OSRBTree<T>::OSRBTree(const T key) : RBTree<T>{ key } {}

#endif
