using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class DecompositionException : ApplicationException
    {
        public DecompositionException(string message) : base(message)
        {

        }
    }
}
