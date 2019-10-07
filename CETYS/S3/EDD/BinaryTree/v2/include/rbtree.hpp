#ifndef __RBTREE__
#define __RBTREE__

#include <tree.hpp>
#include <rbnode.hpp>

template <typename T>
class RBTree : public Tree<T>
{
public:
    RBNode<T>* root;

    RBTree(const T key);
};

template <typename T>
RBTree<T>::RBTree(const T key) : Tree<T>{key} {}

#endif
