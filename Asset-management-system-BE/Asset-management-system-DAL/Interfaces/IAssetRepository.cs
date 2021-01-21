using Asset_management_system_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asset_management_system_DAL
{
    public interface IAssetRepository
    {
        public int SaveAssetDetails(string fileName, string Uri,int parentId);
        public List<AssetFolderStructure> GetAssetListByParentId(int parentId);
        public AssetDetails GetAssetDetailsById(int assetId);
    }
}
