#include "Graf.h"

Graf::Graf()
{
	liczbaWierzcholkow = 0;
	liczbaKrawedzi = 0;
}

Graf::~Graf()
{
}


void Graf::zbudujGraf()
{
	// Wczytaj wierzcholki i krawedzie
	std::cout << "Podaj ilosc wierzcholkow: ";
	std::cin >> liczbaWierzcholkow;
	std::cout << "Podaj ilosc krawedzi: ";
	std::cin >> liczbaKrawedzi; 

	for (int i = 1; i <= liczbaKrawedzi; i++)         
	{
		int w1, w2;
		std::cout << "Krawedz " << i << ": " << std::endl;
		std::cin >> w1;
		std::cin >> w2;	

		l[w1].push_back(w2);
		l[w2].push_back(w1);
	}
	std::cout << std::endl;
}

void Graf::dodajKrawedz(int w1, int w2)
{
	if (l[w1].size() == 0)
	{
		liczbaWierzcholkow++;
	}

	if (l[w2].size() == 0)
	{
		liczbaWierzcholkow++;
	}

	l[w1].push_back(w2);
	l[w2].push_back(w1);
	liczbaKrawedzi++;
}

void Graf::usunKrawedz(int w1, int w2)
{
	bool czyUsunieto = false;
	std::vector<int>::iterator position = std::find(l[w1].begin(), l[w1].end(), w2);
	if (position != l[w1].end())
	{
		l[w1].erase(position);
		czyUsunieto = true;
	}

	position = std::find(l[w2].begin(), l[w2].end(), w1);
	if (position != l[w2].end())
	{
		l[w2].erase(position);
		czyUsunieto = true;
	}

	if (czyUsunieto == true)
	{
		std::cout << "Usunieto krawedz: " << w1 << " -> " << w2 << std::endl;
		if (l[w1].size() == 0)
		{
			liczbaWierzcholkow--;
		}

		if (l[w2].size() == 0)
		{
			liczbaWierzcholkow--;
		}

		liczbaKrawedzi--;
	}
	else
	{
		std::cout << "Blad usuwania krawedzi! Podana krawedz " << w1 << " -> " << w2 << " nie istnieje." << std::endl;
	}
	std::cout << std::endl;
}

void Graf::wyswietlListeSasiadow()
{
	if (liczbaWierzcholkow == 0)
	{
		std::cout << "Graf jest pusty!" << std::endl;
	}
	else
	{
		std::cout << "Lista sasiedztwa wyglada nastepujaco: " << std::endl;
		for (int i = 1; i < liczbaWierzcholkow + 1; i++)
		{
			std::cout << "Wierzcholek " << i << ": ";

			for (int j = 0; j < l[i].size(); j++)
			{
				if (j != l[i].size() - 1)
				{
					std::cout << l[i][j] << " , ";
				}
				else
				{
					std::cout << l[i][j] << std::endl;
				}
			}
		}
	}
	std::cout << std::endl;
}

void Graf::wyswietlMacierzSasiadow()
{
	if (liczbaWierzcholkow == 0)
	{
		std::cout << "Graf jest pusty!" << std::endl;
	}
	else
	{
		std::cout << "Macierz sasiedztwa wyglada nastepujaco: " << std::endl;
		std::cout << "  ";
		for (int i = 1; i < zwrocLiczbeWierzcholkow() + 1; i++)
		{
			std::cout << i << " ";
		}
		std::cout << std::endl;

		bool czyJest = false;

		for (int i = 1; i < zwrocLiczbeWierzcholkow() + 1; i++)
		{
			std::cout << i << " ";
			for (int j = 1; j < zwrocLiczbeWierzcholkow() + 1; j++)
			{
				for (int w = 0; w < l[i].size(); w++)
				{
					if (l[i][w] == j)
					{
						czyJest = true;
					}
				}

				if (czyJest == true)
				{
					std::cout << 1 << " ";
				}
				else
				{
					std::cout << 0 << " ";
				}
				czyJest = false;

				if (j == zwrocLiczbeWierzcholkow())
				{
					std::cout << std::endl;
				}
			}
		}
	}
	std::cout << std::endl;
}

int const Graf::zwrocLiczbeWierzcholkow()
{
	return liczbaWierzcholkow;
}

int const Graf::zwrocLiczbeKrawedzi()
{
	return liczbaKrawedzi;
}