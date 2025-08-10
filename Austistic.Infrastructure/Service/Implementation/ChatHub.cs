using Austistic.Core.DTOs.Response.Friend;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class ChatHub : Hub
    {
        private readonly IFriendService _friendService;

        public ChatHub(IFriendService friendService)
        {
            _friendService = friendService;
        }
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessageToRoom(string roomName, string userId,string plainMessage ,string message)
        {
            var addMessage = await _friendService.AddMessage(userId, roomName, plainMessage, message);
            var newMsg = new ChatHubRespMsg()
            {
                imageUrl = message,
                userId = userId,
                text = plainMessage,
                status = "Unread",
                roomName = roomName,
                time = addMessage.Result.Created.ToShortTimeString()
            };
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userId, newMsg);
        }


    }

}
