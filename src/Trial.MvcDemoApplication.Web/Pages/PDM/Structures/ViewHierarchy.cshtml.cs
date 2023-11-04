using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Threading.Tasks;
using System;
using Trial.MvcDemoApplication.PDM;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Structures;

public class ViewHierarchyModel : MvcDemoApplicationPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public StructureHierarchyDto? Structure { get; set; }
    private readonly IStructureAppService _structureAppService;
    
    public ViewHierarchyModel(IStructureAppService structureAppService)
    {
        _structureAppService = structureAppService;
    }

    public async Task OnGetAsync()
    {
        Structure = await _structureAppService.GetStructureHierarchyAsync(Id);
    }
}
