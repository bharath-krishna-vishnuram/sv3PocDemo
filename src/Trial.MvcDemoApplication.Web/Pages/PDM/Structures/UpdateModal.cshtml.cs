using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Trial.MvcDemoApplication.PDM;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.ObjectMapping;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Structures;

public class UpdateModalModel : MvcDemoApplicationPageModel
{

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public SelectListItem SelectedStructureName { get; set; } = new();

    [BindProperty]
    public CreateUpdateStructureModal StructureData { get; set; } = new();

    private readonly IStructureAppService _structureAppService;

    public UpdateModalModel(IStructureAppService structureAppService)
    {
        _structureAppService = structureAppService;
    }

    public async Task OnGetAsync()
    {
        var structureDetails = await _structureAppService.GetAsync(Id);
        StructureData = ObjectMapper.Map<StructureDto, CreateUpdateStructureModal>(structureDetails);
        SelectedStructureName = new SelectListItem(structureDetails.Name, structureDetails.Id.ToString());
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var structureDetails = ObjectMapper.Map<CreateUpdateStructureModal, CreateStructureDto>(StructureData);
        var _ = await _structureAppService.UpdateAsync(Id, structureDetails);
        return NoContent();
    }
}
