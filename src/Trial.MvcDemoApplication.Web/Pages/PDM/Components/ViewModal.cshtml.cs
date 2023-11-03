using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PDM.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Components
{
    public class ViewModalModel : PageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        
        public Guid StructureId { get; set; }

        [BindProperty]
        public ComponentDetailsDto Component { get; set; } = new();
        [BindProperty]
        public List<DescriptorOptionsDto> DescriptorDetailsList { get; set; } = new();
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
    }
}