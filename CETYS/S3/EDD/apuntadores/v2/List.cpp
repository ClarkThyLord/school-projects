#include <vector>
#include <iostream>

template <class T>
struct Node {
    T payload;
    Node<T>* prev = nullptr;
    Node<T>* next = nullptr;

    public:
    Node(T payload)
    {
        this->payload = payload;
    }
};

template <class T>
class List{
    int size = 0;
    Node<T>* head = nullptr;
    
    public:
        List() {}

        List(T payload) : List()
        {
            insert(payload);
        }

        template<size_t N>
        List(T (&payloads)[N]) : List()
        {
            for (auto payload : payloads) insert(payload);
        }

        List(std::initializer_list<T> const &payloads) : List()
        {
            for (auto payload : payloads) insert(payload);
        }

        void insert(T payload)
        {
            if (head == nullptr) head = new Node<T>(payload);
            else for (Node<T>* current = head; true; current = current -> next) if (current -> next == nullptr) { current -> next = new Node<T>(payload); break; }
            size++;
        } 
    
        void erase(T payload)
        {
        }
    
        void erasem()
        {
        }

        T search(T payload)
        {
        }

        T get(int index)
        {
        }
    
        T rget(int index)
        {
        }

        T minimum()
        {
        }

        T maximum()
        {
            int res = 0;
        }

        Node<T>* succesor(T payload)
        {
        }

        Node<T>* predecessor(int payload)
        {
        }
    
        void unique()
        {
        }
    
        void split(T value, List *min_list, List *max_list)
        {
        }
    
        void print()
        {
            for (Node<T>* current = head; current != nullptr; current = current -> next) std::cout << current -> payload << (current -> next == nullptr ? "" : ", ");
        }
};

int main(void)
{
    List<int> test;
    test.insert(1);
    test.insert(2);
    test.insert(3);
    
    // List<int> test = *new List<int>(1);
    // test.insert(2);
    // test.insert(3);

    // int payloads[] = { 1, 2, 3 };
    // List<int> test = *new List<int>(payloads);
    
    // List<int> test = *new List<int>({ 1, 2, 3});
    
    test.print();
}