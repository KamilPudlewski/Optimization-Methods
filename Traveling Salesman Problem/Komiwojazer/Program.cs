using System;
using System.Collections.Generic;

namespace Komiwojazer
{
    class Program
    {
        static void Main(string[] args)
        {
            //int tabLenght = 5;
            //double[][] matrix = new double[tabLenght][];
            //matrix[0] = new double[] { double.PositiveInfinity, 1, 1, 1, 1, 1 };
            //matrix[1] = new double[] { 1, double.PositiveInfinity, 1, 1, 1 };
            //matrix[2] = new double[] { 1, 1, double.PositiveInfinity, 1, 1 };
            //matrix[3] = new double[] { 1, 1, 1, double.PositiveInfinity, 1 };
            //matrix[4] = new double[] { 1, 1, 1, 1, double.PositiveInfinity };

            //int tabLenght = 6;
            //double[][] matrix = new double[tabLenght][];
            //matrix[0] = new double[] { double.PositiveInfinity, 3, 93, 13, 33, 9 };
            //matrix[1] = new double[] { 4, double.PositiveInfinity, 77, 42, 21, 16 };
            //matrix[2] = new double[] { 45, 17, double.PositiveInfinity, 36, 16, 28 };
            //matrix[3] = new double[] { 39, 90, 80, double.PositiveInfinity, 56, 7 };
            //matrix[4] = new double[] { 28, 46, 88, 33, double.PositiveInfinity, 25 };
            //matrix[5] = new double[] { 3, 88, 18, 46, 92, double.PositiveInfinity };

            int tabLenght = 7;
            double[][] matrix = new double[tabLenght][];
            matrix[0] = new double[] { double.PositiveInfinity, 42, 137, 1, 187, 387, 343 };
            matrix[1] = new double[] { 42, double.PositiveInfinity, 99, 201, 165, 371, 335 };
            matrix[2] = new double[] { 137, 99, double.PositiveInfinity, 92, 97, 301, 281 };
            matrix[3] = new double[] { 241, 201, 92, double.PositiveInfinity, 128, 275, 282 };
            matrix[4] = new double[] { 187, 165, 97, 128, double.PositiveInfinity, 209, 184 };
            matrix[5] = new double[] { 387, 371, 301, 275, 209, double.PositiveInfinity, 75 };
            matrix[6] = new double[] { 343, 335, 281, 282, 184, 75, double.PositiveInfinity };

            Komiwojazer k = new Komiwojazer();
            k.wczytajMacierz(matrix);
            k.rozwiazProblemKomiwojazera();

            Console.ReadLine();
        }


    }
}
