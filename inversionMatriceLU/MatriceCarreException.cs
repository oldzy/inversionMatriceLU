using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class MatriceCarreException : ApplicationException
    {
        public MatriceCarreException(string message) : base(message)
        {
        }
    }
}
