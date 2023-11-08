using System.Runtime.Serialization;

namespace Application.Enums;

public enum StorageFileType
{
    [EnumMember] Document,
    [EnumMember] Media,
    [EnumMember] SourceCode,
    [EnumMember] Archive,
    [EnumMember] Other
}
