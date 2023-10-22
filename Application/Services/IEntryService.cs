using Application.Models;

namespace Application.Services;

public interface IEntryService
{
    ValueTask<List<IStorageEntry>> Get(string directoryPath, StorageDirectoryFilter filter);
    ValueTask<List<StorageFile>> GetFiles(string directoryPath, StorageFileFilter filter);
}
