using Microsoft.Extensions.Configuration;
using System.IO;
using WebShop.Application.Common.Helpers;
using WebShop.Application.Common.Interfaces;

namespace WebShop.Infrastructure.Services;

public class FileService : IFileService {
    private readonly string _filesStorage;
    public FileService(IConfiguration configuration) {
        var storage = configuration.GetStorage("Uploads");
        Guard.Against.Null(storage, message: "Storage 'Uploads' not found.");
        _filesStorage = storage;
    }

    public Task DeleteFileAsync(string context, string fileName) {
        throw new NotImplementedException();
    }

    public Task<Stream> GetFileAsync(string context, string filename) {
        throw new NotImplementedException();
    }

    public Task<bool> IsFileExistAsync(string context, string filename) {
        throw new NotImplementedException();
    }

    private string GetNewFilePath(string context, Stream fileContent) {
        // read 4 bytes of stream to extract file extention
        var ext = new BinaryReader(fileContent).ReadBytes(4);
        // bring stream pointer back
        fileContent.Position -= ext.Length;

        return Path.Combine(
            _filesStorage,
            context,
            Path.ChangeExtension(
                Guid.NewGuid().ToString(),
                ImageHelper.GetImageFormat(ext).ToString()));
    }

    public async Task<string> UploadFileAsync(string context, Stream fileContent) {
        // generate random unique path to file
        var path = GetNewFilePath(context, fileContent);

        using ( var fs = File.OpenWrite(path) ) {
            await fileContent.CopyToAsync(fs);
        }

        return path;
    }

    public Task<string> UploadImageAsync(string context, Stream fileContent) {
        throw new NotImplementedException();
    }
}
