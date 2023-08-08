using WebShop.Application.CQRS.Files.Images.Queries.GetImage;

namespace WebShop.Application.Common.Interfaces;

public interface IFileService {
    Task<bool> IsFileExistAsync(string context, string filename);
    Task<GetImageQueryResult> GetFileAsync(string context, string filename);
    Task<string> UploadFileAsync(string context, Stream fileContent);
    Task DeleteFileAsync(string context, string fileName);
    Task DeleteFileAsync(string fullPath);

    Task<bool> IsImageExistAsync(string context, string filename, int size);
    Task<GetImageQueryResult> GetImageAsync(string context, string filename, int size);
    Task<string> UploadImageAsync(string context, Stream fileContent);
    Task<bool> IsImage(Stream content);
    Task DeleteImagesAsync(string fullPath);
}
