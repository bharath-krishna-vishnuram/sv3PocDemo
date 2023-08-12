using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Trial.MvcDemoApplication.Data;

/* This is used if database provider does't define
 * IMvcDemoApplicationDbSchemaMigrator implementation.
 */
public class NullMvcDemoApplicationDbSchemaMigrator : IMvcDemoApplicationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
