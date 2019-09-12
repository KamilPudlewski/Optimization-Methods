using System;
using System.Collections.Generic;

namespace AlgorytmPlecakowy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Algorytm plecakowy:");

            //List<int> koszt = new List<int> { 120, 120, 10, 120, 120 };
            //List<int> waga = new List<int> { 9, 10, 7, 10, 10 };

            //List<int> koszt = new List<int> { 5, 5, 5, 5, 20 };
            //List<int> waga = new List<int> { 5, 5, 5, 5, 20 };

            List<int> koszt = new List<int> { 12, 5, 10, 8, 3, 4, 2, 1, 20 };
            List<int> waga = new List<int> { 8, 10, 7, 6, 3, 8, 6, 5, 15 };

            //List<int> koszt = new List<int> { 100, 1, 1, 0, 1, 1 };
            //List<int> waga = new List<int> { 1, 10, 10, 0, 10, 10 };

            //List<int> kosztTab = new List<int> { 1, 1, 1, 1, 1, 1 };
            //List<int> wagiTab = new List<int> { 20, 20, 20, 20, 20, 20 };
            int ograniczenie = 20;

            AlgorytmPlecakowy plecak = new AlgorytmPlecakowy(koszt, waga, ograniczenie);
            //plecak.SortujWedlugWartoscuIlorazuWartosciWag();
            //plecak.UstawPrzedmiotKtoryNieMaBycWLozony(4);
            plecak.WykonajAlgorytmPlecakowy();
            Console.ReadKey();
        }
    }
}
