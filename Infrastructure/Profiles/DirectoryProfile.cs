
using Application.Models;
using AutoMapper;

namespace Infrastructure.Profiles;

public class DirectoryProfile: Profile
{
    public DirectoryProfile()
    {
        CreateMap<DirectoryInfo, StorageDirectory>()
            .ForMember(src => src.Name, opt => opt.MapFrom
            (dest => dest.Name))
            .ForMember(src => src.Path, opt => opt.MapFrom
            (dest => dest.FullName))
            .ForMember(src => src.ItemCount, opt => opt.MapFrom
            (dest => dest.EnumerateFileSystemInfos().Count()));
    }
}
