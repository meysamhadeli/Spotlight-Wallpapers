using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Communication.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace Communication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Client();
            while (true)
            {
                Console.WriteLine("Please enter your message and press enter:");
                string readLine = Console.ReadLine();
                var message = new MessageDto {Message = readLine};
                var data = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
                var requestMessage = new RequestMessage(HttpMethod.Post, new Uri("http://localhost:5000/Ping/Conversation"), data);

                try
                {
                    var request = await client.SendAsync<IResponseMessage>(requestMessage);
                    var content = await request.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response your message is: {content}");
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Bye");
                    throw;
                }
            }

        }
    }
}

//**Simple sample with return  response string**//

// class Program
// {
//     static async Task Main(string[] args)
//     {
//         var client = new Client();
//         while (true)
//         {
//             Console.WriteLine("Please enter your message and press enter:");
//             string readLine = Console.ReadLine();
//             try
//             {
//                 var response = await client.SendAsync(readLine);
//                 Console.WriteLine($"Response your message is: {response}");
//             }
//             catch (TaskCanceledException e)
//             {
//                 Console.WriteLine("Bye");
//                 throw;
//             }
//         }
//
//     }
// }