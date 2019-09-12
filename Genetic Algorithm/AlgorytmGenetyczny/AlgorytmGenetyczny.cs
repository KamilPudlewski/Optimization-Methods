using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorytmGenetyczny
{
    public class Hromosom : IComparable
    {
        public int ocena;
        public int[] hromosom;

        public Hromosom(int wielkosc)
        {
            hromosom = new int[wielkosc];
            for (int i = 0; i < wielkosc; i++)
            {
                hromosom[i] = -1;
            }
        }

        public int CompareTo(object obj)
        {
            if ((obj as Hromosom).ocena > this.ocena)
                return -1;
            else
            if ((obj as Hromosom).ocena < this.ocena)
                return 1;
            else
                return 0;
        }
    }

    class AlgorytmGenetyczny
    {
        double[][] macierz;
        List<Hromosom> hromosomy;
        private int licznikPokolen = 0;
        public int liczbaPokolen = 10000;
        public int liczbaPopulacji = 100;
        public int procentSelekcja = 10; // Procent najlepszych hromosomów w selekcji rankingowej
        public int prawdopodobienstwoMutacji = 20; // Procent prawdopodobienstwa zajscia mutacji

        public AlgorytmGenetyczny()
        {
            
        }

        public void WczytajMacierz(double[][] nowaMacierz)
        {
            macierz = nowaMacierz;
        }

        private bool ZnajdzWystapienieHromosomu(List<Hromosom> hromosomy, Hromosom hrom)
        {
            bool status = false;
            foreach (Hromosom el in hromosomy)
            {
                if (el.hromosom.SequenceEqual(hrom.hromosom))
                {    
                    status = true;
                }
            }
            return status;
        }

        public void WykonajAlgorytm()
        {
            hromosomy = GenerujHromosomy(liczbaPopulacji);
            Ocena(hromosomy);
            hromosomy.Sort();

            Console.WriteLine("Poczatkowa populacja skladajaca sie z " + liczbaPopulacji + " osobnikow:");
            WyswietlPopulacje();

            do
            {
                List<Hromosom> noweHromosomy = SelekcjaRankingowa(ref hromosomy);
                SelekcjaKrzyżowanie(ref noweHromosomy);

                hromosomy = noweHromosomy;
                Ocena(hromosomy);
                hromosomy.Sort();

                // Wyswietlanie postepu pokolen
                if (licznikPokolen % 1000 == 0)
                {
                    Console.WriteLine("Pokolenie numer: " + licznikPokolen);
                }

                licznikPokolen++;
            } while (WarunekSTOP());

            Console.WriteLine();
            Console.WriteLine("Koncowa populacja pokolenia numer " + liczbaPokolen + ":");
            WyswietlPopulacje();
            

            Console.WriteLine("Najlepszy wynik z kosztem " + hromosomy[0].ocena + " osiagnieto dla: ");

            for (int i = 0; i < macierz.Length; i++)
            {
                Console.Write(hromosomy[0].hromosom[i]);
                if (i != macierz.Length - 1)
                {
                    Console.Write("->");
                }
            }
        }

        public void WyswietlPopulacje()
        {
            foreach (Hromosom el in hromosomy)
            {
                Console.WriteLine("Wynik: " + el.ocena.ToString());
                for (int i = 0; i < macierz.Length; i++)
                {
                    Console.Write(el.hromosom[i]);
                    if (i != macierz.Length - 1)
                    {
                        Console.Write("->");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool WarunekSTOP()
        {
            if (licznikPokolen > liczbaPokolen)
            {
                return false;
            }
            //else if (hromosomy[0].ocena < 800) // Dodatkowy warunek stopu sprawdzajazy dotychczasowy wynik
            //{
            //    return false;
            //}
            else
            {
                return true;
            }
        }

        private List<Hromosom> SelekcjaRankingowa(ref List<Hromosom> hromosomy)
        {
            int liczbaWybranychHromosomow = (int)(hromosomy.Count * (procentSelekcja * 0.01));
            List<Hromosom> wynik = new List<Hromosom>();
            for (int i = 0; i <= liczbaWybranychHromosomow; i++)
            {
                wynik.Add(new Hromosom(macierz.Length) { hromosom = (int[])hromosomy[i].hromosom.Clone() });
            }
            return wynik;
        }

        private void SelekcjaKrzyżowanie(ref List<Hromosom> hromosomy)
        {
            Random rand = new Random();
            int pozycjaA, pozycjaB, mutacja;
            while (hromosomy.Count < liczbaPopulacji)
            {
                pozycjaA = rand.Next(0, hromosomy.Count - 1);
                pozycjaB = rand.Next(0, hromosomy.Count - 1);
                mutacja = rand.Next(0, 100);

                List<Hromosom> krzyzowka = KrzyżowaniePar(hromosomy[pozycjaA], hromosomy[pozycjaB]);
                if (mutacja < prawdopodobienstwoMutacji)
                {
                    Mutacja(ref hromosomy);
                }

                if(!ZnajdzWystapienieHromosomu(hromosomy, krzyzowka[0]))
                {
                    hromosomy.Add(krzyzowka[0]);
                }

                if (!ZnajdzWystapienieHromosomu(hromosomy, krzyzowka[1]))
                {
                    hromosomy.Add(krzyzowka[1]);
                }
            }
        }

        private void Mutacja(ref List<Hromosom> hromosomy)
        {
            Random random = new Random();
            int losowyHromosom = random.Next(0, hromosomy.Count - 1);
            int miejsceA = random.Next(0, macierz.Length);
            int miejsceB = random.Next(0, macierz.Length);

            int tmp = hromosomy[losowyHromosom].hromosom[miejsceA];
            hromosomy[losowyHromosom].hromosom[miejsceA] = hromosomy[losowyHromosom].hromosom[miejsceB];
            hromosomy[losowyHromosom].hromosom[miejsceB] = tmp;
        }

        private List<Hromosom> KrzyżowaniePar(Hromosom a, Hromosom b)
        {
            Random random = new Random();
            int pozycjaA = random.Next(0, macierz.Length - 1);
            int pozycjaB = random.Next(0, macierz.Length - 1);

            if (pozycjaA > pozycjaB)
            {
                int tmp = pozycjaA;
                pozycjaA = pozycjaB;
                pozycjaB = tmp;
            }
            List<Hromosom> noweHomosomyAB = new List<Hromosom>();
            Hromosom nowyA = new Hromosom(macierz.Length);
            Hromosom nowyB = new Hromosom(macierz.Length);

            for (int i = pozycjaA; i <= pozycjaB; i++)
            {
                nowyA.hromosom[i] = a.hromosom[i];
                nowyB.hromosom[i] = b.hromosom[i];
            }

            // Tworzenie osobnika nowyA z b
            for (int i = 0; i < macierz.Length; i++)
            {
                int pozycja = -1;
                bool wystapienieB = false;
                for (int j = 0; j < macierz.Length; j++)
                {
                    if (nowyA.hromosom[j] == -1 && pozycja == -1)
                    {
                        pozycja = j;
                    }
                    if (nowyA.hromosom[j] == b.hromosom[i])
                    {
                        wystapienieB = true;
                        break;
                    }
                }
                if (!wystapienieB)
                {
                    nowyA.hromosom[pozycja] = b.hromosom[i];
                }
            }


            // Tworzenie osobnika nowyB z a
            for (int i = 0; i < macierz.Length; i++)
            {
                int pozycja = -1;
                bool wystapienieA = false;
                for (int j = 0; j < macierz.Length; j++)
                {
                    if (nowyB.hromosom[j] == -1 && pozycja == -1)
                    {
                        pozycja = j;
                    }
                    if (nowyB.hromosom[j] == a.hromosom[i])
                    {
                        wystapienieA = true;
                        break;
                    }
                }
                if (!wystapienieA)
                {
                    nowyB.hromosom[pozycja] = a.hromosom[i];
                }
            }

            noweHomosomyAB.Add(nowyA);
            noweHomosomyAB.Add(nowyB);
            return noweHomosomyAB;
        }

        private void Ocena(List<Hromosom> hromosom)
        {
            for (int j = 0; j < hromosom.Count; j++)
            {
                hromosom[j].ocena = 0;
                for (int i = 0; i < macierz.Length - 1; i++)
                {
                    hromosom[j].ocena += (int)macierz[hromosom[j].hromosom[i]][hromosom[j].hromosom[i + 1]];
                }
                hromosom[j].ocena += (int)macierz[hromosom[j].hromosom[0]][hromosom[j].hromosom[macierz.Length - 1]];
            }
        }

        private List<Hromosom> GenerujHromosomy(int n)
        {
            Random random = new Random();
            List<Hromosom> hromosomy = new List<Hromosom>();

            int[] tab;
            while (true)
            {
                tab = new int[macierz.Length];
                for (int i = 0; i < macierz.Length; i++)
                {
                    tab[i] = -1;
                }
                int pozycja = 0;

                while (true)
                {
                    int wartosc = random.Next(0, macierz.Length);

                    if (pozycja >= 0 && pozycja < macierz.Length && tab[pozycja] == -1 && wartosc < macierz.Length)
                    {
                        if (tab.All(x => x != wartosc))
                        {
                            tab[pozycja] = wartosc;
                            pozycja++;
                        }
                    }

                    if (tab.All(x => x != -1))
                    {
                        break;
                    }
                }

                if (!ZnajdzWystapienieHromosomu(hromosomy, new Hromosom(macierz.Length) { ocena = -1, hromosom = (int[])tab.Clone() }))
                {
                    hromosomy.Add(new Hromosom(macierz.Length) { ocena = -1, hromosom = (int[])tab.Clone() });
                }

                if (hromosomy.Count == n)
                {
                    break;
                }
            }
            return hromosomy;
        }
    }
}