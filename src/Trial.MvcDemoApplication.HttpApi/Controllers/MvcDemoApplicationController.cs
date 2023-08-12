using Trial.MvcDemoApplication.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Trial.MvcDemoApplication.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MvcDemoApplicationController : AbpControllerBase
{
    protected MvcDemoApplicationController()
    {
        LocalizationResource = typeof(MvcDemoApplicationResource);
    }
}
