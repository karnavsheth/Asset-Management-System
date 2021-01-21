using System;
using System.Collections.Generic;

#nullable disable

namespace Asset_management_system_DAL.Models
{
    public partial class AssetFolderStructure
    {
        public AssetFolderStructure()
        {
            AssetMetadata = new HashSet<AssetMetadatum>();
            AssetVariants = new HashSet<AssetVariant>();
            InverseParent = new HashSet<AssetFolderStructure>();
        }

        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public bool IsFolder { get; set; }
        public string BlobStoragePath { get; set; }
        public int? ParentId { get; set; }

        public virtual AssetFolderStructure Parent { get; set; }
        public virtual ICollection<AssetMetadatum> AssetMetadata { get; set; }
        public virtual ICollection<AssetVariant> AssetVariants { get; set; }
        public virtual ICollection<AssetFolderStructure> InverseParent { get; set; }
    }
}
