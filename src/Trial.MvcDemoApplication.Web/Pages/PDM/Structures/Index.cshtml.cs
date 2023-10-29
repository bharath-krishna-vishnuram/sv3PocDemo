using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.Structures;

public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
    public static string ConvertToHtmlTree(StructureHierarchyDto data)
    {

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
