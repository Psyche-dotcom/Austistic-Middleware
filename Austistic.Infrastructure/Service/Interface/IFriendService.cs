﻿using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Response.Auth;
using Austistic.Core.Entities;

namespace Austistic.Infrastructure.Service.Interface
{
    public interface IFriendService
    {
        Task<ResponseDto<List<UserInfo>>> SuggestFriends(string userId);
        Task<ResponseDto<List<UserInfo>>> GetFriends(string userId);
        Task<ResponseDto<string>> SendFriendRequest(string userId, string friendId);
        Task<ResponseDto<string>> ApproveFriendRequest(string userId, string friendId, FriendStatus friendStatus);
        Task<ResponseDto<List<UserInfo>>> GetPendingFriendRequests(string userId);
    }
}
