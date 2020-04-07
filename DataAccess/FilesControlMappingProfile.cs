using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.EntityModels;
using Domain;

namespace DataAccess
{
    public class FilesControlMappingProfile : Profile
    {
        public FilesControlMappingProfile()
        {
            CreateMap<FileMetadataEntity, FileMetadata>()
                .ForMember(dest => dest.Expires, opt => opt.ResolveUsing(src => (src.Expires - DateTime.Now).Days))
                .ForMember(dest => dest.DT_RowId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
