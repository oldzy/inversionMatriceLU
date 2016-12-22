using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class DeterminantException : ApplicationException
    {
        public double Determinant { get; set; }
        public DeterminantException(string message, double determinant) : base(message)
        {
            Determinant = determinant;
        }
    }
}
