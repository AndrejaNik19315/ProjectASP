using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EntityNotActiveException : Exception
    {
        public EntityNotActiveException()
        {
        }

        public EntityNotActiveException(string message) : base(message)
        {
        }
    }
}
