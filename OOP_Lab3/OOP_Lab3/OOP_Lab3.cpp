#include <iostream>

using namespace std;


// T - обобщенный тип данных
template<typename T>
class List {
public:
    List();
    ~List();

    void pop_front();
    void push_front(T data);
    void pop_back();
    void insert(T value, int index);
    void removeAt(int index);
    void clear();
    void push_back(T data);
    int get_Size() { return Size; }

    T& operator[](const int index); // индекс - номер элемента, который нужно вернуть

private:
    // Шаблонный класс для изменения данных всех типов
    template<typename T>
    struct Node {
    public:
        Node* pNext;

        T data;

        Node(T data = T(), Node* pNext = nullptr) { // Т.к при добавлении 1 элемента нет указателя на следующий, будет поумолчанию использоваться Node* pNext = nullptr
            this->data = data;
            this->pNext = pNext;
        }
    };
    Node<T>* head; // head - голова списка (самый первый элемент), (т.к все элементы списка будут выделятся в динамической памяти)

    int Size; // Количество элементов
};

template<typename T>
List<T>::List() {
    Size = 0; 
    head = nullptr; 
}

template<typename T>
List<T>::~List() {
    cout << "~List()";
    clear();
}

template<typename T>
void List<T>::pop_front() {
    Node<T>* temp = head; 

    head = head->pNext; 
    delete temp; 
    Size--;
}

template<typename T>
void List<T>::push_front(T data) { 
    head = new Node<T>(data, head);
    Size++;
}

template<typename T>
void List<T>::pop_back() { 
    removeAt(Size - 1);
}

template<typename T>
void List<T>::insert(T value, int index) {
    if (index == 0) {
        push_front(value);
    }
    else {
        Node<T>* previous = this->head;
        
        for (int i = 0; i < index - 1; i++) {
            previous = previous->pNext;
        }
        Node<T>* newNode = new Node<T>(value, previous->pNext);
        
        previous->pNext = newNode;

        Size++;
    }
}

template<typename T>
void List<T>::removeAt(int index) {
    if (index = 0) {
        pop_front();
    }
    else {
        Node<T>* previous = this->head;
        for (int i = 0; i < index - 1; i++) {
            previous = previous->pNext;
        }

        Node<T>* toDelete = previous->pNext;
        
        previous->pNext = toDelete->pNext;

        delete toDelete;
        Size--;
    }
}

template<typename T>
void List<T>::clear() {
    while (Size) {
        pop_front();
    }
}

template<typename T>
void List<T>::push_back(T data) { 
    if (head == nullptr) {
        head = new Node<T>(data); 
    }
    else { 
        Node<T>* current = this->head; 

        while (current->pNext != nullptr) {
            current = current->pNext; 
        }

        current->pNext = new Node<T>(data);
    }
    Size++;
}

template<typename T>
T& List<T>::operator[](const int index) {
    Node<T>* current = this->head;
    int counter = 0;
    while (current != nullptr) {
        if (counter == index) { 
            return current->data;
        }
        current = current->pNext;
        counter++;
    }
}

int main() {
    List<int> lst;
    lst.push_back(5);
    lst.push_back(5);
    lst.push_back(6);
    cout << lst.get_Size() << endl;
    cout << lst[2];
    return 0;
}