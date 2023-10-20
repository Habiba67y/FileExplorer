using Application.Brokers;
using Application.Models;
using AutoMapper;

namespace Infrastructure.Brokers;

public class DriveBroker : IDriveBroker
{
    private readonly IMapper _mapper;
    public DriveBroker(IMapper mapper)
    { 
         _mapper = mapper;
    }

    public IEnumerable<StorageDrive> Get()
    => DriveInfo.GetDrives().Select(drive => _mapper.Map<StorageDrive>(drive));
}
