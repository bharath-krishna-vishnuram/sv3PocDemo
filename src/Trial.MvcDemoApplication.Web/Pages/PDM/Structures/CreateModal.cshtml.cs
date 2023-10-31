using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Structures;

public class CreateModalModel : MvcDemoApplicationPageModel
{
    [BindProperty]
    public CreateUpdateStructureModal StructureData { get; set; } = new();

    private readonly IStructureAppService _structureAppService;

    public CreateModalModel(IStructureAppService structureAppService)
    {
        _structureAppService = structureAppService;
    }
    public void OnGet()
    {
        StructureData = new();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var apiInput = ObjectMapper.Map<CreateUpdateStructureModal, CreateStructureDto>(StructureData);
        var _ = await _structureAppService.CreateAsync(apiInput);
        return new OkObjectResult(_);
    }
}

public class CreateUpdateStructureModal
{
    [DynamicFormIgnore]
    public Guid NameId { get; set; }
    public string Description { get; set; } = string.Empty;
    public StructureType Type { get; set; }
}

