using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Trial.MvcDemoApplication.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MvcDemoApplicationDbContextFactory : IDesignTimeDbContextFactory<MvcDemoApplicationDbContext>
{
    public MvcDemoApplicationDbContext CreateDbContext(string[] args)
    {
        MvcDemoApplicationEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<MvcDemoApplicationDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new MvcDemoApplicationDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Trial.MvcDemoApplication.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
