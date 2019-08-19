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
    
    private:
        Node<T>* getN(int index)
        {
            if (abs(index) >= size) throw std::invalid_argument("index out of bound");
            else if (index < 0) index = size + index;
            Node<T>* current = head;
            for (int i = 0; i < index; i++) current = current -> next;
            return current;
        }

        Node<T>* searchN(T payload)
        {
            for (Node<T>* current = head; current != nullptr; current = current -> next) if (current -> payload == payload) return current;
            return nullptr;
        }

        void eraseN(Node<T>* node)
        {
            if (node -> prev == nullptr) head = node -> next;
            else node -> prev -> next = node -> next;
            if (node -> next != nullptr) node -> next -> prev = node -> prev;
            delete node;
            size--;
        }

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

        int Size()
        {
            return size;
        }

        T get(int index)
        {
            return getN(index) -> payload;
        }

        int search(T payload)
        {
            int index = 0;
            for (Node<T>* current = head; current != nullptr; current = current -> next)
            {
                if (current -> payload == payload) return index++;
                index++;
            }
            return -1;
        }

        int succesor(T payload)
        {
            int res = search(payload);
            return res >= 0 ? res + 1 : -1;
        }

        int predecessor(int payload)
        {
            int res = search(payload);
            return res >= 1 ? res - 1 : -1;
        }

        void insert(T payload)
        {
            if (head == nullptr) head = new Node<T>(payload);
            else for (Node<T>* current = head; true; current = current -> next) if (current -> next == nullptr)
            {
                current -> next = new Node<T>(payload);
                current -> next -> prev = current;
                break;
            }
            size++;
        }
    
        void erase(int index)
        {
            eraseN(getN(index));
        }
    
        void find_erase(T payload)
        {
            eraseN(searchN(payload));
        }
    
        void erasem()
        {
            if (size == 1 || size == 2) eraseN(head);
            else if (size > 2)
            {
                Node<T>* current = head;
                for (int i = 0; i < size / 2; i++) current = current -> next;
                eraseN(current);
            }
        }

        int minimum()
        {
            if (size > 0)
            {
                int index = -1;
                int result = 0;
                Node<T>* resultN = head;
                for (Node<T>* current = head; current != nullptr; current = current -> next)
                {
                    index++;
                    if (current -> payload < resultN -> payload)
                    {
                        result = index;
                        resultN = current;
                    }
                }
                return result;
            }
            else return -1;
        }

        int maximum()
        {
            if (size > 0)
            {
                int index = -1;
                int result = 0;
                Node<T>* resultN = head;
                for (Node<T>* current = head; current != nullptr; current = current -> next)
                {
                    index++;
                    if (current -> payload > resultN -> payload)
                    {
                        result = index;
                        resultN = current;
                    }
                }
                return result;
            }
            else return -1;
        }
    
        void unique()
        {
            List<T> uniques;
            for (Node<T>* current = head; current != nullptr; current = current -> next)
            {
                if (uniques.search(current -> payload) == -1) uniques.insert(current -> payload);
                else eraseN(current);
            }
        }
    
        void split(T value, List<T> *min_list, List<T> *max_list)
        {
            for (Node<T>* current = head; current != nullptr; current = current -> next)
            {
                if (current -> payload < value) min_list -> insert(current -> payload);
                else max_list -> insert(current -> payload);
            }
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
    std::cout << "\n---\n";


    test.erase(1);
    // test.erase(2);
    // test.erase(3);

    test.print();
    std::cout << "\n---\n";


    test.insert(7);
    test.insert(4);
    test.insert(5);
    test.erasem();

    test.print();
    std::cout << "\n---\n";


    std::cout << test.get(0) << ", ";
    std::cout << test.get(1) << ", ";
    std::cout << test.get(2) << ", ";
    std::cout << test.get(3) << ", ";
    try { std::cout << test.get(4); } catch(const std::exception& e) { std::cout << e.what() << ", "; }
    std::cout << test.get(-1);

    std::cout << '\n';
    test.print();
    std::cout << "\n---\n";


    test.insert(1);
    test.insert(1);
    test.insert(-5);
    test.insert(1);
    test.insert(5);
    test.insert(-4);
    test.insert(-3);
    test.insert(3);
    test.insert(-2);
    test.insert(4);
    test.insert(-1);
    test.insert(-1);

    test.print();
    std::cout << "\n---\n";

    test.unique();

    test.print();
    std::cout << "\n---\n";


    List<int> mins, maxs;
    test.split(0, &mins, &maxs);

    test.print();
    std::cout << " -> ";
    mins.print();
    std::cout << " | ";
    maxs.print();
    std::cout << "\n---\n";


    test.print();
    std::cout << " -> min: " << test.get(test.minimum());
    std::cout << " | max: " << test.get(test.maximum());
    std::cout << "\n---\n";
}