using Microsoft.Extensions.Configuration;
using System.IO;
using WebShop.Application.Common.Helpers;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Files.Images.Queries.GetImage;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<GetImageQueryResult> GetFileAsync(string context, string filename) {
        var path = GetFullPath(context, filename);

        var res = new GetImageQueryResult() {
            Extention = Path.GetExtension(path),
            Stream = File.OpenRead(path)
        };

        return res;
    }

    public async Task<bool> IsFileExistAsync(string context, string filename) {
        var path = GetFullPath(context, filename);
        return File.Exists(path);
    }
    private string GetFullPath(string context, string filename) {
        var res = $"{_filesStorage}{context}/{filename}";
        return res;
    }

    private string GetNewFilePath(string context, Stream fileContent) {
        // read 4 bytes of stream to extract file extention
        var ext = new BinaryReader(fileContent).ReadBytes(4);
        // bring stream pointer back
        fileContent.Position -= ext.Length;

        return GetFullPath(context, Path.ChangeExtension(
                Guid.NewGuid().ToString(),
                ImageHelper.GetImageFormat(ext).ToString()));
    }

    public async Task<string> UploadFileAsync(string context, Stream fileContent) {
        // generate random unique path to file
        var path = GetNewFilePath(context, fileContent);

        using ( var fs = File.OpenWrite(path) ) {
            await fileContent.CopyToAsync(fs);
        }

        return path.Replace(_filesStorage, "");
    }

    public Task<string> UploadImageAsync(string context, Stream fileContent) {
        throw new NotImplementedException();
    }
}
