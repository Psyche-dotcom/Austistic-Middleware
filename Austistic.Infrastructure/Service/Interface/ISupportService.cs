using AlpaStock.Core.DTOs;
using Austistic.Core.Entities;

namespace Austistic.Infrastructure.Service.Interface
{
    public interface ISupportService
    {
        Task<ResponseDto<string>> CreateTicket(string userid, string Message);
        Task<ResponseDto<string>> AddNewMessage(string ticketid, string Message, bool IsAdmin);
        Task<ResponseDto<string>> CloseTicket(string ticketid);
        Task<ResponseDto<List<SupportTicket>>> RetrieveUserTicket(string userid);
        Task<ResponseDto<List<SupportTicket>>> RetrieveAllActiveUserTicketForAdmin();
    }
}
