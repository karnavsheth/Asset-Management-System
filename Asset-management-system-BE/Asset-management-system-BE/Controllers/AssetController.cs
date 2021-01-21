using System;
using Asset_management_system_service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asset_management_system_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
         }
        [Route("GetAssets")]
        public IActionResult GetAssetListByParentId(int parentId)
        {
            try
            {
                var response = _assetService.GetAssetListByParentId(parentId);
                if(response.Count==0)
                {
                    return NoContent();
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Route("GetAssetDetails")]
        public IActionResult GetAssetDetailsById(int assetId)
        {
            try
            {
                var response = _assetService.GetAssetDetailsById(assetId);
                if (response == null)
                {
                    return NoContent();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
