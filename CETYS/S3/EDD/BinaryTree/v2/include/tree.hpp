#ifndef __TREE__
#define __TREE__

#include <node.hpp>
#include <string>

template <typename T>
class Tree
{
private:
    Node<T> *min(Node<T> *node) const;
    Node<T> *max(Node<T> *node) const;

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
    if (root == nullptr)
        root = new Node<T>(val);
    else
    {
        for (Node<T> *current = root; current;)
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
}

template <typename T>
void Tree<T>::remove(const T val)
{
    Node<T> *node = search(val);
    if (node)
    {
        if (node->left == nullptr && node->right == nullptr)
        {
            if (root == node)
                root = nullptr;
            else if (root->parent->data > val)
                root->left = nullptr;
            else
                root->right = nullptr;
            delete node;
        }
        else if (node->left && node->right == nullptr)
        {
            if (root->parent->data > val)
                root->left = node->left;
            else
                root->right = node->left;
            delete node;
        }
        else if (node->left == nullptr && node->right)
        {
            if (root->parent->data > val)
                root->left = node->right;
            else
                root->right = node->right;
            delete node;
        }
        else
        {
            Node<T> *temp = predecessor(node);
            node->data = temp->data;
            remove(temp->data);
        }
    }
}

template <typename T>
Node<T> *Tree<T>::search(const T val) const
{
    for (Node<T> *current = root; current;)
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
    min(root);
}

template <typename T>
Node<T> *Tree<T>::min(Node<T> *node) const
{
    for (Node<T> *current = node; current;)
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
    max(root);
}

template <typename T>
Node<T> *Tree<T>::max(Node<T> *node) const
{
    for (Node<T> *current = node; current;)
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
        return max(node->left);
    else
        return node->parent;
}

template <typename T>
Node<T> *Tree<T>::successor(Node<T> *node) const
{
    if (node->right)
        return min(node->right);
    else
        return node->parent;
}

#endif
