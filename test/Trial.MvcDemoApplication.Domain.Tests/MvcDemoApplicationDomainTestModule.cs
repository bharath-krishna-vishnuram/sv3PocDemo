using Trial.MvcDemoApplication.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Trial.MvcDemoApplication;

[DependsOn(
    typeof(MvcDemoApplicationEntityFrameworkCoreTestModule)
    )]
public class MvcDemoApplicationDomainTestModule : AbpModule
{

}
