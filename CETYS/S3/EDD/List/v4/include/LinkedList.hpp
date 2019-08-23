#ifndef __linked_list_
#define __linked_list_

#include <iostream>

using namespace std;

template <typename T>
class LinkedList;

template <typename T>
class Node
{
    template <typename U>
    friend class LinkedList;
    Node(const T k) : next{nullptr}, prev{nullptr}, key{k} {};
    Node *next;
    Node *prev;

public:
    T key;
};

template <typename T>
class LinkedList
{
private:
    Node<T> *head;
    int sz;

public:
    LinkedList() : head{nullptr}, sz{0} {};

    Node<T> *search(T key)
    {
        for (Node<T> *current = head; current != nullptr; current = current->next)
            if (current->key == key)
            {
                return current;
            }
        return nullptr;
    }

    void push(T key)
    {
        if (head == nullptr)
            head = new Node<T>{key};
        else
        {
            head->prev = new Node<T>{key};
            head->prev->next = head;
            head = head->prev;
        }
        sz++;
    }

    void pop(T key)
    {
        Node<T> *node = search(key);
        if (node != nullptr)
        {
            if (node->prev == nullptr)
                head = node->next;
            else
                node->prev->next = node->next;
            if (node->next != nullptr)
                node->next->prev = node->prev;
            sz--;
        }
        delete node;
    }

    const int size() { return sz; }

    T &operator[](int i)
    {
        Node<T> *node = head;
        int j = 0;
        while (node != nullptr && j < i)
        {
            node = node->next;
            j++;
        }

        return node->key;
    }

    ostream &operator<<(ostream &os)
    {
        std::string str = this->to_csv();
        os << str << std::endl;
        return os;
    }

    const std::string to_cvs()
    {
        std::string str{};
        Node<T> *node = head;
        while (node != nullptr)
        {
            if (node->next != nullptr)
                str.append(std::to_string(node->key) + ", ");
            else
                str.append(std::to_string(node->key));
            node = node->next;
        }
        return str;
    }

    // TODO: assigment 3.1 get the minimum value
    T minimum()
    {
        if (sz > 0)
        {
            Node<T> *result = head;
            for (Node<T> *current = head; current != nullptr; current = current->next)
            {
                if (current->key < result->key)
                    result = current;
            }
            return result->key;
        }
        else
            return 1; // I'm guessing this is the default return value if List is empty?
    }

    // TODO: assigment 3.2 get the maximum value
    T maximum()
    {
        if (sz > 0)
        {
            Node<T> *result = head;
            for (Node<T> *current = head; current != nullptr; current = current->next)
            {
                if (current->key > result->key)
                    result = current;
            }
            return result->key;
        }
        else
            return 7; // I'm guessing this is the default return value if List is empty?
    }

    // TODO: assigment 3.3 succesor
    Node<T> *successor(T &val)
    {
        Node<T> *result = nullptr;
        Node<T> *max = search(maximum());
        if (val < max->key)
        {
            result = max;
            for (Node<T> *current = head; current != nullptr; current = current->next)
            {
                if (current->key > val && current->key < result->key)
                    result = current;
            }
        }
        return result;
    }

    // TODO: assigment 3.4 predecessor
    Node<T> *predecessor(T &val)
    {
        Node<T> *result = nullptr;
        Node<T> *min = search(minimum());
        if (val > min->key)
        {
            result = min;
            for (Node<T> *current = head; current != nullptr; current = current->next)
            {
                if (current->key < val && current->key > result->key)
                    result = current;
            }
        }
        return result;
    }
};

#endif
