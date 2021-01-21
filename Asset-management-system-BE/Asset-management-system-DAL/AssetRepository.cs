using Asset_management_system_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asset_management_system_DAL
{

    // TODO Exception handling and logging
    public class AssetRepository : IAssetRepository
    {
        private AssetManagementSystemDBContext _DbContext { get; }

        public AssetRepository(AssetManagementSystemDBContext DbContext)
        {
            _DbContext = DbContext;
        }

        public int SaveAssetDetails(string fileName, string Uri,int parentId)
        {
            try
            {
                AssetFolderStructure assetFolderStructure = new AssetFolderStructure();
                assetFolderStructure.AssetName = fileName;
                assetFolderStructure.BlobStoragePath = Uri;
                assetFolderStructure.ParentId = parentId;
                _DbContext.AssetFolderStructures.Add(assetFolderStructure);
                _DbContext.SaveChanges();
                return assetFolderStructure.AssetId;
            }
            catch (Exception ex)
            {
                
                return -1;
            }
        }

        public List<AssetFolderStructure> GetAssetListByParentId(int parentId)
        {
            var assetFolderStructures = _DbContext.AssetFolderStructures.Where(x => x.ParentId == parentId).ToList();
            return assetFolderStructures;
        }

        public AssetDetails GetAssetDetailsById(int assetId)
        {
            var assetDetails =
                (from m in _DbContext.AssetMetadata
                 join a in _DbContext.AssetFolderStructures on m.AssetId equals a.AssetId
                 where a.AssetId == assetId
                 select (new AssetDetails { AssetId = m.AssetId, AssetCaption = m.AssetCaption, AssetTags = m.AssetTags, AssetName = a.AssetName, BlobStoragePath = a.BlobStoragePath })).FirstOrDefault();
            if (assetDetails != null)
            {
                assetDetails.assetVariants = _DbContext.AssetVariants.Where(x => x.AssetId == assetId).ToList();
            }
            return assetDetails;
        }
    }
}
