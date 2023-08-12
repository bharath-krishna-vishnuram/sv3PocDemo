using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Trial.MvcDemoApplication.Web;

[Dependency(ReplaceServices = true)]
public class MvcDemoApplicationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MvcDemoApplication";
}
