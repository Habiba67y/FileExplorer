using Application.Brokers;
using Application.Models;
using Application.Services;
using System.IO;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IFileBroker _broker;
    public FileService(IFileBroker broker)
    {
        _broker = broker;
    }

    public ValueTask<StorageFile> GetByPath(string path)
    => new ValueTask<StorageFile>(_broker.GetByPath(path));

    public ValueTask<List<StorageFile>> GetFilesByPath(IEnumerable<string> directoriesPath)
    => new ValueTask<List<StorageFile>>(directoriesPath.Select(path => _broker.GetByPath(path)).ToList());
}
