using AlpaStock.Core.DTOs;
using AlpaStock.Core.Repositories.Interface;
using Austistic.Core.DTOs.Request;
using Austistic.Core.Entities;
using Austistic.Core.Repositories.Interface;
using Austistic.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class SymbolService : ISymbolService
    {
        private readonly IAutisticRepository<CategorySymbol> _categorySymbolRepo;
        private readonly IAutisticRepository<SymbolImage> _symbolImageRepo;
        private readonly ILogger<SymbolService> _logger;
        private readonly IHelper _helper;
        private readonly IAccountRepo _accountRepo;
        public SymbolService(IAutisticRepository<SymbolImage> symbolImageRepo,
            IAutisticRepository<CategorySymbol> categorySymbolRepo,
            IAccountRepo accountRepo,
            ILogger<SymbolService> logger, IHelper helper)
        {
            _symbolImageRepo = symbolImageRepo;
            _categorySymbolRepo = categorySymbolRepo;
            _accountRepo = accountRepo;
            _logger = logger;
            _helper = helper;
        }
        public async Task<ResponseDto<string>> CreateCatgory(string userid, string CatType, string CategoryName)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUser = await _accountRepo.FindUserByIdAsync(userid);
                if (checkUser == null)
                {
                    response.ErrorMessages = new List<string>() { "User id is invalid" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                if (CatType == AustisticEnum.Owned.ToString())
                {
                    var checkCategory = await _categorySymbolRepo
                    .GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid
                    && u.CategoryType == AustisticEnum.Owned.ToString());
                    if (checkCategory != null)
                    {
                        response.ErrorMessages = new List<string>() { "User already create own category" };
                        response.StatusCode = 400;
                        response.DisplayMessage = "Error";
                        return response;
                    }
                }
                var checkCateName = await _categorySymbolRepo
                    .GetQueryable().FirstOrDefaultAsync(u => u.CategoryName == CategoryName);
                if (checkCateName != null)
                {
                    response.ErrorMessages = new List<string>() { "Category already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                if (CatType == AustisticEnum.Owned.ToString())
                {

                    await _categorySymbolRepo.Add(new CategorySymbol { CategoryName = "PersonalIcon", CategoryType = CatType, UserId = userid });
                    await _categorySymbolRepo.SaveChanges();
                    response.DisplayMessage = "Success";
                    response.StatusCode = 200;
                    response.Result = "Category created successfully";
                    return response;
                }

                await _categorySymbolRepo.Add(new CategorySymbol { CategoryName = CategoryName, CategoryType = CatType, UserId = userid });
                await _categorySymbolRepo.SaveChanges();
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Category created successfully";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating category" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<SymbolImage>> GetSymbolImageByte(string SymbolIdentifier)
        {
            var response = new ResponseDto<SymbolImage>();
            try
            {
                var retriveSymbol = await _symbolImageRepo.GetQueryable().
                    FirstOrDefaultAsync(x=>x.SymbolIdentifier == SymbolIdentifier);
                if(retriveSymbol == null)
                {
                    response.ErrorMessages = new List<string>() { "symbol not available" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = retriveSymbol;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting symbol data" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> DeleteSymbol(string SymbolIdentifier)
        {
            var response = new ResponseDto<string>();
            try
            {
                var retriveSymbol = await _symbolImageRepo.GetQueryable().
                    FirstOrDefaultAsync(x=>x.SymbolIdentifier == SymbolIdentifier);
                if(retriveSymbol == null)
                {
                    response.ErrorMessages = new List<string>() { "symbol not available" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                _symbolImageRepo.Delete(retriveSymbol);
                await _symbolImageRepo.SaveChanges();
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Symbol deleted success";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating category" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<string>>> GetAllSymbolIncat(string catid)
        {
            var response = new ResponseDto<List<string>>();
            try
            {
                var retrieveSymbol = await _symbolImageRepo.GetQueryable()
                    .Where(u => u.CategorySymbolId == catid)
                    .Select(u => u.SymbolIdentifier)
                    .ToListAsync();
               
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = retrieveSymbol;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting symbol in category" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<List<CategorySymbol>>> GetAllcat(string userid)
        {
            var response = new ResponseDto<List<CategorySymbol>>();
            try
            {
                var retrieveCats = await _categorySymbolRepo.GetQueryable()
                    .Where(u => u.CategoryType == AustisticEnum.Admin.ToString() || u.UserId == userid)
                    .ToListAsync();

                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = retrieveCats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting all category for user");
                response.ErrorMessages = new List<string> { "Error in getting all category for user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
            }

            return response;
        }
        public async Task<ResponseDto<string>> UploadSymbolCategory(string CategoryName, IFormFile file, string Description)
        {
            var response = new ResponseDto<string>();
            try
            {
                
                var checkCateName = await _categorySymbolRepo
                   .GetQueryable().FirstOrDefaultAsync(u => u.CategoryName == CategoryName);
                if (checkCateName == null)
                {
                    response.ErrorMessages = new List<string>() { "Category already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var allowedTypes = new[] { "image/png", "image/svg+xml" };
                if (!allowedTypes.Contains(file.ContentType))
                {
                    response.ErrorMessages = new List<string>() { "User id is invalid" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var fileBytes = ms.ToArray();

                var image = new SymbolImage
                {
                    CategorySymbolId = checkCateName.Id,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    ImageData = fileBytes,
                    Description = Description,
                    SymbolIdentifier = _helper.GenerateSecureRandomAlphanumeric(10)
                };
                await _symbolImageRepo.Add(image);
                await _symbolImageRepo.SaveChanges();
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = "Symbol uploaded successfully";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating symbol" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
