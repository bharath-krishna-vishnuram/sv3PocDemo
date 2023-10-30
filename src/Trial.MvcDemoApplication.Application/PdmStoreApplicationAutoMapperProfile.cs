using AutoMapper;
using PDM.Models;
using System;
using System.Linq;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;

namespace Trial.MvcDemoApplication;

public class PdmStoreApplicationAutoMapperProfile : Profile
{
    public PdmStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<TextElement, TextElementDto>();

        CreateMap<Structure, StructureDto>();
        CreateMap<Structure, StructureHierarchyDto>();
        CreateMap<Component, ComponentHierarchyDto>()
            .ForMember(dest => dest.SubComponents, options => options.
            MapFrom(src => src.SubComponents.OrderBy(rec => rec.AssociatedStructureElement.ElementOrder)));

        CreateMap<Structure, IdNameDto<Guid>>();
            CreateMap<Component, IdNameDto<Guid>>();
            CreateMap<ComponentDescriptor, IdNameDto<Guid>>();
            CreateMap<DescriptorOption, IdNameDto<Guid>>();
    }
}
