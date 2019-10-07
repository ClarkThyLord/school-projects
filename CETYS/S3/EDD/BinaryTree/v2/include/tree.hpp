#ifndef __TREE__
#define __TREE__

#include <node.hpp>
#include <string>

template <typename T>
class Tree
{
public:
    Node<T> *root;

    Tree(const T rootval);

    void insert(const T val);
    void remove(const T val);
    Node<T> *search(const T val) const;

    Node<T> *min() const;
    Node<T> *max() const;
    Node<T> *predecessor(Node<T> *node) const;
    Node<T> *successor(Node<T> *node) const;
};

template <typename T>
Tree<T>::Tree(const T rootval) : root{new Node<T>{rootval}} {}

template <typename T>
void Tree<T>::insert(const T val)
{
    for (Node<T> *current = head; current;)
    {
        if (current->data == val)
            return;
        else if (current->data > val)
            if (current->left)
                current = current->left;
            else
                current->left = new Node<T>(val, current);
        else if (current->data < val)
            if (current->right)
                current = current->right;
            else
                current->right = new Node<T>(val, current);
    }
}

template <typename T>
void Tree<T>::remove(const T val)
{
    Node<T> *node = this->search(val);
    if (node->parent)
    {
        if (node->left == nullptr && node->right == nullptr)
        {
            if (node->parent->left && node->parent->left->data == val)
                node->parent->left = nullptr;
            else
                node->parent->right = nullptr;
            delete node;
        }
        else if (node->left && node->right == nullptr)
        {
            if (node->parent->left && node->parent->left->data == val)
                node->parent->left = node->left;
            else
                node->parent->right = node->left;
            delete node;
        }
        else if (node->left == nullptr && node->right)
        {
            if (node->parent->left && node->parent->left->data == val)
                node->parent->left = node->right;
            else
                node->parent->right = node->right;
            delete node;
        }
        else
        {
            Node<T> *y = nullptr;
            for (Node<T> *current = node->right; current;)
            {
                if (current->left)
                    current = current->left;
                else
                {
                    y = current;
                    break;
                }
            }
            remove(y->data);
            node->data = y->data;
        }
    }
    else
    {
        delete head;
        head = nullptr;
    }
}

template <typename T>
Node<T> *Tree<T>::search(const T val) const
{
    for (Node<T> *current = head; current;)
    {
        if (current->data == val)
            return current;
        else if (current->data > val)
            current = current->left;
        else if (current->data < val)
            current = current->right;
    }
    return nullptr;
}

template <typename T>
Node<T> *Tree<T>::min() const
{
    for (Node<T> *current = head; current;)
    {
        if (current->left)
            current = current->left;
        else
            return current;
    }
}

template <typename T>
Node<T> *Tree<T>::max() const
{
    for (Node<T> *current = head; current;)
    {
        if (current->right)
            current = current->right;
        else
            return current;
    }
}

template <typename T>
Node<T> *Tree<T>::predecessor(Node<T> *node) const
{
    if (node->left)
    {
        for (Node<T> *current = node->left; current;)
        {
            if (current->right)
                current = current->right;
            else
                return current;
        }
    }
    else
        return node->parent;
}

template <typename T>
Node<T> *Tree<T>::successor(Node<T> *node) const
{
    if (node->right)
    {
        for (Node<T> *current = node->right; current;)
        {
            if (current->left)
                current = current->left;
            else
                return current;
        }
    }
    else
        return node->parent;
}

#endif
