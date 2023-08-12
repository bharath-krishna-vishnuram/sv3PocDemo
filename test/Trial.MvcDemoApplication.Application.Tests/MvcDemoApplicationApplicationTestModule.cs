using Volo.Abp.Modularity;

namespace Trial.MvcDemoApplication;

[DependsOn(
    typeof(MvcDemoApplicationApplicationModule),
    typeof(MvcDemoApplicationDomainTestModule)
    )]
public class MvcDemoApplicationApplicationTestModule : AbpModule
{

}
