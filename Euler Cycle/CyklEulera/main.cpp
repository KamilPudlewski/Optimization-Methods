#include <iostream>
#include "Graf.h"
#include "Metody.h"

int main()
{
	Metody sprawdz;

	//Graf graf1;
	//graf1.zbudujGraf();
	//graf1.wyswietlListeSasiadow();
	//graf1.wyswietlMacierzSasiadow();
	//sprawdz.CyklEulera(graf1);

	//Graf graf2;
	//graf2.dodajKrawedz(1, 2);
	//graf2.dodajKrawedz(2, 3);
	//graf2.dodajKrawedz(3, 1);
	//graf2.dodajKrawedz(3, 4);
	//graf2.dodajKrawedz(4, 5);
	//graf2.dodajKrawedz(5, 3);
	//graf2.wyswietlListeSasiadow();
	//graf2.wyswietlMacierzSasiadow();
	//sprawdz.CyklEulera(graf2);

	//Graf graf3;
	//graf3.dodajKrawedz(1, 2);
	//graf3.dodajKrawedz(2, 3);
	//graf3.dodajKrawedz(3, 1);
	//graf3.dodajKrawedz(3, 4);
	//graf3.wyswietlListeSasiadow();
	//graf3.wyswietlMacierzSasiadow();
	//sprawdz.CyklEulera(graf3);

	//graf3.usunKrawedz(3, 4);
	//graf3.wyswietlListeSasiadow();
	//graf3.wyswietlMacierzSasiadow();
	//sprawdz.CyklEulera(graf3);

	Graf graf4;
	graf4.dodajKrawedz(1, 2);
	graf4.dodajKrawedz(1, 3);
	graf4.dodajKrawedz(1, 5);
	graf4.dodajKrawedz(1, 6);
	graf4.dodajKrawedz(2, 4);
	graf4.dodajKrawedz(2, 6);
	graf4.dodajKrawedz(2, 5);
	graf4.dodajKrawedz(3, 5);
	graf4.dodajKrawedz(3, 4);
	graf4.dodajKrawedz(3, 6);
	graf4.dodajKrawedz(4, 6);
	graf4.dodajKrawedz(4, 5);
	graf4.wyswietlListeSasiadow();
	graf4.wyswietlMacierzSasiadow();
	sprawdz.CyklEulera(graf4);

	system("PAUSE");
}