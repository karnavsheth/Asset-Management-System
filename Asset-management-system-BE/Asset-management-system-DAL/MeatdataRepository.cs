using Asset_management_system_DAL.Models;
using System;


namespace Asset_management_system_DAL
{
    public class MetadataRepository:IMetadataRepository
    {
        private AssetManagementSystemDBContext _DbContext { get; }

        public MetadataRepository(AssetManagementSystemDBContext DbContext)
        {
            _DbContext = DbContext;
        }

        public bool SaveMetadata(string caption, string tags,int assetId)
        {
            try
            {
                AssetMetadatum assetMetadata = new AssetMetadatum();
                assetMetadata.AssetId = assetId;
                assetMetadata.AssetCaption = caption;
                assetMetadata.AssetTags = tags;
                _DbContext.AssetMetadata.Add(assetMetadata);
                _DbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
