using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KolorowanieGrafu
{
    public class Graf
    {
        private int[][] matrix;
        private int tabLen;

        public Graf(int iloscWierzcholkow)
        {
            tabLen = iloscWierzcholkow + 1;
            matrix = new int[tabLen][];
            for (int i = 0; i < tabLen; i++)
            {
                matrix[i] = new int[tabLen];
            }
        }

        public void DodajKrawedz(int wierzcholek1, int wierzcholek2)
        {
            if (wierzcholek1 == 0 || wierzcholek2 == 0)
            {
                throw new ArgumentException("Poczatkowy wierzcholek grafu oznaczany jest indeksem 1, a nie 0!");
            }

            if (wierzcholek1 < 0 || wierzcholek2 < 0)
            {
                throw new ArgumentException("Wierzcholek nie moze byc ujemny!");
            }

            if (wierzcholek1 > tabLen || wierzcholek2 > tabLen)
            {
                throw new ArgumentException("Wierzcholek jest wiekszy od zainicjowanego grafu!");
            }

            matrix[wierzcholek1][wierzcholek2] = 1;
            matrix[wierzcholek2][wierzcholek1] = 1;
        }

        public int[][] ZwrocMacierzGrafu()
        {
            return matrix;
        }
    }


    public class KolorowanieGrafu
    {
        List<int> listColors = new List<int>();
        List<int> listUsingColors = new List<int>();
        List<int[]> listWynik = new List<int[]>();
        List<int>[] U;
        int[] VColor;
        int[] l_k;
        int[][] matrix;
        int tabLen = 0;

        public KolorowanieGrafu(Graf graf)
        {
            matrix = graf.ZwrocMacierzGrafu();
            tabLen = matrix.Length;

            U = new List<int>[tabLen];
            VColor = new int[tabLen];
            l_k = new int[tabLen];
        }

        public void Koloruj()
        {
            for (int i = 0; i < tabLen; i++)
            {
                l_k[i] = int.MinValue;
                U[i] = new List<int>();
                listColors.Add(i);
                VColor[i] = int.MinValue;
            }
            listColors.Add(tabLen);

            AlgorytmZNawrotami(tabLen - 1);
            WyswietlWynik();
        }

        List<int> Kopiuj(List<int> listToClone)
        {
            return listToClone.Select((item) => item).ToList();
        }

        List<int> UK(int k, int l)
        {
            List<int> usingVColors = new List<int>();

            // Pozyskiwanie informacji o uzytych kolorach sasiadow wierzcholka k
            for (int i = 1; i < tabLen; i++)
            {
                if (matrix[k][i] == 1)
                {
                    if (VColor[i] != int.MinValue)
                    {
                        usingVColors.Add(VColor[i]);
                    }
                }
            }

            // Sprawdzanie jakie kolory moga byc uzyte z listy wszystkich kolorow ktorych nie posiadaja sasiedzi
            List<int> j = listUsingColors.Where(p => !usingVColors.Any((p2) => p2 == p)).ToList();
            j.Remove(0);

            // Jezeli nie znaleziono zadnego mozliwego do uzycia koloru
            if (j.Count == 0)
            {
                if (listUsingColors.Max() + 1 <= l)
                {
                    return new List<int>() { listUsingColors.Max() + 1 };
                }
            }
            // Jezeli istnieje przynajmniej jeden kolor ktory mozna uzyc
            else
            {
                if (listUsingColors.Max() + 1 <= l)
                {
                    j.Add(listUsingColors.Max() + 1);
                }
                return j.Where((x) => x <= l).ToArray().ToList();
            }

            return new List<int>();
        }

        void f(int v, int kolor)
        {
            Console.WriteLine("Pokolorowano wierzcholek " + v + " kolorem " + kolor);
            VColor[v] = kolor;
            if (!listUsingColors.Contains(kolor))
            {
                listUsingColors.Add(kolor);
            }
        }

        int znajdzf(int l)
        {
            int minIndex = int.MaxValue;
            int minColor = int.MaxValue;

            for (int i = 1; i < tabLen; i++)
            {
                if (VColor[i] >= l && minColor > VColor[i] && minIndex > i)
                {
                    minColor = VColor[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }

        void AlgorytmZNawrotami(int n)
        {
            int l = 1;
            int k = 1;
            l_k[k] = l;
            f(1, 1);
            int q = n + 1;
            int lowerbound = int.MaxValue;
            bool increase = true;

            do
            {
                if (k == 0)
                    break;

                if (increase)
                {
                    l_k[k] = l;
                    k = k + 1;
                    U[k] = UK(k, l + 1);
                }

                if (U[k].Count == 0)
                {
                    k = k - 1;
                    l = l_k[k];
                    increase = false;
                }
                else
                {
                    // Kolorowanie wierzcholka
                    int j = 0;
                    j = U[k].Min();
                    U[k].Remove(j);
                    f(k, j);

                    if (j > l)
                    {
                        l = l + 1;
                    }

                    if (k < n)
                    {
                        increase = true;
                    }
                    else
                    {
                        // Zapamietywanie aktualnego rozwiazania
                        if (listUsingColors.Count < lowerbound)
                        {
                            lowerbound = listUsingColors.Count;
                        }
                        listWynik.Add((int[])VColor.Clone());

                        
                        // Znalezienie wierzcholka pokolorowanego maksymalnym kolorem l w danej iteracji
                        int i = znajdzf(l);

                        // Cofniecie i usuniecie kolorzu ze zbiorow U
                        for (int ind = 1; ind <= i - 1; ind++)
                        {
                            for (int jnd = l; jnd <= q - 1; jnd++)
                            {
                                U[ind].Remove(jnd);
                            }
                        }
                        q = l;
                        l = q - 1;
                        k = i - 1;
                        increase = false;
                    }
                }
            } while (((k != 1) || (q != lowerbound))); // Koniec w momencie gdy cofniemy sie do pierwszego elementu lub q bedzie rowne lowerbound
        }

        private void WyswietlUMozliweKolory(int k)
        {
            Console.Write("U_" + (k).ToString() + "=" + '{');
            for (int i = 0; i < U[k].Count; i++)
            {
                if (i != U[k].Count - 1)
                {
                    Console.Write((U[k][i]).ToString() + ',');
                }
                else
                {
                    Console.Write((U[k][i]).ToString());
                }
            }
            Console.WriteLine("}");
        }

        private void WyswietlWynik()
        {
            Console.WriteLine();
            Console.WriteLine("Uzyskany wynik:");
            for (int i = 0; i < listWynik.Count(); i++)
            {
                for (int j = 1; j < listWynik[0].Length; j++)
                {
                    Console.WriteLine("Pokolorowano wierzcholek " + j + " kolorem " + listWynik[i][j]);
                }
            }
        }
    }
}
