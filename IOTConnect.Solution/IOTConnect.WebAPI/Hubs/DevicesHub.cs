using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Hubs
{
    public class DevicesHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        //public async Task SendMessage(string topic, string message)
        //{
        //    if (Clients != null &&
        //        Clients.All != null)
        //    {
        //        await Clients.All.SendAsync("MqttReceived", topic, message);
        //    }
        //    else 
        //    {
        //        return;
        //    }
            
        //}
    }
}