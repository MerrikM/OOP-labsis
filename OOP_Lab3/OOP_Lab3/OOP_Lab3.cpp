#include <random>
#include <iostream>


using namespace std;

template <typename T>
class List {
public:
    List() {
        printf("List()\n");
    }
    ~List() {
        printf("~List()\n");
        clear();
    }


    void push_back(T data) {
        //Если список пуст, то создаем новую ноду и делаем ее и Head и Tail
        if (Head == nullptr)
        {
            Head = new Node<T>(data);
            Tail = Head;
        }
        //Если список не пустой, то создаем новую ноду и делаем ее pNext у Tail, затем делаем ее новым Tail.
        else
        {
            Tail->pNext = new Node<T>(data);
            Tail = Tail->pNext;
        }
        Size++;
    }

    void push_front(T data)
    {
        if (Head == nullptr)
        {
            Node<T>* NewNode = new Node<T>(data);
            NewNode->pNext = Head;
            Head = NewNode;
            Tail = Head;
            Size++;
        }
        else
        {
            Node<T>* NewNode = new Node<T>(data);
            NewNode->pNext = Head;
            Head = NewNode;
            Size++;
        }

    }

    T pop_front() { // T функция, для возвращения данных, чтобы была возможность их удалить
        //если список пустой, ничего не делаем
        if (Head == nullptr)
        {
            return nullptr;
        }
        //Если в списке остался последний элемент, удаляем его и присваиваем Head и Tail значение nullptr
        else if (Head->pNext == nullptr)
        {
            T tempData = Head->data;
            delete Head;
            Head = nullptr;
            Tail = nullptr;
            Size--;
            return tempData;
        }
        // Создаем временную ноду, сохраняем в нее значение Head. 
        // Делаем следующий после Head элемент новым Head, удаляем временную ноду (удаляем старый Head).
        else
        {
            Node<T>* tempNode = Head;
            T tempData = Head->data;

            Head = Head->pNext;

            // удаляем указатель на старый head
            delete tempNode;

            Size--;

            return tempData;
        }
    }

    T pop_back() {
        if (Head == nullptr)
        {
            return nullptr;
        }
        //Если в списке остался последний элемент, удаляем его и присваиваем Head и Tail значение nullptr
        else if (Head->pNext == nullptr)
        {
            T tempData = Head->data;
            delete Head;
            Head = nullptr;
            Tail = nullptr;
            Size--;
            return tempData;
        }
        else
        {
            // Node previous - предпоследняя нода
            // Создаю предпоследний элемент, у следующего элемента запоминаю данные, 
            // удаляю следующий элемент и предпоследний элемент делаю хвостом
            T returnData;
            Node<T>* previous = this->Head;

            while (previous->pNext != Tail)
                previous = previous->pNext;
            returnData = Tail->data; // Запомнили данные, которые были в последней ноде (в хвосте)
            Tail = previous; // Предпоследний элемент стал хвостом
            delete previous->pNext; 
            Size--;
            return returnData;

        }
    }


    void insert(T data, int index) {

        if (index < 0 || index - GetSize() > 0) // Если попытаться добавить элемент в индекс, который на 1 больше крайнего
        {
            return;
        }
        else if (index == 0 || Head == nullptr) // Если список или добавить ноду в 0 индекс
        {
            push_front(data);
        }
        else if (index == GetSize()) { // Если нужно добавить последний элемент
            push_back(data);
        }
        else {
            //Находим элемент, который предшествует элементу с индексом, по которому нужно вставить новый элемент.
            Node<T>* previous = this->Head;
            for (int i = 0; i < index - 1; i++)
            {
                previous = previous->pNext;
            }
            //Создаю новую ноду, ставлю ее указатель pNext на элемент index+1
            //Затем указатель pNext у previous ставлю на новосозданную ноду
            Node<T>* newNode = new Node<T>(data);
            newNode->pNext = previous->pNext;
            previous->pNext = newNode;
            Size++;
        }
    }

    T remove(int index)
    {
        if (index < 0 || index - GetSize() > -1) // Удаляем только существующие индексы элементов
        {
            return nullptr;
        }
        else if (index == 0 || Head == nullptr) // Если нужно удалить первый элемент
        {
            return pop_front();
        }
        else {
            //Находим элемент, который предшествует элементу с индексом, по которому нужно вставить новый элемент.
            Node<T>* previous = this->Head;
            for (int i = 0; i < index - 1; i++)
            {
                previous = previous->pNext;
            }
            //Создаю ноду ToDelete, ставлю ее указатель pNext на элемент с нужным index
            //Затем указатель pNext у previous ставлю на новосозданную ноду
            Node<T>* ToDeleteNode = previous->pNext;
            T ToDeleteData = ToDeleteNode->data;
            previous->pNext = ToDeleteNode->pNext;
            delete ToDeleteNode;
            Size--;
            return ToDeleteData;
        }
    }

    void clear()
    {
        while (Size)
        {
            T temp = pop_front();
            delete temp;
        }
    }

    //Вернуть размер списка
    int GetSize() { return Size; }

    ////Перегрузка оператор [], для отладки, чтобы удобно выводить элементы
    //T& operator[](const int index) { // Работаем со списком, как с массивом
    //    int counter = 0;
    //    Node<T>* current = this->Head;

    //    while (current != nullptr)
    //    {
    //        if (counter == index)
    //        {
    //            return current->data;
    //        }
    //        current = current->pNext;
    //        counter++;
    //    }
    //}

private:
    // Шаблонный класс для изменения данных всех типов
    template <typename T>
    class Node {
    public:
        Node* pNext; // Указатель на следующую  ноду

        // Взаимодействие и хранение данных через поле обобщенного типа, 
        // тип объекта будет известен в момент создания класса List
        T data;

        // Передаем в конструктор данные (объект) и указатель на следующую ноду
        // Для проверки вставки объекта в конец списка, мы будем передавать только данные,
        // а указателя на след. элемент у нас не будет, поэтому, контролируем то, что будет
        // находится в этом указателе (на что он будет указывать)
        // pNext последнего элемента будет указывать на null

        // Для поля data, при добавлении элемента заранее подгатавливаем его, но данных для этого элемента еще нет,
        // поэтому там будет мусор (T data)
        // Присвоим ему значение поумолчанию (передаем класс T data=T())
        // При передаче класса, вызовется конструктор класса поумолчанию
        // Если же передадим примитивный тип данных, то будет присвоен примитивный тип данных data
        // Лишь бы не было мусора \_(-_-)_/
        Node() : data(T()), pNext(nullptr) {
            printf("Node()\n");
        }
        Node(T _data) : data(_data), pNext(nullptr) {
            printf("Node(T _data)\n");
        }
        ~Node() {
            //printf("~Node()\n");
        }
    };

    Node<T>* Tail; // Указатель на последний элемент
    Node<T>* Head; // Указатель на головной элемент
    // Фиксируем кол-во элементов, чтобы не пересчитывать позже, чтобы в момент, когда нужно узнать размер списка,
    // не пробегать по всему списку и считать их, мы можем сразу вернуть значение переменной Size
    int Size;

};

class Point {
public:
    Point() : x(0), y(0)
    {
        printf("Point()\n");
    }
    Point(int x, int y)
    {
        printf("Point(int %d, int %d)\n", x, y);
        this->x = x;
        this->y = y;
    }
    Point(const Point& T) {
        printf("Point(const Point& T)\n");
        this->x = T.x;
        this->y = T.y;
    }
    virtual ~Point() {
        //printf("~Point(%d, %d)\n", x, y);
    }
    int GetCordX() {
        return x;
    }
    virtual void output() {
        printf("x = %d y = %d\n", x, y);
    }
protected:
    int x;
    int y;
};

class ColoredPoint : public Point {
protected:
    int color;
public:
    ColoredPoint() : Point(), color(0) {
        printf("ColoredPoint()\n");
    }
    ColoredPoint(int x, int y, int color) : Point(x, y), color(color) {
        printf("ColoredPoint(int %d, int %d, int %d)\n", x, y, color);
    }
    ColoredPoint(const ColoredPoint& cp) : Point(cp), color(cp.color) {
        printf("ColoredPoint(const Point& p)\n");
    }
    ~ColoredPoint() override {
        //printf("~ColoredPoint(%d, %d, %d)\n", x, y, color);
    }
    void output() override {
        printf("x = %d, y = %d, color = %d\n", x, y, color);
    }
    void changeColor(char newColor) {
        color = newColor;
    }
};

int main()
{
    //srand(time(NULL));
    List<Point*> PointList;
    Point* toDeletePoint;
    time_t begin = time(NULL);
    const int LIST_SIZE = 10000;
    int SwitchRand;
    srand(time(NULL));
    random_device random_device;
    mt19937 generator(random_device());
    uniform_int_distribution<> distribution(1, 100000); // Диапазон генерации
    for (int i = 0; i < LIST_SIZE; i++)
    {
        //int x = distribution(generator) % 10;
        SwitchRand = rand() % 6;
        switch (SwitchRand)
        {
        case 0:
            PointList.push_back(new Point(distribution(generator), distribution(generator)));
            cout << "push_back" << endl;
            break;
        case 1:
            PointList.push_back(new ColoredPoint(distribution(generator), distribution(generator), rand() % 255));
            cout << "push_back" << endl;
            break;
        case 2:
            // Указатель на данные
            // Возвращаю из поп фронт в туделитпоинт, если туделитпоинт != nullptr (если не пустой), удаляем
            toDeletePoint = PointList.pop_front();
            if (toDeletePoint) {
                delete toDeletePoint;
                cout << "pop_front" << endl;
            }
            break;
        case 3:
            PointList.push_front(new Point(distribution(generator), distribution(generator)));
            cout << "push_front" << endl;
            break;
        case 4:
            PointList.insert(new Point(rand() % 100 * 10, rand() % 100 * 10), distribution(generator));
            cout << "insert" << endl;
            break;
        case 5:
            toDeletePoint = PointList.remove(distribution(generator));
            if (toDeletePoint)
            {
                delete toDeletePoint;
                cout << "remove" << endl;

            }
        case 6:
            toDeletePoint = PointList.pop_back();
            if (toDeletePoint) {
                delete toDeletePoint;
                cout << "pop_back" << endl;
            }
            break;
        default:
            break;
        }
    }
    cout << PointList.GetSize() << endl;

    time_t end = time(NULL);
    cout << "\n\n\n\n\n\n\n\The elapsed time is " << end - begin << "\n\n\n\n\n\n";



    //Проверка метода pop_back()
    /*cout << "List size is " << PointList.GetSize() << endl;
    Point* ToDel = nullptr;
    for (int i = 0; i < 10; i++)
    {
        ToDel = PointList.pop_back();
        if (ToDel != nullptr)
        {
           delete ToDel;
        }

    }
    cout << "List size is " << PointList.GetSize() << endl;*/

    //Проверка метода remove
    /*cout << "List size is " << PointList.GetSize() << endl;
    Point* ToDel = nullptr;
    for (int i = 0; i < 10; i++)
    {
        ToDel = PointList.remove(rand() % 10);
        if (ToDel != nullptr)
        {
           delete ToDel;
        }

    }
    cout << "List size is " << PointList.GetSize() << endl;*/

    //Проверка метода insert
    /*List<int*> intList;
    for (int i = 0; i < 5; i++)
    {
        intList.push_back(new int(100));
    }
    for (int i = 0; i < 10; i++)
    {
        int index = rand() % 15;
        int value = rand() % 50;
        intList.insert(new int(value), index);
        printf("Index = %d\n", index);
        for (int i = 0; i < intList.GetSize(); i++)
        {
            cout << *intList[i] << " ";
        }
        cout << endl << endl;
    }*/



}
