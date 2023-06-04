#pragma once
#include <string>
using namespace std;

// Êëàññ áåç âèðòóëüíûõ ìåòîäîâ
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

// Êëàññ ñ âèðòóëüíûìè ìåòîäàìè
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
	setlocale(LC_ALL, "ru");
	/* ÍÅ ÂÈÐÒÓÀËÜÍÛÅ
	Çàñîâûâàåì â óêàçàòåëü íà êëàññ ïðåäîê Human êëàññ ïîòîìêà Student.
	Èç-çà òîãî, ÷òî ìåòîä Who íå âèðòóàëüíûé, ïðè ïîïûòêå âûçâàòü ðåàëèçàöèþ ìåòîäà Who ó Student, âûçûâàåòñÿ ìåòîä Who ó Human.
	Òàêæå âûçûâàåòñÿ òîëüêî äåñòðóêòîð People, òàê êàê äåñòðóêòîð ïî ñâîåé ñóòè òîæå ìåòîä è â êëàññå People îí íå âèðòóàëüíûé
	*/
	cout << "\n\n______________Íå âèðòóàëüíûå______________" << endl;
	Human* Alexey = new Student();
	Alexey->Who();
	delete Alexey;

	/* ÂÈÐÒÓÀËÜÍÛÅ
	Çàñîâûâåì â óêàçàòåëü íà êëàññ ïðåäîê Animal êëàññ ïîòîìêà Dog.
	Èç-çà òîãî, ÷òî ìåòîä sound - âèðòóàëüíûé, âûâçûâàåòñÿ ðåàëèçàöèÿ ìåòîäà ïåðåêðûòàÿ â êëàññå Dog
	Òàê æå âûçûâàþòñÿ îáà äåñòðóêòîðà: äåñòðóêòîð êëàññà Animal è êëàññà Dog, ò.ê äåñòðóêòîð - âèðòóàëüíûé
	*/
	cout << "\n\n______________Âèðòóàëüíûå______________" << endl;
	Animal* Pug = new Dog();
	Pug->sound();
	delete Pug;

	// Ïðèâåäåíèå òèïîâ
	cout << "\n\n______________Ïðèâåäåíèå òèïîâ______________" << endl;
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
	Âñòðîåííàÿ â C++ ïðîâåðêà
	Êîíñòðóêöèÿ dynamic_cast<Cat*>(zoo[i]) áåð¸ò óêàçàòåëü zoo[i] òèïà Animal*, è èñïîëüçóÿ õðàíèìóþ â í¸ì èíôîðìàöèþ âðåìåíè âûïîëíåíèÿ (RTTI) âûÿñíÿåò, õðàíèòñÿ ëè íà
	ñàìîì äåëå ïî ýòîìó àäðåñó îáúåêò êëàññà Cat* èëè åãî ïîòîìêîâ. Åñëè ýòî òàê, òî êîíñòðóêöèÿ ïðîñòî âîçâðàùàåò òîò æå ñàìûé àäðåñ, íî óæå «ïîä äðóãèì øèëüäèêîì»: òîò æå
	àäðåñ, íî ñ äðóãèì òèïîì, ýòî òåïåðü óêàçàòåëü íå íà Animal, à íà Cat, à çíà÷èò ýòîò àäðåñ
	ìîæíî ïîìåñòèòü â ïåðåìåííóþ Cat *c. Åñëè æå ïî àäðåñó zoo[i] íàõîäèòñÿ êàêîé-òî äðóãîé
	îáúåêò, íå ïîòîìîê Cat, òî óñïåøíîå ïðèâåäåíèå íåâîçìîæíî è dynamic_cast â ýòîì ñëó÷àå
	ïðîñòî âîçâðàùàåò nullptr.
	Â ñëåäóþùåé ñòðî÷êå ìû ïðîâåðÿåì, íå nullptr ëè âåðíóëñÿ, è åñëè íå îí, òî ñ ïîëíîé
	óâåðåííîñòüþ âûçûâàåì ìåòîä catchMouse(), è âñÿ ýòà ñèòóàöèÿ íàçûâàåòñÿ «áåçîïàñíîå ïðèâåäåíèå òèïîâ».
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
