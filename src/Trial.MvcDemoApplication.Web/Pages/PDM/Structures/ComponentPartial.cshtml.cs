using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.Structures
{
    public class ComponentPartialModel : MvcDemoApplicationPageModel
    {
        [BindProperty]
        public ComponentHierarchyDto ComponentData { get; set; } = new();
        public void OnGet(ComponentHierarchyDto component)
        {
            ComponentData = component;
        }
    }
}
