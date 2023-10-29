using AutoMapper;
using PDM.Models;
using System;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication;

public class PdmStoreApplicationAutoMapperProfile : Profile
{
    public PdmStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
            CreateMap<Structure, IdNameDto<Guid>>();
            CreateMap<Component, IdNameDto<Guid>>();
            CreateMap<ComponentDescriptor, IdNameDto<Guid>>();
            CreateMap<DescriptorOption, IdNameDto<Guid>>();
    }
}
