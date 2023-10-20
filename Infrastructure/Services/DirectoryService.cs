using Application.Brokers;
using Application.Models;
using Application.Services;

namespace Infrastructure.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IDirectoryBroker _broker;
    public DirectoryService(IDirectoryBroker broker)
    {
        _broker = broker;
    }

    public ValueTask<StorageDirectory> GetByPath(string path)
    => new ValueTask<StorageDirectory>(_broker.GetByPath(path));

    public ValueTask<List<StorageDirectory>> GetDirectories(string directoryPath)
    => new ValueTask<List<StorageDirectory>>(_broker.GetDirectories(directoryPath).ToList());

    public IEnumerable<string> GetDirectoriesPath(string directoryPath)
    => _broker.GetDirectoriesPath(directoryPath);

    public IEnumerable<string> GetFilesPath(string directoryPath)
    => _broker.GetFilesPath(directoryPath);
}
