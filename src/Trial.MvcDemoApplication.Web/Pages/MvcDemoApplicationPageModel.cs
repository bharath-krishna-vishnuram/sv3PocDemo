using Trial.MvcDemoApplication.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Trial.MvcDemoApplication.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class MvcDemoApplicationPageModel : AbpPageModel
{
    protected MvcDemoApplicationPageModel()
    {
        LocalizationResourceType = typeof(MvcDemoApplicationResource);
    }
}
