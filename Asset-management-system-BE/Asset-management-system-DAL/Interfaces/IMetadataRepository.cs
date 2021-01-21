using System;
using System.Collections.Generic;
using System.Text;

namespace Asset_management_system_DAL
{
    public interface IMetadataRepository
    {
        public bool SaveMetadata(string caption,string tags, int assetId);
    }
}
