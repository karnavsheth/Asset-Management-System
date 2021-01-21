using Asset_management_system_DAL.Models;
using System;


namespace Asset_management_system_DAL
{
    public class ImageVariantRepository : IImageVariantRepository
    {
        // TODO Exception handling and logging
        private AssetManagementSystemDBContext _DbContext { get; }

        public ImageVariantRepository(AssetManagementSystemDBContext DbContext)
        {
            _DbContext = DbContext;
        }

        public bool SaveImageVariantData(string variantName, string Uri, int assetId)
        {
            try
            {
                AssetVariant assetVariant = new AssetVariant();
                assetVariant.AssetId = assetId;
                assetVariant.AssetVariantName = variantName;
                assetVariant.BlobStoragePath = Uri;
                _DbContext.AssetVariants.Add(assetVariant);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}