using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        [HttpPost(nameof(Conversation))]
        public async Task<ActionResult<string>> Conversation(MessageDto message, CancellationToken cancellationToken)
        {
            // lock input message
            await _semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                switch (message.Message)
                {
                    case "Hello":
                        await Task.Delay(1000, cancellationToken);
                        return "Hi";
                    case "Bye":
                        if (cancellationToken.IsCancellationRequested)
                            throw new TaskCanceledException();
                      
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        return "Bye";
                    case "Ping":
                        return "Pong";
                    default: return BadRequest("Invalid input message!");
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}