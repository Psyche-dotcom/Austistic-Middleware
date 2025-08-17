using AlpaStock.Core.DTOs;
using AlpaStock.Core.Repositories.Interface;
using Austistic.Core.Entities;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class SupportService : ISupportService
    {
        private readonly IAutisticRepository<SupportTicket> _supportTicketRepo;
        private readonly IAutisticRepository<SupportMessage> _supportMessageRepo;
        private readonly IHelper _helper;
        private readonly ILogger<SupportService> _logger;
        public SupportService(IAutisticRepository<SupportTicket> supportTicketRepo,
            IAutisticRepository<SupportMessage> supportMessageRepo, IHelper helper,
            ILogger<SupportService> logger)
        {
            _supportTicketRepo = supportTicketRepo;
            _supportMessageRepo = supportMessageRepo;
            _helper = helper;
            _logger = logger;
        }
        public async Task<ResponseDto<string>> CreateTicket(string userid, string Message)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkTicket = await _supportTicketRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid && u.Status == "Open");
                if (checkTicket != null)
                {
                    response.ErrorMessages = new List<string>() { "There is an open ticket already" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var ticket = await _supportTicketRepo.Add(new SupportTicket()
                {
                    UserId = userid,
                    TicketNumber = _helper.GenerateSecureRandomAlphanumeric(15),

                });
                await _supportMessageRepo.Add(new SupportMessage()
                {
                    IsAdmin = false,
                    Message = Message,
                    SupportTicketId = ticket.Id,


                });
                await _supportTicketRepo.SaveChanges();
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Ticket created successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating ticket" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> AddNewMessage(string ticketid, string Message, bool IsAdmin)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkTicket = await _supportTicketRepo.GetQueryable().FirstOrDefaultAsync(u => u.TicketNumber == ticketid && u.Status == "Open");
                if (checkTicket == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid ticket id" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                await _supportMessageRepo.Add(new SupportMessage()
                {
                    IsAdmin = IsAdmin,
                    Message = Message,
                    SupportTicketId = ticketid,
                });
                await _supportTicketRepo.SaveChanges();

                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Mesaage sent successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in sending message" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> CloseTicket(string ticketid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkTicket = await _supportTicketRepo.GetQueryable().FirstOrDefaultAsync(u => u.TicketNumber == ticketid);
                if (checkTicket == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid ticket number" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkTicket.Status != "Open")
                {
                    response.ErrorMessages = new List<string>() { " ticket id already closed" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                checkTicket.Status = "Resolved";
                _supportTicketRepo.Update(checkTicket);
                await _supportTicketRepo.SaveChanges();

                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Ticket close successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in closing ticket" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<List<SupportTicket>>> RetrieveUserTicket(string userid)
        {
            var response = new ResponseDto<List<SupportTicket>>();
            try
            {
                var checkTicket = await _supportTicketRepo.GetQueryable()
                    .Include(u => u.SupportMessages).
                    Where(u => u.UserId == userid).ToListAsync();

                await _supportTicketRepo.SaveChanges();

                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = checkTicket;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting ticket" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<List<SupportTicket>>> RetrieveAllActiveUserTicketForAdmin()
        {
            var response = new ResponseDto<List<SupportTicket>>();
            try
            {
                var checkTicket = await _supportTicketRepo.GetQueryable()
                    .Include(u => u.SupportMessages).
                    Where(u => u.Status == "Open").ToListAsync();

                await _supportTicketRepo.SaveChanges();

                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = checkTicket;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting ticket" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

    }
}
