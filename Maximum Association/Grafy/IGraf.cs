using System;
using System.Collections.Generic;

namespace Grafy
{
    public class Wierzcholek
    {
        public int id;
        private int etykieta;

        public Wierzcholek(int wierzcholek)
        {
            id = wierzcholek;
            etykieta = 0;
        }

        public Wierzcholek(Wierzcholek wierzcholek)
        {
            id = wierzcholek.id;
            etykieta = wierzcholek.etykieta;
        }

        public Wierzcholek(int wierzcholek, int ustawEtykiete)
        {
            id = wierzcholek;
            etykieta = ustawEtykiete;
        }

        public static explicit operator Wierzcholek(int w)
        {
            return new Wierzcholek(w);
        }

        public static explicit operator int(Wierzcholek w)
        {
            return w.id;
        }

        public void UstawEtykiete(int etykieta)
        {
            this.etykieta = etykieta;
        }

        public int ZwrocEtykiete()
        {
            return etykieta;
        }
    }

    public class Krawedz
    {
        public int w1;
        public int w2;
        private int waga;

        public Krawedz(int wiercholek1, int wierzcholek2)
        {
            w1 = wiercholek1;
            w2 = wierzcholek2;
            waga = 0;
        }

        public Krawedz(Wierzcholek wiercholek1, Wierzcholek wierzcholek2)
        {
            w1 = wiercholek1.id;
            w2 = wierzcholek2.id;
            waga = 0;
        }

        public Krawedz(int wiercholek1, int wierzcholek2, int ustawWage)
        {
            w1 = wiercholek1;
            w2 = wierzcholek2;
            waga = ustawWage;
        }

        public Krawedz(Wierzcholek wiercholek1, Wierzcholek wierzcholek2, int ustawWage)
        {
            w1 = wiercholek1.id;
            w2 = wierzcholek2.id;
            waga = ustawWage;
        }
        
        public void UstawWierzcholek1(Wierzcholek wierzcholek1)
        {
            w1 = wierzcholek1.id;
        }

        public void UstawWierzcholek2(Wierzcholek wierzcholek2)
        {
            w2 = wierzcholek2.id;
        }

        public void ZamienWierzcholki()
        {
            int tmp = w1;
            w1 = w2;
            w2 = tmp;
        }

        public bool Zawiera(int wartosc)
        {
            return wartosc.Equals(w1) || wartosc.Equals(w2);
        }

        public bool Zawiera(Wierzcholek wierzcholek)
        {
            return wierzcholek.id.Equals(w1) || wierzcholek.id.Equals(w2);
        }

        public void UstawWage(int waga)
        {
            this.waga = waga;
        }

        public int ZwrocWage()
        {
            return waga;
        }
    }

    public class IGraf
    {
        protected List<Wierzcholek> listaWierzcholkow = new List<Wierzcholek>();
        protected List<Krawedz> listaKrawedzi = new List<Krawedz>();
        protected List<List<int>> listaSasiedztwa = new List<List<int>>();
        protected List<List<int>> macierzSasiedztwa = new List<List<int>>();
    }
}
