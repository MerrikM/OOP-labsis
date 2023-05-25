#pragma once
#include <iostream>
#include <string>
using namespace std;

class Base {
protected:
	string name;
public:
	Base() {
		name = "";
		printf("Base()\n");
	}

	Base(string name) {
		this->name = name;
		printf("Base(string name)\n");
	}

	Base(const Base& base) {
		this->name = base.name;
		printf("Base(const Base& base)\n");
	}

	Base(Base* base) {
		this->name = base->name;
		printf("Base(Base * base)\n");
	}

	virtual ~Base() {
		printf("~Base()\n");
	}

	void classname() {
		printf("Base\n");
	}
};

class Desc : public Base {
public:
	Desc() {
		name = "";
		printf("Desc()\n");
	}

	Desc(string name) {
		this->name = this->name;
		printf("Desc(string name)\n");
	}

	Desc(const Desc& desc) {
		this->name = desc.name;
		printf("Desc(const Desc& desc)\n");
	}

	Desc(Desc* desc) {
		this->name = desc->name;
		printf("Desc(Desc* desc)\n");
	}

	virtual ~Desc() {
		printf("~Desc()");
	}

	void classname() {
		printf("Desc\n");
	}
};

void in1(Base b) {
	printf("in func 1\n");
}

void in2(Base* b) {
	printf("in func 2\n");
}

void in3(Base& b) {
	printf("in func 3\n");
}

// Создаст локальный объект и скопирует его
Base out1() {
	Base b("out1()");
	return b;
}

// Созданный внутри ф-ии объект не удалится. Утечка памяти
Base out2() {
	Base* b = new Base("out2()");
	return *b;
}

// Перед выходом из ф-ии локальныйй объект удаеляется и в main вернется удаленный объект
Base* out3() {
	Base b("out3()");
	return &b;
}

// Созданный внутри объект и его адрес из функции возвращаются в main и помещаются в перменную
Base* out4() {
	Base* b = new Base("out3()");
	return b;
}

// Локальный объект удаляет перед выходом из функции
Base& out5() {
	Base b("out5()");
	return b;
}

// Проблема с удалением объекта, ссылки не удаляют объекты, на которые указывают
Base& out6() {
	Base* b = new Base("out6()");
	return *b;
}

unique_ptr<Base> uniq_out(){
	return make_unique<Base>("uniq_out()");
}


void BaseDesc() {

	/*Base("HelloWorld");
	Base t1("Hello");
	t1 = Base("World");
	Base t2 = Base("Wierd");
	printf("\n");*/

	//TEST4 in1,2,3
	/*Base o("o");
	Base* po = new Base("po");
	cout << "\nBefore in1(o)\n";
	in1(o);
	cout << "After in1(o)\n";
	cout << "\nBefore in2(&o)\n";
	in2(&o);
	cout << "After in2(&o)\n";
	cout << "\nBefore in3(o)\n";
	in3(o);
	cout << "After in3(o)\n";
	cout << "\nBefore in1(*po)\n";
	in1(*po);
	cout << "After in1(*po)\n";
	cout << "\nBefore in2(po)\n";
	in2(po);
	cout << "After in2(po)\n";
	cout << "\nBefore in3(*po)\n";
	in3(*po);
	cout << "After in3(*po)\n";
	delete po;*/

	/*printf("Before out1 first way\n");
	Base o1 = out1();
	printf("After out1 first way\n");*/

	/*Base o1_2;
	printf("Before out1 second way\n");
	o1_2 = out1();
	printf("After out1 second way\n");*/

	//First way of out2 => утечка памяти. Динамический объект созданный внутри out2 не удаляется после выхода из функции.
	/*printf("Before out2 first way\n");
	Base o2 = out2();
	printf("After out2 first way\n");*/

	//Также утечка памяти
	/*Base o2_2;
	printf("Before out2 second way\n");
	o2_2 = out2();
	printf("After out2 second way\n");*/

	//Объект удаляет внутри функции, и в main возвращается уже удаленный объект. Локальный объект, созданный внутри ф-ии, вернуть нельзя
	/*printf("Before out3\n");
	Base* o3 = out3();
	printf("After out3\n");
	cout << "Work with deleted object:\n" << "Print number of object - " << (int)o3 % 1000 << "\nTrying to use method - ";
	o3->classname();*/

	//Мы теперь отвественны за удаление указателя
	/*printf("Before out4\n");
	Base* o4 = out4();
	printf("After out4\n");*/

	//Объект также как в out3 удаляется внутри фукнции, и в main присваивается пустое значение
	/*printf("Before out5\n");
	Base& o5 = out5();
	printf("After out5\n");
	cout << "Work with deleted object:\n" << "Print number of object - " << (int)&o5 % 1000 << "\nTrying to use method - ";
	o5.classname();*/

	//Ответственность за удаление указателя
	/*printf("Before out6\n");
	Base& o6 = out6();
	printf("After out6\n");*/
}