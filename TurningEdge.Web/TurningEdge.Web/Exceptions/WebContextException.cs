using System;
using System.Collections.Generic;
using System.Text;

namespace TurningEdge.Web.Exceptions
{
    [Serializable]
    public class WebContextException : Exception
    {
        public WebContextException() { }
        public WebContextException(string message) : base(message) { }
        public WebContextException(string message, Exception inner) : base(message, inner) { }
        protected WebContextException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
