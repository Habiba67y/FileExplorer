using Application.Filters;
using Application.Models;
using Application.Services;
using Application.Settings;
using Microsoft.Extensions.Options;
using System.IO.Pipelines;

namespace Infrastructure.Services;

public class DirectoryProcessingService : IDirectoryProcessingService
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    public DirectoryProcessingService(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService;
        _fileService = fileService;
    }
    public async ValueTask<List<IStorageEntry>> Get(string directoryPath, StorageDirectoryEntryFilterModel filter)
    {
        var entries = new List<IStorageEntry>();

        //var callCounts = 2; // file bn directory larni bo'lgani uchun 2 ta kolleksiyaga, call count ni 2 ta qildim
        //var filterSize1 = filter.PageSize / callCounts + filter.PageSize % 2;
        //var filterSize2 = filter.PageSize / callCounts;

        var directories = await _directoryService.GetDirectories(directoryPath, filter);
        var files = await _fileService.GetFilesByPath(_directoryService.GetFilesPath(directoryPath));

        //var size1 = directories.Count() < filterSize1 ? directories.Count() : filterSize1;
        //var size2 = files.Count() < filterSize2 ? files.Count() : filterSize2;
        //if (directories.Count() >= filterSize1 && files.Count() < filterSize2)
        //    size1 += filterSize2 - files.Count();
        //if (files.Count() >= filterSize2 && directories.Count() < filterSize2)
        //    size2 += filterSize1 - directories.Count();

        if (filter.IncludeFiles)
            entries.AddRange(files.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize));

        if (filter.IncludeDirectories)
            entries.AddRange(directories.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize));

        return entries.OrderBy(e => e.Name).Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize).ToList();
    }
}
