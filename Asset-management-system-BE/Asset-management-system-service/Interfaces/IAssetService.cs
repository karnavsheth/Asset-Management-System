using Asset_management_system_DAL;
using Asset_management_system_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asset_management_system_service
{
   public  interface IAssetService
    {

        public List<AssetFolderStructure> GetAssetListByParentId(int parentId);

        public AssetDetails GetAssetDetailsById(int assetId);
    }
}
