using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    public class MatriceCarre
    {
        public static readonly int NULL = 0;
        public static readonly int UNITE = 1;
        private static string format = null;

        private static int precision = 3;//Precision par défaut

        #region PROPRIETES STATIQUES

        public static int Precision
        {
            get { return precision; }
            set
            {
                precision = value;
                StringBuilder res = new StringBuilder();
                for (int i = 0; i < precision; i++)
                {
                    res.Append("0");
                }
                format = "{0," + (precision + 3) + ":0." + res.ToString() + "}";
            }
        }

        #endregion

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

        private int ordre = 0;

        #region PROPRIETES

        public double[,] Matrice
        {
            get { return matrice; }
            set
            {
                if (value.GetLength(0) == value.GetLength(1))
                    matrice = value;
                else
                    throw new MatriceCarreException("La matrice entrée en paramètre n'est pas carrée");
            }
        }

        public int Ordre
        {
            get { return ordre; }
        }

        #endregion

        #region CONSTRUCTEURS

        public MatriceCarre(int n, int type)
        {
            Matrice = new double[n, n];
            ordre = n;
            if (type == UNITE)
                for (int i = 0; i < n; i++)
                    Matrice[i, i] = 1;
        }

        public MatriceCarre(double[,] m)
        {
            try
            {
                Matrice = (double[,])m.Clone();
                ordre = m.GetLength(0);
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }

        #endregion

        public MatriceCarre Transpose()
        {
            MatriceCarre res = new MatriceCarre(ordre, NULL);

            for (int i = 0; i < ordre; i++)
            {
                for (int j = 0; j < ordre; j++)
                {
                    res.Matrice[i, j] = Matrice[j, i];
                }
            }

            return res;
        }

        public MatriceCarre Diagonale1()
        {
            MatriceCarre res = new MatriceCarre(Matrice);

            for (int i = 0; i < res.Ordre; i++)
            {
                res.Matrice[i, i] = 1;
            }

            return res;
        }

        #region SURCHARGE OPERATEUR

        public static MatriceCarre operator +(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.ordre, NULL);
            for (int i = 0; i < m.ordre; i++)
            {
                for (int j = 0; j < m.ordre; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] + n;
                }
            }
            return res;
        }
        public static MatriceCarre operator +(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.ordre == m2.ordre)
            {
                res = new MatriceCarre(m1.ordre, NULL);
                for (int i = 0; i < m1.ordre; i++)
                {
                    for (int j = 0; j < m1.ordre; j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] + m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.ordre, NULL);
            for (int i = 0; i < m.ordre; i++)
            {
                for (int j = 0; j < m.ordre; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] - n;
                }
            }
            return res;
        }
        public static MatriceCarre operator -(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.ordre == m2.ordre)
            {
                res = new MatriceCarre(m1.ordre, NULL);
                for (int i = 0; i < m1.ordre; i++)
                {
                    for (int j = 0; j < m1.ordre; j++)
                    {
                        res.Matrice[i, j] = m1.Matrice[i, j] - m2.Matrice[i, j];
                    }
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m, double n)
        {
            MatriceCarre res = new MatriceCarre(m.ordre, NULL);
            for (int i = 0; i < m.ordre; i++)
            {
                for (int j = 0; j < m.ordre; j++)
                {
                    res.Matrice[i, j] = m.Matrice[i, j] * n;
                }
            }
            return res;
        }
        public static MatriceCarre operator *(MatriceCarre m1, MatriceCarre m2)
        {
            MatriceCarre res = null;
            if (m1.ordre == m2.ordre)
            {
                res = new MatriceCarre(m1.ordre, NULL);
                for (int i = 0; i < m1.ordre; i++)
                {
                    for (int j = 0; j < m1.ordre; j++)
                    {
                        res.Matrice[i, j] = 0;
                        for (int k = 0; k < m1.ordre; k++)
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
                res = new MatriceCarre(m.ordre, NULL);
                for (int i = 0; i < m.ordre; i++)
                {
                    for (int j = 0; j < m.ordre; j++)
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
            for (int i = 0; i < ordre; i++)
            {
                for (int j = 0; j < ordre; j++)
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
                if (ordre == m.ordre)
                {
                    for (int i = 0; i < ordre; i++)
                    {
                        for (int j = 0; j < ordre; j++)
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
            for (int i = 0; i < ordre; i++)
            {
                for (int j = 0; j < ordre; j++)
                {
                    res += Matrice[i, j].GetHashCode();
                }
            }
            return res;
        }

        #endregion
    }
}
