using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    public class MethodeLU
    {
        #region ATTRIBUTS

        private MatriceCarre matA;
        private MatriceCarre matP;
        private MatriceCarre matL;
        private MatriceCarre matU;
        private int permutation;

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

        public int p
        {
            get { return permutation; }
            set { permutation = value; }
        }

        #endregion

        public MethodeLU(MatriceCarre matrice)
        {
            A = matrice;
            P = new MatriceCarre(A.Deg, MatriceCarre.UNITE);
            L = new MatriceCarre(A.Deg, MatriceCarre.NULL);
            U = new MatriceCarre(A.Matrice);
            p = 0;
        }

        public void Inversion()
        {
            if (Decomposition())
            {
                if(Determinant() != 0)
                {

                }
            }
            
        }

        private bool Decomposition()
        {
            for (int k = 0; k < A.Deg - 1; k++)
            {
                for (int i = k + 1; i < A.Deg; i++)
                {
                    if (U.Matrice[k, k] == 0)
                    {
                        int n = Pivot(U, k);
                        Pivotage(k, n, P);
                        Pivotage(k, n, L);
                        Pivotage(k, n, U);
                        p++;
                    }
                    L.Matrice[i, k] = U.Matrice[i, k] / U.Matrice[k, k];
                    for (int j = 0; j < A.Deg; j++)
                    {
                        U.Matrice[i, j] = U.Matrice[i, j] - (L.Matrice[i, k] * U.Matrice[k, j]);
                    }
                }
            }

            for (int i = 0; i < A.Deg; i++)
            {
                L.Matrice[i, i] = 1;
            }

            return A == P.Transpose() * L * U;
        }

        private double Determinant()
        {
            double m = 1;
            for (int i = 0; i < A.Deg; i++)
            {
                m *= U.Matrice[i, i];
            }
            return Math.Pow(-1, p) * m;
        }

        private int Pivot(MatriceCarre U, int l)
        {
            int ln = l + 1;
            while (U.Matrice[ln, l] == 0 && ln < U.Deg)
            {
                ln++;
            }
            return ln;
        }

        private void Pivotage(int l, int ln, MatriceCarre M)
        {
            for (int i = 0; i < M.Deg; i++)
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
