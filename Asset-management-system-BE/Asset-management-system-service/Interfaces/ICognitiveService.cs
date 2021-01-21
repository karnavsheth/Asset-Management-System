using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Asset_management_system_service
{
    public interface ICognitiveService
    {
        public Task<string> GenerateImageVariantAndMetadata(Byte[] content, string fileName, int parentId);
    }
}
