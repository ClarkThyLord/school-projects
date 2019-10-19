#ifndef _RB_TREE_
#define _RB_TREE_

#include <color.h>

template <typename T>
class OSRBNode
{
public:
    T data;
    OSRBNode *parent;
    OSRBNode *left;
    OSRBNode *right;
    Color color;
    OSRBNode(const T data);
    int size;
};

template <typename T>
OSRBNode<T>::OSRBNode(T data)
{
    this->data = data;
    this->parent = nullptr;
    this->left = nullptr;
    this->right = nullptr;
    this->color = Color::RED;
}

#endif
