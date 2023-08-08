using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using System.Text;
using WebShop.Application.Common.Helpers;
using WebShop.Application.Common.Interfaces;
using WebShop.Application.CQRS.Files.Images.Queries.GetImage;
using static System.Net.Mime.MediaTypeNames;
using Image = SixLabors.ImageSharp.Image;

namespace WebShop.Infrastructure.Services;

public class FileService : IFileService {
    private readonly IEnumerable<int> imageSizes;
    private readonly string _filesStorage;

    public FileService(IConfiguration configuration) {
        // get from configuration all sizes for resizing image
        imageSizes = configuration
            .GetSection("Images:Sizes")
            .GetChildren()
            .Select(x => int.Parse(x.Value ?? ""));

        Guard.Against.Zero(imageSizes.Count(), message: "Image sizes not found");
        Guard.Against.Null(imageSizes.Count(), message: "Image sizes not found");

        var storage = configuration.GetStorage("Uploads");
        Guard.Against.Null(storage, message: "Storage 'Uploads' not found.");
        _filesStorage = storage;
    }

    public async Task DeleteFileAsync(string context, string fileName) {
        await DeleteFileAsync(Path.Combine(context, fileName));
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
        return await IsFileExistAsync(path);
    }
    private async Task<bool> IsFileExistAsync(string path) {
        return File.Exists(path);
    }

    private string GetFullPath(string context, string filename) {
        var res = $"{_filesStorage}{context}/{filename}";
        return res;
    }
    private string GetFullPath(string fullpath) {
        var res = $"{_filesStorage}{fullpath}";
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

    public async Task<string> UploadImageAsync(string context, Stream fileContent) {
        // generate random unique name
        string randomName = Guid.NewGuid().ToString();

        // get ImageSharp image
        using var image = await Image.LoadAsync(fileContent);

        // get file format from loaded image
        string format = image.Metadata.DecodedImageFormat.Name.ToLower();

        // get filename
        string fileName = Path.ChangeExtension(randomName, format);

        // save image at different sizes
        var encoder = new JpegEncoder { Quality = 90 };
        foreach ( int size in imageSizes ) {
            // resize only width to save aspect ratio
            var resizedImage = image.Clone(x => x.Resize(size, 0));

            // get full path
            string storePath = Path.Combine(_filesStorage, context, $"{size}_{fileName}");

            // save to file
            await resizedImage.SaveAsync($"{storePath}", encoder);
        }
        return $"{context}/{fileName}";
    }

    public async Task DeleteFileAsync(string fullPath) {
        fullPath = GetFullPath(fullPath);

        if ( await IsFileExistAsync(fullPath) == false )
            throw new NotFoundException(fullPath, "");

        File.Delete(fullPath);
    }

    public async Task<bool> IsImageExistAsync(string context, string filename, int size) {
        return await IsFileExistAsync(context, $"{size}_{filename}");
    }

    public async Task<GetImageQueryResult> GetImageAsync(string context, string filename, int size) {
        return await GetFileAsync(context, $"{size}_{filename}");
    }

    public async Task<bool> IsImage(Stream content) {
        try {
            var imageInfo = await Image.IdentifyAsync(content);
            content.Position = 0;
            return true;
        }
        catch ( ImageFormatException ) {
            return false;
        }
    }

    public async Task DeleteImagesAsync(string fullPath) {
        var parts = fullPath.Split('/');
        var context = parts[0];
        var fileName = parts[1];
        foreach ( int size in imageSizes ) {
            await DeleteFileAsync(context, $"{size}_{fileName}");
        }
    }
}
