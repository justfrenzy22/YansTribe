using bll.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace pl.controllers
{
    public class CDNController : Controller
    {
        private readonly ICDNService _cdnService;

        public CDNController(ICDNService cdnService)
        {
            this._cdnService = cdnService;
        }

        [HttpGet("/cdn/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var fileBytes = _cdnService.GetFileBytes(fileName);
            if (fileBytes == null)
                return NotFound();

            var contentType = _cdnService.GetContentType(fileName) ?? "application/octet-stream";
            return File(fileBytes, contentType);
        }

    }

}