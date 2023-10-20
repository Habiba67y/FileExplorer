using Application.Models;

namespace Application.Services;

public interface IDirectoryService
{
    IEnumerable<string> GetDirectoriesPath(string directoryPath);
    IEnumerable<string> GetFilesPath(string directoryPath);
    ValueTask<List<StorageDirectory>> GetDirectories(string directoryPath);
    ValueTask<StorageDirectory> GetByPath(string path);
}
