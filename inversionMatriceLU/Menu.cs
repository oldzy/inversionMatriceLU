using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace inversionMatriceLU
{
    public static class Menu
    {
        public static bool Demarrer()
        {
            Console.Clear();
            if (Question("La précision par défaut est de 3.\nVoulez vous spécifier une autre précision? ([O]ui/[N]on) : ", true))
            {
                Console.Clear();
                while (!ChoixPrecision())
                {
                    Console.Clear();
                }
            }
            MatriceCarre matrice;
            InversionLU inv;
            if (AfficherMenu() == 1)
            {
                matrice = new MatriceCarre(RecupérerMatrice());
                inv = new InversionLU(matrice);
                try
                {
                    inv.Inversion();
                }
                catch(DecompositionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(DeterminantException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                do
                {
                    matrice = ChoixFichier();
                    if (matrice == null)
                        Console.ReadKey();
                } while (matrice == null);

                inv = new InversionLU(matrice);
                Console.WriteLine(inv.A);
                try
                {
                    inv.Inversion();
                }
                catch (DecompositionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DeterminantException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Question("Voulez-vous relancer le programme? ([O]ui/[N]on) : ", false);
        }

        private static bool ChoixPrecision()
        {
            bool res = false;
            Console.Write("Veuillez choisir une précision (un nombre entier) : ");
            try
            {
                MatriceCarre.Precision = Convert.ToInt32(Console.ReadLine());
                res = true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return res;
        }

        private static MatriceCarre ChoixFichier()
        {
            MatriceCarre matrice = null;
            bool trouverFichier;
            string fichierTexte;
            do
            {
                Console.Clear();
                Console.Write("Veuillez entrez le chemin du fichier : ");
                fichierTexte = Console.ReadLine();
                trouverFichier = File.Exists(fichierTexte);
                if (!trouverFichier)
                {
                    Console.WriteLine("Le fichier n'existe pas.\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                }
            } while (!trouverFichier);

            try
            {
                double[,] mat = Fichier.Lire(fichierTexte);
                if (mat != null)
                {
                    matrice = new MatriceCarre(mat);
                }
            }
            catch (MatriceCarreException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return matrice;
        }

        private static bool Question(string message, bool clear)
        {
            string c;
            bool res = false;
            do
            {
                if(clear)
                    Console.Clear();
                Console.Write(message);
                c = Console.ReadLine();
            } while (c != "o" && c != "O" && c != "n" && c != "N");
            if (c == "o" || c == "O")
                res = true;
            return res;
        }

        private static int AfficherMenu()
        {
            string c;
            do
            {
                Console.Clear();
                Console.Write("Voulez-vous :\n\t1)Entrer une matrice manuellement\n\t2)Entrer une matrice à partir d'un fichier\nVeuillez sélectionner une option : ");
                c = Console.ReadLine();
            } while (c != "1" && c != "2");
            return (int)Char.GetNumericValue(c[0]);
        }

        private static double[,] RecupérerMatrice()
        {
            double[,] mat = null;
            int i = 0;
            Console.Clear();
            Console.WriteLine("Entrez ligne par ligne la matrice. Séparez chaque élément par un espace et pressez la touche Enter pour encoder la ligne suivant : ");
            do
            {
                if (RemplirLigne(ref mat, Console.ReadLine(), i))
                    i++;
            } while (i < mat.GetLength(0));
            Console.Clear();
            return mat;
        }

        private static bool RemplirLigne(ref double[,] mat, string ligne, int l)
        {
            bool res = true;
            string[] nombres = ligne.Split(' ');
            if (mat == null)
                mat = new double[nombres.Length, nombres.Length];
            if (nombres.Length == mat.GetLength(0))
            {
                for (int i = 0; i < nombres.Length; i++)
                {
                    try
                    {
                        mat[l, i] = Convert.ToDouble(nombres[i]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Veuillez n'entrer que des nombres réels.");
                        res = false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Ce que vous essayez d'encoder n'est pas une matrice.");
                        res = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ce que vous essayez d'encoder n'est pas une matrice.");
                res = false;
            }
            return res;
        }
    }
}
