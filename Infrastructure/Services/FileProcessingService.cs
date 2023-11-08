using Application.Filters;
using Application.Models;
using Application.Services;
using Application.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class FileProcessingService : IFileProcessingService
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    public FileProcessingService(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService;
        _fileService = fileService;
    }

    public async ValueTask<List<StorageFile>> GetByFilterAsync(StorageFileFilterModel filterModel)
    {
        var filteredFilesPath = _directoryService
            .GetFilesPath(filterModel.DirectoryPath)
            .Where(filePath => filterModel.FilesType.Contains(_fileService.GetFileType(filePath)));

        return await _fileService.GetFilesByPath(filteredFilesPath);
    }

    public async ValueTask<StorageFileFilterDataModel> GetFilterDataModelAsync(string directoryPath)
    {
        var files = await _fileService.GetFilesByPath(_directoryService.GetFilesPath(directoryPath));
        var filesDetails = _fileService.GetFilesDetails(files);
        return new StorageFileFilterDataModel 
        {
            FilterData = filesDetails.ToList(),
        };

    }
}
