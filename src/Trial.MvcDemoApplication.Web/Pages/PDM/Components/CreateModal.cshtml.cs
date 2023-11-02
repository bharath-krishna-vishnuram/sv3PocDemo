using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;
using Trial.MvcDemoApplication.Web.Pages.PDM.Structures;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Components;

public class CreateModalModel : MvcDemoApplicationPageModel
{

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid ParentComponentId { get; set; }
    [BindProperty]
    public CreateUpdateComponentModal ComponentData { get; set; } = new();

    private readonly IComponentAppService _componentAppService;

    public CreateModalModel(IComponentAppService componentAppService)
    {
        _componentAppService = componentAppService;
    }
    public void OnGet()
    {
        ComponentData = new()
        {
            ParentComponentId = ParentComponentId,
        };
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var apiInput = ObjectMapper.Map<CreateUpdateComponentModal, CreateUpdateComponentDto>(ComponentData);
        var _ = await _componentAppService.CreateAsync(apiInput);
        return new OkObjectResult(_);
    }
}

public class CreateUpdateComponentModal
{
    [DynamicFormIgnore]
    public Guid NameId { get; set; }
    [HiddenInput]
    public Guid ParentComponentId { get; set; }
}
