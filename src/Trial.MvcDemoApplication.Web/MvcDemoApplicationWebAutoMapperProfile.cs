using AutoMapper;
using PDM.Models;
using Trial.MvcDemoApplication.PDM;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Trial.MvcDemoApplication.Web.Pages.PDM.Components;
using Trial.MvcDemoApplication.Web.Pages.PDM.Structures;

namespace Trial.MvcDemoApplication.Web;

public class MvcDemoApplicationWebAutoMapperProfile : Profile
{
    public MvcDemoApplicationWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<TextElementDto, CreateTextElementDto> ();
        CreateMap<CreateTextElementDto, TextElement> ();

        CreateMap<StructureDto, CreateStructureDto> ();
        CreateMap<CreateStructureDto, Structure> ();
        CreateMap<CreateUpdateStructureModal, Structure> ();
        CreateMap<CreateUpdateStructureModal, CreateStructureDto> ();
        CreateMap<StructureDto, CreateUpdateStructureModal>();

        CreateMap< CreateUpdateComponentModal, CreateUpdateComponentDto> ();
    }
}
