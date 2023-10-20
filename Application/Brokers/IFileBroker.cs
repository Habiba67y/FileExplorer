using Application.Models;

namespace Application.Brokers;

public interface IFileBroker
{
    StorageFile GetByPath(string path);
}
