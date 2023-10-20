using Application.Models;

namespace Application.Services;

public interface IFileService
{
    ValueTask<StorageFile> GetByPath(string path);
    ValueTask<List<StorageFile>> GetFilesByPath(IEnumerable<string> directoriesPath);
}
