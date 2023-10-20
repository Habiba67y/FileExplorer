using Application.Models;

namespace Application.Brokers;

public interface IDirectoryBroker
{
    IEnumerable<string> GetDirectoriesPath(string directoryPath);
    IEnumerable<string> GetFilesPath(string directoryPath);
    IEnumerable<StorageDirectory> GetDirectories(string directoryPath);
    StorageDirectory GetByPath(string path);
}
