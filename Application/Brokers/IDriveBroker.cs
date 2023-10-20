using Application.Models;

namespace Application.Brokers;

public interface IDriveBroker
{
    IEnumerable<StorageDrive> Get();
}
