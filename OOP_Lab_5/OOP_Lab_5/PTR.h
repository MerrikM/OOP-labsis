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
	��� ������ ���������� ����� shared_ptr, �������� ������ �� �� ��� �� ������, ������� ������ ��� ����� ��������� ������������� ��
	�������, � ��� ��������� shared_ptr ���������, ������� ������ �����������. ��� ������
	�� ��������� ���� � ������ ����� �����.

	� shared_ptr ������� ������� � ��������� ��� � ������� � �������� �� ��
	���, ��� ��� �� � � ������������� � ������ ����������. ������� ������ �������������
	������������� � ����������� ��� ����� ����� ���������� � �������� ���������� ���������� ������ ��������� ��������������
	*/
	shared_ptr<Point> p = make_shared<Point>(1, 2);
	p = pass_object(p);
	shared_ptr<Point> p2 = p;
	p2->dump();
}