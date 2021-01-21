using System;
using System.Collections.Generic;
using System.Text;

namespace Asset_management_system_DAL
{
    public interface IImageVariantRepository
    {
        public bool SaveImageVariantData(string variantName, string Uri, int assetId);
    }
}
