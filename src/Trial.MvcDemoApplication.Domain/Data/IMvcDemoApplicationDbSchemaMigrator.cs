using System.Threading.Tasks;

namespace Trial.MvcDemoApplication.Data;

public interface IMvcDemoApplicationDbSchemaMigrator
{
    Task MigrateAsync();
}
