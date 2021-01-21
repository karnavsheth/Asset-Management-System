using System;
using System.Collections.Generic;

#nullable disable

namespace Asset_management_system_DAL.Models
{
    public partial class AssetVariant
    {
        public int AssetVariantId { get; set; }
        public string AssetVariantName { get; set; }
        public string BlobStoragePath { get; set; }
        public int? AssetId { get; set; }

        public virtual AssetFolderStructure Asset { get; set; }
    }
}
