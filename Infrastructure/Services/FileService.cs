using Application.Brokers;
using Application.Enums;
using Application.Filters;
using Application.Models;
using Application.Services;
using Application.Settings;
using Microsoft.Extensions.Options;
using System.IO;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly FileFilterSettings _fileFilterSettings;
    private readonly FileStorageSettings _fileStorageSettings;
    private readonly IFileBroker _broker;
    public FileService
        (
        IOptions<FileFilterSettings> fileFilterSettings,
        IOptions<FileStorageSettings> fileStorageSettings,
        IFileBroker broker
        )
    {
        _fileFilterSettings = fileFilterSettings.Value;
        _fileStorageSettings = fileStorageSettings.Value;
        _broker = broker;
    }

    public ValueTask<StorageFile> GetByPath(string path)
    => new(_broker.GetByPath(path));

    public ValueTask<List<StorageFile>> GetFilesByPath(IEnumerable<string> directoriesPath)
    => new(directoriesPath.Select(path => _broker.GetByPath(path)).ToList());

    public IEnumerable<StorageFilesDetails> GetFilesDetails(IEnumerable<StorageFile> files)
    {
        var filesType = files.Select(file => (File: file, Type: GetFileType(file.Path)));

        return filesType
            .GroupBy(fileType => fileType.Type)
            .Select(fileGroup => new StorageFilesDetails
            {
                FileType = fileGroup.Key,
                DisplayeName = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == fileGroup.Key)?.DisplayName ?? "Other files",
                Count = fileGroup.Count(),
                Size = fileGroup.Sum(file => file.File.Size),
                ImageUrl = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.FileType == fileGroup.Key)?.DisplayName ?? _fileStorageSettings.FileImageUrl,
            });
    }

    public StorageFileType GetFileType(string filePath)
    {
        var fileExtension = Path.GetExtension(filePath).TrimStart('.');
        var matchedFileType = _fileFilterSettings.FileExtensions.FirstOrDefault(extension => extension.Extensions.Contains(fileExtension));

        return matchedFileType?.FileType ?? StorageFileType.Other;
    }
}
