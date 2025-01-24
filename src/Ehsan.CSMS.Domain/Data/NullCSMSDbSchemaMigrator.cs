using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ehsan.CSMS.Data;

/* This is used if database provider does't define
 * ICSMSDbSchemaMigrator implementation.
 */
public class NullCSMSDbSchemaMigrator : ICSMSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
