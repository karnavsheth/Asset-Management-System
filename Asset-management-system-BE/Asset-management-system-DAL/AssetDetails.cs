using Asset_management_system_DAL.Models;
using System.Collections.Generic;

namespace Asset_management_system_DAL
{
   public class AssetDetails
    {
        public string AssetCaption { get; set; }
        public string AssetTags { get; set; }
        public int? AssetId { get; set; }
        public string BlobStoragePath { get; set; }
        public string AssetName { get; set; }
        public List<AssetVariant> assetVariants { get; set; }
    }
}
