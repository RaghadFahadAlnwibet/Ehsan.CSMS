using System.Threading.Tasks;

namespace Ehsan.CSMS.Data;

public interface ICSMSDbSchemaMigrator
{
    Task MigrateAsync();
}
