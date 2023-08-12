using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trial.MvcDemoApplication.Data;
using Volo.Abp.DependencyInjection;

namespace Trial.MvcDemoApplication.EntityFrameworkCore;

public class EntityFrameworkCoreMvcDemoApplicationDbSchemaMigrator
    : IMvcDemoApplicationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMvcDemoApplicationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MvcDemoApplicationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MvcDemoApplicationDbContext>()
            .Database
            .MigrateAsync();
    }
}
