using Asset_management_system_commonLibarary;
using Asset_management_system_DAL;
using Asset_management_system_DAL.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Asset_management_system_service
{
    public class CognitiveService : ICognitiveService
    {
        private readonly IComputerVisionClient _visionClient;
        private readonly ConfigurationValues _Configuration;
        private readonly IBlobService _blobService;
        private readonly IMetadataRepository _metadataRepository;
        private readonly IImageVariantRepository _imageVariantRepository;
        private readonly IAssetRepository _assetRepository;

        private List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects
            };

        public CognitiveService(IOptions<ConfigurationValues> Configuration, IComputerVisionClient visionClient, IBlobService blobService, AssetManagementSystemDBContext DbContext, IMetadataRepository metadataRepository, IImageVariantRepository imageVariantRepository, IAssetRepository assetRepository)
        {
            _Configuration = Configuration.Value;
            _visionClient = visionClient;
            _blobService = blobService;
            _metadataRepository = metadataRepository;
            _imageVariantRepository = imageVariantRepository;
            _assetRepository = assetRepository;
        }

        public async Task<string> GenerateImageVariantAndMetadata(Byte[] byteArray, string fileName, int parentId)
        {
            try
            {
                //Save Original Image Details and get AssetId for the same         
                var assetId = _assetRepository.SaveAssetDetails(fileName, await SaveImageDataInBlobStorage(new MemoryStream(byteArray), fileName),parentId);
                if (assetId != -1)
                {
                    //Task 1: Generate and save image variants in blob storage, also save their details in DB.
                    await GenerateAndSaveImageVariants(byteArray, fileName, assetId);
                    //Task 2: Get Image Metadata and save it in DB.
                    await GetImageMetadata(byteArray, fileName, assetId);

                    //var task1 = SomeLongRunningTask();
                    //var task2 = SomeOtherLongRunningTask();

                    //await Task.WhenAll(task1, task2);
                    return "success";
                }
                else
                {
                    throw new Exception("Some error occured while saving asset.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task GenerateAndSaveImageVariants(Byte[] byteArray, string fileName, int assetId)
        {
            var imageVariants = _Configuration.ImageVariants;
            foreach (var variant in imageVariants)
            {
                var fileVariantName = Path.GetFileNameWithoutExtension(fileName) + "_" + variant.w + "x" + variant.h + ".jpg";
                var result = await _visionClient.GenerateThumbnailInStreamAsync(variant.w, variant.h, new MemoryStream(byteArray), true);
                var variantMemoryStream = new MemoryStream();
                await result.CopyToAsync(variantMemoryStream);
                variantMemoryStream.Position = 0;
                var response = _imageVariantRepository.SaveImageVariantData(fileVariantName, await SaveImageDataInBlobStorage(variantMemoryStream, fileVariantName), assetId);
            }
        }

        private async Task GetImageMetadata(Byte[] byteArray, string fileName, int assetId)
        {
            var MetadataDetails = await _visionClient.AnalyzeImageInStreamAsync(new MemoryStream(byteArray), features);
            var result = _metadataRepository.SaveMetadata(MetadataDetails.Description.Captions[0].Text, string.Join(", ", MetadataDetails.Description.Tags), assetId);
        }

        private async Task<string> SaveImageDataInBlobStorage(Stream stream, string fileName)
        {
            return (await _blobService.UploadFileBlobAsync("imagecontainer", stream, "image/jpeg", fileName)).ToString();
        }
    }
}
