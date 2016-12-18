using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] m1 = { { 1, 0, 0, 1, 0, 1 }, { -1, 0, 0, 2, 0, 2 }, { 0, 1, 2, 0, 2, 0 }, { 0, -1, 1, 0, 1, 0 }, { 0, 0, 3, 0, -3, 0 }, { 0, 0, 0, 3, 0, -3 } };
            MatriceCarre test1 = new MatriceCarre(m1);
            double[,] m2 = { { 1, 1, 1 }, { 1, 1, 2 }, { 1, 2, 2 } };
            MatriceCarre test2 = new MatriceCarre(m2);
            MatriceCarre test3 = new MatriceCarre(10, MatriceCarre.UNITE);
            MatriceCarre test4 = new MatriceCarre(3, MatriceCarre.NULL);

            /*Console.WriteLine(test1 != test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);*/
            //Console.WriteLine(Inversion.Inversion(test2)["A"]);
            //Console.WriteLine(Inversion.Inversion(test2)["L"]);
            Inversion inv = new Inversion(test2);
            Console.WriteLine(inv.DecompositionLU());
        }
    }
}
