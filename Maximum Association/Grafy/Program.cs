using System;

namespace Grafy
{
    class Program
    {
        public static void TestujMaksymalneSkojarzenie()
        {
            GrafDwudzielny grafd = new GrafDwudzielny();
            grafd.DodajWierzcholek(1, 1);
            grafd.DodajWierzcholek(2, 1);
            grafd.DodajWierzcholek(3, 1);
            grafd.DodajWierzcholek(4, 1);

            grafd.DodajWierzcholek(5, 2);
            grafd.DodajWierzcholek(6, 2);
            grafd.DodajWierzcholek(7, 2);
            grafd.DodajWierzcholek(8, 2);

            grafd.DodajKrawedz(1, 5);
            grafd.DodajKrawedz(1, 6);
            grafd.DodajKrawedz(1, 7);

            grafd.DodajKrawedz(2, 5);
            grafd.DodajKrawedz(2, 8);

            grafd.DodajKrawedz(3, 5);
            grafd.DodajKrawedz(3, 8);

            grafd.DodajKrawedz(4, 5);

            grafd.ZbudujListeSasiedztwa();
            grafd.ZbudujMacierzSasiedztwa();

            Console.WriteLine("Macierz sasiedztwa:");
            grafd.WyswietlMacierzSasiedztwa();

            MaksymalneSkojarzenie mskoj = new MaksymalneSkojarzenie(grafd);
            Console.WriteLine("Maksymalne skojarzenie:");
            mskoj.wyswietlMaksymalneSkojarzenie();
        }

        static void Main(string[] args)
        {
            TestujMaksymalneSkojarzenie();
            Console.ReadLine();
        }
    }
}
