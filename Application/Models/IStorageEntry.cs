using Application.Enums;

namespace Application.Models;

public interface IStorageEntry
{
    string Name { get; set; }
    string Path { get; set; }
    StorageItemType EntryType { get; set; }
}
