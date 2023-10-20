using Application.Brokers;
using Application.Models;
using AutoMapper;

namespace Infrastructure.Brokers;

public class FileBroker : IFileBroker
{
    private readonly IMapper _mapper;
    public FileBroker(IMapper mapper)
    {
        _mapper = mapper;
    }
    public StorageFile GetByPath(string path)
    => _mapper.Map<StorageFile>(new FileInfo(path));
}
