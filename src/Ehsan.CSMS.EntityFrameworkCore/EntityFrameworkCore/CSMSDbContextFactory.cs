using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ehsan.CSMS.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CSMSDbContextFactory : IDesignTimeDbContextFactory<CSMSDbContext>
{
    public CSMSDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        CSMSEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<CSMSDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new CSMSDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Ehsan.CSMS.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
