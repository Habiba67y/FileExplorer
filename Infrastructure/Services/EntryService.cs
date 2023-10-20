using Application.Models;
using Application.Services;

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
    public async ValueTask<List<IStorageEntry>> Get(string directoryPath, FilterPagination filter)
    {
        var entries = new List<IStorageEntry>();
        var callCounts = 2; // file bn directory larni bo'lgani uchun 2 ta kolleksiyaga, call count ni 2 ta qildim
        var size = filter.PageSize / callCounts + filter.PageSize % 2;
        var directories = await _directoryService.GetDirectories(directoryPath);
        var files = await _fileService.GetFilesByPath(_directoryService.GetFilesPath(directoryPath));

        var size1 = directories.Count() <  size ? directories.Count() : size;
        var size2 = files.Count()<size ? files.Count() : size;
        if (directories.Count() >= size && files.Count() < size)
            size1 += size - files.Count();
        if (files.Count() >= size && directories.Count() < size)
            size2 += size - directories.Count();

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
        return entries;//.Skip((filter.PageToken - 1) * filter.PageSize).Take(filter.PageSize).ToList();
    }
}
