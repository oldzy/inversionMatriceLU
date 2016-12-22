using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace inversionMatriceLU
{
    public static class Fichier
    {
        public static double[,] Lire(string fichierTexte)
        {
            double[,] mat = null;
            try
            {
                string[] lignes = File.ReadAllLines(fichierTexte);
                mat = new double[lignes.Length, lignes[0].Split(' ').Length];
                for (int i = 0; i < lignes.Length; i++)
                {
                    string[] nombres = lignes[i].Split(' ');
                    for (int j = 0; j < nombres.Length; j++)
                    {
                        mat[i, j] = double.Parse(nombres[j]);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                mat = null;
            }
            catch (FormatException)
            {
                Console.WriteLine("le fichier " + Path.GetFullPath(fichierTexte) + " ne contient pas que des nombres réels");
                mat = null;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ce qui est contenu dans le fichier " + Path.GetFullPath(fichierTexte) + " n'est pas une matrice");
                mat = null;
            }
            return mat;
        }

        public static void ViderFichier(string fichierTexte)
        {
            if (File.Exists(fichierTexte))
                File.WriteAllText(fichierTexte, "");
        }

        public static void Ecrire(string fichierTexte, string message)
        {
            using (StreamWriter w = File.AppendText(fichierTexte))
            {
                w.WriteLine(message);
            }
        }
    }
}
