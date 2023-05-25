#pragma once
#include <string>
using namespace std;

// Класс без виртульных методов
class Human {
private:
	int age;
	string name;
public:
	Human() {
		printf("Human()\n");
	}
	~Human() {
		printf("~Human()\n");
	}
	void Sound() {
		printf("Hello!\n");
	}
	void Who() {
		printf("I'm human\n");
	}
};

class Student : public Human {
public:
	Student() {
		printf("Student()\n");
	}
	~Student() {
		printf("~Student()\n");
	}
	void Who() {
		printf("I am human and student\n");
	}
	void business() {
		printf("studying at UUST\n");
	}
};

// Класс с виртульными методами
class Animal {
private:
	string name;
public:
	Animal() {
		printf("Animal\n");
	}
	virtual ~Animal() {
		printf("~Animal\n");
	}

	virtual string classname() {
		return "Animal";
	}

	virtual bool isA(const string& who) {
		return who == "Animal";
	}

	virtual void sound() {
		printf("\n");
	}

	void method1() {
		printf("In Animal::method1()\n");
		method2();
	}

	virtual void method2() {
		printf("In Animal::method2()\n");
	}

};

class Cat : public Animal {
public:
	Cat() {
		printf("Cat()\n");
	}

	~Cat() override {
		printf("~Cat()\n");
	}

	string classname() override {
		return "Cat";
	}

	void sound() override {
		printf("Mew-mew\n");
	}

	void catchMouse() {
		printf("eating mouse\n");
	}

	bool isA(const string& who) override {
		return (who == "Animal") || (who == "Cat");
	}

	void method2() {
		printf("In Cat::method2()\n");
	}
};

class Manul : public Cat {
public:
	bool isA(const string& who) override {
		return (who == "Manul") || (Cat::isA(who));
	}
};

class Dog : public Animal {
public:

	Dog() {
		printf("Dog()\n");
	}

	~Dog() override{
		printf("~Dog()\n");
	}

	string classname() override{
		return "Dog";
	}

	void sound() override {
		printf("Gaf-gaf\n");
	}

	void catchCat() {
		printf("eating cat\n");
	}

	bool isA(const string& who) override {
		return(who == "Animal") || (who == "Dog");
	}
};

class Pug : public Dog {
public:
	bool isA(const string& who) override {
		return(who == "Pug") || (Dog::isA(who));
	}
};

void Virtual() {
	/* НЕ ВИРТУАЛЬНЫЕ
	Засовываем в указатель на класс предок Human класс потомка Student.
	Из-за того, что метод Who не виртуальный, при попытке вызвать реализацию метода Who у Student, вызывается метод Who у Human.
	Также вызывается только деструктор People, так как деструктор по своей сути тоже метод и в классе People он не виртуальный
	*/
	cout << "\n\n______________Не виртуальные______________" << endl;
	Human* Alexey = new Student();
	Alexey->Who();
	delete Alexey;

	/* ВИРТУАЛЬНЫЕ
	Засовывем в указатель на класс предок Animal класс потомка Dog.
	Из-за того, что метод sound - виртуальный, вывзывается реализация метода перекрытая в классе Dog
	Так же вызываются оба деструктора: деструктор класса Animal и класса Dog, т.к деструктор - виртуальный
	*/
	cout << "\n\n______________Виртуальные______________" << endl;
	Animal* Pug = new Dog();
	Pug->sound();
	delete Pug;

	// Приведение типов
	cout << "\n\n______________Приведение типов______________" << endl;
	const int ZOO_SIZE = 10;
	Animal* zoo[ZOO_SIZE];
	for (int i = 0; i < ZOO_SIZE; i++) {
		int rand_value = rand() % 2 + 1;
		if (rand_value == 1)
			zoo[i] = new Dog();
		else
			zoo[i] = new Cat();
	}
	/*
	Встроенная в C++ проверка
	Конструкция dynamic_cast<Cat*>(zoo[i]) берёт указатель zoo[i] типа Animal*, и используя хранимую в нём информацию времени выполнения (RTTI) выясняет, хранится ли на
	самом деле по этому адресу объект класса Cat* или его потомков. Если это так, то конструкция просто возвращает тот же самый адрес, но уже «под другим шильдиком»: тот же
	адрес, но с другим типом, это теперь указатель не на Animal, а на Cat, а значит этот адрес
	можно поместить в переменную Cat *c. Если же по адресу zoo[i] находится какой-то другой
	объект, не потомок Cat, то успешное приведение невозможно и dynamic_cast в этом случае
	просто возвращает nullptr.
	В следующей строчке мы проверяем, не nullptr ли вернулся, и если не он, то с полной
	уверенностью вызываем метод catchMouse(), и вся эта ситуация называется «безопасное приведение типов».
	*/
	/*for (int i = 0; i < ZOO_SIZE; i++) {
		Cat* d = dynamic_cast<Cat*>(zoo[i]);
		if (d != nullptr)
			d->sound();
	}*/

	for (int i = 0; i < ZOO_SIZE; i++) {
		if (zoo[i]->isA("Dog"))
			static_cast<Dog*>(zoo[i])->catchCat();
		else
			static_cast<Cat*>(zoo[i])->catchMouse();
	}

	cout << "\n\n______________method1, method2______________" << endl;
	Animal* a = new Animal();
	a->method1();
	a->method2();
	delete a;

	Cat* c = new Cat();
	c->method2();
	delete c;
}