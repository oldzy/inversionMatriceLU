using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    public static class DecompositionLU
    {
        public static Dictionary<string, MatriceCarre> Inversion(MatriceCarre m)
        {
            Dictionary<string, MatriceCarre> resultat = new Dictionary<string, MatriceCarre>();
            MatriceCarre[] tab = Gauss(m);
            resultat.Add("A", tab[0]);
            resultat.Add("L", tab[1]);

            return resultat;
        }

        private static MatriceCarre[] Gauss(MatriceCarre m)
        {
            MatriceCarre A = new MatriceCarre(m.Matrice);
            MatriceCarre L = new MatriceCarre(m.Deg);

            for (int i = 0; i < A.Deg - 1; i++)
            {
                for (int j = i + 1; j < A.Deg; j++)
                {
                    if (A.Matrice[i, i] == 0)
                        Pivot(i, A, L);
                    L.Matrice[j, i] = A.Matrice[j, i] / A.Matrice[i, i];
                    for (int k = 0; k < A.Deg; k++)
                    {
                        A.Matrice[j, k] = A.Matrice[j, k] - (L.Matrice[j, i] * A.Matrice[i, k]);
                    }
                }
            }

            return new MatriceCarre[] { A, L };
        }

        private static void Pivot(int n, MatriceCarre A, MatriceCarre L)
        {
            int i = n + 1;
            while (A.Matrice[i, n] == 0 && i < A.Deg)
            {
                i++;
            }
            Pivotage(n, i, A, L);
        }

        private static void Pivotage(int l, int ln, MatriceCarre A, MatriceCarre L)
        {
            for (int i = 0; i < A.Deg; i++)
            {
                Swap(ref A.Matrice[l, i], ref A.Matrice[ln, i]);
            }
            for (int i = 0; i < L.Deg; i++)
            {
                Swap(ref L.Matrice[ln, i], ref L.Matrice[ln, i]);
            }
        }

        private static void Swap(ref double nb1, ref double nb2)
        {
            double temp = nb1;
            nb1 = nb2;
            nb2 = temp;
        }
    }
}
