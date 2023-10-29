using AutoMapper;
using PDM.Models;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web;

public class MvcDemoApplicationWebAutoMapperProfile : Profile
{
    public MvcDemoApplicationWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<TextElementDto, CreateTextElementDto> ();
        CreateMap<CreateTextElementDto, TextElement> ();
    }
}
