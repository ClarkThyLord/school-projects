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
        for (Node<T> *current = root; current != nullptr;)
        {
            if (current->data == val)
                return;
            else if (current->data > val)
            {
                if (current->left == nullptr)
                {
                    current->left = new Node<T>(val, current);
                    return;
                }
                else
                    current = current->left;
            }
            else
            {
                if (current->right == nullptr)
                {
                    current->right = new Node<T>(val, current);
                    return;
                }
                else
                    current = current->right;
            }
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
            else if (node->parent->data > val)
                node->parent->left = nullptr;
            else
                node->parent->right = nullptr;
            delete node;
        }
        else if (node->left && node->right == nullptr)
        {
            if (node->parent->data > val)
                node->parent->left = node->left;
            else
                node->parent->right = node->left;
            delete node;
        }
        else if (node->left == nullptr && node->right)
        {
            if (node->parent->data > val)
                node->parent->left = node->right;
            else
                node->parent->right = node->right;
            delete node;
        }
        else
        {
            Node<T> *temp = predecessor(node);
            T temp_val = temp->data;
            remove(temp->data);
            node->data = temp_val;
        }
    }
}

template <typename T>
Node<T> *Tree<T>::search(const T val) const
{
    if (root)
    {
        for (Node<T> *current = root; current != nullptr)
        {
            if (current == nullptr)
                return nullptr;
            else if (current->data == val)
                return current;
            else if (current->data > val)
                current = current->left;
            else
                current = current->right;
        }
    }
    return nullptr;
}

template <typename T>
Node<T> *Tree<T>::min() const
{
    return min(root);
}

template <typename T>
Node<T> *Tree<T>::min(Node<T> *node) const
{
    if (node)
    {
        for (Node<T> *current = node; current;)
        {
            if (current->left)
                current = current->left;
            else
                return current;
        }
    }
    return nullptr;
}

template <typename T>
Node<T> *Tree<T>::max() const
{
    return max(root);
}

template <typename T>
Node<T> *Tree<T>::max(Node<T> *node) const
{
    if (node)
    {
        for (Node<T> *current = node; current;)
        {
            if (current->right)
                current = current->right;
            else
                return current;
        }
    }
    return nullptr;
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
