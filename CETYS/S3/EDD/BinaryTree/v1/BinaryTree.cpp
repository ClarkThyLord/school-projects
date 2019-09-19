#include <iostream>

template <typename T>
struct Node
{
    T payload;
    Node<T> *left = nullptr;
    Node<T> *right = nullptr;

    Node(T payload)
    {
        this->payload = payload;
    }
};

template <typename T>
class BinaryTree
{
private:
    int size;
    Node<T> *head = nullptr;

    void balance()
    {
        Node<T> current = head;
        while (current)
        {
        }
    }

public:
    bool contains(T payload)
    {
        if (head == nullptr)
            return false;
        else
        {
            Node<T> *current = head;
            while (true)
            {
                if (current->payload == payload)
                    return true;
                else if (payload < current->payload)
                {
                    if (current->left == nullptr)
                        return false;
                    else
                        current = current->left;
                }
                else
                {
                    if (current->right == nullptr)
                        return false;
                    else
                        current = current->right;
                }
            }
        }
    }

    T *successor(T payload)
    {
    }

    T *predecessor(T payload)
    {
    }

    T *minimum()
    {
        for (Node<T> *current = head; current; current = current->left)
        {
            if (current->left == nullptr)
                return &(current->payload);
        }
        return nullptr;
    }

    T *maximum()
    {
        for (Node<T> *current = head; current; current = current->right)
        {
            if (current->right == nullptr)
                return &(current->payload);
        }
        return nullptr;
    }

    void insert(T payload)
    {
        if (head == nullptr)
            head = new Node<T>(payload);
        else
        {
            Node<T> *current = head;
            while (true)
            {
                if (payload < current->payload)
                {
                    if (current->left == nullptr)
                        current->left = &new Node<T>(payload);
                    break;
                    else current = current->left;
                }
                else
                {
                    if (current->right == nullptr)
                        current->right = &new Node<T>(payload);
                    break;
                    else current = current->right;
                }
            }
        }
        size++;
    }

    void print()
    {
        std::cout << head->payload;
    }
};

int main(void)
{
    BinaryTree<int> *binary_tree = new BinaryTree<int>();
    binary_tree->append(3);
    binary_tree->print();
    return 0;
}