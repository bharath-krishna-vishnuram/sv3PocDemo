using System;
using System.Collections.Generic;
using System.Text;
using Trial.MvcDemoApplication.Localization;
using Volo.Abp.Application.Services;

namespace Trial.MvcDemoApplication;

/* Inherit your application services from this class.
 */
public abstract class MvcDemoApplicationAppService : ApplicationService
{
    protected MvcDemoApplicationAppService()
    {
        LocalizationResource = typeof(MvcDemoApplicationResource);
    }
}
