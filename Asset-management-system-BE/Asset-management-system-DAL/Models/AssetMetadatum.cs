using System;
using System.Collections.Generic;

#nullable disable

namespace Asset_management_system_DAL.Models
{
    public partial class AssetMetadatum
    {
        public int AssetMetaDataId { get; set; }
        public string AssetCaption { get; set; }
        public string AssetTags { get; set; }
        public int? AssetId { get; set; }

        public virtual AssetFolderStructure Asset { get; set; }
    }
}
