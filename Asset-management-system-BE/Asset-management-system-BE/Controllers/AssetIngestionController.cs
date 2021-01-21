using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Asset_management_system_commonLibarary;
using Asset_management_system_service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Options;

namespace Asset_management_system_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetIngestionController : ControllerBase
    {
        private ICognitiveService _cognitiveService;

        public AssetIngestionController(ICognitiveService cognitiveService)
        {
            _cognitiveService = cognitiveService;
        }

        [HttpPost]
        [Route("~/api/UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file,[FromForm]int parentId)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                byte[] FileByteArray = memoryStream.ToArray();//sending it as bytestream
                var result = await _cognitiveService.GenerateImageVariantAndMetadata(FileByteArray, file.FileName,parentId);
            }
            catch (Exception Ex)
            {
                return StatusCode(500,new ObjectResult(Ex));
            }

            return Ok();
        }
    }
}
