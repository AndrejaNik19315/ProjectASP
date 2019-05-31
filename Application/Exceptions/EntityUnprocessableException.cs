using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    class EntityUnprocessableException : Exception
    {
        public EntityUnprocessableException()
        {
        }

        public EntityUnprocessableException(string message) : base(message)
        {
        }
    }
}
