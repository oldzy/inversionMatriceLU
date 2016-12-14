using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class Matrice
    {
        public static readonly string format = null;
        public static int precision = 3;

        static Matrice()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < precision; i++)
            {
                res.Append("0");
            }
            format = "{0," + (precision + 3) + ":0." + res.ToString() + "}";
        }

        private double[,] mat = null;

        public double[,] Mat
        {
            get { return mat; }
            set { mat = value; }
        }

        public Matrice(int n)
        {
            Mat = new double[n, n];
        }

        #region SURCHARGE OPERATEUR

        #endregion

        #region SURCHARGE METHODE OBJECT
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    res.Append(string.Format(format, Mat[i, j]) + " ");
                }
                res.Append("\n");
            }
            return res.ToString();
        }
        public override bool Equals(object obj)
        {
            Matrice m = obj as Matrice;
            bool res = true;
            if (m == null)
                res = false;
            else
            {
                if (this.Mat.GetLength(0) == m.Mat.GetLength(0) && this.Mat.GetLength(1) == m.Mat.GetLength(1))
                {
                    for (int i = 0; i < this.Mat.GetLength(0); i++)
                    {
                        for (int j = 0; j < this.Mat.GetLength(1); j++)
                        {
                            if (this.Mat[i, j] != m.Mat[i, j])
                                res = false;
                        }
                    }
                }
                else
                    res = false;
            }
            return res;
        }
        public override int GetHashCode()
        {
            int res = 0;
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    res += Mat[i, j].GetHashCode();
                }
            }
            return res;
        }
        #endregion
    }
}
