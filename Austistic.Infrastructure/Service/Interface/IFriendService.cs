using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Response.Auth;
using Austistic.Core.DTOs.Response.Friend;
using Austistic.Core.Entities;

namespace Austistic.Infrastructure.Service.Interface
{
    public interface IFriendService
    {
        Task<ResponseDto<int>> GetUnreadMessageCount(string userId);
        Task<ResponseDto<List<UserInfo>>> SuggestFriends(string userId, int limit, string filter);
        Task<ResponseDto<List<UserInfo>>> GetFriends(string userId);
        Task<ResponseDto<string>> SendFriendRequest(string userId, string friendId);
        Task<ResponseDto<List<GetAllFriendResp>>> GetAllActiveFriends(string userId);
        Task<ResponseDto<string>> ApproveFriendRequest(string friend_requestId, FriendStatus friendStatus);
        Task<ResponseDto<List<UserInfo>>> GetPendingFriendRequests(string userId);
        Task<ResponseDto<List<UserInfo>>> GetSentAndReceiveFriends(string userId, string type, string filter);
        Task<ResponseDto<string>> DeleteFriendRequest(string friend_requestId);
        Task<ResponseDto<List<RoomMessageResp>>> GetRoomMessage(string userId, string RoomName);
        Task<ResponseDto<RoomMessages>> AddMessage(string userId, string roomName, string plainMessageText, string mainMessage);
    }
}
