using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inversionMatriceLU
{
    class PasMatriceCarreException : ApplicationException
    {
        public string message { get; set; }

        public PasMatriceCarreException() { }

        public PasMatriceCarreException(string message):base(message)
        {

        }
    }
}
