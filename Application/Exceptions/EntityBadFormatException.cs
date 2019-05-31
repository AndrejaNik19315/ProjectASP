using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class EntityBadFormatException : Exception
    {
        public EntityBadFormatException()
        {
        }

        public EntityBadFormatException(string message) : base(message)
        {
        }
    }
}
