using Microsoft.AspNetCore.Http;

namespace Austistic.Core.DTOs.Request.Symbol
{
    public class UploadImagReq
    {

        public string CategoryName { get; set; }
        public IFormFile file { get; set; }
        public string Description { get; set; }
    }
}
