using System;

namespace KolorowanieGrafu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kolorowanie Grafu!");

            // Storzenie grafu skladajacego sie z 6 wierzcholkow
            Graf graf = new Graf(6);

            graf.DodajKrawedz(1, 3);
            graf.DodajKrawedz(1, 2);
            graf.DodajKrawedz(1, 4);
            graf.DodajKrawedz(1, 5);
            graf.DodajKrawedz(1, 6);

            graf.DodajKrawedz(2, 3);
            graf.DodajKrawedz(2, 5);
            graf.DodajKrawedz(2, 6);
            graf.DodajKrawedz(2, 4);

            graf.DodajKrawedz(3, 6);

            graf.DodajKrawedz(3, 4);
            graf.DodajKrawedz(4, 5);
            graf.DodajKrawedz(4, 6);

            graf.DodajKrawedz(5, 6);
            graf.DodajKrawedz(5, 3);


            KolorowanieGrafu kg = new KolorowanieGrafu(graf);
            kg.Koloruj();

            Console.ReadKey();
        }
    }
}
