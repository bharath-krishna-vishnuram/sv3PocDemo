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
    public Guid? Id { get; set; }

    [BindProperty]
    public StructureHierarchyDto? Structure { get; set; }
    private readonly IStructureAppService _structureAppService;
    
    public ViewHierarchyModel(IStructureAppService structureAppService)
    {
        _structureAppService = structureAppService;
    }

    public async Task OnGetAsync()
    {
        if (Id.HasValue)
            Structure = await _structureAppService.GetStructureHierarchyAsync(Id.Value);
    }
    public static string ConvertToHtmlTree(StructureHierarchyDto? data)
    {
        if (data == null)
            return "<div/>";
        void BuildTree(ComponentHierarchyDto? data, StringBuilder builder)
        {
            if (data is null)
            {
                return;
            }
            if (data?.SubComponents != null)
            {
                builder.Append("<li>");
                builder.Append($"<span class='tf-nc'>{data.Name}</span>");
                builder.Append("<ul>");

                foreach (var subComponent in data.SubComponents)
                {
                    BuildTree(subComponent, builder);
                }

                builder.Append("</ul>");
                builder.Append("</li>");
            }
            else
            {
                builder.Append($"<li><span class='tf-nc'>{data?.Name}</span></li>");
            }
        }
        if (data.RootComponent == null)
        {
            return string.Empty;
        }
        StringBuilder htmlBuilder = new();

        htmlBuilder.Append("<div class='tf-tree example'><ul>");
        BuildTree(data?.RootComponent, htmlBuilder);
        htmlBuilder.Append("</ul></div>").Replace("<ul></ul>", "");

        return htmlBuilder.ToString();
    }
}
