using Application.Models;
using Application.Services;
using System.IO.Pipelines;

namespace Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    public EntryService(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService;
        _fileService = fileService;
    }
    public async ValueTask<List<IStorageEntry>> Get(string directoryPath, StorageDirectoryFilter filter)
    {
        var entries = new List<IStorageEntry>();
        var callCounts = 2; // file bn directory larni bo'lgani uchun 2 ta kolleksiyaga, call count ni 2 ta qildim
        var filterSize1 = filter.PageSize / callCounts + filter.PageSize % 2;
        var filterSize2 = filter.PageSize / callCounts;

        var directories = await _directoryService.GetDirectories(directoryPath);
        var files = await _fileService.GetFilesByPath(_directoryService.GetFilesPath(directoryPath));

        var size1 = directories.Count() < filterSize1 ? directories.Count() : filterSize1;
        var size2 = files.Count() < filterSize2 ? files.Count() : filterSize2;
        if (directories.Count() >= filterSize1 && files.Count() < filterSize2)
            size1 += filterSize2 - files.Count();
        if (files.Count() >= filterSize2 && directories.Count() < filterSize2)
            size2 += filterSize1 - directories.Count();

        if (filter.IncludeDirectories && filter.IncludeFiles)
        {
            entries.AddRange(directories.Skip((filter.PageToken - 1) * size1).Take(size1));
            entries.AddRange(files.Skip((filter.PageToken - 1) * size2).Take(size2));
        }
        else
        {
            if (filter.IncludeFiles)
                entries.AddRange(files.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize));
            if (filter.IncludeDirectories)
                entries.AddRange(directories.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize));
        }
        return entries;
    }
    public async ValueTask<List<StorageFile>> GetFiles(string directoryPath, StorageFileFilter filter)
    {
        var files = await _fileService.GetFilesByPath(_directoryService.GetFilesPath(directoryPath));

        foreach (var path in _directoryService.GetDirectoriesPath(directoryPath))
            files.AddRange(await _fileService.GetFilesByPath(_directoryService.GetFilesPath(path)));

        if (filter.IncludeDocumentFiles && filter.IncludeImageFiles && filter.IncludeSourceCodeFiles)
            return files;
        else
        {
            var documentExtensions = new List<string> { ".doc", ".pdf"};
            var imageExtensions = new List<string> 
            { ".gif", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp", ".png", ".svg" };
            var sourceCoudeFileExtensions = new List<string> { ".c", ".py", ".cs", ".json", ".php"};
            var includeFiles = new List<StorageFile>();

            foreach (var file in files)
            {
                if ((filter.IncludeDocumentFiles && documentExtensions.Contains(file.Extension))
                    || (filter.IncludeImageFiles && imageExtensions.Contains(file.Extension))
                    || (filter.IncludeSourceCodeFiles && sourceCoudeFileExtensions.Contains(file.Extension)))
                    includeFiles.Add(file);
            }
            return includeFiles.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize).ToList();
        }
    }
}
