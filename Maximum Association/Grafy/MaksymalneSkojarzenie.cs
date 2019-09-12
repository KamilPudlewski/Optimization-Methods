using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafy
{
    class MaksymalneSkojarzenie
    {
        private GrafDwudzielny grafSkierowany;

        public MaksymalneSkojarzenie(GrafDwudzielny grafd)
        {
            grafSkierowany = grafd;
        }

        public void wyswietlMaksymalneSkojarzenie()
        {
            var historiaSkojarzen = ZnajdzMaksymalneSkojarzenie();
            int licznik = 0;

            foreach (var krawedzSkojarzona in historiaSkojarzen)
            {
                Console.Write("Wierzcholki " + licznik + " : ");
                Console.Write("[");
                foreach (Krawedz skojarzenie in krawedzSkojarzona)
                {
                    Console.Write("(" + skojarzenie.w1 + ", " + skojarzenie.w2 + ") ");
                }
                Console.Write("]");
                Console.WriteLine();
                licznik++;
            }
        }

        public GrafDwudzielny ZwrocGraf()
        {
            return grafSkierowany;
        }

        public void ZnajdzSkojarzeniePoczatkowe()
        {
            if (grafSkierowany.ZwrocSkojarzenia().Count() > 0) throw new ArgumentException("Graf już posiada skojarzenia!");
            grafSkierowany.listaV1Wolne = grafSkierowany.ZwrocZbiorWolnychV1().ToList();
            foreach (Wierzcholek wierzcholek in grafSkierowany.listaV1Wolne)
            {
                grafSkierowany.listaV2Wolne = grafSkierowany.ZwrocZbiorWolnychV2().ToList();
                // Zbieranie informacji o wszystkich sasiadach wierzcholka
                var sasiedzi = grafSkierowany.ZwrocSasiadow(wierzcholek);
                foreach (Wierzcholek sasiad in sasiedzi)
                {
                    if (grafSkierowany.ListaV2WolneZawiera((int)sasiad))
                    {
                        grafSkierowany.DodajSkojarzenie(sasiad, wierzcholek);
                        break;
                    }
                }
            }
        }

        public IList<List<Krawedz>> ZnajdzMaksymalneSkojarzenie()
        {
            List<List<Krawedz>> historiaSkojarzen = new List<List<Krawedz>>();
            IEnumerable<Krawedz> sciezkaPowiekszajaca = new List<Krawedz>();
            do
            {
                sciezkaPowiekszajaca = ZnajdzSciezkePowiekszajaca();
                historiaSkojarzen.Add(grafSkierowany.ZwrocSkojarzenia().ToList());
                if (sciezkaPowiekszajaca != null)
                {
                    grafSkierowany.SumaOdjacCzescWspolna(sciezkaPowiekszajaca.ToList());
                }
            } while (sciezkaPowiekszajaca != null);
            return historiaSkojarzen;
        }

        public IEnumerable<Krawedz> ZnajdzSciezkePowiekszajaca()
        {
            grafSkierowany.listaV1Wolne = grafSkierowany.ZwrocZbiorWolnychV1().ToList();
            grafSkierowany.listaV2Wolne = grafSkierowany.ZwrocZbiorWolnychV2().ToList();
            List<Stack<Wierzcholek>> zbiorSciezek = new List<Stack<Wierzcholek>>();
            foreach (var wierzcholek in grafSkierowany.listaV1Wolne)
            {
                Stack<Wierzcholek> result = new Stack<Wierzcholek>();
                DFS(wierzcholek, result);
                if (result.Count < 2) continue;
                zbiorSciezek.Add(result);
            }
            if (zbiorSciezek.Count == 0) return null;

            Stack<Wierzcholek> najwiekszaSciezka = new Stack<Wierzcholek>();
            foreach (var sciezka in zbiorSciezek)
            {
                if (najwiekszaSciezka.Count < sciezka.Count || najwiekszaSciezka == null)
                {
                    najwiekszaSciezka = sciezka;
                }
            }

            // Laczenie par ze najwiekszej scieżki
            List<Krawedz> sciezkaPowiekszajaca = new List<Krawedz>();
            Wierzcholek cel = najwiekszaSciezka.Pop();
            while (najwiekszaSciezka.Count > 0)
            {
                Wierzcholek start = najwiekszaSciezka.Pop();
                Krawedz para = new Krawedz(start, cel);
                sciezkaPowiekszajaca.Insert(0, para);
                cel = start;
            }
            return sciezkaPowiekszajaca;
        }

        private bool DFS(Wierzcholek wierzcholek, Stack<Wierzcholek> stos)
        {
            // Przeszukiwane sciezki w glab
            foreach (Wierzcholek w in grafSkierowany.listaV2Wolne)
            {
                if (w.id == wierzcholek.id)
                {
                    stos.Push(wierzcholek);
                    return true;
                }
            }

            Stack<Wierzcholek> rozszerzonyStos = Rozszerz(wierzcholek, ref stos);
            while (rozszerzonyStos.Count > 0)
            {
                Wierzcholek zdjetyWierzcholek = rozszerzonyStos.Pop();
                if (DFS(zdjetyWierzcholek, stos))
                {
                    return true;
                }
                if (stos.Peek().id.Equals(zdjetyWierzcholek.id))
                {
                    stos.Pop();
                }
            }
            return false;
        }

        private Stack<Wierzcholek> Rozszerz(Wierzcholek wierzcholek, ref Stack<Wierzcholek> stos)
        {
            if (wierzcholek == null) return null;
            Stack<Wierzcholek> returnStack = new Stack<Wierzcholek>();
            // Zwrocenie sasiadow wierzcholka
            foreach (var sasiad in grafSkierowany.ZwrocSasiadow(wierzcholek))
            {
                // Zabezpieczenie przed cyklami
                bool czyByl = false;

                foreach (var w in stos)
                {
                    if (sasiad.id == w.id)
                    {
                        czyByl = true;
                    }
                }

                if (!czyByl)
                {
                    returnStack.Push(sasiad);
                }
            }
            stos.Push(wierzcholek);
            return returnStack;
        }
    }
}
