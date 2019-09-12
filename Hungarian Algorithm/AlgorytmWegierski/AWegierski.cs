using System;

namespace AlgorytmWegierski
{
    public class AWegierski
    {
        private int[,] macierz;

        // Odejmowanie od każdego wiersza jego minimum
        private static void Krok1(int[,] macierz)
        {
            int min = int.MaxValue;
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    if (macierz[i, j] < min)
                        min = macierz[i, j];
                }
                for (int j = 0; j < macierz.GetLength(1); j++)
                    macierz[i, j] -= min;

                min = int.MaxValue;
            }
        }

        // Odejmowanie od każdej kolumny jej minimum
        private static void Krok2(int[,] macierz)
        {
            int min = int.MaxValue;
            for (int j = 0; j < macierz.GetLength(0); j++)
            {
                for (int i = 0; i < macierz.GetLength(1); i++)
                {
                    if (macierz[i, j] < min)
                        min = macierz[i, j];
                }
                for (int i = 0; i < macierz.GetLength(1); i++)
                    macierz[i, j] -= min;

                min = int.MaxValue;
            }
        }

        // Wyznaczenie zer niezależnych
        private static int[,] ZeraNiezalezne(int[,] macierz)
        {
            int[,] zera = new int[macierz.GetLength(0), macierz.GetLength(1)];
            int[] zeraWiersze = IleZer(macierz, zera);
            // Kontynuuj skreślanie dopóki zostaną skreślone wszystkie zera
            while (LiczbaWszystkichZer(zeraWiersze) != 0)
            {
                // Powtarzanie dopóki sa zera w wierszach
                while (Jedynki(zeraWiersze) > 0)
                {
                    JednoZeroWWierszuIKolumnie(macierz, zeraWiersze, ref zera);
                    zeraWiersze = IleZer(macierz, zera);
                }
                WiecejZer(macierz, ref zeraWiersze, ref zera);
            }
            return zera;
        }

        // Rysowanie linii na podstawie zer niezależnych
        private static int[,] RysujLinie(int[,] macierz, ref int liczbaZerNiezaleznych, out int[,] zera)
        {
            liczbaZerNiezaleznych = 0;
            int[,] linie = new int[macierz.GetLength(0), macierz.GetLength(1)];
            zera = ZeraNiezalezne(macierz);
            int[] wierszeZaznaczone = new int[linie.GetLength(0)];
            int[] kolumnyZaznaczone = new int[linie.GetLength(0)];
            int licznik = int.MaxValue;

            for (int i = 0; i < wierszeZaznaczone.Length; i++)
                wierszeZaznaczone[i] = 1;
            for (int i = 0; i < zera.GetLength(0); i++)
            {
                for (int j = 0; j < zera.GetLength(0); j++)
                {
                    if (zera[i, j] == 1)
                        wierszeZaznaczone[i] = 0;
                }
            }

            while (licznik != 0)
            {
                licznik = 0;
                for (int i = 0; i < zera.GetLength(0); i++)
                {
                    for (int j = 0; j < zera.GetLength(0); j++)
                    {
                        if (wierszeZaznaczone[i] == 1 && macierz[i, j] == 0 && kolumnyZaznaczone[j] == 0)
                        {
                            kolumnyZaznaczone[j] = 1;
                            licznik++;
                        }
                    }
                }
                for (int i = 0; i < zera.GetLength(0); i++)
                {
                    for (int j = 0; j < zera.GetLength(0); j++)
                    {
                        if (kolumnyZaznaczone[j] == 1 && zera[i, j] == 1)
                            wierszeZaznaczone[i] = 1;
                    }
                }
            }
            for (int i = 0; i < zera.GetLength(0); i++)
            {
                for (int j = 0; j < zera.GetLength(0); j++)
                {
                    if (wierszeZaznaczone[i] == 1 && macierz[i, j] == 0)
                        kolumnyZaznaczone[j] = 1;
                }
            }

            // Wyznaczenie macierzy skresleń
            for (int i = 0; i < linie.GetLength(0); i++)
            {
                for (int j = 0; j < linie.GetLength(0); j++)
                {
                    if (wierszeZaznaczone[i] == 0)
                        linie[i, j]++;
                    if (kolumnyZaznaczone[j] == 1)
                        linie[i, j]++;
                }
            }

            // Podsumowanie ilości zer niezależnych
            for (int i = 0; i < wierszeZaznaczone.Length; i++)
            {
                if (wierszeZaznaczone[i] == 0)
                    liczbaZerNiezaleznych++;
                if (kolumnyZaznaczone[i] == 1)
                    liczbaZerNiezaleznych++;
            }

            return linie;
        }

        // Zwiększenie liczby zer niezależnych
        private static void Krok4(int[,] macierz, int[,] linie)
        {
            int min = int.MaxValue;
            for (int i = 0; i < linie.GetLength(0); i++)
            {
                for (int j = 0; j < linie.GetLength(1); j++)
                {
                    if (linie[i, j] == 0)
                        min = Math.Min(min, macierz[i, j]);
                }
            }
            for (int i = 0; i < linie.GetLength(0); i++)
            {
                for (int j = 0; j < linie.GetLength(1); j++)
                {
                    if (linie[i, j] == 0)
                        macierz[i, j] -= min;
                    else if (linie[i, j] == 2)
                        macierz[i, j] += min;
                }
            }
        }

        // Metoda zwracająca i wyświetlająca wynik
        private static int PoliczWynik(int[,] macierz, int[,] wynikowa)
        {
            int wynik = 0;
            string tekst = "Ostateczny wynik to: ";
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(0); j++)
                {
                    if (wynikowa[j, i] == 1)
                    {
                        wynik += (int)macierz[j, i];
                        tekst += macierz[j, i];
                        if (i == macierz.GetLength(0) - 1)
                            tekst += " = ";
                        else
                            tekst += " + ";
                    }
                }
            }
            tekst += wynik;
            Console.WriteLine(tekst + ".\n");
            return wynik;
        }

        // Wykonanie algorytmu
        private static int Wykonaj(int[,] macierzPoczatkowa)
        {
            int[,] macierz = KopiujMacierz(macierzPoczatkowa);

            int liczbaZerNiezaleznych = 0;
            int[,] noweLinieWykreslone = new int[macierz.GetLength(0), macierz.GetLength(0)];
            int[,] zera = new int[macierz.GetLength(0), macierz.GetLength(1)];

            AWegierski.Krok1(macierz);
            int poczatkowaLiczbaZerNiezaleznych = 0;
            int[,] pierwsze = RysujLinie(macierz, ref poczatkowaLiczbaZerNiezaleznych, out zera);
            if (poczatkowaLiczbaZerNiezaleznych < macierz.GetLength(0))
            {
                AWegierski.Krok2(macierz);
                poczatkowaLiczbaZerNiezaleznych = 0;
                pierwsze = RysujLinie(macierz, ref poczatkowaLiczbaZerNiezaleznych, out zera);
                if (poczatkowaLiczbaZerNiezaleznych < macierz.GetLength(0))
                {
                    while (liczbaZerNiezaleznych < macierz.GetLength(0))
                    {
                        noweLinieWykreslone = AWegierski.RysujLinie(macierz, ref liczbaZerNiezaleznych, out zera);
                        AWegierski.Krok4(macierz, noweLinieWykreslone);
                    }
                }
            }
            WyswietlMacierz(macierzPoczatkowa, ZeraNiezalezne(macierz));
            return PoliczWynik(macierzPoczatkowa, zera);
        }

        #region MetodyPomocnicze
        private static int[,] KopiujMacierz(int[,] macierzZrodlowa)
        {
            int[,] macierzSkopiowana = new int[macierzZrodlowa.GetLength(0), macierzZrodlowa.GetLength(1)];
            for (int i = 0; i < macierzZrodlowa.GetLength(0); i++)
            {
                for (int j = 0; j < macierzZrodlowa.GetLength(1); j++)
                    macierzSkopiowana[i, j] = macierzZrodlowa[i, j];
            }
            return macierzSkopiowana;
        }

        private static void WiecejZer(int[,] macierz, ref int[] zeraWiersze, ref int[,] zera)
        {
            for (int i = 0; i < zeraWiersze.Length; i++)
            {
                if (Jedynki(zeraWiersze) >= 1)
                    return;
                if (zeraWiersze[i] >= 2)
                {
                    int Zera = int.MaxValue;
                    int idx = 0;
                    for (int j = macierz.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (macierz[i, j] == 0 && zera[i, j] == 0)
                        {
                            int ileZer = 0;
                            for (int k = 0; k < macierz.GetLength(1); k++)
                            {
                                if (k != i && macierz[k, j] == 0)
                                    ileZer++;
                            }
                            if (Zera > ileZer)
                            {
                                Zera = ileZer;
                                idx = j;
                            }
                        }
                    }
                    zera[i, idx] = 1;
                    WykreslZera(macierz, ref zera, i, idx);
                }
                zeraWiersze = IleZer(macierz, zera);
            }
        }

        private static void WykreslZera(int[,] macierz, ref int[,] zera, int r, int c)
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                if (macierz[r, i] == 0 && zera[r, i] == 0)
                    zera[r, i] = 2;
                if (macierz[i, c] == 0 && zera[i, c] == 0)
                    zera[i, c] = 2;
            }
        }

        private static void JednoZeroWWierszuIKolumnie(int[,] macierz, int[] zeraWiersze, ref int[,] zera)
        {
            for (int i = 0; i < zeraWiersze.Length; i++)
            {
                if (zeraWiersze[i] == 1)
                {
                    for (int j = 0; j < macierz.GetLength(0); j++)
                    {
                        if (macierz[i, j] == 0 && zera[i, j] == 0)
                        {
                            zera[i, j] = 1;
                            for (int k = 0; k < macierz.GetLength(0); k++)
                            {
                                if (macierz[k, j] == 0 && zera[k, j] == 0)
                                {
                                    zera[k, j] = 2;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int LiczbaWszystkichZer(int[] zeraWiersze)
        {
            int liczbazer = 0;
            for (int i = 0; i < zeraWiersze.Length; i++)
                liczbazer += zeraWiersze[i];
            return liczbazer;
        }

        private static int Jedynki(int[] zeraWiersze)
        {
            int jedynki = 0;
            for (int i = 0; i < zeraWiersze.Length; i++)
            {
                if (zeraWiersze[i] == 1)
                    jedynki++;
            }
            return jedynki;
        }

        private static int[] IleZer(int[,] macierz, int[,] zera)
        {
            int[] liczbaZer = new int[macierz.GetLength(0)];
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    if (macierz[i, j] == 0 && zera[i, j] == 0)
                        liczbaZer[i]++;
                }
            }
            return liczbaZer;
        }
        #endregion

        #region Wyswietlanie
        private static void WyswietlMacierz(int[,] macierz, int[,] zera)
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    if (zera[i, j] == 1)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;

                    if (macierz[i, j] < 10)
                        Console.Write(macierz[i, j] + "     ");

                    if (macierz[i, j] >= 10 && macierz[i, j] < 100)
                        Console.Write(macierz[i, j] + "    ");

                    if (macierz[i, j] >= 100 && macierz[i, j] < 1000)
                        Console.Write(macierz[i, j] + "   ");

                    if (macierz[i, j] >= 1000)
                        Console.Write(macierz[i, j] + "  ");

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion

        public void WczytajMacierz(int[,] wczytanaMacierz)
        {
            macierz = wczytanaMacierz;
        }

        public void ZmienWagiNaPrzeciwne()
        {
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    macierz[i, j] = -macierz[i, j];
                }
            }
        }

        public void Wykonaj()
        {
            Wykonaj(macierz);
        }
    }
}
