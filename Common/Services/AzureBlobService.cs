using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Common.Helppers;
using Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Common.Services;

public class AzureBlobService : IAzureBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobService(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Application:ConnectionStringStorage"]);
    }

    public async Task UploadBlobsAsync(List<IFormFile> files, string nameContainer)
    {
        //throw new RequestFailedException("error");
        var container = _blobServiceClient.GetBlobContainerClient(nameContainer);
        await container.CreateIfNotExistsAsync();
        foreach (var file in files)
        {
            var blobClient = container.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream());
        }
    }

    public async Task<bool> Delete(string containerName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);

        try
        {
            // Delete the specified container and handle the exception.
            await container.DeleteIfExistsAsync();
            return true;
        }
        catch (RequestFailedException e)
        {
            return false;
        }
    }

    public async Task<IEnumerable<string>> GetAllBlobs(string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        if (await containerClient.ExistsAsync())
        {
            var files = new List<string>();

            var blobs = containerClient.GetBlobsAsync();

            await foreach (var item in blobs) files.Add(item.Name);

            return files;
        }

        return null;
    }

    public async Task<FileHelper> DownloadAsync(string containerName, string name)
    {
        BlobClient blobClient;
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await using (var memoryStream = new MemoryStream())
        {
            blobClient = containerClient.GetBlobClient(name);
            await blobClient.DownloadToAsync(memoryStream);
        }

        var steam = blobClient.OpenReadAsync().Result;
        BlobProperties blobProperties = await blobClient.GetPropertiesAsync();
        return new FileHelper
        {
            ContentType = blobProperties.ContentType,
            FileStream = steam,
            Name = blobClient.Name
        };
    }

}