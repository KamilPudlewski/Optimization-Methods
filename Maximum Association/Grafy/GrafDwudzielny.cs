using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafy
{
    public class GrafDwudzielny : IGraf
    {
        public List<Wierzcholek> listaV1 = new List<Wierzcholek>();
        public List<Wierzcholek> listaV2 = new List<Wierzcholek>();
        public List<Wierzcholek> listaV1Wolne = new List<Wierzcholek>();
        public List<Wierzcholek> listaV2Wolne = new List<Wierzcholek>();
        List<Krawedz> listaKrawedziSkojarzonych = new List<Krawedz>();

        public GrafDwudzielny()
        {

        }

        public void DodajWierzcholek(int i, int v)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == i) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == i) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == i) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == i) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                Console.WriteLine("Błąd! Wierzcholek " + i + " juz istnieje w grafie!");
            }
            else
            {
                if (v == 1)
                {
                    listaWierzcholkow.Add(new Wierzcholek(i));
                    listaV1.Add(new Wierzcholek(i));
                    listaV1Wolne.Add(new Wierzcholek(i));
                }
                else if (v == 2)
                {
                    listaWierzcholkow.Add(new Wierzcholek(i));
                    listaV2.Add(new Wierzcholek(i));
                    listaV2Wolne.Add(new Wierzcholek(i));
                }
                else
                {
                    Console.WriteLine("Wartość " + v + " jest niepoprawna dla grafu dwudzielnego!");
                }
            }
        }

        public void DodajWierzcholek(Wierzcholek wierzcholek, int v)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == wierzcholek.id) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == wierzcholek.id) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == wierzcholek.id) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == wierzcholek.id) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                Console.WriteLine("Błąd! Wierzcholek " + wierzcholek.id + " juz istnieje w grafie!");
            }
            else
            {
                if (v == 1)
                {
                    listaWierzcholkow.Add(wierzcholek);
                    listaV1.Add(wierzcholek);
                    listaV1Wolne.Add(wierzcholek);
                }
                else if (v == 2)
                {
                    listaWierzcholkow.Add(wierzcholek);
                    listaV2.Add(wierzcholek);
                    listaV2Wolne.Add(wierzcholek);
                }
                else
                {
                    Console.WriteLine("Wartość " + v + " jest niepoprawna dla grafu dwudzielnego!");
                }
            }
        }

        public void DodajWierzholek(IEnumerable<Wierzcholek> zbior_wierzholkow)
        {
            if (zbior_wierzholkow == null) throw new ArgumentNullException();
            using (var iterator = zbior_wierzholkow.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    if (iterator.Current != null && !listaWierzcholkow.Contains(iterator.Current))
                    {
                        listaWierzcholkow.Add(iterator.Current);
                    }
                }
            }
        }

        public void DodajKrawedz(int w1, int w2)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                listaKrawedzi.Add(new Krawedz(w1, w2));
            }
            else
            {
                throw new ArgumentException("Nie istnieje podany wierzcholek!");
            }
        }

        public void DodajKrawedz(int w1, int w2, int waga)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                Krawedz tmpKrawedz = new Krawedz(w1, w2);
                tmpKrawedz.UstawWage(waga);
                listaKrawedzi.Add(tmpKrawedz);
            }
            else
            {
                throw new ArgumentException("Nie istnieje podany wierzcholek!");
            }
        }

        public void DodajKrawedz(Krawedz krawedz)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == krawedz.w1) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == krawedz.w2) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == krawedz.w1) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == krawedz.w2) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                listaKrawedzi.Add(krawedz);
            }
            else
            {
                throw new ArgumentException("Nie istnieje podany wierzcholek!");
            }
        }

        public void DodajKrawedzN(int w1, int w2)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == w1) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == w2) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                listaKrawedzi.Add(new Krawedz(w1, w2));
                listaKrawedzi.Add(new Krawedz(w2, w1));
            }
            else
            {
                throw new ArgumentException("Nie istnieje podany wierzcholek!");
            }
        }

        public void DodajKrawedzN(Krawedz krawedz)
        {
            bool istniejeW1V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == krawedz.w1) ? true : false; });
            bool istniejeW2V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == krawedz.w2) ? true : false; });

            bool istniejeW1V2 = listaV2.Exists((Wierzcholek w) => { return (w.id == krawedz.w1) ? true : false; });
            bool istniejeW2V1 = listaV1.Exists((Wierzcholek w) => { return (w.id == krawedz.w2) ? true : false; });

            if ((istniejeW1V1 && istniejeW2V2) || (istniejeW1V2 && istniejeW2V1))
            {
                listaKrawedzi.Add(krawedz);
                listaKrawedzi.Add(new Krawedz(krawedz.w2, krawedz.w1));
            }
            else
            {
                throw new ArgumentException("Nie istnieje podany wierzcholek!");
            }
        }

        public bool UsunKrawedz(Krawedz krawedz)
        {
            if (krawedz == null) throw new ArgumentNullException("Krawedz jest pusta");
            Krawedz tmp = new Krawedz(0, 0);
            int i = 0;

            while (i != listaKrawedzi.Count)
            {
                foreach (Krawedz k in listaKrawedzi)
                {
                    if (k.w1 == krawedz.w1 && k.w2 == krawedz.w2)
                    {
                        tmp = k;
                        break;
                    }
                }
                i++;
            }

            listaKrawedzi.Remove(tmp);
            return true;
        }



        //public bool DodajEtykiete(Wierzcholek wierzcholek, int etykieta)
        //{
        //    if (wierzcholek == null) throw new ArgumentNullException("Wierzchołek jest pusty");
        //    bool czyWierzcholekIstnieje = false;
        //    foreach (Wierzcholek w in listaWierzcholkow)
        //    {
        //        if (w.id == wierzcholek.id)
        //        {
        //            w.etykieta = etykieta;
        //            czyWierzcholekIstnieje = true;
        //        }
        //    }

        //    if (czyWierzcholekIstnieje)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public void DodajSkojarzenie(int w1, int w2)
        {
            Krawedz skojarzona = new Krawedz(w1, w2);
            listaKrawedziSkojarzonych.Add(skojarzona);
        }

        public void DodajSkojarzenie(Krawedz skojarzona)
        {
            if (skojarzona == null) throw new ArgumentNullException("Krawedz skojarzona jest pusta!");
            listaKrawedziSkojarzonych.Add(skojarzona);
        }

        public void DodajSkojarzenie(Wierzcholek w1, Wierzcholek w2)
        {
            if (w1 == null) throw new ArgumentNullException("Wierzcholek" + w1.id + " jest pusty!");
            if (w2 == null) throw new ArgumentNullException("Wierzcholek" + w2.id + " jest pusty!");
            listaKrawedziSkojarzonych.Add(new Krawedz(w1, w2));
        }

        public void DodajSkojarzenie(IEnumerable<Krawedz> skojarzenia)
        {
            if (skojarzenia == null) throw new ArgumentNullException("Skojarzenia są puste!");
            using (var iterator = skojarzenia.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    if (iterator.Current != null && !listaKrawedziSkojarzonych.Contains(iterator.Current)) listaKrawedziSkojarzonych.Add(iterator.Current);
                }
            }
        }

        public void SumaOdjacCzescWspolna(IList<Krawedz> sciezkaPowiekszajaca)
        {
            IList<Krawedz> suma = SumaZListaSkojarzonych(sciezkaPowiekszajaca).ToList();
            IList<Krawedz> wspolna = CzescWspolnaZListaSkojarzonych(sciezkaPowiekszajaca).ToList();
            foreach (Krawedz para in wspolna)
            {
                // Usuniecie czesci wspolnej skojarzen
                UsunKrawedz(para);

                List<Krawedz> tmpDoUsuniecia = new List<Krawedz>();
                foreach (Krawedz k in suma)
                {
                    if (k.w1 == para.w1 && k.w2 == para.w2)
                    {
                        tmpDoUsuniecia.Add(k);
                    }
                }

                foreach (Krawedz k in tmpDoUsuniecia)
                {
                    suma.Remove(k);
                }

                para.ZamienWierzcholki();
                DodajKrawedz(para.w1, para.w2);
            }

            foreach (var para in suma)
            {
                if (ListaV1Zawiera(para.w1) && ListaV2Zawiera(para.w2))
                {
                        UsunKrawedz(para);
                        para.ZamienWierzcholki();
                        DodajKrawedz(para.w1, para.w2);
                }
            }

            listaKrawedziSkojarzonych = suma.ToList();
        }

        public IEnumerable<Krawedz> SumaZListaSkojarzonych(IList<Krawedz> sciezkaPowiekszajaca)
        {
            if (sciezkaPowiekszajaca == null) throw new ArgumentNullException("Sciezka jest pusta!");
            IList<Krawedz> wynik = ZwrocSkojarzenia().ToList();
            foreach (var para in sciezkaPowiekszajaca)
            {
                //bool znaleziony = false;

                //foreach (var w in wynik)
                //{
                //    if (w.w1 == para.w1 && w.w2 == para.w2)
                //    {
                //        znaleziony = true;
                //    }

                //    if (!znaleziony)
                //    {
                //        wynik.Add(para);
                //        znaleziony = false;
                //        break;
                //    }
                //}

                if (!wynik.Contains(para)) wynik.Add(para);
            }
            return wynik;
        }

        public IEnumerable<Krawedz> CzescWspolnaZListaSkojarzonych(IList<Krawedz> sciezkaPowiekszajaca)
        {
            if (sciezkaPowiekszajaca == null) throw new ArgumentNullException("Sciezka jest pusta!");
            IList<Krawedz> wynik = new List<Krawedz>();
            foreach (var krawedz in sciezkaPowiekszajaca)
            {
                foreach (Krawedz kr in listaKrawedziSkojarzonych)
                {
                    if (kr.w1 == krawedz.w1 && kr.w2 == krawedz.w2)
                    {
                        wynik.Add(krawedz);
                    }
                }
            }
            return wynik;
        }

        public IEnumerable<Krawedz> ZwrocKrawedzieWierzcholka(Wierzcholek wierzcholek)
        {
            if (wierzcholek == null) throw new ArgumentNullException("Wierzchołek jest pusty!");
            // KP
            //if (!listaWierzcholkow.Contains(wierzcholek)) throw new ArgumentException("Wierzchołek nie istnieje w grafie!");
            foreach (Krawedz krawedz in listaKrawedzi)
            {
                if (krawedz.Zawiera(wierzcholek)) yield return krawedz;
            }
        }

        public void KopiujKrawedzieSkojarzoneIZbiory(GrafDwudzielny graf)
        {
            if (graf == null) throw new ArgumentNullException("Graf jest pusty!");
            listaKrawedziSkojarzonych = graf.ZwrocSkojarzenia().ToList();
            listaV1 = graf.ZwrocZbiorV1().ToList();
            listaV2 = graf.ZwrocZbiorV2().ToList();
        }

        // SPRAWDZIC
        public bool JestSkojarzony(Wierzcholek wierzcholek)
        {
            if (wierzcholek == null) throw new ArgumentNullException("Wierzcholek jest pusty!");
            bool czyIstnieje = false;

            foreach (Wierzcholek w in listaWierzcholkow)
            {
                if (w.id == wierzcholek.id)
                {
                    czyIstnieje = true;
                }
            }

            if (czyIstnieje == false || wierzcholek == null) return false;

            //if (!listaWierzcholkow.Contains(wierzcholek) || wierzcholek == null) return false;

            foreach (Krawedz skojarzenie in listaKrawedziSkojarzonych)
            {
                if (skojarzenie.Zawiera(wierzcholek)) return true;
            }
            return false;
        }

        public int ZwrocWage(Wierzcholek w1, Wierzcholek w2)
        {
            foreach (Krawedz krawedz in listaKrawedzi)
            {
                if (krawedz.w1 == w1.id && krawedz.w2 == w2.id)
                {
                    return krawedz.ZwrocWage();
                }
            }

            throw new ArgumentException("Podana krawiedz nie istnieje!");
        }

        public void KopiujWagi(GrafDwudzielny graf)
        {
            //graf.listaKrawedzi = this.listaKrawedzi;
            listaKrawedzi = graf.listaKrawedzi;
        }

        #region Zawieranie
        public bool Zawiera(IList<Krawedz> lprzeszukiwan, Krawedz zawiera)
        {
            foreach(Krawedz p in lprzeszukiwan)
            {
                if (p.w1 == zawiera.w1 && p.w2 == zawiera.w2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Zawiera(IList<Wierzcholek> lprzeszukiwan, Wierzcholek zawiera)
        {
            foreach (Wierzcholek p in lprzeszukiwan)
            {
                if (p.id == zawiera.id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ListaV1Zawiera(int wierzcholek)
        {
            foreach (Wierzcholek w in listaV1)
            {
                if (w.id == wierzcholek)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ListaV2Zawiera(int wierzcholek)
        {
            foreach (Wierzcholek w in listaV2)
            {
                if (w.id == wierzcholek)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ListaV1WolneZawiera(int wierzcholek)
        {
            foreach (Wierzcholek w in listaV1Wolne)
            {
                if (w.id == wierzcholek)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ListaV2WolneZawiera(int wierzcholek)
        {
            foreach (Wierzcholek w in listaV2Wolne)
            {
                if (w.id == wierzcholek)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Sasiedztwa

        public void ZbudujListeSasiedztwa()
        {
            int tmpNajwiekszy = 0;
            for (int i = 0; i < listaV1.Count; i++)
            {
                if (listaV1[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaV1[i].id;
                }
            }
            for (int i = 0; i < listaV2.Count; i++)
            {
                if (listaV2[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaV2[i].id;
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
            for (int i = 0; i < listaV1.Count; i++)
            {
                if (listaV1[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaV1[i].id;
                }
            }
            for (int i = 0; i < listaV2.Count; i++)
            {
                if (listaV2[i].id > tmpNajwiekszy)
                {
                    tmpNajwiekszy = listaV2[i].id;
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
            if (listaV1.Count == 0 || listaV2.Count == 0)
            {
                Console.WriteLine("Lista wierzcholkow jest pusta!");
                return;
            }

            Console.Write("Lista wierzchołków V1: ");
            for (int i = 0; i < listaV1.Count; i++)
            {
                if (i != listaV1.Count - 1)
                {
                    Console.Write(listaV1[i].id + " , ");
                }
                else
                {
                    Console.WriteLine(listaV1[i].id);
                }
            }

            Console.Write("Lista wierzchołków V2: ");
            for (int i = 0; i < listaV2.Count; i++)
            {
                if (i != listaV2.Count - 1)
                {
                    Console.Write(listaV2[i].id + " , ");
                }
                else
                {
                    Console.WriteLine(listaV2[i].id);
                }
            }
            Console.WriteLine();
        }

        public void WyswietlListeSasiedztwa()
        {
            List<int> wszystkieListyV = new List<int>();
            foreach (Wierzcholek w in listaV1)
            {
                wszystkieListyV.Add(w.id);
            }
            foreach (Wierzcholek w in listaV2)
            {
                wszystkieListyV.Add(w.id);
            }
            wszystkieListyV.Sort();

            bool jestNaLiscieWierzcholkow = false;
            List<int> listaWieszcholkowNiestworzonych = new List<int>();


            for (int i = 0; i < macierzSasiedztwa.Capacity; i++)
            {
                for (int lw = 0; lw < listaV1.Count; lw++)
                {
                    if (listaV1[lw].id == i)
                    {
                        jestNaLiscieWierzcholkow = true;
                    }
                }
                for (int lw = 0; lw < listaV2.Count; lw++)
                {
                    if (listaV2[lw].id == i)
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


            for (int i = 0; i < wszystkieListyV.Count; i++)
            {
                if (i == 0 && listaWieszcholkowNiestworzonych[0] == 0)
                {
                    Console.Write("Wierzcholek " + wszystkieListyV[i] + " : ");
                    IEnumerable<Krawedz> dopasowanie = listaKrawedzi.FindAll(el => el.w1 == wszystkieListyV[i]);
                    foreach (Krawedz k in dopasowanie)
                    {
                        Console.Write(k.w2 + " ");
                    }
                    Console.WriteLine();
                }

                if (!listaWieszcholkowNiestworzonych.Exists((int w) => { return (w == i) ? true : false; }))
                {
                    Console.Write("Wierzcholek " + wszystkieListyV[i] + " : ");
                    IEnumerable<Krawedz> dopasowanie = listaKrawedzi.FindAll(el => el.w1 == wszystkieListyV[i]);
                    foreach (Krawedz k in dopasowanie)
                    {
                        Console.Write(k.w2 + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        public void WyswietlMacierzSasiedztwa()
        {
            List<int> wszystkieListyV = new List<int>();
            Console.Write("  ");
            foreach (Wierzcholek w in listaV1)
            {
                wszystkieListyV.Add(w.id);
            }
            foreach (Wierzcholek w in listaV2)
            {
                wszystkieListyV.Add(w.id);
            }
            wszystkieListyV.Sort();
            foreach (int w in wszystkieListyV)
            {
                Console.Write(w + " ");
            }

            Console.WriteLine();

            bool jestNaLiscieWierzcholkow = false;
            List<int> listaWieszcholkowNiestworzonych = new List<int>();


            for (int i = 0; i < macierzSasiedztwa.Capacity; i++)
            {
                for (int lw = 0; lw < listaV1.Count; lw++)
                {
                    if (listaV1[lw].id == i)
                    {
                        jestNaLiscieWierzcholkow = true;
                    }
                }
                for (int lw = 0; lw < listaV2.Count; lw++)
                {
                    if (listaV2[lw].id == i)
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
        public IEnumerable<Wierzcholek> ZwrocZbiorV1()
        {
            return listaV1;
        }

        public IEnumerable<Wierzcholek> ZwrocZbiorV2()
        {
            return listaV2;
        }

        public IEnumerable<Wierzcholek> ZwrocWierzholki()
        {
            return listaWierzcholkow;
        }

        public IEnumerable<Wierzcholek> ZwrocZbiorWolnychV1()
        {
            IEnumerable<Wierzcholek> result = new List<Wierzcholek>();
            foreach (var wierzcholek in listaV1)
            {
                int licznik = 0;
                foreach (var krawedz in listaKrawedziSkojarzonych)
                {
                    if (krawedz.Zawiera(wierzcholek)) licznik++;
                }
                if (licznik == 0)
                {
                    yield return wierzcholek;
                }
            }
        }

        public IEnumerable<Wierzcholek> ZwrocZbiorWolnychV2()
        {
            foreach (var wierzcholek in listaV2)
            {
                int licznik = 0;
                foreach (var krawedz in listaKrawedziSkojarzonych)
                {
                    if (krawedz.Zawiera((wierzcholek)))
                    {
                        licznik++;
                    }
                }
                if (licznik == 0)
                {
                    yield return wierzcholek;
                }
            }
        }

        public IEnumerable<Wierzcholek> ZwrocSasiadow(Wierzcholek wierzholek)
        {
            foreach (Krawedz para in listaKrawedzi)
            {
                if (para.w1.Equals(wierzholek.id))
                {
                    yield return (Wierzcholek)para.w2;
                }
            }
        }

        public IEnumerable<Krawedz> ZwrocKrawedzie()
        {
            return listaKrawedzi;
        }

        public int ZwrocEtykieteWierzcholka(int wierzcholek)
        {
            int etykieta = 0;
            foreach (Wierzcholek w in listaWierzcholkow)
            {
                if (w.id == wierzcholek)
                {
                    etykieta = w.ZwrocEtykiete();
                }
            }
            return etykieta;
        }

        // DO POPRAWY
        public Krawedz ZwrocKrawedz(Wierzcholek w1, Wierzcholek w2)
        {
            // --- zabezbieczenie
            if (w1 == null || w2 == null) throw new ArgumentNullException("Conajmniej jeden wierzchołek jest pusty!");
            if (!Zawiera(listaWierzcholkow, w1) || !Zawiera(listaWierzcholkow, w2)) throw new ArgumentException("Conajmniej jeden z wierzchołków nie jest zawarty w grafie!");

            Krawedz para = new Krawedz(w1, w2);
            if (Zawiera(listaKrawedzi, para)) return listaKrawedzi.Find(x => x.w1 == para.w1 && x.w2 == para.w2);
            throw new ArgumentException("Krawędź nie istnieje w tym grafie!");
        }

        // DO POPRAWY
        public Krawedz ZwrocSkojarzenie(Wierzcholek wierzcholek)
        {
            if (wierzcholek == null) throw new ArgumentNullException("Wierzcholek jest pusty!");

            bool czyIstnieje = false;
            foreach (Wierzcholek w in listaWierzcholkow)
            {
                if (w.id == wierzcholek.id)
                {
                    czyIstnieje = true;
                }
            }

            if (!czyIstnieje) throw new ArgumentException("Wierzcholek nie istnieje!");


            if (!JestSkojarzony(wierzcholek)) return null;
            return listaKrawedziSkojarzonych.Select(x => x).Where(x => x.Zawiera(wierzcholek)).First();
        }

        public IEnumerable<Krawedz> ZwrocSkojarzenia()
        {
            foreach (var krawedz in listaKrawedziSkojarzonych)
            {
                if (ListaV2Zawiera(krawedz.w1) && ListaV1Zawiera(krawedz.w2))
                {
                    yield return krawedz;
                }
                else
                {
                    yield return new Krawedz(krawedz.w2, krawedz.w1);
                }
            }
        }

        #endregion

        public void UsunSkojarzenia()
        {
            listaKrawedziSkojarzonych.Clear();
        }
    }
}
