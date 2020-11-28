using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Communication
{
    public interface IResponseMessage
    {
        public HttpContent Content { get;}
        public HttpResponseHeaders Headers { get; }
        public bool IsSuccessStatusCode { get; }
        public HttpStatusCode StatusCode { get;}
    }
}