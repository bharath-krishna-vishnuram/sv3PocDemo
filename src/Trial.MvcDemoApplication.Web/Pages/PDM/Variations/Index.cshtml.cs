using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.Variations;

public class IndexModel : MvcDemoApplicationPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public Guid StructureId { get; set; }
    
    public List<ComponentVariantSetDto> Variants { get; private set; } = new();


    private readonly IComponentAppService _componentAppService;

    public IndexModel(IComponentAppService componentAppService)
    {
        _componentAppService = componentAppService;
    }


    public async Task OnGetAsync()
    {
        try
        {
            Variants = await _componentAppService.GetVariantOptionsAsync(Id);
            StructureId = await _componentAppService.GetStructureIdAsync(Id);
        }
        catch (Exception)
        {

        }
    }
}
