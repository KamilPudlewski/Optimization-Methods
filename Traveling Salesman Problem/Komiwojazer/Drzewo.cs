using System;
using System.Collections.Generic;
using System.Text;

namespace Komiwojazer
{
    public class Node : ICloneable
    {
        public Node rodzic;
        public Node lewo;
        public Node prawo;
        public double LB = 0.0;
        public double[][] matrix;
        public double[] minX;
        public double[] minY;
        public double X;
        public double Y;
        public List<Tuple<int, int>> lista_odw;
        public List<int> odwiedzone;

        private Node()
        {
            odwiedzone = new List<int>();
            lista_odw = new List<Tuple<int, int>>();
            rodzic = null;
            lewo = null;
            prawo = null;
        }

        public Node(double[][] _matrix, double _LB, double[] _minX, double[] _minY) : this()
        {
            matrix = _matrix;
            LB = _LB;
            minX = _minX;
            minY = _minY;
        }

        public object Clone()
        {
            Node tmp = new Node();
            tmp.X = X;
            tmp.Y = Y;
            tmp.rodzic = this;
            tmp.LB = LB;
            tmp.matrix = new double[matrix.Length][];
            tmp.minX = new double[minX.Length];
            tmp.minY = new double[minY.Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                tmp.matrix[i] = new double[matrix.Length];
                for (int j = 0; j < matrix.Length; j++)
                {
                    tmp.matrix[i][j] = matrix[i][j];
                }
                tmp.minX[i] = minX[i];
                tmp.minX[i] = minX[i];
            }

            foreach (var item in lista_odw)
            {
                tmp.lista_odw.Add(item);
            }

            foreach (var item in odwiedzone)
            {
                tmp.odwiedzone.Add(item);
            }

            return tmp;
        }
    }
}
