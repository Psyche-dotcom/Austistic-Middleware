

using AlpaStock.Core.DTOs;
using Austistic.Core.DTOs.Request.Auth;
using Austistic.Core.DTOs.Response.Auth;
using Microsoft.AspNetCore.Http;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> ToggleUserShouldShow(string userId);
        Task<ResponseDto<string>> RegisterAdmin(SignUp signUp);
        Task<ResponseDto<string>> UploadUserProfilePicture(string email, IFormFile file);
        Task<ResponseDto<string>> UpdateUser(string email, UpdateUserDto updateUser);
        Task<ResponseDto<string>> DeleteUser(string email);
        Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role);
        Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn);
        Task<ResponseDto<PaginatedUser>> GetAllAdminAsync(int pageNumber, int perPageSize);
        Task<ResponseDto<UserInfo>> UserInfoAsync(string userId);
        Task<ResponseDto<string>> ForgotPassword(string CompanyEmail);
        Task<ResponseDto<string>> ConfirmEmailAsync(int token, string email);
        Task<ResponseDto<string>> ResetPassword(ResetPassword resetPassword);
        Task<ResponseDto<string>> SuspendUserAsync(string useremail);
        Task<ResponseDto<string>> UnSuspendUserAsync(string useremail);
        Task<ResponseDto<string>> UpdateUserRole(string email, string role);


    }
}
