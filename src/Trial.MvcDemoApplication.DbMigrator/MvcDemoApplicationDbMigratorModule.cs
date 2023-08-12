using Trial.MvcDemoApplication.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Trial.MvcDemoApplication.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MvcDemoApplicationEntityFrameworkCoreModule),
    typeof(MvcDemoApplicationApplicationContractsModule)
    )]
public class MvcDemoApplicationDbMigratorModule : AbpModule
{
}
