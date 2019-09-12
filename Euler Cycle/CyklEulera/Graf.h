#pragma once
#include <iostream>
#include <vector>

#define WIELKOSC 100

class Graf

{
public:
	Graf();
	~Graf();

	void zbudujGraf();
	void dodajKrawedz(int w1, int w2);
	void usunKrawedz(int w1, int w2);
	void wyswietlListeSasiadow();
	void wyswietlMacierzSasiadow();
	int const zwrocLiczbeWierzcholkow();
	int const zwrocLiczbeKrawedzi();
	std::vector<int>l[WIELKOSC];

private:
	int liczbaWierzcholkow;
	int liczbaKrawedzi;
};

