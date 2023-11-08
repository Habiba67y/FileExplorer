using Application.Enums;

namespace Application.Settings;

public class FileExtensionSettings
{
    public StorageFileType FileType { get; set; }
    public string DisplayName { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Extensions { get; set; }
}
