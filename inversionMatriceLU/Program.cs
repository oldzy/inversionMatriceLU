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
            double[,] m2 = { { 1, 1, 1 }, { 1, 1, 2 }, { 2, 2, 3 } };
            MatriceCarre test2 = new MatriceCarre(m2);
            double[,] m3 = { { 0.001, 2 }, { 1, 1 } };
            MatriceCarre test3 = new MatriceCarre(m3);
            MatriceCarre test4 = new MatriceCarre(3, MatriceCarre.NULL);

            Console.WriteLine(test1);
            try
            {
                InversionLU inv = new InversionLU(test1);
                Console.WriteLine(inv.Inverse);
            }catch(ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
