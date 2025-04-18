﻿using AlpaStock.Core.DTOs;
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
        Task<ResponseDto<List<string>>> GetAllSymbolIncat(string catid);
        Task<ResponseDto<List<CategorySymbol>>> GetAllcat (string userid);
    }
}
