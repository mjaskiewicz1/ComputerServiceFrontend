using Common.Helppers;
using Microsoft.AspNetCore.Http;

namespace Common.Interfaces;

public interface IAzureBlobService
{
    public Task<IEnumerable<string>> GetAllBlobs(string containerName);
    public Task<FileHelper> DownloadAsync(string containerName, string name);
    public Task UploadBlobsAsync(List<IFormFile> files, string nameContainer);
    public Task<bool> Delete(string containerName);
}