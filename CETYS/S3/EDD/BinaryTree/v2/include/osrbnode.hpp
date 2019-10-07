#ifndef __ORBNODE__
#define __ORBNODE__

#include <rbnode.hpp>

template <typename T>
class OSRBNode : public RBNode<T>
{
public:
    OSRBNode* parent;
    OSRBNode* left;
    OSRBNode* right;

    int size;

    OSRBNode(const T val);
};

template <typename T>
OSRBNode<T>::OSRBNode(const T val) : RBNode<T>{ val }, size{ 1 } {}

#endif
