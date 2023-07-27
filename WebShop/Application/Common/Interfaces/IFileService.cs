﻿namespace WebShop.Application.Common.Interfaces;

public interface IFileService {
    Task<bool> IsFileExistAsync(string context, string filename);
    Task<Stream> GetFileAsync(string context, string filename);
    Task<string> UploadFileAsync(string context, Stream fileContent);
    Task<string> UploadImageAsync(string context, Stream fileContent);
    Task DeleteFileAsync(string context, string fileName);
}
