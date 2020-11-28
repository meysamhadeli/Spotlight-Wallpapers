using System;
using System.Net.Http;
using Communication.Services;

namespace Communication
{
    public class RequestMessage : IRequestMessage
    {
        public HttpMethod Method { get;}
        public Uri RequestUri { get;}
        public HttpContent Content { get; }
        
        public RequestMessage(HttpMethod method, Uri requestUri, HttpContent content)
        {
            Method = method;
            RequestUri = requestUri;
            Content = content;
        }
    }
}