using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PDM.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Components;

public class ViewModalModel : PageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public Guid DescriptorNameId { get; set; }


    [BindProperty]
    public Guid OptionsNameId { get; set; }
    [BindProperty]
    public Guid AssociatedDescriptorId { get; set; }

    public Guid StructureId { get; set; }

    [BindProperty]
    public ComponentDetailsDto Component { get; set; } = new();
    [BindProperty]
    public List<DescriptorOptionsDto> DescriptorDetailsList { get; set; } = new();

    [BindProperty]
    public Guid ConstraintComponentId { get; set; }

    [BindProperty]
    public Guid ConstraintDescriptorId { get; set; }

    [BindProperty]
    public List<IdNameDto<Guid>> ConstraintComponentDescriptors { get; set; } = new();

    private readonly IComponentAppService _componentAppService;

    public ViewModalModel(IComponentAppService componentAppService)
    {
        _componentAppService = componentAppService;
    }


    public async Task OnGetAsync()
    {
        Component = await _componentAppService.GetAsync(Id);
        StructureId = await _componentAppService.GetStructureIdAsync(Id);
        //DescriptorDetailsList = await _componentAppService.GetAllDescriptorDetailsAsync(Id);
    }
    public async Task<IActionResult> OnPostAsync()
    {
        await _componentAppService.AddDescriptorAsync(Id, DescriptorNameId);
        return RedirectToPage("/PDM/Components/ViewModal", new { id = Id });
    }
    public async Task<IActionResult> OnPostAddOptionAsync()
    {
        await _componentAppService.AddDescriptorOptionsAsync(AssociatedDescriptorId, OptionsNameId);
        return RedirectToPage("/PDM/Components/ViewModal", new { id = Id });
    }
    public async Task<IActionResult> OnPostAddConstraintAsync()
    {
        await _componentAppService.AddConstraintDescriptorAsync(Id, ConstraintDescriptorId);
        return RedirectToPage("/PDM/Components/ViewModal", new { id = Id });
    }

}
