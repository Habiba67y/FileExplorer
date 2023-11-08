using Application.Enums;

namespace Application.Models;

public class StorageDirectory : IStorageEntry
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long ItemCount { get; set; }
    public StorageItemType EntryType { get; set; } = StorageItemType.Directory;
}
