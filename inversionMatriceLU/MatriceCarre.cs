using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    public class MatriceCarre
    {
        public static readonly string format = null;
        public static int precision = 2; //Precision par défaut

        #region CONSTRUCTEUR STATIQUE

        static MatriceCarre()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < precision; i++)
            {
                res.Append("0");
            }
            format = "{0," + (precision + 3) + ":0." + res.ToString() + "}";
        }

        #endregion

        private double[,] matrice = null;

        private int deg = 0;

        #region PROPRIETES

        public double[,] Matrice
        {
            get { return matrice; }
            set
            {
                if (value.GetLength(0) == value.GetLength(1))
                    matrice = value;
                else
                    throw new PasMatriceCarreException("La matrice entrée en paramètre n'est pas carrée");
            }
        }

        public int Deg
        {
            get { return deg; }
        }

        #endregion

        #region CONSTRUCTEURS

        public MatriceCarre(int n)
        {
            Matrice = new double[n, n];
            deg = n;
        }

        public MatriceCarre(double[,] m)
        {
            try
            {
                Matrice = m;
                deg = m.GetLength(0);
            }
            catch(PasMatriceCarreException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SURCHARGE OPERATEUR

        public static MatriceCarre operator +(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Deg);
            for (int i = 0; i < m.Deg; i++)
            {
                for (int j = 0; j < m.Deg; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] + n;
                }
            }
            return res;
        }
        public static MatriceCarre operator +(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Deg == m2.Deg)
            {
                res = new MatriceCarre(m1.Deg);
                for (int i = 0; i < m1.Deg; i++)
                {
                    for (int j = 0; j < m1.Deg; j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] + m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Deg);
            for (int i = 0; i < m.Deg; i++)
            {
                for (int j = 0; j < m.Deg; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] - n;
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Deg == m2.Deg)
            {
                res = new MatriceCarre(m1.Deg);
                for (int i = 0; i < m1.Deg; i++)
                {
                    for (int j = 0; j < m1.Deg; j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] - m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.Deg);
            for (int i = 0; i < m.Deg; i++)
            {
                for (int j = 0; j < m.Deg; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] * n;
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.Deg == m2.Deg)
            {
                res = new MatriceCarre(m1.Deg);
                for (int i = 0; i < m1.Deg; i++)
                {
                    for (int j = 0; j < m1.Deg; j++)
                    {
                        res.Matrice[i, j] = 0;
                        for (int k = 0; k < m1.Deg; k++)
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
                res = new MatriceCarre(m.Deg);
                for (int i = 0; i < m.Deg; i++)
                {
                    for (int j = 0; j < m.Deg; j++)
                    {
                        res.Matrice[i, j] = m.Matrice[i, j] / n;
                    }
                }
            }
            return res;
        }
        public static bool operator ==(MatriceCarre m1, MatriceCarre m2)
        {
            if (ReferenceEquals(m1, null))
            {
                return ReferenceEquals(m2, null);
            }
            return m1.Equals(m2);
        }
        public static bool operator !=(MatriceCarre m1, MatriceCarre m2)
        {
            return !(m1 == m2);
        }

        #endregion

        #region SURCHARGE METHODE OBJECT

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < Deg; i++)
            {
                for (int j = 0; j < Deg; j++)
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
                if (Deg == m.Deg)
                {
                    for (int i = 0; i < Deg; i++)
                    {
                        for (int j = 0; j < Deg; j++)
                        {
                            if (Matrice[i, j] != m.Matrice[i, j])
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
            for (int i = 0; i < Deg; i++)
            {
                for (int j = 0; j < Deg; j++)
                {
                    res += Matrice[i, j].GetHashCode();
                }
            }
            return res;
        }

        #endregion
    }
}
