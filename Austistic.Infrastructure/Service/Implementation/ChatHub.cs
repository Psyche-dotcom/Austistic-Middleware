using Austistic.Core.DTOs.Response.Friend;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class ChatHub : Hub
    {
        private readonly IFriendService _friendService;
        private static readonly Dictionary<string, string> _onlineUsers = new();
        public ChatHub(IFriendService friendService)
        {
            _friendService = friendService;
        }

        public async Task RegisterUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                _onlineUsers[userId] = Context.ConnectionId;

                // Optionally mark user online in DB
                await _friendService.ChangeOnlineStatus(userId,true);

                // Notify everyone
                await Clients.All.SendAsync("UserOnline", userId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Find the userId linked to this connection
            var userId = _onlineUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

            if (!string.IsNullOrEmpty(userId))
            {
                _onlineUsers.Remove(userId);

              
                await _friendService.ChangeOnlineStatus(userId, false);

                
                await Clients.All.SendAsync("UserOffline", userId);
            }

            await base.OnDisconnectedAsync(exception);
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
