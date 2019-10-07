#ifndef __RB_TREE__
#define __RB_TREE__

#include <node.hpp>
#include <color.h>

template <typename T>
class RBNode : public Node<T>
{
public:
    RBNode* parent;
    RBNode* left;
    RBNode* right;

    Color color;

    RBNode(const T key);
};

template <typename T>
RBNode<T>::RBNode(const T key) : Node<T>(key), color{ Color::RED } {}

#endif
