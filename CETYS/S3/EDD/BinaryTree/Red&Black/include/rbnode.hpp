#ifndef _RB_TREE_
#define _RB_TREE_

#include <color.h>

template <typename T>
class RBNode
{
public:
    T data;
    RBNode *parent;
    RBNode *left;
    RBNode *right;
    Color color;
    RBNode(const T data, const Color color = Color::RED);
};

template <typename T>
RBNode<T>::RBNode(T data, Color color = Color::RED)
{
    this->data = data;
    this->color = color;
    this->left = nullptr;
    this->right = nullptr;
    this->parent = nullptr;
}

#endif
