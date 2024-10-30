using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace API.Services;

public class BlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobService(BlobServiceClient blobServiceClient, string containerName)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync();
        
        var blobClient = containerClient.GetBlobClient($"{folderName}/{file.FileName}");
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return blobClient.Uri.ToString();
    }
    
    public async Task DeleteFolderAsync(string folderName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobs = container.GetBlobsAsync(prefix: folderName + "/");

        await foreach (var blob in blobs)
        {
            var blobClient = container.GetBlobClient(blob.Name);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}