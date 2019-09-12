using System;
using System.Collections.Generic;

namespace Grafy
{
    public class GrafN : IGraf
    {
        public GrafN()
        {

        }

        public void DodajWierzcholek(int i)
        {
            bool istniejeW = listaWierzcholkow.Exists((Wierzcholek w) => { return (w.id == i) ? true : false; });

            if (istniejeW)
            {

                Console.WriteLine("Błąd! Wierzcholek " + i + " juz istnieje w grafie nieskierowanym!");
            }
            else
            {
                listaWierzcholkow.Add(new Wierzcholek(i));
            }
        }

        public void DodajKrawedz(int w1, int w2)
        {
            bool istniejeW1 = listaWierzcholkow.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2 = listaWierzcholkow.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            if (istniejeW1 && istniejeW2)
            {
                listaKrawedzi.Add(new Krawedz(w1, w2));
            }
            else
            {
                Console.WriteLine("Nieprawidlowy wierzcholek!");
            }
        }

        #region Sasiedztwa
        public void ZbudujListeSasiedztwa()
        {
            int tmpNajwiekszy = 0;
            for (int i = 0; i < listaWierzcholkow.Count; i++)
            {
                if (listaWierzcholkow[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaWierzcholkow[i].id;
                }
            }

            for (int i = 0; i < tmpNajwiekszy + 1; i++)
            {
                List<int> row = new List<int>();
                IEnumerable<Krawedz> dopasowanie = listaKrawedzi.FindAll(el => el.w1 == i);
                foreach (Krawedz k in dopasowanie)
                {
                    row.Add(k.w2);
                }
                listaSasiedztwa.Add(row);
            }
        }

        public void ZbudujMacierzSasiedztwa()
        {
            int tmpNajwiekszy = 0;
            for (int i = 0; i < listaWierzcholkow.Count; i++)
            {
                if (listaWierzcholkow[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaWierzcholkow[i].id;
                }
            }

            for (int i = 0; i < tmpNajwiekszy + 1; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < tmpNajwiekszy + 1; j++)
                {
                    row.Add(0);
                }
                macierzSasiedztwa.Add(row);
            }

            foreach (Krawedz k in listaKrawedzi)
            {
                macierzSasiedztwa[k.w1][k.w2] = 1;
            } 
        }
        #endregion

        #region Wyswietlanie
        public void WyswietlWierzcholki()
        {
            if (listaWierzcholkow.Count == 0)
            {
                Console.WriteLine("Lista wierzcholkow jest pusta!");
                return;
            }

            Console.Write("Lista wierzchołków to: ");
            for (int i = 0; i < listaWierzcholkow.Count; i++)
            {
                if (i != listaWierzcholkow.Count - 1)
                {
                    Console.Write(listaWierzcholkow[i].id + " , ");
                }
                else
                {
                    Console.WriteLine(listaWierzcholkow[i].id);
                }
            }
            Console.WriteLine();
        }

        public void WyswietlListeSasiedztwa()
        {
            for (int i = 0; i < listaWierzcholkow.Count; i++)
            {
                Console.Write("Wierzcholek " + listaWierzcholkow[i].id + " : ");
                IEnumerable<Krawedz> dopasowanie = listaKrawedzi.FindAll(el => el.w1 == listaWierzcholkow[i].id);
                foreach (Krawedz k in dopasowanie)
                {
                    Console.Write(k.w2 + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void WyswietlMacierzSasiedztwa()
        {
            Console.Write("  ");
            foreach (Wierzcholek w in listaWierzcholkow)
            {
                Console.Write(w.id + " ");
            }
            Console.WriteLine();

            bool jestNaLiscieWierzcholkow = false;
            List<int> listaWieszcholkowNiestworzonych = new List<int>();


            for (int i = 0; i < macierzSasiedztwa.Capacity; i++)
            {
                for (int lw = 0; lw < listaWierzcholkow.Count; lw++)
                {
                    if (listaWierzcholkow[lw].id == i)
                    {
                        jestNaLiscieWierzcholkow = true;
                    }
                }

                if (!jestNaLiscieWierzcholkow)
                {
                    listaWieszcholkowNiestworzonych.Add(i);
                }
                jestNaLiscieWierzcholkow = false;
            }

            for (int i = 0; i < macierzSasiedztwa.Capacity; i++)
            {
                if (!listaWieszcholkowNiestworzonych.Exists((int w) => { return (w == i) ? true : false; }))
                {
                    Console.Write(i + " ");
                    for (int j = 0; j < macierzSasiedztwa[i].Capacity; j++)
                    {
                        if (!listaWieszcholkowNiestworzonych.Exists((int w) => { return (w == j) ? true : false; }))
                        {
                            Console.Write(macierzSasiedztwa[i][j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        #endregion

        #region Zwracanie
        public int ZwrocLiczbeWierzcholkow()
        {
            return listaWierzcholkow.Count;
        }

        public int ZwrocLiczbeKrawedzi()
        {
            return listaKrawedzi.Count;
        }

        #endregion
    }
}
