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
class Heap
{
private:
    int size = 0;
    Node<T> *head = nullptr;

public:
    Heap() {}

    int Size() { return size; }

    void insert(T payload)
    {
        if (head == nullptr)
            head = new Node<T>(payload);
        else
        {
            if (head->payload == payload)
                return;
            else if (head->payload < payload)
            {
                payload = head->payload;
                head->payload = payload;
            }
        }
    }
};

int main(void)
{
    Heap<int> test;

    std::cout << test.Size();

    return 0;
}