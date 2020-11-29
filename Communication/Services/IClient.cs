using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Communication.Services
{
    public interface IClient
    {
        Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage) where TResponseMessage : IResponseMessage;
        Task<IResponseMessage> SendAsync(IRequestMessage requestMessage);
        Task<string> SendAsync(string message);

    }
}