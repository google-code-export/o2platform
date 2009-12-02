using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq
{
    public class NLinqException : ApplicationException
    {
        public NLinqException()
            : base()
        {
        }

        public NLinqException(string message)
            : base(message)
        {
        }

        public NLinqException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
