using AlpaStock.Core.Context;
using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Response.Auth;
using Austistic.Core.DTOs.Response.Friend;
using Austistic.Core.Entities;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class FriendService : IFriendService
    {

        private readonly AustisticContext _context;
        private readonly ILogger<FriendService> _logger;
        private readonly IHelper _helper;

        public FriendService(AustisticContext context,
            ILogger<FriendService> logger, IHelper helper)
        {

            _context = context;
            _logger = logger;
            _helper = helper;
        }



        public async Task<ResponseDto<List<UserInfo>>> SuggestFriends(string userId, int limit, string filter)
        {
            var response = new ResponseDto<List<UserInfo>>();
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                int minAge = user.Age - 5;
                int maxAge = user.Age + 10;
                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.ToLower();
                    var Friendusers = await _context.Users.Where(u => (u.FirstName.ToLower().Contains(filter)
                    || u.LastName.ToLower().Contains(filter)
                    || u.UserName.ToLower().Contains(filter)) && u.ShouldShowOnSearch)
                        .Select(u => new UserInfo
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
                    .Take(limit)
                    .ToListAsync();
                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "Success";
                    response.Result = Friendusers;
                    return response;
                }

                var suggestedFriends = await _context.Users
                    .Where(u => u.Id != userId &&
                     u.ShouldShowOnSearch &&
                                u.Age >= minAge &&
                                u.Age <= maxAge &&
                                !_context.Friends.Any(f =>
                                    (f.UserId == userId && f.FriendUserId == u.Id) ||
                                    (f.UserId == u.Id && f.FriendUserId == userId)))
                    .Select(u => new UserInfo
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
                    .Take(limit)
                    .ToListAsync();

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = suggestedFriends;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.ErrorMessages = new List<string> { "Error in getting suggested friends" };
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
                    .Where(f =>
                        (f.UserId == userId && f.Status == FriendStatus.Approved) ||
                        (f.FriendUserId == userId && f.Status == FriendStatus.Approved))
                    .Select(f => new UserInfo
                    {
                        Id = f.Id,
                        UserId = f.UserId == userId ? f.FriendUser.Id : f.User.Id,
                        Email = f.UserId == userId ? f.FriendUser.Email : f.User.Email,
                        UserName = f.UserId == userId ? f.FriendUser.UserName : f.User.UserName,
                        FirstName = f.UserId == userId ? f.FriendUser.FirstName : f.User.FirstName,
                        LastName = f.UserId == userId ? f.FriendUser.LastName : f.User.LastName,
                        Country = f.UserId == userId ? f.FriendUser.Country : f.User.Country,
                        PhoneNumber = f.UserId == userId ? f.FriendUser.PhoneNumber : f.User.PhoneNumber,
                        ProfilePicture = f.UserId == userId ? f.FriendUser.ProfilePicture : f.User.ProfilePicture,
                        Age = f.UserId == userId ? f.FriendUser.Age : f.User.Age,
                        Gender = f.UserId == userId ? f.FriendUser.Gender : f.User.Gender,
                    })
                    .ToListAsync();

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = friends;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.ErrorMessages = new List<string> { "Error in getting user's friends" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<GetAllFriendResp>>> GetAllActiveFriends(string userId)
        {
            var response = new ResponseDto<List<GetAllFriendResp>>();
            try
            {
                var friends = await _context.Friends
                    .Where(f =>
                        (f.UserId == userId && f.Status == FriendStatus.Approved) ||
                        (f.FriendUserId == userId && f.Status == FriendStatus.Approved))
                    .Select(f => new GetAllFriendResp
                    {
                        chatRooomName = f.Room.RoomName,
                        UserId = f.UserId == userId ? f.FriendUser.Id : f.User.Id,
                        name = f.UserId == userId ? f.FriendUser.FirstName + " " + f.FriendUser.LastName : f.User.FirstName + " " + f.User.LastName,
                        message = f.Room.Messages.OrderByDescending(u => u.Created).FirstOrDefault().DisplayMessage,
                        time = f.Room.Messages.OrderByDescending(u => u.Created).FirstOrDefault().Created.ToString(),
                        unreadCount = f.Room.Messages.Count(msg => msg.SentById != userId && !msg.ReadCount.Any(read => read.UserId == userId)),
                        url = f.UserId == userId ? f.FriendUser.ProfilePicture : f.User.ProfilePicture,

                    }).ToListAsync();

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = friends;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.ErrorMessages = new List<string> { "Error in getting user's friends" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<RoomMessageResp>>> GetRoomMessage(string userId, string RoomName)
        {
            var response = new ResponseDto<List<RoomMessageResp>>();
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(u => u.RoomName == RoomName);
                if (room == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid room name" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                // Get all messages in the room
                var roomMessages = await _context.RoomMessages
                    .Include(m => m.ReadCount) // include read status
                    .Where(u => u.RoomId == room.Id)
                    .ToListAsync();

                // Mark messages as read for this user (if not already)
                var unreadMessages = roomMessages
                    .Where(m => !m.ReadCount.Any(rc => rc.UserId == userId)) // not read by this user yet
                    .ToList();

                if (unreadMessages.Any())
                {
                    var readEntries = unreadMessages.Select(m => new ReadMassageCount
                    {
                        MessageId = m.Id,   // from BaseEntity
                        UserId = userId,
                        ReadAt = DateTime.UtcNow
                    }).ToList();

                    await _context.ReadMassageCount.AddRangeAsync(readEntries);
                    await _context.SaveChangesAsync();
                }

                // Map to DTO
                var retrieveMessages = roomMessages.Select(m => new RoomMessageResp
                {
                    imageUrl = m.Message,
                    isMe = userId == m.SentById,
                    text = m.DisplayMessage,
                    time = m.Created.ToString(),
                    status = m.SentById == userId || m.ReadCount.Any(rc => rc.UserId == userId) ? "Read" : "UnRead"
                }).ToList();

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = retrieveMessages;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.ErrorMessages = new List<string> { "Error in getting user's messages" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<List<UserInfo>>> GetSentAndReceiveFriends(string userId, string type,  string? filter)
        {
            var response = new ResponseDto<List<UserInfo>>();
            try
            {
                if (type == "sent")
                {


                    var friends = _context.Friends
                           .Where(f =>
                               (f.UserId == userId && f.Status == FriendStatus.Pending)
                             )
                           .Select(f => new UserInfo
                           {
                               UserId = f.FriendUser.Id,
                               Id = f.Id,
                               Email = f.FriendUser.Email,
                               UserName = f.FriendUser.UserName,
                               FirstName = f.FriendUser.FirstName,
                               LastName = f.FriendUser.LastName,
                               Country = f.FriendUser.Country,
                               PhoneNumber = f.FriendUser.PhoneNumber,
                               ProfilePicture = f.FriendUser.ProfilePicture,
                               Age = f.FriendUser.Age,
                               Gender = f.FriendUser.Gender,
                           });
                           if (!filter.IsNullOrEmpty())
                    {
                        filter = filter.ToLower();
                        friends= friends.Where(u => (u.FirstName.ToLower().Contains(filter)
                    || u.LastName.ToLower().Contains(filter)
                    || u.UserName.ToLower().Contains(filter)));
                    }

                    response.StatusCode = StatusCodes.Status200OK;
                    response.DisplayMessage = "Success";
                    response.Result = await friends.ToListAsync();
                    return response;


                }
                var friendReq = _context.Friends
                           .Where(f =>
                               (f.FriendUserId == userId && f.Status == FriendStatus.Pending)
                             )
                           .Select(f => new UserInfo
                           {
                               Id = f.Id,
                               UserId = f.User.Id,
                               Email = f.User.Email,
                               UserName = f.User.UserName,
                               FirstName = f.User.FirstName,
                               LastName = f.User.LastName,
                               Country = f.User.Country,
                               PhoneNumber = f.User.PhoneNumber,
                               ProfilePicture = f.User.ProfilePicture,
                               Age = f.User.Age,
                               Gender = f.User.Gender,
                           });
                if (!filter.IsNullOrEmpty())
                {
                    filter = filter.ToLower();
                    friendReq = friendReq.Where(u => (u.FirstName.ToLower().Contains(filter)
                || u.LastName.ToLower().Contains(filter)
                || u.UserName.ToLower().Contains(filter)));
                }


                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = await friendReq.ToListAsync();
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.ErrorMessages = new List<string> { "Error in getting user's friends" };
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
                var existingFriendship = await _context.Friends.FirstOrDefaultAsync(f =>
    (f.UserId == userId && f.FriendUserId == friendId) ||
    (f.UserId == friendId && f.FriendUserId == userId));
                if (existingFriendship != null)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>
            {
                existingFriendship.Status == FriendStatus.Pending
                    ? "A friend request is already pending between you and this user."
                    : "You are already friends with this user."
            };
                    return response;
                }

                var friendRequest = new Friend
                {
                    UserId = userId,
                    FriendUserId = friendId,
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
        public async Task<ResponseDto<RoomMessages>> AddMessage(string userId, string roomName, string plainMessageText, string mainMessage)
        {
            var response = new ResponseDto<RoomMessages>();
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(u => u.RoomName == roomName);
                if (room == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid room name" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                ;
                var msgRequest = new RoomMessages
                {
                    SentById = userId,
                    DisplayMessage = plainMessageText,
                    MessageType = "Text",
                    Message = mainMessage,
                    RoomId = room.Id
                };

                await _context.RoomMessages.AddAsync(msgRequest);
                await _context.SaveChangesAsync();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = msgRequest;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in saving message successfully" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }

        }

        public async Task<ResponseDto<int>> GetUnreadMessageCount(string userId)
        {
            var response = new ResponseDto<int>();
            try
            {
                // Get all rooms that user belongs to
                var userRoomIds = await _context.Rooms
                    .Where(r => r.Friend.UserId == userId || r.Friend.FriendUserId == userId)
                    .Select(r => r.Id)
                    .ToListAsync();

                if (!userRoomIds.Any())
                {
                    response.DisplayMessage = "Success";
                    response.Result = 0;
                    response.StatusCode = 200;
                    return response;

                }

                // Count unread messages inside those rooms
                var unreadCount = await _context.RoomMessages
                    .Include(m => m.ReadCount)
                    .Where(m => userRoomIds.Contains(m.RoomId) &&
                                m.SentById != userId &&             // exclude self-sent
                                !m.ReadCount.Any(rc => rc.UserId == userId)) // not yet read
                    .CountAsync();
                response.DisplayMessage = "Success";
                response.Result = unreadCount;
                response.StatusCode = 200;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching unread message count for user {UserId}", userId);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Error while fetching unread message count for user" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<string>> ApproveFriendRequest(string friend_requestId, FriendStatus friendStatus)
        {
            var response = new ResponseDto<string>();
            try
            {
                var friendRequest = await _context.Friends
               .FirstOrDefaultAsync(f => f.Id == friend_requestId && f.Status == FriendStatus.Pending);

                if (friendRequest == null)
                {
                    response.ErrorMessages = new List<string>() { "Changing friend status not successfully" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (friendStatus == FriendStatus.Approved)
                {
                    var roomname = _helper.GenerateSecureRandomAlphanumeric(10);
                    await _context.Rooms.AddAsync(new Room()
                    {
                        FriendId = friend_requestId,
                        RoomName = roomname
                    });
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
        public async Task<ResponseDto<string>> DeleteFriendRequest(string friend_requestId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var friendRequest = await _context.Friends
               .FirstOrDefaultAsync(f => f.Id == friend_requestId);

                if (friendRequest == null)
                {
                    response.ErrorMessages = new List<string>() { "Delete friend not successfully" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var friendRoom = await _context.Rooms.FirstOrDefaultAsync(u => u.FriendId == friend_requestId);
                if (friendRoom != null)
                {
                    _context.Rooms.Remove(friendRoom);
                }

                _context.Friends.Remove(friendRequest);
                _context.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = $"Friend request deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in deleting friend request status" };
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
                .Where(f => f.FriendUserId == userId && f.Status == FriendStatus.Pending).Select(u => new UserInfo()
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    Email = u.User.Email,
                    UserName = u.User.UserName,
                    FirstName = u.User.FirstName,
                    LastName = u.User.LastName,
                    Country = u.User.Country,
                    PhoneNumber = u.User.PhoneNumber,
                    ProfilePicture = u.User.ProfilePicture,
                    Age = u.User.Age,
                    Gender = u.User.Gender,
                }).ToListAsync();
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
