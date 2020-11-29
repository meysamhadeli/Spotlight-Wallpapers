using System;
using System.Net.Http;

namespace Communication
{
    public interface IRequestMessage
    {
        public HttpMethod Method { get;}
        public Uri RequestUri { get;}
        
        public HttpContent Content { get; }
    }

}