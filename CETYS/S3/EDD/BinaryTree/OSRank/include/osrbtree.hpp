#ifndef _OSRBTree_
#define _OSRBTree_

#include <osrbnode.hpp>

template <typename T>
class OSRBTree
{
public:
    OSRBNode<T> *root;

    OSRBTree(T key);

    void left_rotate(OSRBNode<T> *node);
    void right_rotate(OSRBNode<T> *node);

    void insert(T data);
    void fixup(OSRBNode<T> *node);

    int OS_RANK(OSRBNode<T> node);
    OSRBNode<T> OS_SELECT(int index);
};

template <typename T>
OSRBTree<T>::OSRBTree(T data) : root{new OSRBNode<T>{data}}
{
    root->size = 1;
    root->color = Color::BLACK;
}

template <typename T>
void OSRBTree<T>::left_rotate(OSRBNode<T> *node)
{
    OSRBNode<T> *sub_node = node->right;
    node->right = sub_node->left;
    if (sub_node->left)
        sub_node->left->parent = node;
    sub_node->parent = node->parent;
    if (!node->parent)
        root = sub_node;
    else if (node == node->parent->left)
        node->parent->left = sub_node;
    else
        node->parent->right = sub_node;
    sub_node->left = node;
    node->parent = sub_node;
    sub_node->size = node->size;
    int sub_size = 0;
    if (node->left)
        sub_size += node->left->size;
    if (node->right)
        sub_size += node->right->size;
    node->size = sub_size + 1;
}

template <typename T>
void OSRBTree<T>::right_rotate(OSRBNode<T> *node)
{
    OSRBNode<T> *sub_node = node->left;
    node->left = sub_node->right;
    if (sub_node->right)
        sub_node->right->parent = node;
    sub_node->parent = node->parent;
    if (!node->parent)
        root = sub_node;
    else if (node == node->parent->right)
        node->parent->right = sub_node;
    else
        node->parent->left = sub_node;
    sub_node->right = node;
    node->parent = sub_node;
    sub_node->size = node->size;
    int sub_size = 0;
    if (node->left)
        sub_size += node->left->size;
    if (node->right)
        sub_size += node->right->size;
    node->size = sub_size + 1;
}

template <typename T>
void OSRBTree<T>::insert(T data)
{
    if (!root)
    {
        root = new OSRBNode<T>(data);
        root->color = Color::BLACK;
        root->size = 1;
    }
    else
    {
        OSRBNode<T> *temp = root;
        OSRBNode<T> *previous = nullptr;
        while (temp)
        {
            previous = temp;
            if (data < temp->data)
                temp = temp->left;
            else
                temp = temp->right;
            previous->size = previous->size + 1;
        }
        temp = new OSRBNode<T>(data);

        temp->parent = previous;

        if (data < previous->data)
            previous->left = temp;
        else
            previous->right = temp;
        temp->color = Color::RED;
        temp->size = 1;
        fixup(temp);
    }
}

template <typename T>
void OSRBTree<T>::fixup(OSRBNode<T> *node)
{
    while (node != root && node->parent->color == Color::RED)
    {
        if (node->parent == node->parent->parent->left)
        {
            OSRBNode<T> *sub_node = node->parent->parent->right;
            if (!sub_node)
            {
                if (node == node->parent->right)
                {
                    node = node->parent;
                    left_rotate(node);
                }
                node->parent->color = Color::BLACK;
                node->parent->parent->color = Color::RED;
                right_rotate(node->parent->parent);
            }
            else
            {
                if (sub_node->color == Color::RED)
                {
                    node->parent->color = Color::BLACK;
                    sub_node->color = Color::BLACK;
                    node->parent->parent->color = Color::RED;
                    node = node->parent->parent;
                }
            }
        }
        else
        {
            OSRBNode<T> *sub_node = node->parent->parent->left;
            if (!sub_node)
            {
                if (node == node->parent->left)
                {
                    node = node->parent;
                    right_rotate(node);
                }
                node->parent->color = Color::BLACK;
                node->parent->parent->color = Color::RED;
                left_rotate(node->parent->parent);
            }
            else
            {
                if (sub_node->color == Color::RED)
                {
                    node->parent->color = Color::BLACK;
                    sub_node->color = Color::BLACK;
                    node->parent->parent->color = Color::RED;
                    node = node->parent->parent;
                }
            }
        }
    }
    root->color = Color::BLACK;
}

template <typename T>
OSRBNode<T> OSRBTree<T>::OS_SELECT(int index)
{
    OSRBNode<T> node = root;
    if (index == node.left.size)
        return node;
    else if (index < node.left.size)
        return OS_SELECT(node.left, index);
    else
        return OS_SELECT(node.right, index - node.left.size);
}

template <typename T>
int OSRBTree<T>::OS_RANK(OSRBNode<T> node)
{
    int sub_node_size = node.left.size + 1;
    OSRBNode<T> sub_node = node;
    while (sub_node != this.root)
    {
        if (sub_node == sub_node.parent.right)
            sub_node_size += sub_node.parent.left.size + 1;
        sub_node = sub_node.parent;
    }
    return sub_node_size;
}

#endif
