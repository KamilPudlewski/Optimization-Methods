using System;
using System.Collections.Generic;
using System.Text;

namespace Komiwojazer
{
    public class Komiwojazer
    {
        double[][] matrix;

        public Komiwojazer()
        {
            
        }

        public void wczytajMacierz(double[][] _matrix)
        {
            matrix = _matrix;
        }

        public void rozwiazProblemKomiwojazera()
        {
            int tablen = matrix.Length;
            double MaxWynik = double.MinValue;

            double[][] matrix2 = new double[tablen][];
            List<Node> kolejkaNode = new List<Node>();
            List<Node> kolejkaNodeWynik = new List<Node>();


            for (int i = 0; i < tablen; i++)
            {
                matrix2[i] = new double[tablen];
                for (int j = 0; j < tablen; j++)
                {
                    matrix2[i][j] = matrix[i][j];
                }
            }

            Node root = new Node(matrix, 0, new double[tablen], new double[tablen]);
            root.LB = PrzygotowanieZer(root.matrix, out root.minX, out root.minY);
            KolejkaPiorytetowa_Dodaj(ref kolejkaNode, root);

            while (kolejkaNode.Count > 0)
            {
                Node elem = KolejkaPiorytetowa_Zdejmij(ref kolejkaNode);
                if (MaxWynik != double.MinValue)
                    if (elem.LB > MaxWynik) continue;

                elem.LB += PrzygotowanieZer(elem.matrix, out elem.minX, out elem.minY);
                Tuple<int, int, double, double, double> wynikZera = wyborNajlepsegoZera(ref elem.matrix);
                elem.lista_odw.Add(new Tuple<int, int>(wynikZera.Item1, wynikZera.Item2));

                if (CzyKoniec(ref elem.matrix))
                {
                    if (elem.LB > MaxWynik)
                        MaxWynik = elem.LB;

                    elem.lista_odw.RemoveAll((x) => x.Item1 == 0 && x.Item2 == 0);
                    kolejkaNodeWynik.Add((Node)elem.Clone());
                }
                else
                {
                    // lewy
                    elem.lewo = (Node)elem.Clone();
                    Wykreśl_i_podmien(ref elem.lewo.matrix, wynikZera.Item1, wynikZera.Item2);
                    elem.lewo.LB += PrzygotowanieZer(elem.lewo.matrix, out elem.lewo.minX, out elem.lewo.minY);
                    Cykl(ref elem.lewo.matrix, elem.lista_odw);
                    KolejkaPiorytetowa_Dodaj(ref kolejkaNode, elem.lewo);


                    //prawy
                    elem.prawo = (Node)elem.Clone();
                    elem.prawo.matrix[wynikZera.Item1][wynikZera.Item2] = double.PositiveInfinity;
                    elem.prawo.matrix[wynikZera.Item2][wynikZera.Item1] = double.PositiveInfinity;
                    elem.prawo.LB += wynikZera.Item3;
                    Odejmij_w_XY_value(ref elem.prawo.matrix, wynikZera.Item1, wynikZera.Item2, wynikZera.Item4, wynikZera.Item5);
                    KolejkaPiorytetowa_Dodaj(ref kolejkaNode, elem.prawo);
                }
            }

            int ostatniWynik = kolejkaNodeWynik.Count;
            int iloscIteracji = 1;

            Console.WriteLine("Lista odwiedzen:");
            foreach (var item in kolejkaNodeWynik)
            {
                double suma = 0;
                if (iloscIteracji == ostatniWynik)
                {
                    foreach (var sciezkaOdwiedzona in item.lista_odw)
                    {
                        suma += matrix2[sciezkaOdwiedzona.Item1][sciezkaOdwiedzona.Item2];
                        Console.WriteLine(sciezkaOdwiedzona + ":" + matrix2[sciezkaOdwiedzona.Item1][sciezkaOdwiedzona.Item2].ToString());
                    }
                    Console.WriteLine("----------");
                    Console.WriteLine("Suma:" + suma.ToString());
                    Console.WriteLine();
                }
                iloscIteracji++;
            }
        }

        static bool CzyKoniec(ref double[][] tabG)
        {
            bool wynik = true;
            for (int i = 0; i < tabG.Length; i++)
            {
                for (int j = 0; j < tabG.Length; j++)
                {
                    if (tabG[i][j] != double.MaxValue && tabG[i][j] != double.PositiveInfinity)
                    {
                        wynik = false;
                        break;
                    }
                }
            }
            return wynik;
        }

        static void Odejmij_w_XY_value(ref double[][] tabG, int X, int Y, double Xvalue, double Yvalue)
        {
            for (int i = 0; i < tabG.Length; i++)
            {
                if (tabG[X][i] != double.PositiveInfinity && tabG[X][i] != 0)
                {
                    tabG[X][i] -= Xvalue;
                }
            }

            for (int i = 0; i < tabG.Length; i++)
            {
                if (tabG[i][Y] != double.PositiveInfinity && tabG[i][Y] != 0)
                {
                    tabG[i][Y] -= Yvalue;
                }
            }
        }

        static Tuple<int, int, double, double, double> wyborNajlepsegoZera(ref double[][] tabG)
        { // bierze każde zer oblicza LB z minimow w W i K, bierze najwyzsze
            double LB = double.MinValue;
            int pozX = 0;
            int pozY = 0;
            double najminX = double.MaxValue;
            double najminY = double.MaxValue;
            for (int i = 0; i < tabG.Length; i++)
            {
                for (int j = 0; j < tabG.Length; j++)
                {
                    if (tabG[i][j] == 0)
                    {
                        int licznikZer = 0;
                        double minX = double.MaxValue;
                        double minY = double.MaxValue;
                        for (int k = 0; k < tabG.Length; k++)
                        { // dla wierszy
                            if (tabG[i][k] == 0)
                            {
                                licznikZer++;
                                if (licznikZer >= 2)
                                {
                                    if (tabG[i][k] < minX)
                                    {
                                        minX = tabG[i][k];
                                    }
                                }
                            }
                            else
                            if (tabG[i][k] < minX)
                            {
                                minX = tabG[i][k];
                            }
                        }

                        licznikZer = 0;
                        for (int k = 0; k < tabG.Length; k++)
                        { // dla kolumny
                            if (tabG[k][j] == 0)
                            {
                                licznikZer++;
                                if (licznikZer >= 2)
                                {
                                    if (tabG[k][j] < minY)
                                    {
                                        minY = tabG[k][j];
                                    }
                                }
                            }
                            else
                            if (tabG[k][j] < minY)
                            {
                                minY = tabG[k][j];
                            }
                        }
                        if (LB < minX + minY)
                        {
                            najminX = minX;
                            najminY = minY;
                            LB = minX + minY;
                            pozX = i;
                            pozY = j;
                        }
                    }
                }
            }
            return new Tuple<int, int, double, double, double>(pozX, pozY, LB, najminX, najminY);
        }

        static void Cykl(ref double[][] tabG, List<Tuple<int, int>> lista_odw)
        {
            for (int i = 0; i < lista_odw.Count; i++)
            {
                for (int j = 0; j < lista_odw.Count; j++)
                {
                    if (i == j) continue;
                    // 1-1
                    if (lista_odw[i].Item1 == lista_odw[j].Item1)
                    {
                        tabG[lista_odw[i].Item2][lista_odw[j].Item2] = double.PositiveInfinity;
                        tabG[lista_odw[j].Item2][lista_odw[i].Item2] = double.PositiveInfinity;
                    }

                    // 1-2
                    if (lista_odw[i].Item1 == lista_odw[j].Item2)
                    {
                        tabG[lista_odw[i].Item2][lista_odw[j].Item1] = double.PositiveInfinity;
                        tabG[lista_odw[j].Item1][lista_odw[i].Item2] = double.PositiveInfinity;
                    }

                    // 2-1
                    if (lista_odw[i].Item2 == lista_odw[j].Item1)
                    {
                        tabG[lista_odw[i].Item1][lista_odw[j].Item2] = double.PositiveInfinity;
                        tabG[lista_odw[j].Item2][lista_odw[i].Item1] = double.PositiveInfinity;
                    }

                    // 2-2
                    if (lista_odw[i].Item2 == lista_odw[j].Item2)
                    {
                        tabG[lista_odw[i].Item1][lista_odw[j].Item1] = double.PositiveInfinity;
                        tabG[lista_odw[j].Item1][lista_odw[i].Item1] = double.PositiveInfinity;
                    }
                }
            }
        }

        static public void Wykreśl_i_podmien(ref double[][] tabG, int pX, int pY)
        {
            for (int i = 0; i < tabG.Length; i++)
            {
                tabG[pX][i] = double.MaxValue;
                tabG[i][pY] = double.MaxValue;
            }
            if (tabG[pY][pX] != double.PositiveInfinity)
                tabG[pY][pX] = double.PositiveInfinity;
            if (tabG[pX][pY] != double.PositiveInfinity)
                tabG[pX][pY] = double.PositiveInfinity;
        }

        static public Node KolejkaPiorytetowa_Zdejmij(ref List<Node> kolejka)
        {
            Node tmp = null;
            if (kolejka.Count > 0)
            {
                tmp = kolejka[0];
                kolejka.RemoveAt(0);
                return tmp;
            }
            return tmp;
        }

        static public void KolejkaPiorytetowa_Dodaj(ref List<Node> kolejka, Node element)
        {
            if (kolejka.Count == 0)
                kolejka.Add(element);
            else
            {
                for (int i = 0; i < kolejka.Count; i++)
                {
                    if (kolejka[i].LB > element.LB)
                    {
                        kolejka.Insert(i, element);
                        break;
                    }
                    else if (kolejka[i].LB == element.LB)
                    {
                        kolejka.Insert(i, element);
                        break;
                    }
                }
                if (kolejka.Count == 1)
                {
                    kolejka.Add(element);
                }
            }
        }

        static private double PrzygotowanieZer(double[][] tabG, out double[] minX, out double[] minY)
        {
            double LB = 0;
            minX = new double[tabG.Length];
            minY = new double[tabG.Length];

            //dla wierszy
            for (int i = 0; i < tabG.Length; i++)
            {
                minX[i] = double.MaxValue;
                for (int j = 0; j < tabG.Length; j++)
                {
                    if (tabG[i][j] < minX[i] && tabG[i][j] != double.PositiveInfinity && tabG[i][j] != double.MaxValue)
                    {
                        minX[i] = tabG[i][j];
                    }
                }
                if (minX[i] != 0 && minX[i] != double.MaxValue)
                {
                    for (int j = 0; j < tabG.Length; j++)
                    {
                        if (tabG[i][j] != double.PositiveInfinity && tabG[i][j] != double.MaxValue)
                            tabG[i][j] -= minX[i];
                    }
                    LB += minX[i];
                }

            }


            //dla kolumn
            for (int i = 0; i < tabG.Length; i++)
            {
                minY[i] = double.MaxValue;
                for (int j = 0; j < tabG.Length; j++)
                {
                    if (tabG[j][i] < minY[i] && tabG[j][i] != double.PositiveInfinity && tabG[j][i] != double.MaxValue)
                    {
                        minY[i] = tabG[j][i];
                    }
                }
                if (minY[i] != 0 && minY[i] != double.MaxValue)
                {
                    for (int j = 0; j < tabG.Length; j++)
                    {
                        if (tabG[j][i] != double.PositiveInfinity && tabG[j][i] != double.MaxValue)
                            tabG[j][i] -= minY[i];
                    }
                    LB += minY[i];
                }

            }
            return LB;
        }

    }
}
