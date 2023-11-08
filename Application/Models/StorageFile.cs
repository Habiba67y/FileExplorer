using Application.Enums;

namespace Application.Models;

public class StorageFile : IStorageEntry
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string DirectoryPath { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
    public StorageItemType EntryType { get; set; } = StorageItemType.File;
}
