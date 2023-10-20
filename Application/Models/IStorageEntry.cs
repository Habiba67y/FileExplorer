namespace Application.Models;

public interface IStorageEntry
{
    string Name { get; set; }
    string Path { get; set; }
    StorageItmeType EntryType { get; set; }
}
