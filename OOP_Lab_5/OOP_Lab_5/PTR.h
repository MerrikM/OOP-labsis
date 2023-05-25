#pragma once

#include <memory>

using namespace std;

class Point {
private:
	int x;
	int y;

public:
	Point() : x(0), y(0) {
		printf("Point()\n");
	}

	Point(int x, int y) : x(x), y(y) {
		printf("Point(int x, int y)\n");
	}

	Point(const Point& t) : x(t.x), y(t.y) {
		printf("Point(const Point &t)\n");
	}

	void dump() {
		printf("%d, %d\n", x, y);
	}

	~Point() {
		printf("~Point()\n");
	}
};

void pass_object(unique_ptr<Point> p) {
	p->dump();
}

unique_ptr<Point> return_uniqueObject(int x, int y) {
	return make_unique<Point>(x, y);
}

shared_ptr<Point> pass_object(shared_ptr<Point> p) {
	p->dump();
	return p;
}

void PTR() {
	cout << "\n\n______________PTR______________" << endl;
	/*unique_ptr<Point> p(new Point(1, 2));
	p->dump();*/


	/*
	Как только появляется новый shared_ptr, хранящий ссылку на всё тот же объект, счётчик ссылок для этого указателя увеличивается на
	единицу, а как очередной shared_ptr удаляется, счётчик ссылок уменьшается. Как только
	он достигнет нуля – объект будет удалён.

	С shared_ptr никаких проблем с передачей его в функцию и возврата из неё
	нет, как нет их и с присваиванием в другую переменную. Счётчик ссылок автоматически
	увеличивается и уменьшается при любом таком присвоении и удалении очередного локального умного указателя соответственно
	*/
	shared_ptr<Point> p = make_shared<Point>(1, 2);
	p = pass_object(p);
	shared_ptr<Point> p2 = p;
	p2->dump();
}