#ifndef _RBTREE_
#define _RBTREE_

#include <rbnode.hpp>

template <typename T>
class RBTree
{
protected:
    void rotate_left(RBNode<T> *node);
    void rotate_right(RBNode<T> *node);

    void fixup(RBNode<T> *node);

public:
    RBNode<T> *root;
    RBTree();
    RBTree(T key);
    void insert(T data);
};

template <typename T>
RBTree<T>::RBTree() {}

template <typename T>
RBTree<T>::RBTree(T data) : root{new RBNode<T>{data, Color::BLACK}} {}

template <typename T>
void RBTree<T>::rotate_left(RBNode<T> *node)
{
    RBNode<T> *node_child = node->right;
    node->right = node_child->left;
    if (node_child->left != nullptr)
        node_child->left->parent = node;
    node_child->parent = node->parent;
    if (node->parent == nullptr)
        root = node_child;
    else if (node == node->parent->left)
        node->parent->left = node_child;
    else
        node->parent->right = node_child;
    node_child->left = node;
    node->parent = node_child;
}

template <typename T>
void RBTree<T>::rotate_right(RBNode<T> *node)
{
    RBNode<T> *node_child = node->left;
    node->left = node_child->right;
    if (node_child->right != nullptr)
        node_child->right->parent = node;
    node_child->parent = node->parent;
    if (node->parent == nullptr)
        root = node_child;
    else if (node == node->parent->right)
        node->parent->right = node_child;
    else
        node->parent->left = node_child;
    node_child->right = node;
    node->parent = node_child;
}

template <typename T>
void RBTree<T>::fixup(RBNode<T> *node)
{
    while (node != root && node->parent->color == Color::RED)
    {
        if (node->parent == node->parent->parent->left)
        {
            RBNode<T> *node_child = node->parent->parent->right;
            if (node_child == nullptr)
            {
                if (node == node->parent->right)
                {
                    node = node->parent;
                    rotate_left(node);
                }
                node->parent->color = Color::BLACK;
                node->parent->parent->color = Color::RED;
                rotate_right(node->parent->parent);
            }
            else
            {
                if (node_child->color == Color::RED)
                {
                    node->parent->color = Color::BLACK;
                    node_child->color = Color::BLACK;
                    node->parent->parent->color = Color::RED;
                    node = node->parent->parent;
                }
            }
        }
        else
        {
            RBNode<T> *node_child = node->parent->parent->left;
            if (node_child == nullptr)
            {
                if (node == node->parent->left)
                {
                    node = node->parent;
                    rotate_right(node);
                }
                node->parent->color = Color::BLACK;
                node->parent->parent->color = Color::RED;
                rotate_left(node->parent->parent);
            }
            else
            {
                if (node_child->color == Color::RED)
                {
                    node->parent->color = Color::BLACK;
                    node_child->color = Color::BLACK;
                    node->parent->parent->color = Color::RED;
                    node = node->parent->parent;
                }
            }
        }
    }
    root->color = Color::BLACK;
}

template <typename T>
void RBTree<T>::insert(T data)
{
    if (root)
    {
        RBNode<T> *temp = root;
        RBNode<T> *previous = nullptr;
        while (temp != nullptr)
        {
            previous = temp;
            if (data < temp->data)
                temp = temp->left;
            else
                temp = temp->right;
        }
        temp = new RBNode<T>(data);

        temp->parent = previous;

        if (data < previous->data)
            previous->left = temp;
        else
            previous->right = temp;
        temp->color = Color::RED;
        fixup(temp);
    }
    else
    {
        RBNode<T> *nuevo = new RBNode<T>(data);
        root = nuevo;
        root->color = Color::BLACK;
    }
}

#endif