#ifndef __TREE__
#define __TREE__

#include <node.hpp>
#include <string>

template <typename T>
class Tree
{
public:
    Node<T>* root;

    Tree(const T rootval);

    void insert(const T val);
    void remove(const T val);
    Node<T>* search(const T val) const;

    Node<T>* min() const;
    Node<T>* max() const;
    Node<T>* predecessor(Node<T>* node) const;
    Node<T>* successor(Node<T>* node) const;
};

template <typename T>
Tree<T>::Tree(const T rootval) : root{ new Node<T>{ rootval } } {}

template <typename T>
void Tree<T>::insert(const T val)
{
}

template <typename T>
void Tree<T>::remove(const T val)
{
}

template <typename T>
Node<T>* Tree<T>::search(const T val) const
{
    return root;
}

#endif
