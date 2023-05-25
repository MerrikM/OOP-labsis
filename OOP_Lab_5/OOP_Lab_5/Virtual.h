#pragma once
#include <string>
using namespace std;

// ����� ��� ���������� �������
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

// ����� � ����������� ��������
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
	/* �� �����������
	���������� � ��������� �� ����� ������ Human ����� ������� Student.
	��-�� ����, ��� ����� Who �� �����������, ��� ������� ������� ���������� ������ Who � Student, ���������� ����� Who � Human.
	����� ���������� ������ ���������� People, ��� ��� ���������� �� ����� ���� ���� ����� � � ������ People �� �� �����������
	*/
	cout << "\n\n______________�� �����������______________" << endl;
	Human* Alexey = new Student();
	Alexey->Who();
	delete Alexey;

	/* �����������
	��������� � ��������� �� ����� ������ Animal ����� ������� Dog.
	��-�� ����, ��� ����� sound - �����������, ����������� ���������� ������ ���������� � ������ Dog
	��� �� ���������� ��� �����������: ���������� ������ Animal � ������ Dog, �.� ���������� - �����������
	*/
	cout << "\n\n______________�����������______________" << endl;
	Animal* Pug = new Dog();
	Pug->sound();
	delete Pug;

	// ���������� �����
	cout << "\n\n______________���������� �����______________" << endl;
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
	���������� � C++ ��������
	����������� dynamic_cast<Cat*>(zoo[i]) ���� ��������� zoo[i] ���� Animal*, � ��������� �������� � �� ���������� ������� ���������� (RTTI) ��������, �������� �� ��
	����� ���� �� ����� ������ ������ ������ Cat* ��� ��� ��������. ���� ��� ���, �� ����������� ������ ���������� ��� �� ����� �����, �� ��� ���� ������ ���������: ��� ��
	�����, �� � ������ �����, ��� ������ ��������� �� �� Animal, � �� Cat, � ������ ���� �����
	����� ��������� � ���������� Cat *c. ���� �� �� ������ zoo[i] ��������� �����-�� ������
	������, �� ������� Cat, �� �������� ���������� ���������� � dynamic_cast � ���� ������
	������ ���������� nullptr.
	� ��������� ������� �� ���������, �� nullptr �� ��������, � ���� �� ��, �� � ������
	������������ �������� ����� catchMouse(), � ��� ��� �������� ���������� ����������� ���������� �����.
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