using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class MatriceCarre
    {
        public static readonly string format = null;
        public static int precision = 2; //Precision par défaut

        static MatriceCarre()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < precision; i++)
            {
                res.Append("0");
            }
            format = "{0," + (precision + 3) + ":0." + res.ToString() + "}";
        }

        private double[,] matrice = null;

        public double[,] Matrice
        {
            get { return matrice; }
            set { if (value.GetLength(0) == value.GetLength(1)) matrice = value; }
        }

        public MatriceCarre(int n)
        {
            Matrice = new double[n, n];
        }

        public MatriceCarre(double[,] m)
        {
            Matrice = m;
        }

        #region SURCHARGE OPERATEUR

        public static MatriceCarre operator +(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Matrice.GetLength(0));
            for (int i = 0; i < m.Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrice.GetLength(1); j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] + n;
                }
            }
            return res;
        }
        public static MatriceCarre operator +(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Matrice.GetLength(0) == m2.Matrice.GetLength(0))
            {
                res = new MatriceCarre(m1.Matrice.GetLength(0));
                for (int i = 0; i < m1.Matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < m1.Matrice.GetLength(1); j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] + m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Matrice.GetLength(0));
            for (int i = 0; i < m.Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrice.GetLength(1); j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] - n;
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Matrice.GetLength(0) == m2.Matrice.GetLength(0))
            {
                res = new MatriceCarre(m1.Matrice.GetLength(0));
                for (int i = 0; i < m1.Matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < m1.Matrice.GetLength(1); j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] - m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Matrice.GetLength(0));
            for (int i = 0; i < m.Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < m.Matrice.GetLength(1); j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] * n;
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Matrice.GetLength(0) == m2.Matrice.GetLength(0))
            {
                res = new MatriceCarre(m1.Matrice.GetLength(0));
                for (int i = 0; i < m1.Matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < m1.Matrice.GetLength(1); j++)
                    {
                        res.Matrice[i, j] = 0;
                        for (int k = 0; k < m1.Matrice.GetLength(1); k++)
                        {
                            res.Matrice[i, j] += m1.Matrice[i, k] * m2.Matrice[k, j];
                        }
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator /(MatriceCarre m, double n)
        {
            MatriceCarre res = null;
            if (n != 0)
            {
                res = new MatriceCarre(m.Matrice.GetLength(0));
                for (int i = 0; i < m.Matrice.GetLength(0); i++)
                {
                    for (int j = 0; j < m.Matrice.GetLength(1); j++)
                    {
                        res.Matrice[i, j] = m.Matrice[i, j] / n;
                    }
                }
            }
            return res;
        }
        public static bool operator ==(MatriceCarre m1, MatriceCarre m2)
        {
            return m1.Equals(m2);
        }
        public static bool operator !=(MatriceCarre m1, MatriceCarre m2)
        {
            return !m1.Equals(m2);
        }

        #endregion

        #region SURCHARGE METHODE OBJECT

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < Matrice.GetLength(1); j++)
                {
                    res.Append(string.Format(format, Matrice[i, j]) + " ");
                }
                res.Append("\n");
            }
            return res.ToString();
        }
        public override bool Equals(object obj)
        {
            MatriceCarre m = obj as MatriceCarre;
            bool res = true;
            if (m == null)
                res = false;
            else
            {
                if (this.Matrice.GetLength(0) == m.Matrice.GetLength(0) && this.Matrice.GetLength(1) == m.Matrice.GetLength(1))
                {
                    for (int i = 0; i < this.Matrice.GetLength(0); i++)
                    {
                        for (int j = 0; j < this.Matrice.GetLength(1); j++)
                        {
                            if (this.Matrice[i, j] != m.Matrice[i, j])
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
            for (int i = 0; i < Matrice.GetLength(0); i++)
            {
                for (int j = 0; j < Matrice.GetLength(1); j++)
                {
                    res += Matrice[i, j].GetHashCode();
                }
            }
            return res;
        }

        #endregion
    }
}
