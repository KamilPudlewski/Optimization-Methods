#include "Metody.h"

Metody::Metody()
{
}

Metody::~Metody()
{
}


void Metody::dfs(int v)
{
	while (!graf.l[v].empty())
	{
		int w = graf.l[v].back();
		graf.l[v].pop_back();
		if (przebyty[v][w] == 0)
		{
			przebyty[v][w] = 1;
			przebyty[w][v] = 1;
			dfs(w);
			wynik.push_back(w);
		}
	}
}

void Metody::CyklEulera(Graf wczytajGraf)
{
	graf = wczytajGraf;

	for (int i = 0; i < graf.zwrocLiczbeWierzcholkow(); i++)
	{
		if (graf.l[i].size() % 2 != 0)
		{
			std::cout << "Graf nie posiada Cyklu Eulera!" << std::endl;
			return;
		}
	}

	std::cout << "Podaj wierzcholek startowy: ";
	std::cin >> wierzcholekStart;
	std::cout << "Cykl Eulera: " << wierzcholekStart << " -> ";
	dfs(wierzcholekStart);
	
	for (int i = 0; i < graf.zwrocLiczbeKrawedzi(); i++)
	{
		if (i != graf.zwrocLiczbeKrawedzi() - 1)
		{
			std::cout << wynik.back() << " -> ";
		}
		else
		{
			std::cout << wynik.back() << std::endl;
		}
		wynik.pop_back();
	}
}