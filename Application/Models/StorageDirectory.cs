namespace Application.Models;

public class StorageDirectory : IStorageEntry
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long ItemCount { get; set; }
    public StorageItmeType EntryType { get; set; } = StorageItmeType.Directory;
}
