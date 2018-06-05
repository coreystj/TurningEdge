using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Exceptions
{

    [Serializable]
    public class ApiException : Exception
    {
        private int _id;
        public ApiException(int id) { _id = id; }
        public ApiException(int id, string message) 
            : base(message) { _id = id; }
        public ApiException(int id, string message, Exception inner) 
            : base(message, inner) { _id = id; }
        protected ApiException(int id,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) { _id = id; }
    }
}
