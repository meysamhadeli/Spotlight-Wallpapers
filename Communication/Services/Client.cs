using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Communication.Services
{
    public class Client : IClient
    {
        public async Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage)
            where TResponseMessage : IResponseMessage
        {
            HttpResponseMessage message;
            IResponseMessage response;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(requestMessage.Method, requestMessage.RequestUri)
                {
                    Content = requestMessage.Content
                };
                var cts = new CancellationTokenSource();
            
                    var content = await requestMessage.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<MessageDto>(content);
                    if (obj.Message == "Bye")
                    {
                        cts.Cancel();
                    }
                    
                    message = await client.SendAsync(request, cts.Token);
                    response = new ResponseMessage(message.Content, message.Headers,
                        message.IsSuccessStatusCode, message.StatusCode);
                    return (TResponseMessage) response;
            }
        }

        public async Task<IResponseMessage> SendAsync(IRequestMessage requestMessage)
        {
            HttpResponseMessage message;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(requestMessage.Method, requestMessage.RequestUri)
                {
                    Content = requestMessage.Content
                };
                var cts = new CancellationTokenSource();

                var content = await requestMessage.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<MessageDto>(content);
                if (obj.Message == "Bye")
                {
                    cts.Cancel();
                }
                
                message = await client.SendAsync(request, cts.Token);
            }

            message.EnsureSuccessStatusCode();

            IResponseMessage response = new ResponseMessage(message.Content, message.Headers,
                message.IsSuccessStatusCode, message.StatusCode);

            return response;
        }

        public async Task<string> SendAsync(string message)
        {
            using (var client = new HttpClient())
            {
                var cts = new CancellationTokenSource();
                
                if (message == "Bye")
                {
                    cts.Cancel();
                }
                
                var data = new StringContent(JsonSerializer.Serialize(new MessageDto() {Message = message}),
                    Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync(new Uri("http://localhost:5000/Ping/Conversation"),
                    data, cts.Token);
                var content = await responseMessage.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}