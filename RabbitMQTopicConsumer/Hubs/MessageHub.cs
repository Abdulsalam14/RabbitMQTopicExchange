using Microsoft.AspNetCore.SignalR;

namespace RabbitMQTopicExchange.Hubs
{
    public class MessageHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("Connected");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Clients.All.SendAsync("Disconnected");
        }
    }
}
