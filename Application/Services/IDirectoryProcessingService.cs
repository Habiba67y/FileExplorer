using Application.Filters;
using Application.Models;

namespace Application.Services;

public interface IDirectoryProcessingService
{
    ValueTask<List<IStorageEntry>> Get(string directoryPath, StorageDirectoryEntryFilterModel filter);
}
