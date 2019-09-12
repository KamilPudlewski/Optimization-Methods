using System;

namespace AlgorytmWegierski
{
    class Program
    {
        static void Main(string[] args)
        {
            AWegierski wegierski = new AWegierski();

            int[,] macierz = { { 1, 1, 1, 1 },
                               { 1, 1, 1, 1 },
                               { 1, 1, 1, 1 },
                               { 1, 1, 1, 1 } };

            //int[,] macierz = { { 14, 5, 8, 7 },
            //                   { 2, 12, 6, 5 },
            //                   { 7, 8, 3, 9 },
            //                   { 2, 4, 6, 10 } };

            wegierski.WczytajMacierz(macierz);
            //wegierski.ZmienWagiNaPrzeciwne();
            wegierski.Wykonaj();
            Console.ReadKey();
        }
    }
}
