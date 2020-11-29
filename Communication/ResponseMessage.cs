using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Communication.Services;

namespace Communication
{
    public class ResponseMessage : IResponseMessage
    {
        public HttpContent Content { get;}
        public bool IsSuccessStatusCode { get; }
        public HttpStatusCode StatusCode { get;}

        public HttpResponseHeaders Headers { get; }

        public ResponseMessage(HttpContent content, HttpResponseHeaders headers, bool isSuccessStatusCode, HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.IsSuccessStatusCode = isSuccessStatusCode;
            this.Headers = headers;
            Content = content;
        }

    }
}