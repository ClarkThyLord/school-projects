#ifndef __NODE__
#define __NODE__

#include <color.h>

template <typename T>
class Node
{
private:
    void insert(Node *n);

public:
    T data;
    Node *parent;
    Node *left;
    Node *right;

    Node(const T data);

    void insert(const T val);
};

template <typename T>
Node<T>::Node(const T data) : data{data}, parent{nullptr}, left{nullptr}, right{nullptr} {}

template <typename T>
void Node<T>::insert(Node<T> *n)
{
    if (n->data >= data)
    {
        if (right == nullptr)
            right = n;
        else
            right->insert(n);
    }
    else
    {
        if (left == nullptr)
            left = n;
        else
            left->insert(n);
    }
}

template <typename T>
void Node<T>::insert(const T val)
{
    insert(new Node{val});
}

#endif
