using Application.Brokers;
using Application.Models;
using AutoMapper;

namespace Infrastructure.Brokers;

public class DirectoryBroker : IDirectoryBroker
{
    private readonly IMapper _mapper;
    public DirectoryBroker(IMapper mapper)
    {
        _mapper = mapper;    
    }
    public StorageDirectory GetByPath(string path)
    => _mapper.Map<StorageDirectory>(new DirectoryInfo(path));

    public IEnumerable<StorageDirectory> GetDirectories(string directoryPath)
    => GetDirectoriesPath(directoryPath).Select(path => GetByPath(path));

    public IEnumerable<string> GetDirectoriesPath(string directoryPath)
        => Directory.EnumerateDirectories(directoryPath);
    public IEnumerable<string> GetFilesPath(string directoryPath)
        => Directory.EnumerateFiles(directoryPath);
    public bool ExistsAsync(string directoryPath) => Directory.Exists(directoryPath);
}
