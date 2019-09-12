#pragma once
#include <iostream>
#include <vector>
#include "Graf.h"

class Metody
{
public:

	Metody();
	~Metody();

	void dfs(int v);
	void CyklEulera(Graf wczytajGraf);

private:
	int przebyty[100][100];			// Przebyte wierzcholki
	std::vector<int>wynik;			// Kolejne wierzcholki w cylku
	Graf graf;

	int wierzcholekStart;
};

