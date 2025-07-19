using AlpaStock.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class MessageService
    {



        //public async Task<ResponseDto<IEnumerable<CommunityChannel>>> RetrieveChannel(string? userid)
        //{
        //    var response = new ResponseDto<IEnumerable<CommunityChannel>>();
        //    try
        //    {
        //        if (userid != null)
        //        {
        //            var allResult = await _communityChannelRepo.GetQueryable().
        //              Where(u => u.CreatedByUserId == userid).ToListAsync();
        //            response.StatusCode = 200;
        //            response.DisplayMessage = "Success";
        //            response.Result = allResult;
        //            return response;
        //        }

        //        var result = await _communityChannelRepo.GetQueryable().Include(u => u.Category)
        //            .Include(u => u.CreatedByUser)
        //         .ToListAsync();
        //        response.StatusCode = 200;
        //        response.DisplayMessage = "Success";
        //        response.Result = result;
        //        return response;


        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        response.DisplayMessage = "Error";
        //        response.ErrorMessages = new List<string>() { "Channel not retrieved successfully" };
        //        response.StatusCode = 400;
        //        return response;
        //    }
        //}

        //public async Task<ResponseDto<CommunityChannelMessage>> AddMessage(string RoomId, string message, string messageType, string sentById)
        //{
        //    var response = new ResponseDto<CommunityChannelMessage>();
        //    try
        //    {
        //        var check = await _communityChannelRepo.GetQueryable().FirstOrDefaultAsync(u => u.ChannelRoomId == RoomId);
        //        if (check == null)
        //        {
        //            response.DisplayMessage = "Error";
        //            response.StatusCode = 400;
        //            response.ErrorMessages = new List<string>() { "invalid Channel name" };
        //            return response;
        //        }
        //        var result = await _communityChannelMessageRepo.Add(new CommunityChannelMessage()
        //        {
        //            ChannelId = check.Id,
        //            Message = message,
        //            MessageType = messageType,
        //            SentById = sentById,
        //        });

        //        await _communityChannelMessageRepo.SaveChanges();
        //        response.StatusCode = 200;
        //        response.DisplayMessage = "Success";
        //        response.Result = result;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        response.DisplayMessage = "Error";
        //        response.ErrorMessages = new List<string>() { "message not sent successfully" };
        //        response.StatusCode = 400;
        //        return response;
        //    }
        //}

        //public async Task<ResponseDto<IEnumerable<CommunityMesaagesReponse>>> RetrieveChannelMessages(string roomId, string userid)
        //{
        //    var response = new ResponseDto<IEnumerable<CommunityMesaagesReponse>>();
        //    try
        //    {
        //        var retrieveChannelId = await _communityChannelRepo.GetQueryable().FirstOrDefaultAsync(u => u.ChannelRoomId == roomId);
        //        if (retrieveChannelId == null)
        //        {
        //            response.DisplayMessage = "Error";
        //            response.ErrorMessages = new List<string>() { "Invalid Channel RoomId" };
        //            response.StatusCode = 400;
        //            return response;
        //        }
        //        var messages = await _communityChannelMessageRepo.GetQueryable().
        //            Where(u => u.ChannelId == retrieveChannelId.Id).
        //            OrderBy(u => u.Created).
        //            Select(u => new CommunityMesaagesReponse
        //            {
        //                Id = u.Id,
        //                IsLiked = u.ChannelMessageLikes.FirstOrDefault(u => u.UserId == userid) != null,
        //                IsUnLiked = u.ChannelMessageUnLikes.FirstOrDefault(u => u.UserId == userid) != null,
        //                Message = u.Message,
        //                MessageType = u.MessageType,
        //                LikeCount = u.ChannelMessageLikes.Count(),
        //                UnLikeCount = u.ChannelMessageUnLikes.Count(),
        //                SentByImgUrl = u.SentBy.ProfilePicture,
        //                SenderName = u.SentBy.FirstName + " " + u.SentBy.LastName,
        //                Created = u.Created,
        //                IsSaved = u.MessageFave.FirstOrDefault(u => u.UserId == userid) != null,

        //            })

        //            .ToListAsync();


        //        response.Result = messages;
        //        response.StatusCode = 200;
        //        response.DisplayMessage = "Success";
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        response.DisplayMessage = "Error";
        //        response.ErrorMessages = new List<string> { "Channel messages not retrieved successfully" };
        //        response.StatusCode = 400;
        //        return response;
        //    }
        //}
    }
}
