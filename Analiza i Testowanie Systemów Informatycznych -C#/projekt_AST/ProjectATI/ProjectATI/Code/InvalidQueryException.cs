using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code
{
    [Serializable]
    class InvalidQueryException : Exception
    {
        public InvalidQueryException()
        {
        }

        public InvalidQueryException(string message) : base(message)
        {
        }

        public InvalidQueryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
