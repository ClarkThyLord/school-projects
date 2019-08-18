#include <iostream>

template <class T>
struct Node {
    T payload;
    Node<T>* next = nullptr;

    public:
    Node(T payload)
    {
        this->payload = payload;
    }
};

template <class T>
class List{
    Node<T>* head;
    
    public:
        List()
        {
            head = nullptr;
        }
    
        void insert(int payload)
        {
            if (head == nullptr) head = new Node<T>(payload);
            else
            {
                for (Node<T>* current = head; true; )
                {
                    if (current -> next == nullptr)
                    {
                        current -> next = new Node<T>(payload);
                        break;
                    }
                    else current = current -> next;
                }
            }
        } 
    
        void erase(int payload)
        {
            if (head -> payload == payload)
            {
                Node<T>* past_head = head;
                head = head -> next;
                delete past_head;
            }
            else
            {
                for (Node<T>* current = head; current != nullptr; )
                {
                    if (current -> next == nullptr) break;
                    if (current -> next -> payload == payload)
                    {
                        Node<T>* past_node = current -> next;
                        current -> next = current -> next -> next;
                        delete past_node;
                    }
                    else current = current -> next;
                }   
            }
        }
    
        void erasem()
        {
            int index = size();
            if (index == 1 || index == 2)
            {
                Node<T>* past_head = head;
                head = head -> next;
                delete past_head;
            }
            else if (index > 2)
            {
                index = index / 2;
                int position = 0;
                for (Node<T>* current = head; current != nullptr; position++)
                {
                    if (position + 1 == index && current -> next != nullptr)
                    {
                        Node<T>* past_node = current -> next;
                        current -> next = current -> next -> next;
                        delete past_node;
                        return;
                    }
                    else if (position > index) break;
                    current = current -> next;
                }
            }
        }
    
        int size()
        {
            int size = 0;
            for (Node<T>* current = head; current != nullptr; size++) current = current -> next;
            return size;
        }

        Node<T>* search(int payload)
        {
            for (Node<T>* current = head; current != nullptr; )
            {
                if (current -> payload == payload) return current;
                current = current -> next;
            }
            return nullptr;
        }

        int get(int index)
        {
            int position = 0;
            for (Node<T>* current = head; current != nullptr; position++)
            {
                if (position == index) return current -> payload;
                current = current -> next;
            }
            return 0;
        }
    
        int rget(int index)
        {
            index = size() - index - 1;
            int position = 0;
            for (Node<T>* current = head; current != nullptr; ++position)
            {
                if (position == index) return current -> payload;
                current = current -> next;
            }
            return 0;
        }

        int minimum()
        {
            int res = 0;
            if (head != nullptr)
            {
                res = head -> payload;
                for (Node<T>* current = head -> next; current != nullptr; )
                {
                    if (current -> payload < res) res = current -> payload;
                    current = current -> next;
                }
            }
            return res;
        }

        int maximum()
        {
            int res = 0;
            if (head != nullptr)
            {
                res = head -> payload;
                for (Node<T>* current = head -> next; current != nullptr; )
                {
                    if (current -> payload > res) res = current -> payload;
                    current = current -> next;
                }
            }
            return res;
        }

        Node<T>* succesor(int payload)
        {
            for (Node<T>* current = head; current != nullptr; )
            {
                if (current -> payload == payload) return current -> next;
                else current = current -> next;
            }
            return nullptr;
        }

        Node<T>* predecessor(int payload)
        {
            for (Node<T>* current = head; current != nullptr; )
            {
                if (current -> next == nullptr) break;
                else if (current -> next -> payload == payload) return current;
                else current = current -> next;
            }
            return nullptr;
        }
    
        void unique()
        {
            if (head != nullptr)
            {
                List valids;
                valids.insert(head -> payload);
                for (Node<T>* current = head; current != nullptr; )
                {
                    if (current -> next == nullptr) break;
                    else if (valids.search(current -> next -> payload) == nullptr) valids.insert(current -> next -> payload);
                    else
                    {
                        Node<T>* past_node = current -> next;
                        current -> next = current -> next -> next;
                        delete past_node;
                    }
                    current = current -> next;
                }
            }
        }
    
        void split(int value, List *min_list, List *max_list)
        {
            for (Node<T>* current = head; current != nullptr; )
            {
                if (current -> payload < value) min_list -> insert(current -> payload);
                else max_list -> insert(current -> payload);
                current = current -> next;
            }
        }
    
        void print()
        {
            for (Node<T>* current = head; current != nullptr; )
            {
                std::cout << current -> payload << (current -> next == nullptr ? "" : ", ");
                current = current -> next;
            }
        }
};

int main(void)
{
    List<int> test;
    test.insert(7);
    test.insert(8);
    test.insert(-1);
    std::cout << test.search(7) -> payload << '\n';
    std::cout << test.search(8) -> payload << '\n';
    test.erase(8);
    std::cout << test.search(8) << '\n';
    test.insert(-11);
    test.insert(12);
    test.insert(402);
    std::cout << test.minimum() << '\n';
    std::cout << test.maximum() << '\n';
    std::cout << test.predecessor(-1) -> payload << '\n';
    std::cout << test.succesor(-1) -> payload << '\n';
    std::cout << test.size() << '\n';
    std::cout << test.get(0) << '\n';
    std::cout << test.get(2) << '\n';
    std::cout << test.get(6) << '\n';
    std::cout << test.rget(0) << '\n';
    std::cout << test.rget(2) << '\n';
    std::cout << test.rget(6) << '\n';
    test.print();
    std::cout << '\n';
    test.erasem();
    test.print();
    std::cout << '\n';
    List<int>* mins = new List<int>();
    List<int>* maxs = new List<int>();
    test.split(0, mins, maxs);
    mins -> print();
    std::cout << '\n';
    maxs -> print();
    std::cout << '\n';
    test.insert(1);
    test.insert(3);
    test.insert(4);
    test.insert(-9);
    test.insert(-11);
    test.insert(12);
    test.insert(12);
    test.insert(-2);
    test.insert(4);
    test.print();
    std::cout << '\n';
    test.unique();
    test.print();
    std::cout << '\n';
}