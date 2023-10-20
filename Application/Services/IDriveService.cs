using Application.Models;

namespace Application.Services;

public interface IDriveService
{
    ValueTask<List<StorageDrive>> GetDrives();
}
