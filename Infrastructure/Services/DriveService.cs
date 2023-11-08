using Application.Brokers;
using Application.Models;
using Application.Services;

namespace Infrastructure.Services;

public class DriveService : IDriveService
{
    private readonly IDriveBroker _broker;
    public DriveService(IDriveBroker broker)
    {
        _broker = broker;    
    }
    public ValueTask<List<StorageDrive>> GetDrives()
    => new(_broker.Get().ToList());
}
