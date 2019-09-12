using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorytmGenetyczny
{
    class Program
    {
        static void Main(string[] args)
        {
            int wielkoscMacierzy = 7;
            double[][] macierz = new double[wielkoscMacierzy][];
            macierz[0] = new double[] { double.PositiveInfinity, 42, 137, 1, 187, 387, 343 };
            macierz[1] = new double[] { 42, double.PositiveInfinity, 99, 201, 165, 371, 335 };
            macierz[2] = new double[] { 137, 99, double.PositiveInfinity, 92, 97, 301, 281 };
            macierz[3] = new double[] { 241, 201, 92, double.PositiveInfinity, 128, 275, 282 };
            macierz[4] = new double[] { 187, 165, 97, 128, double.PositiveInfinity, 209, 184 };
            macierz[5] = new double[] { 387, 371, 301, 275, 209, double.PositiveInfinity, 75 };
            macierz[6] = new double[] { 343, 335, 281, 282, 184, 75, double.PositiveInfinity };

            //int wielkoscMacierzy = 6;
            //double[][] macierz = new double[wielkoscMacierzy][];
            //macierz[0] = new double[] { double.PositiveInfinity, 3, 93, 13, 33, 9 };
            //macierz[1] = new double[] { 4, double.PositiveInfinity, 77, 42, 21, 16 };
            //macierz[2] = new double[] { 45, 17, double.PositiveInfinity, 36, 16, 28 };
            //macierz[3] = new double[] { 39, 90, 80, double.PositiveInfinity, 56, 7 };
            //macierz[4] = new double[] { 28, 46, 88, 33, double.PositiveInfinity, 25 };
            //macierz[5] = new double[] { 3, 88, 18, 46, 92, double.PositiveInfinity };

            AlgorytmGenetyczny gen = new AlgorytmGenetyczny();
            gen.WczytajMacierz(macierz);
            gen.WykonajAlgorytm();

            Console.ReadLine();
        }
    }
}
