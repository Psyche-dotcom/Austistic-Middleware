namespace Austistic.Core.DTOs.Request.Auth
{
    public class ResetAuthTokenSecReq
    {
        public string EmailToken { get; set; }
        public string Token { get; set; }
    }
}
