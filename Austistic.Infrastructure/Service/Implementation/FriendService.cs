using AlpaStock.Core.Context;
using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Response.Auth;
using Austistic.Core.Entities;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class FriendService : IFriendService
    {

        private readonly AustisticContext _context;
        private readonly ILogger<FriendService> _logger;


        public FriendService(AustisticContext context, 
            ILogger<FriendService> logger)
        {

            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<List<UserInfo>>> SuggestFriends(string userId)
        {
            var response = new ResponseDto<List<UserInfo>>();
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var suggestedFriends = await _context.Users
                    .Where(u => u.Id != userId &&
                                u.Age == user.Age &&
                                u.Country == user.Country &&
                                u.Gender == user.Gender &&
                                !_context.Friends.Any(f => (f.UserId == userId && f.FriendId == u.Id) || (f.UserId == u.Id && f.FriendId == userId))).Select(u => new UserInfo()
                                {
                                    Id = u.Id,
                                    Email = u.Email,
                                    UserName = u.UserName,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Country = u.Country,
                                    PhoneNumber = u.PhoneNumber,
                                    ProfilePicture = u.ProfilePicture,
                                    Age = u.Age,
                                    Gender = u.Gender,
                                })
                    .ToListAsync();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = suggestedFriends;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting suggested friend" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<List<UserInfo>>> GetFriends(string userId)
        {
            var response = new ResponseDto<List<UserInfo>>();
            try
            {
                var friends = await _context.Friends
                .Where(f => f.UserId == userId && f.Status == FriendStatus.Approved)
                .Select(u => new UserInfo()
                {
                    Id = u.FriendUser.Id,
                    Email = u.FriendUser.Email,
                    UserName = u.FriendUser.UserName,
                    FirstName = u.FriendUser.FirstName,
                    LastName = u.FriendUser.LastName,
                    Country = u.FriendUser.Country,
                    PhoneNumber = u.FriendUser.PhoneNumber,
                    ProfilePicture = u.FriendUser.ProfilePicture,
                    Age = u.FriendUser.Age,
                    Gender = u.FriendUser.Gender,
                })
                .ToListAsync();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = friends;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting users friend" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> SendFriendRequest(string userId, string friendId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var friendRequest = new Friend
                {
                    UserId = userId,
                    FriendId = friendId,
                    Status = FriendStatus.Pending
                };

                await _context.Friends.AddAsync(friendRequest);
                await _context.SaveChangesAsync();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Friend request sent successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in sending friend request successfully" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }

        }

        public async Task<ResponseDto<string>> ApproveFriendRequest(string userId, string friendId, FriendStatus friendStatus)
        {
            var response = new ResponseDto<string>();
            try
            {
                var friendRequest = await _context.Friends
               .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId && f.Status == FriendStatus.Pending);

                if (friendRequest != null)
                {
                    response.ErrorMessages = new List<string>() { "Changing friend status not successfully" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                friendRequest.Status = friendStatus;
                _context.Friends.Update(friendRequest);
                _context.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = $"Friend request {friendStatus} successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in changing friend request status" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }

        }



        public async Task<ResponseDto<List<UserInfo>>> GetPendingFriendRequests(string userId)
        {

            var response = new ResponseDto<List<UserInfo>>();
            try
            {
                var pendingRequests = await _context.Friends
                .Where(f => f.FriendId == userId && f.Status == FriendStatus.Pending).Select(u => new UserInfo()
                {
                    Id = u.FriendUser.Id,
                    Email = u.FriendUser.Email,
                    UserName = u.FriendUser.UserName,
                    FirstName = u.FriendUser.FirstName,
                    LastName = u.FriendUser.LastName,
                    Country = u.FriendUser.Country,
                    PhoneNumber = u.FriendUser.PhoneNumber,
                    ProfilePicture = u.FriendUser.ProfilePicture,
                    Age = u.FriendUser.Age,
                    Gender = u.FriendUser.Gender,
                })
                .ToListAsync();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = pendingRequests;
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in changing friend request status" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }

}
