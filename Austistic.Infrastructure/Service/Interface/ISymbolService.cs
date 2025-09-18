using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Request;
using Austistic.Core.DTOs.Response.symbol;
using Austistic.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Austistic.Infrastructure.Service.Interface
{
    public interface ISymbolService
    {
        Task<ResponseDto<string>> CreateCatgory(string userid, string CatType, string CategoryName);
        Task<ResponseDto<SymbolImage>> GetSymbolImageByte(string SymbolIdentifier);
        Task<ResponseDto<string>> UploadSymbolCategory(string CategoryName, IFormFile file, string Description);
        Task<ResponseDto<string>> DeleteSymbol(string SymbolIdentifier);
        Task<ResponseDto<List<Symbolresp>>> GetAllSymbolIncat(string catid);
        Task<ResponseDto<List<CategorySymbolDto>>> GetAllcat(string userid); 
        Task<ResponseDto<string>> DeleteCat(string catid);
        Task<ResponseDto<BaseFreePikAPi>> PromptSymbol(string prompt, string userid);
        Task<ResponseDto<string>> WebhookPromptSymbol(IconPreviewWebhookPayload payload);
    }
}
