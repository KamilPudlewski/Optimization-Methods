using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorytmPlecakowy
{
    class AlgorytmPlecakowy
    {
        private List<int> koszt;
        private List<int> waga;
        private int ograniczenie = 0;
        private List<int> K;
        private int N = 0;

        public AlgorytmPlecakowy(List<int> koszt, List<int> waga, int ograniczenie)
        {
            if (ograniczenie < 0)
            {
                throw new ArgumentException("Ograniczenie nie moze byc ujemne!");
            }

            if (koszt == null)
            {
                throw new ArgumentException("Tablica kosztow jest pusta!");
            }

            if (waga == null)
            {
                throw new ArgumentException("Tablica wag jest pusta!");
            }

            if (koszt.Count != waga.Count)
            {
                throw new ArgumentException("Wielkosc list kosztow i wag jest rozna!");
            }


            this.koszt = koszt;
            this.waga = waga;
            this.ograniczenie = ograniczenie;
            this.N = koszt.Count;

            K = new List<int>();
        }

        public void UstawPrzedmiotKtoryNieMaBycWLozony(int przedmiot)
        {
            if (przedmiot < 0)
            {
                throw new ArgumentException("Ograniczenie nie moze byc ujemne!");
            }

            if (przedmiot > N)
            {
                throw new ArgumentException("Nie istnieje podany przedmiot!");
            }

            K.Add(przedmiot);
        }

        public void WyswietlWszystkiePrzedmioty()
        {
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Przedmiot numer: " + i + " kosztuje " + koszt[i] + " waga " + waga[i]);
            }
            Console.WriteLine();
        }

        public void SortujWedlugWartoscuIlorazuWartosciWag()
        {
            List<Tuple<int, double>> tmp = new List<Tuple<int, double>>();

            for (int i = 0; i < N; ++i)
            {
                double iloraz = (double)koszt[i] / waga[i];
                tmp.Add(Tuple.Create(i, iloraz));

            }

            // Sortowanie po ilorazach
            tmp.Sort((x, y) => Comparer<double>.Default.Compare(y.Item2, x.Item2));

            List<int> tmpKoszt = new List<int>();
            List<int> tmpWaga = new List<int>();

            for (int i = 0; i < N; ++i)
            {
                tmpKoszt.Add(koszt[tmp[i].Item1]);
                tmpWaga.Add(waga[tmp[i].Item1]);
            }

            koszt = tmpKoszt;
            waga = tmpWaga;
        }

        bool ListaZawiera(List<int> listaPrzedmiotow, List<int> K)
        {
            if (listaPrzedmiotow.Count == 0) return false;
            for (int i = 0; i < listaPrzedmiotow.Count; i++)
            {
                for (int j = 0; j < K.Count; j++)
                {
                    if (listaPrzedmiotow[i] == K[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //----------------- Generowanie kombinacji ----------------------------------
        public IEnumerable<IEnumerable<T>> CombinationsOfK<T>(T[] data, int k)
        {
            int size = data.Length;

            IEnumerable<IEnumerable<T>> Runner(IEnumerable<T> list, int n)
            {
                int skip = 1;
                foreach (var headList in list.Take(size - k + 1).Select(h => new T[] { h }))
                {
                    if (n == 1)
                        yield return headList;
                    else
                    {
                        foreach (var tailList in Runner(list.Skip(skip), n - 1))
                        {
                            yield return headList.Concat(tailList);
                        }
                        skip++;
                    }
                }
            }
            return Runner(data, k);
        }

        private void WyswietlListe(List<int> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Console.Write(lista[i] + "  ");
            }
            Console.WriteLine();
        }

        private void WyswietlListeList(List<List<int>> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista[0].Count; j++)
                {
                    Console.Write(lista[i][j] + "  ");
                }
                Console.WriteLine();
            }
        }

        private List<int> Kopiuj(List<int> permutacja)
        {
            List<int> kopia = new List<int>();
            foreach (int el in permutacja)
            {
                kopia.Add(el);
            }
            return kopia;
        }

        private void SprawdzOgraniczenie(ref List<List<int>> listaPermutacji)
        {
            int sumaWag = 0;
            List<List<int>> listaPermutacjiSpelniajacaOgraniczenie = new List<List<int>>();

            for (int i = 0; i < listaPermutacji.Count; i++)
            {
                for (int j = 0; j < listaPermutacji[0].Count; j++)
                {
                    sumaWag += waga[listaPermutacji[i][j]];
                }

                if(sumaWag <= ograniczenie)
                {
                    listaPermutacjiSpelniajacaOgraniczenie.Add(Kopiuj(listaPermutacji[i]));
                }
                sumaWag = 0;
            }
            
            listaPermutacji = listaPermutacjiSpelniajacaOgraniczenie;
        }

        private void WyeliminujPrzedmiotyK(ref List<List<int>> listaPermutacji)
        {
            List<List<int>> listaPermutacjiBezPezwdmiotowK = new List<List<int>>();
            for (int i = 0; i < listaPermutacji.Count; i++)
            {
                if (!ListaZawiera(listaPermutacji[i], K))
                {
                    listaPermutacjiBezPezwdmiotowK.Add(listaPermutacji[i]);
                }
            }

            listaPermutacji = listaPermutacjiBezPezwdmiotowK;
        }

        private void Lowerbound(List<List<int>> listaPermutacji, ref int Q, List<List<int>> lowerbound2)
        {
            int sumaKosztow = 0;

            for (int i = 0; i < listaPermutacji.Count; i++)
            {
                for (int j = 0; j < listaPermutacji[0].Count; j++)
                {
                    sumaKosztow += koszt[listaPermutacji[i][j]];
                }

                if (sumaKosztow == Q)
                {
                    lowerbound2.Add(listaPermutacji[i]);
                }

                if (sumaKosztow > Q)
                {
                    Q = sumaKosztow;
                    lowerbound2.Clear();
                    lowerbound2.Add(listaPermutacji[i]);
                }
                sumaKosztow = 0;
            }
        }

        private void WyswietlWyniki(List<List<int>> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("Nie znaleziono wyniku");
            }
            else if (lista.Count == 1)
            {
                Console.WriteLine("Znaleziono " + lista.Count + " wynik");
            }
            else
            {
                Console.WriteLine("Znaleziono " + lista.Count + " wynikow");
            }
            Console.WriteLine();

            int rozmiar = 0;

            foreach (List<int> el in lista)
            {
                if (el.Count > rozmiar)
                {
                    rozmiar = el.Count;
                }
            }

            int[] tablicaPrzemiotow = new int[rozmiar];
            int[] tablicaKosztow = new int[rozmiar];
            int[] tablicaWag = new int[rozmiar];
            int sumaWag = 0;

            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine("---------- Rozwiazanie " + i + " ----------");

                for (int j = 0; j < lista[i].Count; j++)
                {
                    tablicaPrzemiotow[j] = lista[i][j];
                    tablicaKosztow[j] = koszt[lista[i][j]];
                    tablicaWag[j] = waga[lista[i][j]];
                    sumaWag += waga[lista[i][j]];
                }


                if (lista[i].Count == 1)
                {
                    Console.Write("Przedmiot: ");
                }
                else
                {
                    Console.Write("Przedmioty: ");
                }
                for (int j = 0; j < lista[i].Count; j++)
                {
                    if (j == lista[i].Count - 1)
                    {
                        Console.Write(tablicaPrzemiotow[j] + "  ");
                    }
                    else
                    {
                        Console.Write(tablicaPrzemiotow[j] + ",  ");
                    }
                }
                Console.WriteLine();

                Console.Write("Koszt: ");
                for (int j = 0; j < lista[i].Count; j++)
                {
                    if (j == lista[i].Count - 1)
                    {
                        Console.Write(tablicaKosztow[j] + "  ");
                    }
                    else
                    {
                        Console.Write(tablicaKosztow[j] + ",  ");
                    }
                }
                Console.WriteLine();

                Console.Write("Waga: ");
                for (int j = 0; j < lista[i].Count; j++)
                {
                    if (j == lista[i].Count - 1)
                    {
                        Console.Write(tablicaWag[j] + "  ");
                    }
                    else
                    {
                        Console.Write(tablicaWag[j] + ",  ");
                    }
                }
                Console.WriteLine();

                Console.WriteLine("Suma wag: " + sumaWag);
                Console.WriteLine();

                sumaWag = 0;
            }
        }

        private void WyswietlWagePoIndeksach(List<List<int>> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < lista[0].Count; j++)
                {
                    Console.Write(waga[lista[i][j]] + "  ");
                }
                Console.WriteLine();
            }
        }

        public void WykonajAlgorytmPlecakowy()
        {
            Console.WriteLine("Wszystkie przedmioty:");
            WyswietlWszystkiePrzedmioty();

            int Q = 0;
            List<List<int>> loweboundList = new List<List<int>>();

            // K iteracji
            for (int k = 1; k <= N; k++)
            {
                Console.WriteLine("Rozpoczeto algorytm plecakowy iteracja numer " + k);
                int[] data = Enumerable.Range(0, N).ToArray();

                List<List<int>> listaKombinacji = new List<List<int>>();
                List<int> tmp = new List<int>();

                foreach (string comb in CombinationsOfK(data, k).Select(c => string.Join(" ", c)))
                {
                    List<int> kombinacja = comb.Split(' ').Select(Int32.Parse).ToList();
                    listaKombinacji.Add(kombinacja);
                }
                WyswietlListeList(listaKombinacji);
                Console.WriteLine();


                Console.WriteLine("--------------- DO PLECAKA ZMIESCI SIE -------------------");       
                SprawdzOgraniczenie(ref listaKombinacji);
                WyeliminujPrzedmiotyK(ref listaKombinacji);
                WyswietlListeList(listaKombinacji);
                if (listaKombinacji.Count == 0)
                {
                    Console.WriteLine("Zadna kombinacja nie spelnia warunkow!");
                }
                Console.WriteLine();
                

                Console.WriteLine("--------------- WYNIK -------------------");
                Lowerbound(listaKombinacji, ref Q, loweboundList);
                Console.WriteLine("Koszt Q: " + Q );
                WyswietlWyniki(loweboundList);

                Console.WriteLine();
            }
        }
    }
}
