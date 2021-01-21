using Asset_management_system_DAL;
using Asset_management_system_DAL.Models;
using System.Collections.Generic;

namespace Asset_management_system_service
{
   public class AssetService :IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        // TODO Exception handling and logging

        public List<AssetFolderStructure> GetAssetListByParentId(int parentId)
        {
           return _assetRepository.GetAssetListByParentId(parentId);
        }

        public AssetDetails GetAssetDetailsById(int assetId)
        {
            return _assetRepository.GetAssetDetailsById(assetId);
        }
    }
}
