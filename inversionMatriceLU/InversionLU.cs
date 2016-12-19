using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    public class InversionLU
    {
        #region ATTRIBUTS

        private MatriceCarre matA;
        private MatriceCarre matP;
        private MatriceCarre matL;
        private MatriceCarre matU;
        private MatriceCarre matX;
        private MatriceCarre matY;
        private MatriceCarre matInverse;
        private int permutation;
        private double determinant;

        #endregion

        #region PROPRIETES

        public MatriceCarre A
        {
            get { return matA; }
            set { matA = value; }
        }

        public MatriceCarre P
        {
            get { return matP; }
            set { matP = value; }
        }

        public MatriceCarre L
        {
            get { return matL; }
            set { matL = value; }
        }

        public MatriceCarre U
        {
            get { return matU; }
            set { matU = value; }
        }

        public MatriceCarre X
        {
            get { return matX; }
            set { matX = value; }
        }

        public MatriceCarre Y
        {
            get { return matY; }
            set { matY = value; }
        }

        public MatriceCarre Inverse
        {
            get { return matInverse; }
            set { matInverse = value; }
        }

        public int p
        {
            get { return permutation; }
            set { permutation = value; }
        }

        public double Determinant
        {
            get { return determinant; }
            set { determinant = value; }
        }

        #endregion

        public InversionLU(MatriceCarre matrice)
        {
            A = matrice;
            P = new MatriceCarre(A.Ordre, MatriceCarre.UNITE);
            L = new MatriceCarre(A.Ordre, MatriceCarre.NULL);
            U = new MatriceCarre(A.Matrice);
            p = 0;
            Inversion();
        }

        private void Inversion()
        {
            if (Decomposition())
            {
                CalculDeterminant();
                if (Determinant != 0)
                {
                    X = new MatriceCarre(L.Matrice);
                    Y = new MatriceCarre(U.Matrice);
                    InversionL();
                    InversionU();
                    Inverse = Y * X * P;
                }
                else
                {
                    throw new ApplicationException("Cette matrice ne peut pas être inversée car son déterminant est égal à zéro");
                }
            }
            else
            {
                throw new ApplicationException("La décompostion LU n'a pas pu être calculée.");
            }

        }

        private void InversionL()
        {
            for (int i = 0; i < L.Ordre; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    X.Matrice[i, j] = (i == j) ? 1 : 0;
                    for (int k = 0; k < i; k++)
                    {
                        X.Matrice[i, j] -= L.Matrice[i, k] * X.Matrice[k, j];
                    }
                }
            }
        }

        private void InversionU()
        {
            for (int n = U.Ordre - 1; n >= 0; n--)
            {
                for (int j = 0; j < U.Ordre; j++)
                {
                    Y.Matrice[n, j] = (n == j) ? 1 : 0;
                    for (int k = U.Ordre - 1; k > n; k--)
                    {
                        Y.Matrice[n, j] -= U.Matrice[n, k] * Y.Matrice[k, j];
                    }
                    Y.Matrice[n, j] /= U.Matrice[n, n];
                }
            }
        }

        private bool Decomposition()
        {
            bool quitter = false;
            for (int k = 0; !quitter && k < A.Ordre - 1; k++)
            {
                for (int i = k + 1; !quitter && i < A.Ordre; i++)
                {
                    int n = Pivot(U, k);
                    if (n != k)
                    {
                        Pivotage(k, n, P);
                        Pivotage(k, n, L);
                        Pivotage(k, n, U);
                        p++;
                    }
                    if (U.Matrice[k, k] != 0)
                    {
                        L.Matrice[i, k] = U.Matrice[i, k] / U.Matrice[k, k];
                    }
                    else
                    {
                        if (U.Matrice[i, k] != 0)
                            quitter = true;
                        else
                            L.Matrice[i, k] = 0;
                    }
                    for (int j = 0; !quitter && j < A.Ordre; j++)
                    {
                        U.Matrice[i, j] = U.Matrice[i, j] - (L.Matrice[i, k] * U.Matrice[k, j]);
                    }
                }
            }

            for (int i = 0; i < A.Ordre; i++)
            {
                L.Matrice[i, i] = 1;
            }
            return A == P.Transpose() * L * U;
        }

        private void CalculDeterminant()
        {
            double m = 1;
            for (int i = 0; i < A.Ordre; i++)
            {
                m *= U.Matrice[i, i];
            }
            Determinant = Math.Pow(-1, p) * m;
        }

        private int Pivot(MatriceCarre U, int l)
        {
            int res = l;
            for (int i = l + 1; i < U.Ordre; i++)
            {
                if (Math.Abs(U.Matrice[i, l]) > Math.Abs(U.Matrice[l, l]))
                    res = i;
            }
            return res;
        }

        private void Pivotage(int l, int ln, MatriceCarre M)
        {
            for (int i = 0; i < M.Ordre; i++)
            {
                Swap(ref M.Matrice[l, i], ref M.Matrice[ln, i]);
            }
        }

        private void Swap(ref double nb1, ref double nb2)
        {
            double temp = nb1;
            nb1 = nb2;
            nb2 = temp;
        }
    }
}
